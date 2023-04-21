using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EasyLearn.Repositories.Implementations;

public class EnrolmentRepository : BaseRepository<Enrolment>, IEnrolmentRepository
{

    public EnrolmentRepository(EasyLearnDbContext context)
    {
        _context = context;
    }
    public async Task<ICollection<Enrolment>> GetStudentEnrolledCourses(string studentId)
    {
        var enrolments = await _context.Enrolments
            .Include(x => x.Course)
            .Where(y => !y.IsDeleted && y.IsPaid && y.StudentId == studentId).ToListAsync();
        return enrolments;
    }
}