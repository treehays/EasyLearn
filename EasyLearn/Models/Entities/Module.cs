namespace EasyLearn.Models.Entities
{
    public class Module : BaseEntity
    {
        public string CourseId { get; set; }
        public Course Course { get; set; }
    }
}
