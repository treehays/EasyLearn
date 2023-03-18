﻿using EasyLearn.Models.Contracts;

namespace EasyLearn.Models.Entities
{
    public class Course : AuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CourseLanguage { get; set; }
        public string DifficultyLevel { get; set; }
        public string Rating { get; set; }
        public string Requirement { get; set; }
        public double CourseDuration { get; set; }
        public double Price { get; set; }
        public Instructor Instructor { get; set; }
        public string InstructorId { get; set; }
        public IEnumerable<StudentCourse> StudentCourses { get; set; } = new HashSet<StudentCourse>();
        public IEnumerable<Module> Modules { get; set; } = new HashSet<Module>();
        public IEnumerable<Enrolment> Enrolments { get; set; }
        public IEnumerable<CourseCategory> CourseCategories { get; set; }
        public IEnumerable<CourseReview> CourseReviews { get; set; } = new HashSet<CourseReview>();
        public IEnumerable<Payment> Payments { get; set; } = new HashSet<Payment>();

    }
}
