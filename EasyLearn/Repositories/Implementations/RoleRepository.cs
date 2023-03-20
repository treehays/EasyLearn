using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;

namespace EasyLearn.Repositories.Implementations;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    private readonly EasyLearnDbContext _context;

    public RoleRepository(EasyLearnDbContext context)
    {
        _context = context;
    }
}