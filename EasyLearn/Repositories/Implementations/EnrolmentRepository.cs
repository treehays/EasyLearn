using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;

namespace EasyLearn.Repositories.Implementations;

public class EnrolmentRepository : BaseRepository<Enrolment>, IEnrolmentRepository
{
    private readonly EasyLearnDbContext _context;

    public EnrolmentRepository(EasyLearnDbContext context)
    {
        _context = context;
    }
}