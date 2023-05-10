using EasyLearn.Data;
using EasyLearn.GateWays.Payments;
using EasyLearn.GateWays.Payments.PaymentGatewayDTOs;
using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.EnrolmentDTOs;
using EasyLearn.Models.Entities;
using EasyLearn.Models.Enums;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;

namespace EasyLearn.Services.Implementations;

public class EnrolmentService : IEnrolmentService
{
    private readonly IEnrolmentRepository _enrolmentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IPayStackService _payStackService;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IInstructorRepository _instructorRepository;
    private readonly CompanyInfoOption _companyInfoOption;
    private readonly IUserRepository _userRepository;

    public EnrolmentService(IEnrolmentRepository enrolmentRepository, ICourseRepository courseRepository, IPayStackService payStackService, IPaymentRepository paymentRepository, IInstructorRepository instructorRepository, CompanyInfoOption companyInfoOption, IUserRepository userRepository)
    {
        _enrolmentRepository = enrolmentRepository;
        _courseRepository = courseRepository;
        _payStackService = payStackService;
        _paymentRepository = paymentRepository;
        _instructorRepository = instructorRepository;
        _companyInfoOption = companyInfoOption;
        _userRepository = userRepository;
    }

    public async Task<InitializePaymentResponseModel> Create(CreateEnrolmentRequestModel model, string studentId, string userId, string email, string baseUrl)
    {
        var paymentExist = await _paymentRepository.GetAsync(x => x.StudentId == studentId && x.CourseId == model.CourseId);
        if (paymentExist != null)
        {
            if (paymentExist.IsPaid)
            {
                return new InitializePaymentResponseModel
                {
                    message = "You've already paid for this course...",
                    status = false,
                };
            }
            return new InitializePaymentResponseModel
            {
                message = "You've enrolled but not paid, proceed to pay...",
                status = true,
                data = new InitializePaymentData
                {
                    reference = paymentExist.ReferrenceNumber,
                    authorization_url = paymentExist.AuthorizationUri,
                },
            };
        }
        var coursea = await _courseRepository.GetAsync(x => x.Id == model.CourseId);
        var payment = new Payment
        {
            Id = Guid.NewGuid().ToString(),
            PaymentMethod = model.PaymentMethods,
            StudentId = studentId,
            CourseId = model.CourseId,
            CreatedBy = userId,
            CreatedOn = DateTime.Now,
            PaymentAmount = model.AmountPaid,
            ReferrenceNumber = Guid.NewGuid().ToString().Replace('-', 'y'),
        };

        var enrolments = new Enrolment
        {
            Id = Guid.NewGuid().ToString(),
            CompletionStatus = CompletionStatus.NotCompleted,
            StudentId = studentId,
            CreatedBy = userId,
            CreatedOn = payment.CreatedOn,
            PaymentId = payment.Id,
            CourseId = payment.CourseId,
            Payment = payment,
        };
        var studentCourses = new List<StudentCourse>();
        var studentCourse = new StudentCourse
        {
            CourseId = model.CourseId,
            StudentId = studentId,
            CreatedBy = userId,
            CreatedOn = payment.CreatedOn,
            Id = Guid.NewGuid().ToString(),
        };
        var paymentRequest = new InitializePaymentRequestModel
        {
            CallbackUrl = baseUrl,
            CoursePrice = model.AmountPaid,
            Email = email,
            RefrenceNo = payment.ReferrenceNumber,
        };
        var proceedToPay = await _payStackService.InitializePayment(paymentRequest);
        if (!proceedToPay.status)
        {
            return new InitializePaymentResponseModel
            {
                status = false,
                message = "error fro payment gatewy",
                data = new InitializePaymentData
                {
                    authorization_url = baseUrl,
                },
            };
        }

        payment.AuthorizationUri = proceedToPay.data.authorization_url;
        studentCourses.Add(studentCourse);
        coursea.StudentCourses = studentCourses;
        coursea.NumbersOfEnrollment += 1;
        await _enrolmentRepository.AddAsync(enrolments);
        await _enrolmentRepository.SaveChangesAsync();

        return proceedToPay;
    }

    public async Task<EnrolmentsResponseModel> RecentEnrollments(int count)
    {
        var enrollments = await _enrolmentRepository.GetStudentEnrolledCourses(x => x.IsPaid);
        if (enrollments.Count() == 0)
        {
            return new EnrolmentsResponseModel
            {
                Status = false,
                Message = "no enrollment found",
            };
        }
        var topCount = enrollments.OrderBy(x => x.CreatedBy).Take(count);
        return new EnrolmentsResponseModel
        {
            Status = true,
            Message = "all enrolment retrieved...",
            NumberOfEnrollments = enrollments.Count(),
            Data = topCount.Select(x => new EnrolmentDTO
            {
                Id = x.Id,
                StudentName = x.Student.User.FirstName,
                StudentId = x.Student.Id,
                CourseId = x.Course.Id,
                CourseTitle = x.Course.Title,
                CompletionStatus = x.CompletionStatus,
                CreatedOn = x.CreatedOn,
            }),
        };
    }

    public async Task<BaseResponse> VerifyPayment(string refrenceNumber)
    {
        var paymentStatus = await _payStackService.VerifyTransaction(refrenceNumber);
        if (paymentStatus.data.status.ToLower() != "success")
        {
            return new BaseResponse
            {
                Message = "Payment was not successful",
                Status = false,
            };
        }
        var payment = await _paymentRepository.GetAsync(x => x.ReferrenceNumber == refrenceNumber);
        var course = await _courseRepository.GetAsync(x => x.Id == payment.CourseId);
        var courseInstructor = await _instructorRepository.GetInstructorFullDetailAsync(x => x.Id == course.InstructorId);

        var instructorWallet = new Wallet
        {
            Id = Guid.NewGuid().ToString(),
            CreatedBy = paymentStatus.data.reference,
            CreatedOn = paymentStatus.data.created_at,
            Credit = payment.PaymentAmount * 0.10m,
            Description = course.Id,
            UserId = courseInstructor.UserId,
        };


        var adminWallet = new Wallet
        {
            Id = Guid.NewGuid().ToString(),
            CreatedBy = instructorWallet.CreatedBy,
            CreatedOn = instructorWallet.CreatedOn,
            Credit = course.Price,
            Description = course.Id,
            UserId = _companyInfoOption.AdminUserID,
        };

        //var adminId = _companyInfo.AdminUserID;
        var admin = await _userRepository.GetUserWithWalletDetails(x => x.RoleId.ToLower() == "admin");



        admin.Wallet = adminWallet;
        courseInstructor.User.Wallet = instructorWallet;

        //var payment = await _paymentRepository.GetAsync(x => x.ReferrenceNumber == refrenceNumber);
        payment.IsPaid = true;
        payment.ModifiedOn = DateTime.Now;
        payment.ModifiedBy = "Auto";
        var enrolmentStatus = await _enrolmentRepository.GetAsync(x => x.PaymentId == payment.Id);
        enrolmentStatus.IsPaid = true;
        enrolmentStatus.ModifiedBy = payment.ModifiedBy;
        enrolmentStatus.ModifiedOn = payment.ModifiedOn;
        await _paymentRepository.SaveChangesAsync();

        return new BaseResponse
        {
            Message = "Payment succefull, course is now available ",
            Status = true,
        };
    }

}
