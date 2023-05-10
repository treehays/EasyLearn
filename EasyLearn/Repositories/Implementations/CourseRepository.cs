using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EasyLearn.Repositories.Implementations;

public class CourseRepository : BaseRepository<Course>, ICourseRepository
{
    public CourseRepository(EasyLearnDbContext context)
    {
        _context = context;
    }

    public async Task<Course> GetCourseByIdWithStudentCourses(string courseId, string studentId)
    {
        var course = await _context.Courses
            .Include(l => l.StudentCourses)
            .FirstOrDefaultAsync(m => m.Id == courseId && m.StudentCourses
            .Any(x => x.StudentId == studentId && x.CourseId == courseId));
        return course;
    }

    public async Task<Course> GetCourseByIdWithInstructorDetail(Expression<Func<Course, bool>> expression)
    {
        var course = await _context.Courses.AsNoTracking()
            .Include(x => x.CourseCategories).ThenInclude(x => x.Category)
            .Include(x => x.Instructor).ThenInclude(x => x.User)
            .FirstOrDefaultAsync(expression);
        return course;
    }

    public async Task<StudentCourse> StudentIsEnrolled(string courseId, string studentId)
    {
        var studentCourse = await _context.StudentCourses.Include(x => x.Course)
            .FirstOrDefaultAsync(x => x.StudentId == studentId && x.CourseId == courseId);
        return studentCourse;
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