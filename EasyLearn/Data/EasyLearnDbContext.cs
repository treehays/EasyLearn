using EasyLearn.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyLearn.Data
{
    public class EasyLearnDbContext : DbContext
    {
        public EasyLearnDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<CourseReview> CourseReviews { get; set; }
        public DbSet<Enrolment> Enrolments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<InstructorReview> InstructorReviews { get; set; }
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentDetails> PaymentDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AcceptedNigerianBank> AcceptedNigerianBanks { get; set; }


    }
}
