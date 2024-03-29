﻿using EasyLearn.Models.Contracts;

namespace EasyLearn.Models.Entities
{
    public class Module : AuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Resources { get; set; }
        /// <summary>
        /// this wil just be a links to other courses
        /// </summary>
        public string Prerequisites { get; set; }
        public string Objective { get; set; }
        public TimeSpan ModuleDuration { get; set; }
        public int SequenceOfModule { get; set; }
        public string VideoSequence { get; set; }
        public string TemplateId { get; set; }
        public bool IsAvailable { get; set; }
        public string VideoPath { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }

    }
}
