namespace EasyLearn.Models.Entities
{
    public class Instructor : BaseEntity
    {
        public string? VerifyBy { get; set; }
        public DateTime VerifyOn { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public string ModeratorId { get; set; }
        public Moderator Moderator { get; set; }
        public IEnumerable<Course> Courses { get; set; } = new HashSet<Course>();
        public IEnumerable<PaymentDetails> PaymentDetails { get; set; } = new HashSet<PaymentDetails>();
        public IEnumerable<InstructorReview> InstructorReviews { get; set; } = new HashSet<InstructorReview>();

    }
}
