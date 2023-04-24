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
    private readonly IStudentRepository _studentRepository;
    private readonly IPayStackService _payStackService;

    public EnrolmentService(IEnrolmentRepository enrolmentRepository, ICourseRepository courseRepository, IStudentRepository studentRepository, IPayStackService payStackService)
    {
        _enrolmentRepository = enrolmentRepository;
        _courseRepository = courseRepository;
        _studentRepository = studentRepository;
        _payStackService = payStackService;
    }

    public async Task<CreateEnrolmentRequestModel> Create(CreateEnrolmentRequestModel model, string studentId, string userId)
    {
        var enrollmentStatus = await _courseRepository.StudentIsEnrolled(model.CourseId, studentId);
        if (enrollmentStatus != null)
        {
            if (enrollmentStatus.IsPaid)
            {
                return new CreateEnrolmentRequestModel
                {
                    Message = "You've already paid for this course...",
                    Status = true,
                };
            }
            return new CreateEnrolmentRequestModel
            {
                Message = "You've enrolled but not paid, proceed to pay...",
                Status = false,
            };
        }

        var payment = new Payment
        {
            Id = Guid.NewGuid().ToString(),
            PaymentMethod = model.PaymentMethods,
            PaymentStatus = PaymentStatus.Pending,
            //CouponUsed = model.Coupon,
            StudentId = studentId,
            CourseId = model.CourseId,
            CreatedBy = userId,
            CreatedOn = DateTime.Now,
            PaymentAmount = model.AmountPaid,
            ReferrenceNumber = Guid.NewGuid().ToString().Replace('-', 'y'),
        };

        var enrolments = new List<Enrolment>
        {
            new Enrolment
            {
                Id = Guid.NewGuid().ToString(),
                CompletionStatus = CompletionStatus.NotCompleted,
                StudentId = studentId,
                CreatedBy = userId,
                CreatedOn = payment.CreatedOn,
                PaymentId = payment.Id,
                CourseId = payment.CourseId,
                Payment = payment,
            },
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
        studentCourses.Add(studentCourse);

        var student = new Student
        {
            StudentCourses = studentCourses,
            Enrolments = enrolments,
        };
        await _studentRepository.AddAsync(student);
        await _studentRepository.SaveChangesAsync();

        return new CreateEnrolmentRequestModel
        {
            PaymentRequest = new InitializePaymentRequestModel
            {
                RefrenceNo = payment.ReferrenceNumber,
                CoursePrice = model.AmountPaid,
            },
            Message = "Procced to Payment..",
            Status = false,
        };

    }

    public Task<BaseResponse> Delete(string id)
    {
        throw new NotImplementedException();
    }

    public Task<EnrolmentsResponseModel> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<EnrolmentsResponseModel> GetAllPaid()
    {
        throw new NotImplementedException();
    }

    public Task<EnrolmentResponseModel> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<EnrolmentsResponseModel> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<EnrolmentsResponseModel> GetNotPaid()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse> Update(UpdateEnrolmentRequestModel model)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse> UpdateActiveStatus(UpdateEnrolmentRequestModel model)
    {
        throw new NotImplementedException();
    }
}
