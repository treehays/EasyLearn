using EasyLearn.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EasyLearn.Repositories.Interfaces;

public interface IModeratorRepository : IRepository<Moderator>
{

    Task<User> GetFullDetailByIdAsync(Expression<Func<User, bool>> expression);
}