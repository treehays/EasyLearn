using EasyLearn.Models.Contracts;

namespace EasyLearn.Models.Entities
{
    public class User : AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfilePicture { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
        public Student Student { get; set; }
        public Moderator Moderator { get; set; }
        public Admin Admin { get; set; }
        public Instructor Instructor { get; set; }
        public Address Address { get; set; }
        public IEnumerable<Notification> Notifications { get; set; } = new HashSet<Notification>();
    }
}
