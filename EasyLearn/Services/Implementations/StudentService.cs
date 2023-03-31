using BCrypt.Net;
using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.StudentDTOs;
using EasyLearn.Models.Entities;
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
    private readonly IAddressRepository _addressRepository;

    public StudentService(IStudentRepository studentRepository, IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor, IPaymentDetailRepository paymentDetailsRepository, IAddressRepository addressRepository, IWebHostEnvironment webHostEnvironment = null)
    {
        _studentRepository = studentRepository;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _paymentDetailsRepository = paymentDetailsRepository;
        _addressRepository = addressRepository;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<BaseResponse> Create(CreateStudentRequestModel model)
    {
        var emailExist = await _userRepository.ExistByEmailAsync(model.Email);
        if (emailExist)
        {
            return new BaseResponse
            {
                Status = false,
                Message = "Email already exist.",
            };
        }

        string fileRelativePathx = null;

        if (model.formFile != null || model.formFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "profilePictures"); //ppop
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetFileName(model.formFile.FileName);
            fileRelativePathx = "/uploads/profilePictures/" + fileName;
            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.formFile.CopyToAsync(stream);
            }
        }


        var truncUserName = model.Email.IndexOf('@');
        var userName = model.Email.Remove(truncUserName);
        var password = BCrypt.Net.BCrypt.HashPassword(model.Password, SaltRevision.Revision2Y);
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Password = password,
            Gender = model.Gender,
            StudentshipStatus = model.StudentshipStatus,
            RoleId = "Student",
            UserName = userName,
            CreatedOn = DateTime.Now,
            IsActive = true,
        };

        var userAddress = new Address
        {
            Id = Guid.NewGuid().ToString(),
            UserId = user.Id,
        };

        var userPaymentDetail = new PaymentDetails
        {
            Id = Guid.NewGuid().ToString(),
            UserId = user.Id,
        };

        var studentDetail = new Student
        {
            Id = Guid.NewGuid().ToString(),
            UserId = user.Id,
        };
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        await _studentRepository.AddAsync(studentDetail);
        await _userRepository.SaveChangesAsync();


        await _paymentDetailsRepository.AddAsync(userPaymentDetail);
        await _userRepository.SaveChangesAsync();

        await _addressRepository.AddAsync(userAddress);
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

    public async Task<StudentsResponseModel> GetAll()
    {
        var students = await _userRepository.GetAllAsync();
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
        var student = await _userRepository.GetListAsync(x => x.IsActive && !x.IsDeleted);

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
        var student = await _userRepository.GetListAsync(x => !x.IsActive && !x.IsDeleted);

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
        var student = await _userRepository.GetAsync(x => x.Email == email && x.IsActive && !x.IsDeleted);

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
        var student = await _userRepository.GetAsync(x => x.Id == id && x.IsActive && !x.IsDeleted);

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
        var student = await _userRepository.GetListAsync(x => (x.FirstName == name || x.LastName == name || (x.FirstName + x.LastName) == name) && !x.IsActive && !x.IsDeleted);

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
        var student = await _studentRepository.GetFullDetailByIdAsync(x => x.Id == id && x.IsActive && !x.IsDeleted);

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
        var student = await _userRepository.GetAsync(x => x.Id == model.Id && x.IsActive && !x.IsDeleted);
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