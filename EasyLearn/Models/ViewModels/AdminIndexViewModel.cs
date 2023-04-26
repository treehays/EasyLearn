using EasyLearn.Models.DTOs.CourseDTOs;
using EasyLearn.Models.DTOs.EnrolmentDTOs;

namespace EasyLearn.Models.ViewModels;

public class AdminIndexViewModel
{
    public EnrolmentsResponseModel Enrolments { get; set; }
    public CoursesResponseModel Courses { get; set; }
}
