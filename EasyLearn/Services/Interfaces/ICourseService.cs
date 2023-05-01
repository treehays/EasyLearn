using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.CourseDTOs;
using EasyLearn.Models.ViewModels;

namespace EasyLearn.Services.Interfaces;

public interface ICourseService
{
    Task<BaseResponse> Create(CreateCourseRequestModel model, string instructorId);
    Task<CourseResponseModel> GetFullDetailOfCourseById(string id);
    Task<BaseResponse> Delete(string id, string userId);
    Task<CoursesResponseModel> GetAllCoursesByAnInstructor(string instructorId);
    Task<CoursesResponseModel> GetActiveCoursesOfAnInstructor(string instructorId);
    Task<CoursesResponseModel> GetInActiveCoursesOfAnInstructor(string instructorId);
    Task<CoursesResponseModel> GetUnverifiedCoursesOfAnInstructor(string instructorId);
    Task<CoursesEnrolledRequestModel> GetEnrolledCourses(string studentId);
    Task<CoursesEnrolledRequestModel> UnpaidCourse(string studentId);
    Task<CoursesEnrolledRequestModel> StudentActiveCourses(string studentId);
    Task<CoursesEnrolledRequestModel> GetCompletedCourses(string studentId);
    Task<CoursesResponseModel> GetAllCourse();
    Task<BaseResponse> RejectCourse(string courseId, string userId);
    Task<BaseResponse> ApproveCourse(string courseId, string userId);
    Task<CoursesResponseModel> GetAllUnVerifiedCourse();
    Task<CoursesResponseModel> GetAllInActiveCourse();
    Task<CoursesResponseModel> GetAllActiveCourse();
    Task<GlobalSearchResultViewModel> GlobalSearch(string name);
    Task<BaseResponse> Update(UpdateCourseRequestModel model, string userId);
    Task<BaseResponse> UpdateActiveStatus(UpdateCourseActiveStatusRequestModel model, string userId);
    Task<CourseResponseModel> GetById(string id);
    Task<CoursesResponseModel> GetByName(string name);
}