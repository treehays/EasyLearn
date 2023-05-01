using EasyLearn.Models.Entities;
using System.Linq.Expressions;

namespace EasyLearn.Repositories.Interfaces;

public interface IModuleRepository : IRepository<Module>
{
    Task<int> GetLastElement(Expression<Func<Module, bool>> expression);
}