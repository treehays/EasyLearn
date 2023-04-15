using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;

namespace EasyLearn.Repositories.Implementations;

public class CourseRepository : BaseRepository<Course>, ICourseRepository
{
    public CourseRepository(EasyLearnDbContext context)
    {
        _context = context;
    }

    //public Task<Course> SearchCourse(string word)
    //{
    //    var result1 = _context.Courses.Where(t1 => t1.Title.Contains(word)).ToList();
    //    var result2 = _context.CourseCategories.Where(t1 => t1.CategoryId.Contains(word)).ToList();
    //    var result3 = _context.Students.Where(t1 => t1.CreatedBy.Contains(word)).ToList();
    //    var result4 = _context.CourseReviews.Where(t1 => t1.Title.Contains(word)).ToList();
    //    var result5 = _context.Users.Where(t1 => t1.UserName.Contains(word)).ToList();

    //    //var combinedResult = result1.Concat(result2).Concat(result3).Concat(result4).Concat(result5).ToList();
    //    // Combine the results from all five tables into a single list
    //    // Process the combined result as needed
    //    return null;

    //}
}