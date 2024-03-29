﻿using EasyLearn.Models.Contracts;
using EasyLearn.Models.Enums;

namespace EasyLearn.Models.Entities
{
    public class User : AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ProfilePicture { get; set; }
        public string Biography { get; set; }
        public string Skill { get; set; }
        public string Interest { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public StudentshipStatus StudentshipStatus { get; set; }
        public string RoleId { get; set; }
        public bool IsActive { get; set; }
        public bool EmailConfirmed { get; set; }
        public string EmailToken { get; set; }
        public bool PhoneNumberConfirmed { get; set; }

        public Role Role { get; set; }
        public Student Student { get; set; }
        public Moderator Moderator { get; set; }
        public Admin Admin { get; set; }
        public Instructor Instructor { get; set; }
        public Address Address { get; set; }
        public Wallet Wallet { get; set; }
        public ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();
        public ICollection<PaymentDetails> PaymentDetails { get; set; } = new HashSet<PaymentDetails>();

    }
}
