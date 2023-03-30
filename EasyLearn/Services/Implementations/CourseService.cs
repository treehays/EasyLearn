using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.CourseDTOs;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;

namespace EasyLearn.Services.Implementations;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICourseCategoryRepository _courseCategoryRepository;
    private readonly ICategoryRepository _CategoryRepository;


    public CourseService(ICourseRepository courseRepository, IHttpContextAccessor httpContextAccessor, ICourseCategoryRepository courseCategoryRepository, IWebHostEnvironment webHostEnvironment)
    {
        _courseRepository = courseRepository;
        _httpContextAccessor = httpContextAccessor;
        _courseCategoryRepository = courseCategoryRepository;
        _webHostEnvironment = webHostEnvironment;
    }

    /// <summary>
    /// Same instructor can not create course with exactly the saqme name
    public async Task<BaseResponse> Create(CreateCourseRequestModel model)
    {
        var courseInstructor = await _courseRepository.GetAsync(x => x.Title == model.Title && x.InstructorId == model.InstructorId);
        if (courseInstructor != null)
        {
            return new BaseResponse
            {
                Status = false,
                Message = "Course course with same name already exist...",
            };
        }

        string fileRelativePathx = null;

        if (model.FormFile != null || model.FormFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "abdullahpicture", "profilePictures");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetFileName(model.FormFile.FileName);
            fileRelativePathx = "/uploads/profilePictures/" + fileName;
            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.FormFile.CopyToAsync(stream);
            }
        }
        //var category = new List<Category>();
        //foreach (var item in model.CourseCategories)
        //{
        //    //var get = await _CategoryRepository.GetAsync(x => x.Id == item);

        //    //category.Add(get);
        //}

        var cate = model.CourseCategories.Select(async (y) => await _courseCategoryRepository.GetAsync((x => x.Id == y)));
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
            CreatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            CreatedOn = DateTime.Now,
            //CourseCategories = courseCategory,
        };
        foreach (var item in model.CourseCategories)
        {
            var courseCategory = new CourseCategory
            {
                Id = Guid.NewGuid().ToString(),
                CourseId = course.Id,
                CategoryId = item,
            };
            course.CourseCategories.Add(courseCategory);
        }

        await _courseRepository.AddAsync(course);
        await _courseRepository.SaveChangesAsync();
        var cc = new List<CourseCategory>();



        //await _courseCategoryRepository.AddAsync(courseCategory);
        //await _courseRepository.SaveChangesAsync();


        return new BaseResponse
        {
            Status = true,
            Message = "Course Created successfully...",
        };
    }


    public async Task<BaseResponse> Delete(string id)
    {
        var course = await _courseRepository.GetAsync(x => x.Id == id);
        if (course == null)
        {
            return new BaseResponse
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        course.IsDeleted = true;
        course.DeletedOn = DateTime.Now;
        course.DeletedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _courseRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "Course successfully deleted...",
            Status = true,
        };

    }

    public async Task<CoursesRequestModel> GetAll()
    {
        var courses = await _courseRepository.GetAllAsync();
        if (courses == null)
        {
            return new CoursesRequestModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        var coursesModel = new CoursesRequestModel
        {
            Status = true,
            Message = "Course retrieved successfully ...",
            Data = courses.Select(x => new CourseDTOs
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CourseLanguage = x.CourseLanguage,
                DifficultyLevel = x.DifficultyLevel,
                Requirement = x.Requirement,
                CourseDuration = x.CourseDuration,
                InstructorId = x.InstructorId,
                Price = x.Price,
            })
        };
        return coursesModel;
    }

    public async Task<CoursesRequestModel> GetAllActive()
    {
        var courses = await _courseRepository.GetListAsync(x => x.IsActive && !x.IsDeleted);
        if (courses == null)
        {
            return new CoursesRequestModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        var coursesModel = new CoursesRequestModel
        {
            Status = true,
            Message = "Course retrieved successfully ...",
            Data = courses.Select(x => new CourseDTOs
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CourseLanguage = x.CourseLanguage,
                DifficultyLevel = x.DifficultyLevel,
                Requirement = x.Requirement,
                CourseDuration = x.CourseDuration,
                InstructorId = x.InstructorId,
                Price = x.Price,
            })
        };
        return coursesModel;

    }

    public async Task<CoursesRequestModel> GetAllInActive()
    {
        var courses = await _courseRepository.GetListAsync(x => !x.IsActive && !x.IsDeleted);
        if (courses == null)
        {
            return new CoursesRequestModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        var coursesModel = new CoursesRequestModel
        {
            Status = true,
            Message = "Course retrieved successfully ...",
            Data = courses.Select(x => new CourseDTOs
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CourseLanguage = x.CourseLanguage,
                DifficultyLevel = x.DifficultyLevel,
                Requirement = x.Requirement,
                CourseDuration = x.CourseDuration,
                InstructorId = x.InstructorId,
                Price = x.Price,
            })
        };
        return coursesModel;
    }

    public async Task<CourseRequestModel> GetById(string id)
    {
        var course = await _courseRepository.GetAsync(x => x.Id == id);

        if (course == null)
        {
            return new CourseRequestModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        var courseModel = new CourseRequestModel
        {
            Status = true,
            Message = "Course retrieved successfully ...",
            Data = new CourseDTOs
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                CourseLanguage = course.CourseLanguage,
                DifficultyLevel = course.DifficultyLevel,
                Requirement = course.Requirement,
                CourseDuration = course.CourseDuration,
                InstructorId = course.InstructorId,
                Price = course.Price,
            },
        };
        return courseModel;
    }

    public async Task<CoursesRequestModel> GetByName(string name)
    {
        var courses = await _courseRepository.GetListAsync(x => x.Title == name && x.IsActive && !x.IsDeleted);
        if (courses == null)
        {
            return new CoursesRequestModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        var coursesModel = new CoursesRequestModel
        {
            Status = true,
            Message = "Course retrieved successfully ...",
            Data = courses.Select(x => new CourseDTOs
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CourseLanguage = x.CourseLanguage,
                DifficultyLevel = x.DifficultyLevel,
                Requirement = x.Requirement,
                CourseDuration = x.CourseDuration,
                InstructorId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Price = x.Price,
            })
        };
        return coursesModel;
    }

    public async Task<BaseResponse> Update(UpdateCourseRequestModel model)
    {
        var course = await _courseRepository.GetAsync(x => x.Id == model.Id);

        if (course == null)
        {
            return new CourseRequestModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        course.Title = model.Title;
        course.Description = model.Description;
        course.CourseLanguage = model.CourseLanguage;
        course.DifficultyLevel = model.DifficultyLevel;
        course.Requirement = model.Requirement;
        course.CourseDuration = model.CourseDuration;
        course.Price = model.Price;
        course.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        course.ModifiedOn = DateTime.Now;
        await _courseRepository.SaveChangesAsync();

        return new CourseRequestModel
        {
            Message = "Course updated successfully...",
            Status = true,
        };

    }

    public async Task<BaseResponse> UpdateActiveStatus(UpdateCourseActiveStatusRequestModel model)
    {
        var course = await _courseRepository.GetAsync(x => x.Id == model.Id);

        if (course == null)
        {
            return new CourseRequestModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        course.IsActive = model.IsActive;
        course.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        course.ModifiedOn = DateTime.Now;
        await _courseRepository.SaveChangesAsync();

        return new CourseRequestModel
        {
            Message = "Course updated successfully...",
            Status = true,
        };
    }
}

