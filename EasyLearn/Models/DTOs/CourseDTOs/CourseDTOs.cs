namespace EasyLearn.Models.DTOs.CourseDTOs
{
    public class CourseDTOs
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CourseLanguage { get; set; }
        public string DifficultyLevel { get; set; }
        public string Rating { get; set; }
        public string Requirement { get; set; }
        public double CourseDuration { get; set; }
        public double Price { get; set; }
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
        public string CategoryId { get; set; }
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

    }

    public class CourseRequestModel : BaseResponse
    {
        public CourseDTOs Data { get; set; }
    }


    public class CoursesRequestModel : BaseResponse
    {
        public IEnumerable<CourseDTOs> Data { get; set; }
    }


    public class UpdateCourseActiveStatusRequestModel
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
    }

}
