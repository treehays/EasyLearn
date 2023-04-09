using EasyLearn.Models.DTOs.PaymentDTOs;
using EasyLearn.Services.Interfaces;

namespace EasyLearn.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        public async Task<PaymentDTO> MakePayment(PaymentDTO model)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            return null;
        }
    }
}
