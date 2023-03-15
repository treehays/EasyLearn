﻿namespace EasyLearn.Models.Entities
{
    public class Payment : BaseEntity
    {
        public double PaymentAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string CouponUsed { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }

    }
}
