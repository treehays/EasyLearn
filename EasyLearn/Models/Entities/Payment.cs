using EasyLearn.Models.Contracts;
using EasyLearn.Models.Enums;

namespace EasyLearn.Models.Entities
{
    public class Payment : AuditableEntity
    {
        public double PaymentAmount { get; set; }
        public PaymentMethods PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string CouponUsed { get; set; }
        public string CourseId { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }

    }
}
