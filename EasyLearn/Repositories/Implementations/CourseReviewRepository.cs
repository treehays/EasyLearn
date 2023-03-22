using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;

namespace EasyLearn.Repositories.Implementations;

public class CourseReviewRepository : BaseRepository<CourseReview>, ICourseReviewRepository
{
    private readonly EasyLearnDbContext _context;

    public CourseReviewRepository(EasyLearnDbContext context)
    {
        _context = context;
    }
    
}