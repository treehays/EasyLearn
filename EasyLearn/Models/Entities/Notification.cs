namespace EasyLearn.Models.Entities
{
    public class Notification : BaseEntity
    {
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
