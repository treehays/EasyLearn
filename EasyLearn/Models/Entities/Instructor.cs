﻿using EasyLearn.Models.Contracts;

namespace EasyLearn.Models.Entities
{
    public class Instructor : AuditableEntity
    {
        public string Biography { get; set; }
        public string Skill { get; set; }
        public string Interest { get; set; }
        public string? VerifyBy { get; set; }
        public DateTime? VerifyOn { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public string ModeratorId { get; set; }
        public Moderator Moderator { get; set; }
        public IEnumerable<Course> Courses { get; set; } = new HashSet<Course>();
        public IEnumerable<InstructorReview> InstructorReviews { get; set; } = new HashSet<InstructorReview>();

    }
}
