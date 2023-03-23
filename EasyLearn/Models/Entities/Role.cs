using EasyLearn.Models.Contracts;

namespace EasyLearn.Models.Entities
{
    public class Role : AuditableEntity
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
        public IEnumerable<User> User { get; set; }
    }
}
