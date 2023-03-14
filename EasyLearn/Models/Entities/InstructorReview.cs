namespace EasyLearn.Models.Entities
{
    public class InstructorReview : BaseEntity
    {
        public Instructor Instructor { get; set; }
        public string InstructorId { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
}
