using EasyLearn.Data;
using EasyLearn.Models.DTOs.ModeratorDTOs;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EasyLearn.Repositories.Implementations;

public class ModeratorRepository : BaseRepository<Moderator>, IModeratorRepository
{

    public ModeratorRepository(EasyLearnDbContext context)
    {
        _context = context;
    }


    public async Task<User> GetFullDetailByIdAsync(Expression<Func<User, bool>> expression)
    {
        var moderator = await _context.Users
          .Include(a => a.Moderator)
          .Include(b => b.PaymentDetails)
          .Include(b => b.Address)
          .FirstOrDefaultAsync(expression);
        return moderator;
    }
}