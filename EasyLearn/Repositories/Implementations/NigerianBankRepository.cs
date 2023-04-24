using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;

namespace EasyLearn.Repositories.Implementations;

public class NigerianBankRepository : BaseRepository<AcceptedNigerianBank>, INigerianBankRepository
{
    public NigerianBankRepository(EasyLearnDbContext context)
    {
        _context = context;
    }
}
