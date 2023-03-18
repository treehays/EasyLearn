using System.Linq.Expressions;
using EasyLearn.Models.Contracts;

namespace EasyLearn.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        Task<T> Register(T entity);
        Task<T> Update (T entity);
        Task<T> Get (Expression<Func<T,bool>> expression);
        Task<IEnumerable<T>> GetAll (T entity);
        Task<bool> Delete(T entity);
        Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> expression);


    }
}
