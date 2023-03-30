using EasyLearn.Models.Contracts;

namespace EasyLearn.Models.Entities
{
    public class Admin : AuditableEntity
    {
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
