using EasyLearn.Models.DTOs.CourseDTOs;
using EasyLearn.Models.Entities;

namespace EasyLearn.GateWays.Mappers.CourseMappers;

public class CourseMapperService : ICourseMapperService
{
    public CourseDTO ConvertToCourseResponseModel(Course course)
    {
        var courseModel = new CourseDTO
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            CourseLanguage = course.CourseLanguage,
            DifficultyLevel = course.DifficultyLevel,
            Requirement = course.Requirement,
            CourseDuration = course.CourseDuration,
            InstructorId = course.InstructorId,
            Price = course.Price,
            CourseLogo = course.CourseLogo,
            NumbersOfEnrollment = course.NumbersOfEnrollment,
        };
        return courseModel;
    }

}

