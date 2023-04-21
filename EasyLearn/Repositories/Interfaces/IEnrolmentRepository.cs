using EasyLearn.Models.Entities;

namespace EasyLearn.Repositories.Interfaces;

public interface IEnrolmentRepository : IRepository<Enrolment>
{
    Task<ICollection<Enrolment>> GetStudentEnrolledCourses(string studentId);

}