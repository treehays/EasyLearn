using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EasyLearn.Repositories.Implementations;

public class ModuleRepository : BaseRepository<Module>, IModuleRepository
{
    public ModuleRepository(EasyLearnDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetLastElement(Expression<Func<Module, bool>> expression)
    {
        //var value = await _context.Modules.OrderBy(x => x.SequenceOfModule).LastOrDefaultAsync();
        if (await _context.Modules.AnyAsync(expression))
        {
            var value = await _context.Modules.Where(expression).MaxAsync(x => x.SequenceOfModule);
            return value;
        }
        return 0;
    }
}