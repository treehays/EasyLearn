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

public class PayStackResponse
{
    public bool status { get; set; }
    public string message { get; set; }
    public PayStackData data { get; set; }
}

public class PayStackData
{
    public string authorization_url { get; set; }
    public string access_code { get; set; }
    public string reference { get; set; }
}

public class SendMoneyDto
{
    public string AccountNumber { get; set; }
    public string BankCode { get; set; }
    public decimal Amount { get; set; }
}