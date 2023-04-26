using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Models.Entities;

namespace EasyLearn.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> UserRegistration(CreateUserRequestModel model, string baseUrl);
        Task<BaseResponse> EmailVerification(string emailToken);
        //Task<BaseResponse> EmailReVerification(string emailToken);
        //Task<bool> Testing(string email, string OTPKey);
        Task<LoginRequestModel> Login(LoginRequestModel model);
        Task<BaseResponse> UpgradeUser(UserUpgradeRequestModel model, string userId);
        Task<UserResponseModel> GetByIdAsync(string userId);
    }
}
