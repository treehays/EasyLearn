using EasyLearn.Models.Entities;
using EasyLearn.Models.Enums;

namespace EasyLearn.Models.DTOs.PaymentDTOs;

public class PaymentDTO
{
    public int Id { get; set; }
    public string ReferrenceNumber { get; set; }
    public double PaymentAmount { get; set; }
    public PaymentMethods PaymentMethod { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public string CourseId { get; set; }
    public string StudentId { get; set; }

    public Student Student { get; set; }
    public Course Course { get; set; }
}
