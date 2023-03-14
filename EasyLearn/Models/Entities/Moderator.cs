namespace EasyLearn.Models.Entities
{
    public class Moderator : BaseEntity
    {
        public User User { get; set; }
        public string UserId { get; set; }
        public IEnumerable<Instructor> Instructors { get; set; } = new HashSet<Instructor>();
    }
}
