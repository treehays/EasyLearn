using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;

namespace EasyLearn.Repositories.Implementations;

public class ModeratorRepository : BaseRepository<Moderator>, IModeratorRepository
{

    public ModeratorRepository(EasyLearnDbContext context)
    {
        _context = context;
    }
}