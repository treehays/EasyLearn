using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EasyLearn.Repositories.Implementations;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{

    public CategoryRepository(EasyLearnDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistByCategoryNameAsync(string categoryName)
    {
        var categoryExist = await _context.Categories.AnyAsync(x => x.Name == categoryName);
        return categoryExist;
    }
}