using EasyLearn.Models.Contracts;

namespace EasyLearn.Models.Entities
{
    public class CourseReview : AuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string Rating { get; set; }
        public bool IsReported { get; set; }
        public bool IsVerifiedPurchase { get; set; }
        public bool IsVisibile { get; set; }
        public string CourseId { get; set; }
        public string StudentId { get; set; }
        //public string InstructorFeedBack { get; set; }
        // public string UserFeedBack { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
