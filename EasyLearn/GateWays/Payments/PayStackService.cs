using EasyLearn.GateWays.Payments.PaymentGatewayDTOs;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace EasyLearn.GateWays.Payments;

public class PayStackService : IPayStackService
{
    private readonly IConfiguration _configuration;

    public PayStackService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<CreateTransferRecipientResponseModel> CreateTransferRecipient(CreateTransferRecipientRequestModel model)
    {
        var key = _configuration.GetSection("Paystack")["APIKey"];
        var getHttpClient = new HttpClient();
        getHttpClient.DefaultRequestHeaders.Accept.Clear();
        getHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        getHttpClient.BaseAddress = new Uri($"https://api.paystack.co/transferrecipient");
        getHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);
        var response = await getHttpClient.PostAsJsonAsync(getHttpClient.BaseAddress, new
        {
            type = model.Type,
            name = model.Name,
            account_number = model.AccountNumber,
            bank_code = model.BankCode,
            currency = model.Currency,
            description = model.Description,
        });
        var responseString = await response.Content.ReadAsStringAsync();
        var responseObj = JsonSerializer.Deserialize<CreateTransferRecipientResponseModel>(responseString);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return responseObj;
        }
        return responseObj;
    }

    public async Task<InitializePaymentResponseModel> InitializePayment(InitializePaymentRequestModel model)
    {
        var key = _configuration.GetSection("Paystack")["APIKey"];
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var endPoint = "https://api.paystack.co/transaction/initialize";
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);
        var content = new StringContent(JsonSerializer.Serialize(new
        {
            amount = model.CoursePrice,
            email = model.Email,
            reference = model.RefrenceNo,
            currency = "NGN",
            callback_url = model.CallbackUrl,

        }), Encoding.UTF8, "application/json");
        var response = await client.PostAsync(endPoint, content);
        var resString = await response.Content.ReadAsStringAsync();
        var responseObj = JsonSerializer.Deserialize<InitializePaymentResponseModel>(resString);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return responseObj;
        }
        return responseObj;
    }

    public async Task<TransferMoneyToUserResponseModel> TransferMoneyToUser(CreateTransferRecipientResponseModel model)
    {
        var key = _configuration.GetSection("Paystack")["APIKey"];
        var getHttpClient = new HttpClient();
        getHttpClient.DefaultRequestHeaders.Accept.Clear();
        getHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var baseUri = $"https://api.paystack.co/transfer";
        getHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);
        var response = await getHttpClient.PostAsJsonAsync(baseUri, new
        {
            recipient = model.data.recipient_code,
            amount = 56000 * 100,
            reference = Guid.NewGuid().ToString().Replace('-', 'y'),
            currency = "NGN",
            source = "balance",
        });
        var responseString = await response.Content.ReadAsStringAsync();
        //
        var responseObj = JsonSerializer.Deserialize<TransferMoneyToUserResponseModel>(responseString);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return responseObj;
        }
        return responseObj;
    }

    public async Task<VerifyAccountNumberResponseModel> VerifyAccountNumber(VerifyAccountNumberRequestModel model)
    {
        var key = _configuration.GetSection("Paystack")["APIKey"];
        var getHttpClient = new HttpClient();
        getHttpClient.DefaultRequestHeaders.Accept.Clear();
        getHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        getHttpClient.BaseAddress = new Uri($"https://api.paystack.co/bank/resolve?account_number={model.AccountNumber}&bank_code={model.BankCode}");
        getHttpClient.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", key);
        var response = await getHttpClient.GetAsync(getHttpClient.BaseAddress);
        var responseString = await response.Content.ReadAsStringAsync();
        var responseObj = JsonSerializer.Deserialize<VerifyAccountNumberResponseModel>(responseString);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return responseObj;
        }
        return responseObj;
    }
}
