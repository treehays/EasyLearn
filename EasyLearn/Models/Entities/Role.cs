﻿using EasyLearn.Models.Contracts;

namespace EasyLearn.Models.Entities
{
    public class Role : AuditableEntity
    {

        public string Description { get; set; }
        public User User { get; set; }
    }
}
