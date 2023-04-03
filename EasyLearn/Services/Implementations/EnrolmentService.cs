using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.EnrolmentDTOs;
using EasyLearn.Models.Entities;
using EasyLearn.Models.Enums;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;
using System.Security.Claims;

namespace EasyLearn.Services.Implementations
{
    public class EnrolmentService : IEnrolmentService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEnrolmentRepository _enrolmentRepository;
        private readonly IPaymentRepository _paymentRepository;

        public EnrolmentService(IEnrolmentRepository enrolmentRepository, IHttpContextAccessor httpContextAccessor, IPaymentRepository paymentRepository = null)
        {
            _enrolmentRepository = enrolmentRepository;
            _httpContextAccessor = httpContextAccessor;
            _paymentRepository = paymentRepository;
        }

        public async Task<BaseResponse> Create(CreateEnrolmentRequestModel model)
        {
            var payment = new Payment
            {
                PaymentMethod = model.PaymentMethods,
                PaymentStatus = PaymentStatus.Pending,
                CouponUsed = model.Coupon,
                StudentId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Id = Guid.NewGuid().ToString(),
                CourseId = model.CourseId,
                CreatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                CreatedOn = DateTime.Now,
                PaymentAmount = model.AmountPaid,
            };

            var enrolment = new Enrolment
            {
                Id = Guid.NewGuid().ToString(),
                CompletionStatus = CompletionStatus.NotCompleted,
                CreatedBy = payment.StudentId,
                CreatedOn = payment.CreatedOn,
                PaymentId = payment.Id,
            };
            enrolment.Payment = payment;
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
}
