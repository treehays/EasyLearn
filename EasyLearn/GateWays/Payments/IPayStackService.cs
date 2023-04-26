using EasyLearn.GateWays.Payments.PaymentGatewayDTOs;

namespace EasyLearn.GateWays.Payments;

public interface IPayStackService
{
    Task<InitializePaymentResponseModel> InitializePayment(InitializePaymentRequestModel model);
    Task<VerifyAccountNumberResponseModel> VerifyAccountNumber(VerifyAccountNumberRequestModel model);
    Task<CreateTransferRecipientResponseModel> CreateTransferRecipient(CreateTransferRecipientRequestModel model);
    Task<VerifyTransactionResponseModel> VerifyTransaction(string referenceNumber);
    Task<TransferMoneyToUserResponseModel> TransferMoneyToUser(CreateTransferRecipientResponseModel model);

}
