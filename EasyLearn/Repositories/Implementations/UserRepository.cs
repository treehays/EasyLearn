using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;

namespace EasyLearn.Repositories.Implementations;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly EasyLearnDbContext _context;

    public UserRepository(EasyLearnDbContext context)
    {
        _context = context;
    }
    
}