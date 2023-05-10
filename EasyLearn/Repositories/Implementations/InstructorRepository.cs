using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EasyLearn.Repositories.Implementations;

public class InstructorRepository : BaseRepository<Instructor>, IInstructorRepository
{

    public InstructorRepository(EasyLearnDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetFullDetailByIdAsync(Expression<Func<User, bool>> expression)
    {
        var instructor = await _context.Users
              .Include(a => a.Instructor)
              .Include(b => b.PaymentDetails)
              .Include(b => b.Address)
              .FirstOrDefaultAsync(expression);
        return instructor;
    }

    public async Task<Instructor> GetInstructorFullDetailAsync(Expression<Func<Instructor, bool>> expression)
    {
        var instructor = await _context.Instructors
             .Include(a => a.User)
             //.ThenInclude(b => b.PaymentDetails)
             //.Include(b => b.User.Address)
             .FirstOrDefaultAsync(expression);
        return instructor;
    }
}