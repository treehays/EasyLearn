using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.CourseDTOs;

namespace EasyLearn.Services.Interfaces;

public interface ICourseService
{
    Task<BaseResponse> Create(CreateCourseRequestModel model);
    Task<BaseResponse> Delete(string id);
    Task<CoursesRequestModel> GetAllInstructorCourse(string instructorId);
    Task<CoursesRequestModel> GetAllCourse();
    Task<CoursesRequestModel> GetAllActiveInstructorCourse(string instructorId);
    Task<CoursesRequestModel> GetAllInActiveInstructorCourse(string instructorId);
    Task<BaseResponse> Update(UpdateCourseRequestModel model);
    Task<BaseResponse> UpdateActiveStatus(UpdateCourseActiveStatusRequestModel model);
    Task<CourseRequestModel> GetById(string id);
    Task<CoursesRequestModel> GetByName(string name);
}