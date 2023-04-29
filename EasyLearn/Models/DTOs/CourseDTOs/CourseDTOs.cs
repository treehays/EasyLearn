using EasyLearn.Models.DTOs.CategoryDTOs;
using EasyLearn.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EasyLearn.Models.DTOs.CourseDTOs
{
    public class CourseDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public CourseLanguage CourseLanguage { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public int NumbersOfEnrollment { get; set; }
        //public string Rating { get; set; }
        public string Requirement { get; set; }
        public double CourseDuration { get; set; }
        public decimal Price { get; set; }
        public string CourseLogo { get; set; }
        public DateTime CreatedOn { get; set; }
        public string InstructorId { get; set; }
        public string InstructorName { get; set; }
        public List<CategoryNameResponseModel> CategoriesName { get; set; }
        public CompletionStatus CompletionStatus { get; set; }
        public bool IsPaid { get; set; }
        public bool IsActive { get; set; }

    }

    public class CreateCourseRequestModel
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public CourseLanguage CourseLanguage { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public string Requirement { get; set; }
        public double CourseDuration { get; set; }
        public string InstructorId { get; set; }
        public List<string> CourseCategories { get; set; } = new List<string>();
        public decimal Price { get; set; }
        public IFormFile FormFile { get; set; }
        public MultiSelectList multiSelectList { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateCourseRequestModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public CourseLanguage CourseLanguage { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public string Requirement { get; set; }
        public double CourseDuration { get; set; }
        public bool IsActive { get; set; }
        public decimal Price { get; set; }

    }

    public class CourseResponseModel : BaseResponse
    {
        public CourseDTO Data { get; set; }
    }


    public class CoursesResponseModel : BaseResponse
    {
        public int NumberOfCourse { get; set; }
        public IEnumerable<CourseDTO> Data { get; set; }
    }

    public class CoursesEnrolledRequestModel : BaseResponse
    {
        public IEnumerable<CourseDTO> Data { get; set; }


    }


    public class UpdateCourseActiveStatusRequestModel
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
    }

}
