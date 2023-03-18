using EasyLearn.Models.Contracts;

namespace EasyLearn.Models.Entities
{
    public class Student : AuditableEntity
    {
        public User User { get; set; }
        public string UserId { get; set; }
        //public string  EmploymentStatus { get; set; }

        public IEnumerable<StudentCourse> StudentCourses { get; set; } = new HashSet<StudentCourse>();
        public IEnumerable<CourseReview> CourseReviews { get; set; } = new HashSet<CourseReview>();
        public IEnumerable<InstructorReview> InstructorReviews { get; set; } = new HashSet<InstructorReview>();
        public IEnumerable<Enrolment> Enrolments { get; set; } = new HashSet<Enrolment>();
    }
}
