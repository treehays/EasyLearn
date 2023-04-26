using EasyLearn.GateWays.Payments.PaymentGatewayDTOs;
using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.EnrolmentDTOs;

namespace EasyLearn.Services.Interfaces
{
    public interface IEnrolmentService
    {
        Task<InitializePaymentResponseModel> Create(CreateEnrolmentRequestModel model, string studentId, string userId, string email, string baseUrl);
        Task<BaseResponse> VerifyPayment(string RefrenceNumber);
        Task<EnrolmentsResponseModel> RecentEnrollments(int count);
    }
}
