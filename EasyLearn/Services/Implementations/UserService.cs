using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;

namespace EasyLearn.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginRequestModel> Login(string username, string email)
        {
            var user = await _userRepository.GetAsync(x => x.UserName == username || x.Email == email);
            if (user == null)
            {
                return new LoginRequestModel
                {
                    Message = "invalid login details",
                    Status = false,
                };
            }
            var loginModel = new LoginRequestModel
            {
                Message = "Login successfully..",
                Status = true,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId,
                LastName = user.LastName,
                FirstName = user.FirstName,
                ProfilePicture = user.ProfilePicture,
                Id = user.Id,
            };
            return loginModel;
        }
    }
}
