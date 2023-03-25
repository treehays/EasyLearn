using EasyLearn.Models.Entities;

namespace EasyLearn.Repositories.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<bool> ExistByCategoryNameAsync(string categoryName);

}