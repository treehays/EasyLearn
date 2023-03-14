namespace EasyLearn.Models.Entities
{
    public class Course : BaseEntity
    {
        public Instructor Instructor { get; set; }
        public string InstructorId { get; set; }
        public IEnumerable<StudentCourse> StudentCourses { get; set; } = new HashSet<StudentCourse>();
        public IEnumerable<Module> Modules { get; set; } = new HashSet<Module>();
    }
}
