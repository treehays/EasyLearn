using EasyLearn.Models.Contracts;
using EasyLearn.Models.Enums;

namespace EasyLearn.Models.Entities;

public class Enrolment : AuditableEntity
{
    public CompletionStatus CompletionStatus { get; set; }
    public string Grade { get; set; }
    public string CertificateNumber { get; set; }
    public DateTime? AccessExpiration { get; set; }
    public string PaymentId { get; set; }
    public string StudentId { get; set; }
    public string CourseId { get; set; }
    public Student Student { get; set; }
    public Payment Payment { get; set; }
    public Course Course { get; set; }
    //public string InstructorFeedBack { get; set; }
    //public string UserFeedBack { get; set; }
    //public PaymentStatus PaymentStatus { get; set; }

}
