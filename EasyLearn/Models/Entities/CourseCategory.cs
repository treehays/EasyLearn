using EasyLearn.Models.Contracts;

namespace EasyLearn.Models.Entities
{
    public class CourseCategory : AuditableEntity
    {
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
