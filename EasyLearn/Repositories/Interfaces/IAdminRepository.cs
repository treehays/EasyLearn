using EasyLearn.Models.DTOs.AdminDTOs;
using EasyLearn.Models.Entities;

namespace EasyLearn.Repositories.Interfaces;

public interface IAdminRepository : IRepository<Admin>
{
    Task<Admin> GetFullDetailByIdAsync(string id);

}