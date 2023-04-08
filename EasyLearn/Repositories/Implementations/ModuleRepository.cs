using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EasyLearn.Repositories.Implementations;

public class ModuleRepository : BaseRepository<Module>, IModuleRepository
{
    public ModuleRepository(EasyLearnDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetLastElement()
    {
        //var value = await _context.Modules.OrderBy(x => x.SequenceOfModule).LastOrDefaultAsync();
        if (await _context.Modules.AnyAsync())
        {
        var value = await _context.Modules.MaxAsync(x => x.SequenceOfModule);
        return value;
        }
        return 0;
    }
}