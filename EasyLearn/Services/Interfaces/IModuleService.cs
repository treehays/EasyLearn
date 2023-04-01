using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.ModulesDTOs;

namespace EasyLearn.Services.Interfaces
{
    public interface IModuleService
    {
        Task<BaseResponse> Create(CreateModuleRequestModel model);
        Task<BaseResponse> Delete(string id);
        Task<BaseResponse> Update(UpdateModuleRequestModel model);
        Task<ModuleResponseModel> GetById(string id);
        Task<ModuleResponseModel> GetByCourse(string courseId, string moduleId);
        Task<ModulesResponseModel> GetAll();
        Task<ModulesResponseModel> GetNotDeleted();
    }
}
