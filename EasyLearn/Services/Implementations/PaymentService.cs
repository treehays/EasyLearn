using EasyLearn.Models.DTOs.PaymentDTOs;
using EasyLearn.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace EasyLearn.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        public async Task<PaymentDTO> MakePayment(PaymentDTO model)
        {
            var url = "https://api.paystack.co/transaction/initialize";
            var secretKey = "sk_test_c19e12866e49a5f161381926540f17355d76f6fc";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", secretKey);
            var content = new StringContent(JsonSerializer.Serialize(new
            {
                amount = 2000,
                email = "treehays90@gmail.com",
                referrenceNumber = Guid.NewGuid().ToString(),
            },);

            return null;
        }
    }
}
