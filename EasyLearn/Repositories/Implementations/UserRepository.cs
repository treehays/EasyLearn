using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

    public async Task<User> GetUserByTokenAsync(string token)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.EmailToken == token);
        return user;
    }
    public async Task<User> GetFullDetails(Expression<Func<User, bool>> expression)
    {
        var user = await _context.Users
            .Include(x => x.Instructor)
            .Include(x => x.Student)
            .FirstOrDefaultAsync(expression);
        return user;

    }
}