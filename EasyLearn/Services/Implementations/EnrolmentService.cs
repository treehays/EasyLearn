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

    public EnrolmentService(IEnrolmentRepository enrolmentRepository, ICourseRepository courseRepository)
    {
        _enrolmentRepository = enrolmentRepository;
        _courseRepository = courseRepository;
    }

    public async Task<BaseResponse> Create(CreateEnrolmentRequestModel model, string studentId, string userId)
    {


        var course = new StudentCourse
        {
            CourseId = model.CourseId,
            StudentId = studentId,
            CreatedBy = userId,
            CreatedOn = DateTime.Now,
            Id = Guid.NewGuid().ToString(),
        };
        var courses = new List<StudentCourse>();
        courses.Add(course);

        var stdt = await _courseRepository.GetAsync(x => x.Id == model.CourseId);


        var payment = new Payment
        {
            Id = Guid.NewGuid().ToString(),
            PaymentMethod = model.PaymentMethods,
            PaymentStatus = PaymentStatus.Pending,
            //CouponUsed = model.Coupon,
            StudentId = studentId,
            CourseId = model.CourseId,
            CreatedBy = userId,
            CreatedOn = course.CreatedOn,
            PaymentAmount = model.AmountPaid,
        };

        var enrolment = new Enrolment
        {
            Id = Guid.NewGuid().ToString(),
            CompletionStatus = CompletionStatus.NotCompleted,
            StudentId = studentId,
            CreatedBy = userId,
            CreatedOn = course.CreatedOn,
            PaymentId = payment.Id,
            CourseId = course.CourseId,
        };



        stdt.StudentCourses = courses;
        enrolment.Payment = payment;
        //enrolment.Course.StudentCourses = courses;
        await _enrolmentRepository.AddAsync(enrolment);
        await _enrolmentRepository.SaveChangesAsync();

        return new BaseResponse
        {
            Message = "Payment processing..",
            Status = true,
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
