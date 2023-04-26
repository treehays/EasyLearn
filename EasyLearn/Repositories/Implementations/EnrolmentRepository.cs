using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EasyLearn.Repositories.Implementations;

public class EnrolmentRepository : BaseRepository<Enrolment>, IEnrolmentRepository
{

    public EnrolmentRepository(EasyLearnDbContext context)
    {
        _context = context;
    }
    public async Task<ICollection<Enrolment>> GetStudentEnrolledCourses(Expression<Func<Enrolment, bool>> expression)
    {
        var enrolments = await _context.Enrolments
            .Include(x => x.Course)
            .Include(x => x.Student).ThenInclude(x => x.User)
            .Where(expression).ToListAsync();
        return enrolments;
    }
}