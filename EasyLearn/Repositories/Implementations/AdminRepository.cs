using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;

namespace EasyLearn.Repositories.Implementations;

public class AdminRepository : BaseRepository<Admin>, IAdminRepository
{
    private readonly EasyLearnDbContext _context;

    public AdminRepository(EasyLearnDbContext context)
    {
        _context = context;
    }
    
}