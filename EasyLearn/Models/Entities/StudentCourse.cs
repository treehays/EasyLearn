using EasyLearn.Models.Contracts;

namespace EasyLearn.Models.Entities
{
    public class StudentCourse : AuditableEntity
    {

        public string StudentId { get; set; }
        public bool IsPaid { get; set; }
        public Student Student { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
        //public CourseStatus { get; set; }
    }
}
