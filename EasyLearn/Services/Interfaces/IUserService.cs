using EasyLearn.Models.DTOs.UserDTOs;

namespace EasyLearn.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> Testing(string email, string OTPKey);
        Task<LoginRequestModel> Login(LoginRequestModel model);
    }
}
