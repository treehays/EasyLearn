namespace EasyLearn.Models.Entities
{
    public class Student : BaseEntity
    {
        public User User { get; set; }
        public string UserId { get; set; }

        public IEnumerable<StudentCourse> StudentCourses { get; set; } = new HashSet<StudentCourse>();
        public IEnumerable<CourseReview> CourseReviews { get; set; } = new HashSet<CourseReview>();
        public IEnumerable<InstructorReview> InstructorReviews { get; set; } = new HashSet<InstructorReview>();
        public IEnumerable<Enrolment> Enrolments { get; set; } = new HashSet<Enrolment>();
    }
}
