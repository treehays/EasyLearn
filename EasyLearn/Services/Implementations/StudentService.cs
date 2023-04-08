using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.StudentDTOs;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;
using System.Security.Claims;

namespace EasyLearn.Services.Implementations;

public class StudentService : IStudentService
{
    private readonly IUserRepository _userRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IPaymentDetailRepository _paymentDetailsRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IFileManagerService _fileManagerService;
    private readonly IAddressRepository _addressRepository;
    private readonly IUserService _userService;


    public StudentService(IStudentRepository studentRepository, IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor, IPaymentDetailRepository paymentDetailsRepository, IAddressRepository addressRepository, IWebHostEnvironment webHostEnvironment = null, IFileManagerService fileManagerService = null, IUserService userService = null)
    {
        _studentRepository = studentRepository;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _paymentDetailsRepository = paymentDetailsRepository;
        _addressRepository = addressRepository;
        _webHostEnvironment = webHostEnvironment;
        _fileManagerService = fileManagerService;
        _userService = userService;
    }

    public async Task<BaseResponse> StudentRegistration(CreateUserRequestModel model, string baseUrl)
    {
        var student = await _userService.UserRegistration(model,baseUrl);
        if (student == null)
        {
            return new BaseResponse
            {
                Status = false,
                Message = "Email already exist.",
            };
        }
        student.RoleId = "Instructor";
        await _userRepository.AddAsync(student);
        await _userRepository.SaveChangesAsync();




        return new BaseResponse
        {
            Status = true,
            Message = "Account successfully created.",
        };
    }

    public async Task<BaseResponse> Delete(string id)
    {
        var student = await _studentRepository.GetAsync(x => x.Id == id);
        if (student == null)
        {
            return new BaseResponse
            {
                Message = "User not Found...",
                Status = false,
            };
        }

        student.IsDeleted = true;
        student.DeletedOn = DateTime.Now;
        student.DeletedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "Student successfully deleted...",
            Status = true,
        };

    }

    public async Task<StudentsResponseModel> GetAllStudent()
    {
        var students = await _userRepository.GetListAsync(x => x.RoleId == "Student");
        var studentModel = new StudentsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = students.Select(x => new StudentDto
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
            }).AsEnumerable(),
        };
        return studentModel;

    }

    public async Task<StudentsResponseModel> GetAllActive()
    {
        var student = await _userRepository.GetListAsync(x => x.IsActive && !x.IsDeleted && x.RoleId == "Student");

        if (student == null)
        {
            return new StudentsResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var studentModel = new StudentsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = student.Select(x => new StudentDto
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
        return studentModel;
    }

    public async Task<StudentsResponseModel> GetAllInActive()
    {
        var student = await _userRepository.GetListAsync(x => !x.IsActive && !x.IsDeleted && x.RoleId == "Student");

        if (student == null)
        {
            return new StudentsResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var studentModel = new StudentsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = student.Select(x => new StudentDto
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
        return studentModel;
    }

    public async Task<StudentResponseModel> GetByEmail(string email)
    {
        var student = await _userRepository.GetAsync(x => x.Email == email && x.IsActive && !x.IsDeleted && x.RoleId == "Student");

        if (student == null)
        {
            return new StudentResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var studentModel = new StudentResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Password = student.Password,
                ProfilePicture = student.ProfilePicture,
                Biography = student.Biography,
                Skill = student.Skill,
                Interest = student.Interest,
                PhoneNumber = student.PhoneNumber,
                Gender = student.Gender,
                StudentshipStatus = student.StudentshipStatus,
                RoleId = student.RoleId,
            }
        };
        return studentModel;

    }

    public async Task<StudentResponseModel> GetById(string id)
    {
        var student = await _userRepository.GetAsync(x => x.Id == id && x.IsActive && !x.IsDeleted && x.RoleId == "Student");

        if (student == null)
        {
            return new StudentResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var studentModel = new StudentResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Password = student.Password,
                ProfilePicture = student.ProfilePicture,
                Biography = student.Biography,
                Skill = student.Skill,
                Interest = student.Interest,
                PhoneNumber = student.PhoneNumber,
                Gender = student.Gender,
                StudentshipStatus = student.StudentshipStatus,
                RoleId = student.RoleId,
            }
        };
        return studentModel;

    }

    public async Task<StudentsResponseModel> GetByName(string name)
    {
        var student = await _userRepository.GetListAsync(x => (x.FirstName == name || x.LastName == name || (x.FirstName + x.LastName) == name) && !x.IsActive && !x.IsDeleted && x.RoleId == "Student");

        if (student == null)
        {
            return new StudentsResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var studentModel = new StudentsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = student.Select(x => new StudentDto
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
        return studentModel;
    }

    public async Task<StudentResponseModel> GetFullDetailById(string id)
    {
        var student = await _studentRepository.GetFullDetailByIdAsync(x => x.Id == id && x.IsActive && !x.IsDeleted && x.RoleId == "Student");

        if (student == null)
        {
            return new StudentResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var studentModel = new StudentResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Password = student.Password,
                ProfilePicture = student.ProfilePicture,
                Biography = student.Biography,
                Skill = student.Skill,
                Interest = student.Interest,
                PhoneNumber = student.PhoneNumber,
                Gender = student.Gender,
                StudentshipStatus = student.StudentshipStatus,
                RoleId = student.RoleId,
            }
        };
        return studentModel;
    }

    public async Task<BaseResponse> UpdateActiveStatus(UpdateStudentActiveStatusRequestModel model)
    {
        var student = await _userRepository.GetAsync(x => x.Id == model.Id && x.IsActive && !x.IsDeleted && x.RoleId == "Student");
        if (student == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        student.IsActive = model.IsActive;
        student.ModifiedOn = DateTime.Now;
        student.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };
    }

    public async Task<BaseResponse> UpdateAddress(UpdateStudentAddressRequestModel model)
    {
        var student = await _addressRepository.GetAsync(x => x.UserId == model.Id && !x.IsDeleted);
        if (student == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        student.Country = model.Country ?? student.Country;
        student.State = model.State ?? student.State;
        student.City = model.City ?? student.City;
        student.Language = model.Language ?? student.Language;
        student.ModifiedOn = DateTime.Now;
        student.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };
    }

    public async Task<BaseResponse> UpdateBankDetail(UpdateStudentBankDetailRequestModel model)
    {
        var student = await _paymentDetailsRepository.GetAsync(x => x.UserId == model.Id);
        if (student == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        student.BankName = model.BankName;
        student.AccountNumber = model.AccountNumber;
        student.AccountName = model.AccountName;
        student.AccountType = model.AccountType;
        student.ModifiedOn = DateTime.Now;
        student.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };

    }

    public async Task<BaseResponse> UpdatePassword(UpdateStudentPasswordRequestModel model)
    {
        var student = await _userRepository.GetAsync(x => x.Id == model.Id && x.IsActive && !x.IsDeleted);
        if (student == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        student.Password = model.Password ?? student.Password;
        student.ModifiedOn = DateTime.Now;
        student.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };
    }

    public async Task<BaseResponse> UpdateProfile(UpdateStudentProfileRequestModel model)
    {
        var student = await _userRepository.GetAsync(x => x.Id == model.Id);
        if (student == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        student.FirstName = model.FirstName;
        student.LastName = model.LastName;
        student.ProfilePicture = model.ProfilePicture;
        student.Biography = model.Biography;
        student.Skill = model.Skill;
        student.Interest = model.Interest;
        student.PhoneNumber = model.PhoneNumber;
        student.StudentshipStatus = model.StudentshipStatus;
        student.ModifiedOn = DateTime.Now;
        student.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "Successfully Updated..",
            Status = true,
        };
    }
}