using EasyLearn.Models.DTOs.CategoryDTOs;
using EasyLearn.Models.DTOs.CourseDTOs;
using EasyLearn.Models.DTOs.InstructorDTOs;

namespace EasyLearn.Models.ViewModels;

public class GlobalSearchResultViewModel
{
    public CategoriesResponseModel CategoriesResponseModel { get; set; }
    public InstructorsResponseModel InstructorsResponseModel { get; set; }
    public CoursesRequestModel CoursesRequestModel { get; set; }
}
