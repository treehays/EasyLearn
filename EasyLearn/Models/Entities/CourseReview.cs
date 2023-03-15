namespace EasyLearn.Models.Entities
{
    public class CourseReview : BaseEntity
    {
        public string Rating { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string Reported { get; set; }
        public string VerifiedPurchase { get; set; }
        public string Visibility { get; set; }
        public string InstructorFeedBack { get; set; }
        public string UserFeedBack { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
    }
}
