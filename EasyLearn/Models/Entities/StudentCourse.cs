namespace EasyLearn.Models.Entities
{
    public class StudentCourse : BaseEntity
    {
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
    }
}
