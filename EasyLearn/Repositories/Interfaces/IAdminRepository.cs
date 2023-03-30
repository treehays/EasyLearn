using EasyLearn.Models.DTOs.AdminDTOs;
using EasyLearn.Models.Entities;
using System.Linq.Expressions;

namespace EasyLearn.Repositories.Interfaces;

public interface IAdminRepository : IRepository<Admin>
{
    Task<User> GetFullDetailByIdAsync(Expression<Func<User, bool>> expression);

}