using EasyLearn.Models.Contracts;

namespace EasyLearn.Models.Entities
{
    public class Moderator : AuditableEntity
    {
        public string Biography { get; set; }
        public string Skill { get; set; }
        public string Interest { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public IEnumerable<Instructor> Instructors { get; set; } = new HashSet<Instructor>();
    }
}
