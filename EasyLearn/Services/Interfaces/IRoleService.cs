using EasyLearn.Models.DTOs.AdminDTOs;
using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.RoleDTOs;

namespace EasyLearn.Services.Interfaces;

public interface IRoleService
{
    Task<BaseResponse> Create(CreateRoleRequestModel model);
    Task<BaseResponse> Delete(string id);
    Task<BaseResponse> UpdateRole(UpdateRoleProfileRequestModel model);
    Task<RoleResponseModel> GetById(string id);
    Task<RolesResponseModel> GetAll();

}