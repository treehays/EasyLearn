using EasyLearn.Models.Entities;
using System.Linq.Expressions;

namespace EasyLearn.Repositories.Interfaces;

public interface IInstructorRepository : IRepository<Instructor>
{
    Task<User> GetFullDetailByIdAsync(Expression<Func<User, bool>> expression);

}