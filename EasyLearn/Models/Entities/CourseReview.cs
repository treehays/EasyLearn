namespace EasyLearn.Models.Entities
{
    public class CourseReview : BaseEntity
    {
        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
}
