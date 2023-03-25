using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.CourseDTOs;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;
using System.Security.Claims;

namespace EasyLearn.Services.Implementations;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICourseCategoryRepository _courseCategoryRepository;


    public CourseService(ICourseRepository courseRepository, IHttpContextAccessor httpContextAccessor, ICourseCategoryRepository courseCategoryRepository)
    {
        _courseRepository = courseRepository;
        _httpContextAccessor = httpContextAccessor;
        _courseCategoryRepository = courseCategoryRepository;
    }

    /// <summary>
    /// Same instructor can not create course with exactly the saqme name
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<BaseResponse> Create(CreateCourseRequestModel model)
    {
        var courseInstructor = await _courseRepository.GetAsync(x => x.Title == model.Title && x.InstructorId == model.InstructorId);
        var course = new Course
        {
            Id = Guid.NewGuid().ToString(),
            Title = model.Title,
            InstructorId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            Description = model.Description,
            CourseLanguage = model.CourseLanguage,
            DifficultyLevel = model.DifficultyLevel,
            Requirement = model.Requirement,
            CourseDuration = model.CourseDuration,
        };
        await _courseRepository.AddAsync(course);
        await _courseRepository.SaveChangesAsync();

        var courseCategory = new CourseCategory
        {
            CourseId = course.Id,
            CategoryId = model.CategoryId,
        };
        await _courseCategoryRepository.AddAsync(courseCategory);
        await _courseRepository.SaveChangesAsync();

        return new BaseResponse
        {
            Status = true,
            Message = "Course Created successfully...",
        };
    }


    public async Task<BaseResponse> Delete(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<CoursesRequestModel> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<CoursesRequestModel> GetAllActive()
    {
        throw new NotImplementedException();
    }

    public async Task<CoursesRequestModel> GetAllInActive()
    {
        throw new NotImplementedException();
    }

    public async Task<CourseRequestModel> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<CoursesRequestModel> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse> Update(UpdateCourseRequestModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse> UpdateActiveStatus(UpdateCourseActiveStatusRequestModel model)
    {
        throw new NotImplementedException();
    }
}