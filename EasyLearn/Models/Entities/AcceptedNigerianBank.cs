using EasyLearn.Models.Contracts;

namespace EasyLearn.Models.Entities;

public class AcceptedNigerianBank : AuditableEntity
{
    public string BankName { get; set; }
    public string BankCode { get; set; }
}
