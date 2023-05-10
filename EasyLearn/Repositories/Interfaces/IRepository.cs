using EasyLearn.Models.Contracts;
using System.Linq.Expressions;

namespace EasyLearn.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(List<T> entity);
        Task<T> UpdateAsync(T entity);
        Task<List<T>> UpdateRanges(List<T> entity);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<ICollection<T>> GetAllAsync();
        Task DeleteAsync(T entity);
        Task<ICollection<T>> GetListAsync(Expression<Func<T, bool>> expression);
        Task<int> SaveChangesAsync();


    }
}
