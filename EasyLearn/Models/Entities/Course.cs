using EasyLearn.Models.Contracts;
using EasyLearn.Models.Enums;

namespace EasyLearn.Models.Entities
{
    public class Course : AuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public CourseLanguage CourseLanguage { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        //public string Rating { get; set; }
        public string Requirement { get; set; }
        public double CourseDuration { get; set; }
        public decimal Price { get; set; }
        public int NumbersOfEnrollment { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
        public string CourseLogo { get; set; }
        public string InstructorId { get; set; }

        public string Coupon { get; set; }
        public Instructor Instructor { get; set; }
        public IEnumerable<StudentCourse> StudentCourses { get; set; } = new HashSet<StudentCourse>();
        public IEnumerable<Module> Modules { get; set; } = new HashSet<Module>();
        public IEnumerable<Enrolment> Enrolments { get; set; }
        public List<CourseCategory> CourseCategories { get; set; } = new List<CourseCategory>();
        public IEnumerable<CourseReview> CourseReviews { get; set; } = new HashSet<CourseReview>();
        public IEnumerable<Payment> Payments { get; set; } = new HashSet<Payment>();

    }
}
