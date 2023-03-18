using EasyLearn.Models.Contracts;

namespace EasyLearn.Models.Entities
{
    public class Address : AuditableEntity
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Language { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
