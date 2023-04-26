using EasyLearn.Models.Entities;
using System.Linq.Expressions;

namespace EasyLearn.Repositories.Interfaces;

public interface ICourseRepository : IRepository<Course>
{
    Task<Course> GetCourseByIdWithStudentCourses(string courseId, string studentId);
    Task<Course> GetCourseByIdWithInstructor(Expression<Func<Course, bool>> expression);
    Task<StudentCourse> StudentIsEnrolled(string courseId, string studentId);
}