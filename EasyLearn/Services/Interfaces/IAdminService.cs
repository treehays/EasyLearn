using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.AdminDTOs;
using EasyLearn.Models.DTOs.PaymentDetailDTOs;
using EasyLearn.Models.DTOs.UserDTOs;

namespace EasyLearn.Services.Interfaces;

public interface IAdminService
{
    Task<PaymentsDetailRequestModel> GetListOfAdminBankDetails(string UserId);
    Task<BaseResponse> Create(CreateUserRequestModel model);
    Task<BaseResponse> Delete(string id);
    Task<BaseResponse> UpdateProfile(UpdateUserProfileRequestModel model);
    Task<BaseResponse> UpdateBankDetail(UpdateUserBankDetailRequestModel model);
    Task<BaseResponse> UpdateAddress(UpdateUserAddressRequestModel model);
    Task<BaseResponse> UpdatePassword(UpdateUserPasswordRequestModel model);
    Task<BaseResponse> UpdateActiveStatus(UpdateUserActiveStatusRequestModel model);
    Task<AdminResponseModel> GetById(string id);
    Task<PaymentDetailRequestModel> GetBankDetail(string id);
    Task<AdminResponseModel> GetFullDetailById(string id);
    Task<AdminResponseModel> GetByEmail(string email);
    Task<AdminsResponseModel> GetByName(string name);
    Task<AdminsResponseModel> GetAll();
    Task<AdminsResponseModel> GetAllActive();
    Task<AdminsResponseModel> GetAllInActive();
}