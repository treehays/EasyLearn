using EasyLearn.Data;
using EasyLearn.Models.DTOs.AdminDTOs;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EasyLearn.Repositories.Implementations;

public class AdminRepository : BaseRepository<Admin>, IAdminRepository
{
    public AdminRepository(EasyLearnDbContext context)
    {
        _context = context;
    }


    public async Task<User> GetFullDetailByIdAsync(Expression<Func<User, bool>> expression)
    {
        var admin = await _context.Users
          .Include(a => a.Admin)
          .Include(b => b.PaymentDetails)
          .Include(b => b.Address)
          .FirstOrDefaultAsync(expression);
        return admin;
    }

}