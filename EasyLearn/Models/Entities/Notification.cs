using EasyLearn.Models.Contracts;

namespace EasyLearn.Models.Entities
{
    public class Notification : AuditableEntity
    {
        public string NotificationType { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
