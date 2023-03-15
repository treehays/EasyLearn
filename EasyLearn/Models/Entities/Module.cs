namespace EasyLearn.Models.Entities
{
    public class Module : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Resources { get; set; }
        public string Prerequisites { get; set; }
        public string Objective { get; set; }
        public string DifficultyLevel { get; set; }
        public string FeedBack { get; set; }
        public string CompletionStatus { get; set; }
        public double Duration { get; set; }
        public int SequenceOfModule { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
    }
}
