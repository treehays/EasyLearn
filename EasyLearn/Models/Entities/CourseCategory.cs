namespace EasyLearn.Models.Entities
{
    public class CourseCategory : BaseEntity
    {
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
