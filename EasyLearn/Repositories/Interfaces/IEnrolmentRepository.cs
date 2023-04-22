using EasyLearn.Models.Entities;
using System.Linq.Expressions;

namespace EasyLearn.Repositories.Interfaces;

public interface IEnrolmentRepository : IRepository<Enrolment>
{
    Task<ICollection<Enrolment>> GetStudentEnrolledCourses(Expression<Func<Enrolment, bool>> expression);

}