namespace EasyLearn.Models.Entities
{
    public class Role : BaseEntity
    {
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
