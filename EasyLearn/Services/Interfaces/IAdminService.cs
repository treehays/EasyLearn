using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.AdminDTOs;

namespace EasyLearn.Services.Interfaces;

public interface IAdminService
{
    Task<BaseResponse> CreateAdmin(CreateAdminRequestModel model);
}