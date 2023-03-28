namespace EasyLearn.Models.DTOs.PaymentDetailDTOs
{
    public class PaymentDetailDTO
    {
        public string Id { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }

        public string AccountType { get; set; }
    }


    public class PaymentsDetailRequestModel : BaseResponse
    {
        public IEnumerable<PaymentDetailDTO> Data { get; set; }
    }

}
