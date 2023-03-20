using EasyLearn.Models.Contracts;

namespace EasyLearn.Models.Entities
{
    public class PaymentDetails : AuditableEntity
    {
        public string Bank { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
