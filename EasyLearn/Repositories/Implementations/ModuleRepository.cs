using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;

namespace EasyLearn.Repositories.Implementations;

public class ModuleRepository : BaseRepository<Module>, IModuleRepository
{
    public ModuleRepository(EasyLearnDbContext context)
    {
        _context = context;
    }
}