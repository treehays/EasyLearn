using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.ModeratorDTOs;
using EasyLearn.Models.DTOs.PaymentDetailDTOs;
using EasyLearn.Models.DTOs.UserDTOs;

namespace EasyLearn.Services.Interfaces;

public interface IModeratorService
{
    Task<BaseResponse> ModeratorRegistration(CreateUserRequestModel model, string baseUrl);
    Task<BaseResponse> Delete(string id);
    Task<ModeratorsResponseModel> GetAll();
    Task<ModeratorsResponseModel> GetAllInActive();
    Task<ModeratorsResponseModel> GetAllActive();
    Task<ModeratorResponseModel> GetByEmail(string email);
    Task<ModeratorResponseModel> GetById(string id);
    Task<ModeratorResponseModel> GetFullDetailById(string id);
    Task<PaymentDetailRequestModel> GetByPaymentDetail(string id);
    Task<PaymentsDetailRequestModel> GetListOfModeratorBankDetails(string userId);
    Task<ModeratorsResponseModel> GetByName(string name);
    Task<BaseResponse> UpdateActiveStatus(UpdateModeratorActiveStatusRequestModel model);
    Task<BaseResponse> UpdateAddress(UpdateModeratorAddressRequestModel model);
    Task<BaseResponse> UpdateBankDetail(UpdateModeratorBankDetailRequestModel model);
    Task<BaseResponse> UpdatePassword(UpdateModeratorPasswordRequestModel model);
    Task<BaseResponse> UpdateProfile(UpdateModeratorProfileRequestModel model);
}