namespace EasyLearn.Models.Entities
{
    public class User : BaseEntity
    {
        public Student Student { get; set; }
        public Moderator Moderator { get; set; }
        public SuperAdmin SuperAdmin { get; set; }
        public Instructor Instructor { get; set; }
        public Address Address { get; set; }
        public IEnumerable<Notification> Notifications { get; set; } = new HashSet<Notification>();
    }
}
