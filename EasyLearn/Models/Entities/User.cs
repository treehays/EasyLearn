namespace EasyLearn.Models.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfilePicture { get; set; }
        public string Biography { get; set; }
        public string Skill { get; set; }
        public string Interest { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
        public Student Student { get; set; }
        public Moderator Moderator { get; set; }
        public SuperAdmin SuperAdmin { get; set; }
        public Instructor Instructor { get; set; }
        public Address Address { get; set; }
        public IEnumerable<Notification> Notifications { get; set; } = new HashSet<Notification>();
    }
}
