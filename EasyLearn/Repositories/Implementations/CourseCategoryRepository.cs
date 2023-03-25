using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;

namespace EasyLearn.Repositories.Implementations
{
    public class CourseCategoryRepository : BaseRepository<CourseCategory>, ICourseCategoryRepository
    {
        public CourseCategoryRepository(EasyLearnDbContext context)
        {
            _context = context;
        }
    }
}
