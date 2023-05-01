using EasyLearn.GateWays.Email;
using EasyLearn.GateWays.FileManager;
using EasyLearn.GateWays.Mappers.CourseMappers;
using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.CategoryDTOs;
using EasyLearn.Models.DTOs.CourseDTOs;
using EasyLearn.Models.DTOs.EmailSenderDTOs;
using EasyLearn.Models.DTOs.InstructorDTOs;
using EasyLearn.Models.Entities;
using EasyLearn.Models.Enums;
using EasyLearn.Models.ViewModels;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;

namespace EasyLearn.Services.Implementations;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEnrolmentRepository _enrolmentRepository;
    private readonly ISendInBlueEmailService _emailService;
    private readonly IFileManagerService _fileManagerService;
    private readonly ICourseMapperService _courseMapperService;

    public CourseService(ICourseRepository courseRepository, IUserRepository userRepository, ICategoryRepository categoryRepository, IFileManagerService fileManagerService, IEnrolmentRepository enrolmentRepository, ICourseMapperService courseMapperService, ISendInBlueEmailService emailService)
    {
        _courseRepository = courseRepository;
        _userRepository = userRepository;
        _categoryRepository = categoryRepository;
        _fileManagerService = fileManagerService;
        _enrolmentRepository = enrolmentRepository;
        _courseMapperService = courseMapperService;
        _emailService = emailService;
    }

    /// <summary>
    /// Same instructor can not create course with exactly the saqme name
    public async Task<BaseResponse> Create(CreateCourseRequestModel model, string instructorId)
    {
        var courseInstructor = await _courseRepository.GetAsync(x => x.Title == model.Title && x.InstructorId == instructorId);
        if (courseInstructor != null)
        {
            return new BaseResponse
            {
                Status = false,
                Message = "Course course with same name already exist...",
            };
        }


        var filePName = await _fileManagerService.GetFileName(model.FormFile, "uploads", "images", "course");

        var course = new Course
        {
            Id = Guid.NewGuid().ToString(),
            Title = model.Title,
            InstructorId = instructorId,
            Description = model.Description,
            CourseLanguage = model.CourseLanguage,
            DifficultyLevel = model.DifficultyLevel,
            Requirement = model.Requirement,
            CreatedBy = instructorId,
            CreatedOn = DateTime.Now,
            CourseLogo = filePName,
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


    public async Task<CourseResponseModel> GetById(string id)
    {
        var course = await _courseRepository.GetAsync(x => x.Id == id);

        if (course == null)
        {
            return new CourseResponseModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        var courseModel = new CourseResponseModel
        {
            Status = true,
            Message = "Course retrieved successfully ...",
            Data = _courseMapperService.ConvertToCourseResponseModel(course),
        };
        return courseModel;
    }

    public async Task<CoursesResponseModel> GetByName(string name)
    {
        var courses = await _courseRepository.GetListAsync(x =>
        x.IsActive
        && !x.IsDeleted
        && x.Title.ToUpper().Contains(name.ToUpper())
        || x.Description.ToUpper().Contains(name.ToUpper())
        || x.CourseLanguage.ToString().ToUpper().Contains(name.ToUpper()));

        if (courses.Count() > 0)
        {
            return new CoursesResponseModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        var coursesModel = new CoursesResponseModel
        {
            Status = true,
            Message = "Course retrieved successfully ...",
            Data = courses.Select(x => _courseMapperService.ConvertToCourseResponseModel(x)).ToList(),
        };
        return coursesModel;
    }


    public async Task<CoursesResponseModel> GetAllCourse()
    {
        var courses = await _courseRepository.GetListAsync(x => x.IsActive && !x.IsDeleted);
        if (courses == null)
        {
            return new CoursesResponseModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        var coursesModel = new CoursesResponseModel
        {
            Status = true,
            Message = "Course retrieved successfully ...",
            Data = courses.Select(x => _courseMapperService.ConvertToCourseResponseModel(x)).ToList(),
        };
        return coursesModel;
    }


    public async Task<CoursesResponseModel> GetAllUnVerifiedCourse()
    {
        var courses = await _courseRepository.GetListAsync(x => !x.IsVerified && x.IsActive && !x.IsDeleted);
        if (courses == null)
        {
            return new CoursesResponseModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        var coursesModel = new CoursesResponseModel
        {
            Status = true,
            Message = "Course retrieved successfully ...",
            Data = courses.Select(x => _courseMapperService.ConvertToCourseResponseModel(x)).ToList(),
        };
        return coursesModel;
    }


    public async Task<CoursesResponseModel> GetAllActiveCourse()
    {
        var courses = await _courseRepository.GetListAsync(x => x.IsActive && !x.IsDeleted && x.IsVerified);
        if (courses == null)
        {
            return new CoursesResponseModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        var coursesModel = new CoursesResponseModel
        {
            Status = true,
            Message = "Course retrieved successfully ...",
            Data = courses.Select(x => _courseMapperService.ConvertToCourseResponseModel(x)).ToList()
        };
        return coursesModel;

    }

    public async Task<CoursesResponseModel> GetAllInActiveCourse()
    {
        var courses = await _courseRepository.GetListAsync(x => !x.IsActive && !x.IsDeleted);
        if (courses == null)
        {
            return new CoursesResponseModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        var coursesModel = new CoursesResponseModel
        {
            NumberOfCourse = courses.Count(),
            Status = true,
            Message = "Course retrieved successfully ...",
            Data = courses.Select(x => _courseMapperService.ConvertToCourseResponseModel(x)).ToList()
        };
        return coursesModel;
    }


    public async Task<CoursesResponseModel> GetAllCoursesByAnInstructor(string instructorId)
    {
        var courses = await _courseRepository.GetListAsync(x => x.InstructorId == instructorId && !x.IsDeleted);
        if (courses == null)
        {
            return new CoursesResponseModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        var coursesModel = new CoursesResponseModel
        {
            Status = true,
            Message = "Course retrieved successfully ...",
            Data = courses.Select(x => _courseMapperService.ConvertToCourseResponseModel(x)).ToList()
        };
        return coursesModel;
    }

    public async Task<CoursesResponseModel> GetActiveCoursesOfAnInstructor(string instructorId)
    {
        var courses = await _courseRepository.GetListAsync(x => x.InstructorId == instructorId && !x.IsDeleted && x.IsActive && x.IsVerified);
        if (courses == null)
        {
            return new CoursesResponseModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        var coursesModel = new CoursesResponseModel
        {
            Status = true,
            Message = "Course retrieved successfully ...",
            Data = courses.Select(x => _courseMapperService.ConvertToCourseResponseModel(x)).ToList()
        };
        return coursesModel;
    }

    public async Task<CoursesResponseModel> GetInActiveCoursesOfAnInstructor(string instructorId)
    {
        var courses = await _courseRepository.GetListAsync(x => x.InstructorId == instructorId && !x.IsDeleted && !x.IsActive);
        if (courses == null)
        {
            return new CoursesResponseModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        var coursesModel = new CoursesResponseModel
        {
            Status = true,
            Message = "Course retrieved successfully ...",
            Data = courses.Select(x => _courseMapperService.ConvertToCourseResponseModel(x)).ToList()
        };
        return coursesModel;
    }

    public async Task<CoursesResponseModel> GetUnverifiedCoursesOfAnInstructor(string instructorId)
    {
        var courses = await _courseRepository.GetListAsync(x => x.InstructorId == instructorId && !x.IsDeleted && !x.IsVerified);
        if (courses == null)
        {
            return new CoursesResponseModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        var coursesModel = new CoursesResponseModel
        {
            Status = true,
            Message = "Course retrieved successfully ...",
            Data = courses.Select(x => _courseMapperService.ConvertToCourseResponseModel(x)).ToList()
        };
        return coursesModel;
    }


    public async Task<BaseResponse> Delete(string id, string userId)
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
        course.DeletedBy = userId;
        await _courseRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "Course successfully deleted...",
            Status = true,
        };

    }

    public async Task<CoursesEnrolledRequestModel> UnpaidCourse(string studentId)
    {
        var courses = await _enrolmentRepository.GetStudentEnrolledCourses(y => !y.IsDeleted && !y.IsPaid && y.StudentId == studentId);
        if (courses == null)
        {
            return new CoursesEnrolledRequestModel
            {
                Message = "Course has not enrolled into any course yet...",
                Status = false,
            };
        }

        var coursesModel = new CoursesEnrolledRequestModel
        {
            Status = true,
            Message = "Course retrieved successfully ...",
            Data = courses.Select(x => new CourseDTO
            {
                Id = x.Course?.Id,
                Title = x.Course?.Title,
                Description = x.Course?.Description,
                CourseLanguage = x.Course.CourseLanguage,
                DifficultyLevel = x.Course.DifficultyLevel,
                Requirement = x.Course?.Requirement,
                CourseDuration = x.Course.CourseDuration,
                InstructorId = x.Course?.InstructorId,
                Price = x.Course.Price,
                CourseLogo = x.Course?.CourseLogo,
                ShortDescription = x.Course?.ShortDescription,
                IsPaid = x.IsPaid,
                CompletionStatus = x.CompletionStatus,
                NumbersOfEnrollment = x.Course.NumbersOfEnrollment,
            })
        };
        return coursesModel;

    }

    public async Task<CoursesEnrolledRequestModel> GetEnrolledCourses(string studentId)
    {
        var courses = await _enrolmentRepository.GetStudentEnrolledCourses(y => !y.IsDeleted && y.IsPaid && y.StudentId == studentId);
        if (courses == null)
        {
            return new CoursesEnrolledRequestModel
            {
                Message = "Course has not enrolled into any course yet...",
                Status = false,
            };
        }

        var coursesModel = new CoursesEnrolledRequestModel
        {
            Status = true,
            Message = "Course retrieved successfully ...",
            Data = courses.Select(x => new CourseDTO
            {
                Id = x.Course?.Id,
                Title = x.Course?.Title,
                Description = x.Course?.Description,
                CourseLanguage = x.Course.CourseLanguage,
                DifficultyLevel = x.Course.DifficultyLevel,
                Requirement = x.Course?.Requirement,
                CourseDuration = x.Course.CourseDuration,
                InstructorId = x.Course?.InstructorId,
                Price = x.Course.Price,
                CourseLogo = x.Course?.CourseLogo,
                ShortDescription = x.Course?.ShortDescription,
                IsPaid = x.IsPaid,
                CompletionStatus = x.CompletionStatus,
                NumbersOfEnrollment = x.Course.NumbersOfEnrollment,
            })
        };
        return coursesModel;

    }

    public async Task<CoursesEnrolledRequestModel> StudentActiveCourses(string studentId)
    {
        var courses = await _enrolmentRepository.GetStudentEnrolledCourses(y => !y.IsDeleted && y.IsPaid && y.StudentId == studentId && y.CompletionStatus == CompletionStatus.NotCompleted);
        if (courses == null)
        {
            return new CoursesEnrolledRequestModel
            {
                Message = "Course has not enrolled into any course yet...",
                Status = false,
            };
        }

        var coursesModel = new CoursesEnrolledRequestModel
        {
            Status = true,
            Message = "Course retrieved successfully ...",
            Data = courses.Select(x => new CourseDTO
            {
                Id = x.Course?.Id,
                Title = x.Course?.Title,
                Description = x.Course?.Description,
                CourseLanguage = x.Course.CourseLanguage,
                DifficultyLevel = x.Course.DifficultyLevel,
                Requirement = x.Course?.Requirement,
                CourseDuration = x.Course.CourseDuration,
                InstructorId = x.Course?.InstructorId,
                Price = x.Course.Price,
                CourseLogo = x.Course?.CourseLogo,
                ShortDescription = x.Course?.ShortDescription,
                IsPaid = x.IsPaid,
                CompletionStatus = x.CompletionStatus,
                NumbersOfEnrollment = x.Course.NumbersOfEnrollment
            })
        };
        return coursesModel;

    }

    public async Task<CoursesEnrolledRequestModel> GetCompletedCourses(string studentId)
    {
        var courses = await _enrolmentRepository.GetStudentEnrolledCourses(y => !y.IsDeleted && y.IsPaid && y.StudentId == studentId && y.CompletionStatus == CompletionStatus.Completed);
        if (courses == null)
        {
            return new CoursesEnrolledRequestModel
            {
                Message = "Course has not enrolled into any course yet...",
                Status = false,
            };
        }

        var coursesModel = new CoursesEnrolledRequestModel
        {
            Status = true,
            Message = "Course retrieved successfully ...",
            Data = courses.Select(x => new CourseDTO
            {
                Id = x.Course?.Id,
                Title = x.Course?.Title,
                Description = x.Course?.Description,
                CourseLanguage = x.Course.CourseLanguage,
                DifficultyLevel = x.Course.DifficultyLevel,
                Requirement = x.Course?.Requirement,
                CourseDuration = x.Course.CourseDuration,
                InstructorId = x.Course?.InstructorId,
                Price = x.Course.Price,
                CourseLogo = x.Course?.CourseLogo,
                ShortDescription = x.Course?.ShortDescription,
                IsPaid = x.IsPaid,
                CompletionStatus = x.CompletionStatus,
                NumbersOfEnrollment = x.Course.NumbersOfEnrollment,
            })
        };
        return coursesModel;

    }


    public async Task<CourseResponseModel> GetFullDetailOfCourseById(string id)
    {
        var course = await _courseRepository.GetCourseByIdWithInstructorDetail(x => x.Id == id && !x.IsDeleted);

        if (course == null)
        {
            return new CourseResponseModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        //var categoruesName = course.CourseCategories.Select(x => x.Category.Name).ToList();
        //var categoruesNamea = course.CourseCategories.Select(x => new { name = x.Category.Name , link = x.Category.Id}).ToList();
        var categoruesName = course.CourseCategories.Select(x => new CategoryNameResponseModel { Name = x.Category.Name, Id = x.Category.Id }).ToList();
        var courseModel = new CourseResponseModel
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
                InstructorName = $"{course.Instructor.User.FirstName} {course.Instructor.User.FirstName}",
                CategoriesName = categoruesName,
                CourseLogo = course.CourseLogo,
                NumbersOfEnrollment = course.NumbersOfEnrollment,
                ShortDescription = course.ShortDescription,
                CreatedOn = course.CreatedOn,

            },
        };
        return courseModel;
    }


    public async Task<BaseResponse> Update(UpdateCourseRequestModel model, string userId)
    {
        var course = await _courseRepository.GetAsync(x => x.Id == model.Id);

        if (course == null)
        {
            return new CourseResponseModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        course.Title = model.Title ?? course.Title;
        course.Description = model.Description ?? course.Description;
        course.CourseLanguage = model.CourseLanguage;
        course.DifficultyLevel = model.DifficultyLevel;
        course.Requirement = model.Requirement ?? course.Requirement;
        course.Price = model.Price;
        course.ModifiedBy = userId;
        course.ModifiedOn = DateTime.Now;
        course.IsActive = model.IsActive;
        await _courseRepository.SaveChangesAsync();

        return new CourseResponseModel
        {
            Message = "Course updated successfully...",
            Status = true,
        };

    }

    public async Task<BaseResponse> UpdateActiveStatus(UpdateCourseActiveStatusRequestModel model, string userId)
    {
        var course = await _courseRepository.GetAsync(x => x.Id == model.Id);

        if (course == null)
        {
            return new CourseResponseModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        course.IsActive = model.IsActive;
        course.ModifiedBy = userId;
        course.ModifiedOn = DateTime.Now;
        await _courseRepository.SaveChangesAsync();

        return new CourseResponseModel
        {
            Message = "Course updated successfully...",
            Status = true,
        };
    }

    public async Task<BaseResponse> RejectCourse(string courseId, string userId)
    {
        var course = await _courseRepository.GetAsync(x => x.Id == courseId);

        if (course == null)
        {
            return new CourseResponseModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        course.IsActive = true;
        course.ModifiedBy = userId;
        course.ModifiedOn = DateTime.Now;
        await _courseRepository.SaveChangesAsync();
        var senderDetail = new EmailSenderDetails
        {
            EmailToken = $"courselink",
            ReceiverEmail = "treehays90@gmail.com",
            ReceiverName = "Abdulsalam Ahmad",
        };
        var sendInstructorNotification = await _emailService.CourseVerificationTemplate(senderDetail, "courseurl");
        return new CourseResponseModel
        {
            Message = "Course is now verified successfully...",
            Status = true,
        };
    }
    public async Task<BaseResponse> ApproveCourse(string courseId, string userId)
    {
        var course = await _courseRepository.GetAsync(x => x.Id == courseId);

        if (course == null)
        {
            return new CourseResponseModel
            {
                Message = "Course not Found...",
                Status = false,
            };
        }

        course.IsActive = true;
        course.IsVerified = true;
        course.ModifiedBy = userId;
        course.ModifiedOn = DateTime.Now;
        await _courseRepository.SaveChangesAsync();
        var senderDetail = new EmailSenderDetails
        {
            EmailToken = $"courselink",
            ReceiverEmail = "treehays90@gmail.com",
            ReceiverName = "Abdulsalam Ahmad",
        };
        var sendInstructorNotification = await _emailService.CourseVerificationTemplate(senderDetail, "courseurl");
        return new CourseResponseModel
        {
            Message = "Course is now verified successfully...",
            Status = true,
        };
    }

    public async Task<GlobalSearchResultViewModel> GlobalSearch(string name)
    {
        name = name.ToLower();
        var coursesResult = await _courseRepository.GetListAsync(x => x.IsActive && !x.IsDeleted && (
        x.Title.ToLower().Contains(name)
        || x.Description.ToLower().Contains(name)
        /*|| x.CourseLanguage.ToString().Contains(name)*/));

        var courseResponse = new CoursesResponseModel();
        if (coursesResult.Count() > 0)
        {
            courseResponse = new CoursesResponseModel
            {
                Status = true,
                Message = "Course retrieved successfully ...",
                Data = coursesResult.Select(x => _courseMapperService.ConvertToCourseResponseModel(x)).ToList(),
            };
        }

        var instructorResult = await _userRepository.GetListAsync(x =>
         x.RoleId == "Instructor"
         && x.IsActive
         && !x.IsDeleted
         && x.FirstName.ToLower().Contains(name.ToLower())
         || x.LastName.ToLower().Contains(name.ToLower())
         || x.UserName.ToLower().Contains(name.ToLower())
         || x.Interest.ToLower().Contains(name.ToLower()));
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
            CoursesResponseModel = courseResponse,
        };

        return globalResult;
    }

}
