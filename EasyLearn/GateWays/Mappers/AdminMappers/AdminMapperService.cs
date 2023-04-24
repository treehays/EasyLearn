using EasyLearn.Models.DTOs.AdminDTOs;
using EasyLearn.Models.Entities;

namespace EasyLearn.GateWays.Mappers.AdminMappers;

public class AdminMapperService : IAdminMapperService
{

    public AdminDto ConvertToAdminResponseModel(User user)
    {
        var adminModel = new AdminDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Password = user.Password,
            ProfilePicture = user.ProfilePicture,
            Biography = user.Biography,
            Skill = user.Skill,
            Interest = user.Interest,
            PhoneNumber = user.PhoneNumber,
            Gender = user.Gender,
            StudentshipStatus = user.StudentshipStatus,
            RoleId = user.RoleId,
        };
        return adminModel;
    }
}
