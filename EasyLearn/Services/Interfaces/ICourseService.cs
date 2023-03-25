using EasyLearn.Models.DTOs.AdminDTOs;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.CourseDTOs;

namespace EasyLearn.Services.Interfaces;

public interface ICourseService
{
    Task<BaseResponse> Create(CreateCourseRequestModel model);
    Task<BaseResponse> Delete(string id);
    Task<CoursesRequestModel> GetAll();
    Task<CoursesRequestModel> GetAllActive();
    Task<CoursesRequestModel> GetAllInActive();
    Task<BaseResponse> Update(UpdateCourseRequestModel model);
    Task<BaseResponse> UpdateActiveStatus(UpdateCourseActiveStatusRequestModel model);
    Task<CourseRequestModel> GetById(string id);
    Task<CoursesRequestModel> GetByName(string name);
}