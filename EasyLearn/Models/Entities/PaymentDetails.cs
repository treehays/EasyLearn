namespace EasyLearn.Models.Entities
{
    public class PaymentDetails : BaseEntity
    {
        public Instructor Instructor { get; set; }
        public string InstructorId { get; set; }
    }
}
