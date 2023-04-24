using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Models.Entities;

namespace EasyLearn.GateWays.Mappers.UserMappers;

public class UserMapperService : IUserMapperService
{

    public UserDTO ConvertToUserResponseModel(User user)
    {
        var userModel = new UserDTO
        {
            Id = user?.Id,
            FirstName = user?.FirstName,
            LastName = user?.LastName,
            Email = user?.Email,
            Password = user?.Password,
            ProfilePicture = user?.ProfilePicture,
            Biography = user?.Biography,
            Skill = user?.Skill,
            Interest = user?.Interest,
            PhoneNumber = user?.PhoneNumber,
            Gender = user.Gender,
            StudentshipStatus = user.StudentshipStatus,
            RoleId = user?.RoleId,
            EmailConfirmed = user.EmailConfirmed,
            EmailToken = user?.EmailToken,
            UserName = user?.UserName,
            IsActive = user.IsActive,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed

        };
        return userModel;
    }
}
