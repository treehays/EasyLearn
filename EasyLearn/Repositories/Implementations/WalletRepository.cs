using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;

namespace EasyLearn.Repositories.Implementations;

public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
{
    public WalletRepository(EasyLearnDbContext context)
    {
        _context = context;
    }
}
