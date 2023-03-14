namespace EasyLearn.Models.Entities
{
    public class SuperAdmin : BaseEntity
    {
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
