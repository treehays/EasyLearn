using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.CourseDTOs;
using EasyLearn.Models.ViewModels;

namespace EasyLearn.Services.Interfaces;

public interface ICourseService
{
    Task<BaseResponse> Create(CreateCourseRequestModel model);
    Task<CourseResponseModel> GetCourseByIdFull(string id);
    Task<BaseResponse> Delete(string id, string userId);
    Task<CoursesResponseModel> GetAllInstructorCourse(string instructorId);
    Task<CoursesEnrolledRequestModel> GetEnrolledCourses(string studentId);
    Task<CoursesEnrolledRequestModel> StudentActiveCourses(string studentId);
    Task<CoursesEnrolledRequestModel> GetCompletedCourses(string studentId);
    Task<CoursesResponseModel> GetAllCourse();
    Task<CoursesResponseModel> GetAllActiveCourse(string instructorId);
    Task<CoursesResponseModel> GetAllInActiveCourse();
    Task<CoursesResponseModel> GetAllInActiveCourse(string instructorId);
    Task<CoursesResponseModel> GetAllActiveCourse();
    Task<GlobalSearchResultViewModel> GlobalSearch(string name);
    Task<BaseResponse> Update(UpdateCourseRequestModel model);
    Task<BaseResponse> UpdateActiveStatus(UpdateCourseActiveStatusRequestModel model);
    Task<CourseResponseModel> GetById(string id);
    Task<CoursesResponseModel> GetByName(string name);
}