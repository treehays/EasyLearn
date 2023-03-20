using System.Linq.Expressions;
using EasyLearn.Data;
using EasyLearn.Models.Contracts;
using EasyLearn.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EasyLearn.Repositories.Implementations;

public class BaseRepository <T> :  IRepository<T> where T : BaseEntity, new()
{
    private readonly EasyLearnDbContext _context;
    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        //await _context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// I think  the save can be made wthout tjid method
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
     public async Task<T> UpdateAsync(T entity)
     {
         _context.Entry(entity).State = EntityState.Modified; 
         return  await Task.FromResult(entity);
     }

    public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
    {
        var entity = await _context.Set<T>().FirstOrDefaultAsync(expression);
        return entity;
    }

    public async Task<IEnumerable<T>> GetAllAsync(T entity)
    {
        var entities = await _context.Set<T>().AsNoTracking().ToListAsync();
        return entities;
    }

    public Task DeleteAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        //_context.Set<T>().Remove(entity);
        //await _context.SaveChangesAsync();
        return  Task.CompletedTask;
    }

    public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> expression)
    {
        var entities = await _context.Set<T>().AsNoTracking().Where(expression).ToListAsync();
        return entities;
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}