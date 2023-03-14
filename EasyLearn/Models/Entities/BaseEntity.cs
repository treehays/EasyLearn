namespace EasyLearn.Models.Entities
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public string DeleteBy { get; set; }
        public DateTime DeleteOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
