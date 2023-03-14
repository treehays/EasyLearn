namespace EasyLearn.Models.Entities
{
    public class Address : BaseEntity
    {
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
