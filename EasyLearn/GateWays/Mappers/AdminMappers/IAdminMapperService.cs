using EasyLearn.Models.DTOs.AdminDTOs;
using EasyLearn.Models.Entities;

namespace EasyLearn.GateWays.Mappers.AdminMappers;

public interface IAdminMapperService
{
    AdminDto ConvertToAdminResponseModel(User user);

}
