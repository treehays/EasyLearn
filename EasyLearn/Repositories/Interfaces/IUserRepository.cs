using EasyLearn.Models.DTOs.PaymentDetailDTOs;
using EasyLearn.Models.Entities;

namespace EasyLearn.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<bool> ExistByEmailAsync(string email);
}