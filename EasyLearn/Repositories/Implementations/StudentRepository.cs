using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EasyLearn.Repositories.Implementations;

public class StudentRepository : BaseRepository<Student>, IStudentRepository
{
    public StudentRepository(EasyLearnDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetFullDetailByIdAsync(Expression<Func<User, bool>> expression)
    {
        var student = await _context.Users
          .Include(a => a.Student)
          .Include(b => b.PaymentDetails)
          .Include(c => c.Address)
          .FirstOrDefaultAsync(expression);
        return student;
    }
}