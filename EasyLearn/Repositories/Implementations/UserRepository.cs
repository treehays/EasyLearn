using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EasyLearn.Repositories.Implementations;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(EasyLearnDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistByEmailAsync(string email)
    {
        var emailChecker = await _context.Users.AnyAsync(user => user.Email == email);
        return emailChecker;
    }
}