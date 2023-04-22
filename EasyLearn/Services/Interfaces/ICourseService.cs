using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.CourseDTOs;
using EasyLearn.Models.ViewModels;

namespace EasyLearn.Services.Interfaces;

public interface ICourseService
{
    Task<BaseResponse> Create(CreateCourseRequestModel model);
    Task<BaseResponse> Delete(string id, string userId);
    Task<CoursesRequestModel> GetAllInstructorCourse(string instructorId);
    Task<CoursesEnrolledRequestModel> GetEnrolledCourses(string studentId);
    Task<CoursesEnrolledRequestModel> StudentActiveCourses(string studentId);
    Task<CoursesEnrolledRequestModel> GetCompletedCourses(string studentId);
    Task<CoursesRequestModel> GetAllCourse();
    Task<CoursesRequestModel> GetAllActiveInstructorCourse(string instructorId);
    Task<CoursesRequestModel> GetAllInActiveInstructorCourse(string instructorId);
    Task<GlobalSearchResultViewModel> GlobalSearch(string name);
    Task<BaseResponse> Update(UpdateCourseRequestModel model);
    Task<BaseResponse> UpdateActiveStatus(UpdateCourseActiveStatusRequestModel model);
    Task<CourseRequestModel> GetById(string id);
    Task<CoursesRequestModel> GetByName(string name);
}