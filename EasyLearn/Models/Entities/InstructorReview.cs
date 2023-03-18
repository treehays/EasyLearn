using EasyLearn.Models.Contracts;

namespace EasyLearn.Models.Entities
{
    public class InstructorReview : AuditableEntity
    {
        public string Rating { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string Reported { get; set; }
        //public string VerifiedPurchase { get; set; }
        public string Visibility { get; set; }
        //public string InstructorFeedBack { get; set; }
        //public string UserFeedBack { get; set; }

        public Instructor Instructor { get; set; }
        public string InstructorId { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
}
