using EasyLearn.Data;
using EasyLearn.Models.DTOs.AdminDTOs;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EasyLearn.Repositories.Implementations;

public class AdminRepository : BaseRepository<Admin>, IAdminRepository
{
    public AdminRepository(EasyLearnDbContext context)
    {
        _context = context;
    }


    public async Task<Admin> GetFullDetailByIdAsync(string id)
    {
        var admin = await _context.Admins
            .Include(a => a.User)
            .ThenInclude(b => b.Address)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        return admin;
    }
    
}