using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.PaymentDetailDTOs;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Models.Entities;

namespace EasyLearn.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> UserRegistration(CreateUserRequestModel model, string baseUrl);
        Task<BaseResponse> EmailVerification(string emailToken, string userId);
        Task<UserResponseModel> PasswordRessetConfirmation(string emailToken, string userId);
        Task<BaseResponse> ResetPassword(string email, string baseUrl);
        Task<BaseResponse> UpdateUserPassword(UpdateUserPasswordRequestModel model, string userId);

        //Task<BaseResponse> EmailReVerification(string emailToken);
        //Task<bool> Testing(string email, string OTPKey);
        Task<BaseResponse> UpdateAddress(UpdateUserAddressRequestModel model, string userId);
        Task<LoginRequestModel> Login(LoginRequestModel model);
        Task<BaseResponse> UpdateBankDetail(UpdateUserBankDetailRequestModel model, string userId);
        Task<PaymentDetailRequestModel> GetPaymentDetailPaymentId(string paymentId, string userId);
        Task<PaymentsDetailRequestModel> ListOfUserBankDetails(string userId);
        Task<UserResponseModel> GetFullDetailById(string id);
        Task<BaseResponse> UpdateProfile(UpdateUserProfileRequestModel model, string userId);
        Task<UsersResponseModel> GetByUsersNameOrEmail(string emailOrname);
        Task<BaseResponse> UpgradeUser(UserUpgradeRequestModel model, string userId);
        Task<UserResponseModel> GetByIdAsync(string userId);
    }
}
