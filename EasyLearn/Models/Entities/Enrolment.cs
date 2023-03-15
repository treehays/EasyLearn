namespace EasyLearn.Models.Entities
{
    public class Enrolment : BaseEntity
    {
        public string CompletionStatus { get; set; }
        public string Grade { get; set; }
        public string CertificateNumber { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime AccessExpiration { get; set; }
        public string InstructorFeedBack { get; set; }
        public string UserFeedBack { get; set; }
        public string Coupon { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }

    }
}
