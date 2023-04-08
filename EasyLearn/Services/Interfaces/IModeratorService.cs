using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.ModeratorDTOs;
using EasyLearn.Models.DTOs.UserDTOs;

namespace EasyLearn.Services.Interfaces;

public interface IModeratorService
{
    Task<BaseResponse> ModeratorRegistration(CreateUserRequestModel model, string baseUrl);
    Task<BaseResponse> Delete(string id);
    Task<BaseResponse> UpdateProfile(UpdateModeratorProfileRequestModel model);
    Task<BaseResponse> UpdateBankDetail(UpdateModeratorBankDetailRequestModel model);
    Task<BaseResponse> UpdateAddress(UpdateModeratorAddressRequestModel model);
    Task<BaseResponse> UpdatePassword(UpdateModeratorPasswordRequestModel model);
    Task<BaseResponse> UpdateActiveStatus(UpdateModeratorActiveStatusRequestModel model);
    Task<ModeratorResponseModel> GetById(string id);
    Task<ModeratorResponseModel> GetFullDetailById(string id);
    Task<ModeratorResponseModel> GetByEmail(string email);
    Task<ModeratorsResponseModel> GetByName(string name);
    Task<ModeratorsResponseModel> GetAll();
    Task<ModeratorsResponseModel> GetAllActive();
    Task<ModeratorsResponseModel> GetAllInActive();
}