using System.Linq.Expressions;
using EasyLearn.Models.Contracts;

namespace EasyLearn.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(List<T> entity);
        Task<T> UpdateAsync(T entity);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> expression);
        Task<int> SaveChangesAsync();


    }
}
