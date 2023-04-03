﻿using EasyLearn.Models.DTOs.UserDTOs;
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

        public async Task<LoginRequestModel> Login(string email)
        {
            var user = await _userRepository.GetFullDetails(x => x.UserName.ToUpper() == email.ToUpper() || x.Email.ToUpper() == email.ToUpper());
            if (user == null)
            {
                return new LoginRequestModel
                {
                    Message = "invalid login details",
                    Status = false,
                };
            }

            var instructorId = user.Instructor != null ? user.Instructor.Id : null;
            var ModeratorId = user.Moderator != null ? user.Moderator.Id : null;
            var AdminId = user.Admin != null ? user.Admin.Id : null;
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
                Id = instructorId ?? ModeratorId ?? AdminId,
                UserId = user.Id,
            };
            return loginModel;
        }
    }
}
