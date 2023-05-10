using EasyLearn.Models.Contracts;

namespace EasyLearn.Models.Entities;

public class Wallet : AuditableEntity
{
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}
