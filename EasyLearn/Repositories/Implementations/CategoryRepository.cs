using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;

namespace EasyLearn.Repositories.Implementations;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    private readonly EasyLearnDbContext _context;

    public CategoryRepository(EasyLearnDbContext context)
    {
        _context = context;
    }
}