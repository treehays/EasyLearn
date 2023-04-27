using EasyLearn.Models.DTOs.CourseDTOs;
using EasyLearn.Models.Entities;

namespace EasyLearn.GateWays.Mappers.CourseMappers;

public interface ICourseMapperService
{
    CourseDTO ConvertToCourseResponseModel(Course course);
}
