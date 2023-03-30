using EasyLearn.Models.Entities;
using System.Linq.Expressions;

namespace EasyLearn.Repositories.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    Task<User> GetFullDetailByIdAsync(Expression<Func<User, bool>> expression);

}