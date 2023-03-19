using System.Linq.Expressions;
using EasyLearn.Models.Contracts;
using EasyLearn.Repositories.Interfaces;

namespace EasyLearn.Repositories.Implimentations;

public class BaseRepository <T> :  IRepository<T> where T : BaseEntity, new()
{
    
    public async Task<T> Create(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<T> Update(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<T> Get(Expression<Func<T, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<T>> GetAll(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> expression)
    {
        throw new NotImplementedException();
    }
}