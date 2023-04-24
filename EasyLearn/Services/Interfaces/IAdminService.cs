using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.AdminDTOs;
using EasyLearn.Models.DTOs.PaymentDetailDTOs;
using EasyLearn.Models.DTOs.UserDTOs;

namespace EasyLearn.Services.Interfaces;

public interface IAdminService
{
    Task<BaseResponse> AdminRegistration(CreateUserRequestModel model, string baseUrl);
    Task<BaseResponse> Delete(string id);
    Task<UserResponseModel> GetById(string id);
    Task<BaseResponse> UpdateActiveStatus(UpdateUserActiveStatusRequestModel model);
    Task<BaseResponse> UpdateAddress(UpdateUserAddressRequestModel model);
    Task<BaseResponse> UpdateBankDetail(UpdateUserBankDetailRequestModel model);
    Task<BaseResponse> UpdatePassword(UpdateUserPasswordRequestModel model);
    Task<BaseResponse> UpdateProfile(UpdateUserProfileRequestModel model);
    Task<UsersResponseModel> GetAll();
    Task<UsersResponseModel> GetAllActive();
    Task<UsersResponseModel> GetAllInActive();
    Task<UserResponseModel> GetByEmail(string email);
    Task<UsersResponseModel> GetByName(string name);
    Task<PaymentDetailRequestModel> GetByPaymentDetail(string id);
    Task<AdminResponseModel> GetFullDetailById(string id);
    Task<PaymentsDetailRequestModel> GetListOfAdminBankDetails(string UserId);
    //Task<PaymentDetailRequestModel> GetBankDetail(string id);
}