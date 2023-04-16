using EasyLearn.Models.DTOs.CategoryDTOs;
using EasyLearn.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EasyLearn.Models.DTOs.CourseDTOs
{
    public class CourseDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CourseLanguage { get; set; }
        public string DifficultyLevel { get; set; }
        //public string Rating { get; set; }
        public string Requirement { get; set; }
        public double CourseDuration { get; set; }
        public double Price { get; set; }
        public string CourseLogo{ get; set; }
        public string InstructorId { get; set; }

    }

    public class CreateCourseRequestModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CourseLanguage { get; set; }
        public string DifficultyLevel { get; set; }
        public string Requirement { get; set; }
        public double CourseDuration { get; set; }
        public string InstructorId { get; set; }
        public List<string> CourseCategories { get; set; } = new List<string>();
        public double Price { get; set; }
        public IFormFile FormFile { get; set; }
        public MultiSelectList multiSelectList { get; set; }
    }

    public class UpdateCourseRequestModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CourseLanguage { get; set; }
        public string DifficultyLevel { get; set; }
        public string Requirement { get; set; }
        public double CourseDuration { get; set; }
        public double Price { get; set; }

    }

    public class CourseRequestModel : BaseResponse
    {
        public CourseDTO Data { get; set; }
    }


    public class CoursesRequestModel : BaseResponse
    {
        public IEnumerable<CourseDTO> Data { get; set; }
    }


    public class UpdateCourseActiveStatusRequestModel
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
    }

}
