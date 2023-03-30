using EasyLearn.Models.DTOs.PaymentDetailDTOs;
using EasyLearn.Models.Entities;
using System.Linq.Expressions;

namespace EasyLearn.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<bool> ExistByEmailAsync(string email);
    Task<User> GetFullDetails(Expression<Func<User, bool>> expression);
}