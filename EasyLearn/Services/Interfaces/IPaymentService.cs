using EasyLearn.Models.DTOs.PaymentDTOs;

namespace EasyLearn.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentDTO> MakePayment(PaymentDTO model);
    }
}
