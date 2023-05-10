using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.FIleManagerDTOs;
using EasyLearn.Models.DTOs.ModuleDTOs;

namespace EasyLearn.Services.Interfaces
{
    public interface IModuleService
    {
        Task<BaseResponse> Create(CreateModuleRequestModel model, string instructorId);
        Task<BaseResponse> Delete(string id, string userId);
        Task<CSVFileResponseModel> GenerateModuleUploaderTemplate(CSVFileRequestModel model, string UserId, string UserEmail);
        Task<BaseResponse> ModuleUploaderTemplate(string fileName, string userId, string userEmail, string courseId);
        Task<BaseResponse> Update(UpdateModuleRequestModel model, string userId);
        Task<ModuleResponseModel> GetByVideoSequesnce(string id);
        Task<ModuleResponseModel> GetById(string id);
        Task<ModulesResponseModel> GetCourseContentsByCourseInstructor(string courseId, string instructorId);
        Task<ModulesResponseModel> GetCourseContentsByEnrolledStudent(string courseId, string studentId);
        Task<ModuleResponseModel> GetSingleModuleByStudent(string moduleId, string studentId);
        //Task<ModulesResponseModel> GetCourseContents(string courseId);
        Task<ModulesResponseModel> GetAll();
        Task<ModulesResponseModel> GetNotDeleted();
    }
}
