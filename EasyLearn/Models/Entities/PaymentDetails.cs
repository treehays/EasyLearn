using EasyLearn.Models.Contracts;

namespace EasyLearn.Models.Entities
{
    public class PaymentDetails : AuditableEntity
    {
        public string Bank { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public Instructor Instructor { get; set; }
        public string InstructorId { get; set; }
    }
}
