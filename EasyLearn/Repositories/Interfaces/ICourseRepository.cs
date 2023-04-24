using EasyLearn.Models.Entities;

namespace EasyLearn.Repositories.Interfaces;

public interface ICourseRepository : IRepository<Course>
{
    Task<Course> GetCourseByIdDetailed(string courseId, string studentId);
    Task<StudentCourse> StudentIsEnrolled(string courseId, string studentId);
}