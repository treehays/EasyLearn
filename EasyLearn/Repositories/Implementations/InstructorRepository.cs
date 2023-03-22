using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;

namespace EasyLearn.Repositories.Implementations;

public class InstructorRepository : BaseRepository<Instructor>, IInstructorRepository
{

    public InstructorRepository(EasyLearnDbContext context)
    {
        _context = context;
    }
}