using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.AdminDTOs;

namespace EasyLearn.Services.Interfaces;

public interface IAdminService
{
    Task<BaseResponse> Create(CreateAdminRequestModel model);
    Task<BaseResponse> Delete (string id);
    Task<BaseResponse> UpdateProfile(UpdateAdminProfileRequestModel model);
    Task<BaseResponse> UpdateBankDetail (UpdateAdminBankDetailRequestModel model);
    Task<BaseResponse> UpdateAddress (UpdateAdminAddressRequestModel model);
    Task<BaseResponse> UpdatePassword (UpdateAdminPasswordRequestModel model);
    Task<BaseResponse> UpdateActiveStatus (UpdateAdminActiveStatusRequestModel model);
    Task<AdminResponseModel> GetById(string id);
    Task<AdminResponseModel> GetFullDetailById(string id);
    Task<AdminResponseModel> GetByEmail(string email);
    Task<AdminsResponseModel> GetByName(string name);
    Task<AdminsResponseModel> GetAll();
    Task<AdminsResponseModel> GetAllActive();
    Task<AdminsResponseModel> GetAllInActive();
}