namespace EasyLearn.Models.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryImage { get; set; }
        public string Status { get; set; }
        public IEnumerable<CourseCategory> CourseCategories { get; set; }
    }
}
