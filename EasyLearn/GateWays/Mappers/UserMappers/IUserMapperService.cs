using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Models.Entities;

namespace EasyLearn.GateWays.Mappers.UserMappers;

public interface IUserMapperService
{
    UserDTO ConvertToUserResponseModel(User user);

}
