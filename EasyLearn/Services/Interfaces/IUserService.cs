using EasyLearn.Models.DTOs.UserDTOs;

namespace EasyLearn.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginRequestModel> Login(string username, string email);
    }
}
