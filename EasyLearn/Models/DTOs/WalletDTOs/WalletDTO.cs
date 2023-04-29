

namespace EasyLearn.Models.DTOs.WalletDTOs;

public class WalletDTO
{
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }
}


public class CreateWalletequestModel
{
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public string Description { get; set; }
}

public class WalletResponseModel : BaseResponse
{
    public IEnumerable<WalletDTO> Data { get; set; }
}
