using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.CategoryDTOs;
using EasyLearn.Models.DTOs.CourseDTOs;
using EasyLearn.Models.DTOs.CourseReviewDTOs;
using EasyLearn.Models.DTOs.InstructorDTOs;
using EasyLearn.Models.Entities;
using EasyLearn.Models.ViewModels;
using EasyLearn.Repositories.Implementations;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;
using System.Security.Claims;

namespace EasyLearn.Services.Implementations;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;


    public CourseService(ICourseRepository courseRepository, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment, IUserRepository userRepository, ICategoryRepository categoryRepository)
    {
        _courseRepository = courseRepository;
        _httpContextAccessor = httpContextAccessor;
        _webHostEnvironment = webHostEnvironment;
        _userRepository = userRepository;
        _categoryRepository = categoryRepository;
    }

    /// <summary>
    /// Same instructor can not create course with exactly the saqme name
    public async Task<BaseResponse> Create(CreateCourseRequestModel model)
    {
        var instructorId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Actor)?.Value;
        var courseInstructor = await _courseRepository.GetAsync(x => x.Title == model.Title && x.InstructorId == instructorId);
        if (courseInstructor != null)
        {
            return new BaseResponse
            {
                Status = false,
                Message = "Course course with same name already exist...",
            };
        }

        string fileRelativePathx = null;

        if (model.FormFile != null)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetFileName(model.FormFile.FileName);
            fileRelativePathx = "/uploads/images/" + fileName;
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

        //var cate = model.CourseCategories.Select(async (y) => await _courseCategoryRepository.GetAsync((x => x.Id == y)));
        var course = new Course
        {
            Id = Guid.NewGuid().ToString(),
            Title = model.Title,
            InstructorId = instructorId,
            Description = model.Description,
            CourseLanguage = model.CourseLanguage,
            DifficultyLevel = model.DifficultyLevel,
            Requirement = model.Requirement,
            CourseDuration = model.CourseDuration,
            CreatedBy = instructorId,
            CreatedOn = DateTime.Now,
            CourseLogo = fileRelativePathx,
            Price = model.Price,
            IsActive = true,
            //CourseCategories = dd,
        };

        var categoryList = model.CourseCategories.Select(x => new CourseCategory
        {
            Id = Guid.NewGuid().ToString(),
            CourseId = course.Id,
            CategoryId = x,
            CreatedBy = instructorId,
            CreatedOn = course.CreatedOn,
        }).ToList();
        course.CourseCategories = categoryList;

        await _courseRepository.AddAsync(course);
        await _courseRepository.SaveChangesAsync();


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

    public async Task<CoursesRequestModel> GetAllInstructorCourse(string instructorId)
    {
        var courses = await _courseRepository.GetListAsync(x => x.InstructorId == instructorId);
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
            Data = courses.Select(x => new CourseDTO
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

    public async Task<CoursesRequestModel> GetAllActiveInstructorCourse(string instructorId)
    {
        var courses = await _courseRepository.GetListAsync(x => x.IsActive && !x.IsDeleted && x.InstructorId == instructorId);
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
            Data = courses.Select(x => new CourseDTO
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

    public async Task<CoursesRequestModel> GetAllInActiveInstructorCourse(string instructorId)
    {
        var courses = await _courseRepository.GetListAsync(x => !x.IsActive && !x.IsDeleted && x.InstructorId == instructorId);
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
            Data = courses.Select(x => new CourseDTO
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
            Data = new CourseDTO
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
        var courses = await _courseRepository.GetListAsync(x =>
        x.IsActive
        && !x.IsDeleted
        && x.Title.ToUpper().Contains(name.ToUpper())
        || x.Description.ToUpper().Contains(name.ToUpper())
        || x.CourseLanguage.ToString().ToUpper().Contains(name.ToUpper()));

        if (courses.Count() > 0)
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
            Data = courses.Select(x => new CourseDTO
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CourseLanguage = x.CourseLanguage,
                DifficultyLevel = x.DifficultyLevel,
                Requirement = x.Requirement,
                CourseDuration = x.CourseDuration,
                //InstructorId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
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

    public async Task<CoursesRequestModel> GetAllCourse()
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
            Data = courses.Select(x => new CourseDTO
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

    public async Task<GlobalSearchResultViewModel> GlobalSearch(string name)
    {
        var coursesResult = await _courseRepository.GetListAsync(x =>
        x.IsActive
        && !x.IsDeleted
        && x.Title.ToUpper().Contains(name.ToUpper())
        || x.Description.ToUpper().Contains(name.ToUpper())
        || x.CourseLanguage.ToString().ToUpper().Contains(name.ToUpper()));
        var courseResponse = new CoursesRequestModel();
        if (coursesResult.Count() > 0)
        {
            courseResponse = new CoursesRequestModel
            {
                Status = true,
                Message = "Course retrieved successfully ...",
                Data = coursesResult.Select(x => new CourseDTO
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
                    CourseLogo = x.CourseLogo,
                })
            };
        }

        var instructorResult = await _userRepository.GetListAsync(x =>
         x.RoleId == "Instructor"
         && x.IsActive
         && !x.IsDeleted
         && x.FirstName.ToUpper().Contains(name.ToUpper())
         && x.LastName.ToUpper().Contains(name.ToUpper())
         && x.UserName.ToUpper().Contains(name.ToUpper())
         && x.Interest.ToUpper().Contains(name.ToUpper()));
        var instructorResponse = new InstructorsResponseModel();
        if (instructorResult.Count() > 0)
        {
            instructorResponse = new InstructorsResponseModel
            {
                Status = true,
                Message = "Details successfully retrieved...",
                Data = instructorResult.Select(x => new InstructorDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Password = x.Password,
                    ProfilePicture = x.ProfilePicture,
                    Biography = x.Biography,
                    Skill = x.Skill,
                    Interest = x.Interest,
                    PhoneNumber = x.PhoneNumber,
                    Gender = x.Gender,
                    StudentshipStatus = x.StudentshipStatus,
                    RoleId = x.RoleId,
                }),
            };
        }


        var categoriesResult = await _categoryRepository.GetListAsync(x =>
           x.IsAvailable
           && !x.IsDeleted
           && x.Name.ToUpper().Contains(name.ToUpper()));
        var categoriesRespons = new CategoriesResponseModel();

        if (categoriesResult.Count() > 0)
        {
            categoriesRespons = new CategoriesResponseModel
            {
                Status = true,
                Message = "Categories successfully retrieved...",
                Data = categoriesResult.Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CategoryImage = x.CategoryImage,
                    IsAvailable = x.IsAvailable,
                }),
            };
        }


        var globalResult = new GlobalSearchResultViewModel
        {
            CategoriesResponseModel = categoriesRespons,
            InstructorsResponseModel = instructorResponse,
            CoursesRequestModel = courseResponse,
        };

        return globalResult;
    }
}

