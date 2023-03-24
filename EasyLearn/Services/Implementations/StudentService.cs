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
    private readonly IPaymentDetailsRepository _paymentDetailsRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAddressRepository _addressRepository;

    public StudentService(IStudentRepository studentRepository, IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor, IPaymentDetailsRepository paymentDetailsRepository, IAddressRepository addressRepository)
    {
        _studentRepository = studentRepository;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _paymentDetailsRepository = paymentDetailsRepository;
        _addressRepository = addressRepository;
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
        await _userRepository.AddAsync(user);
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
            Data = students.Select(x => new StudentDtos
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

    public Task<StudentsResponseModel> GetAllActive()
    {
        throw new NotImplementedException();
    }

    public Task<StudentsResponseModel> GetAllInActive()
    {
        throw new NotImplementedException();
    }

    public Task<StudentResponseModel> GetByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<StudentResponseModel> GetById(string id)
    {
        var student = await _userRepository.GetAsync(x => x.Id == id);
        var studentModel = new StudentResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = new StudentDtos
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

    public Task<StudentsResponseModel> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<StudentResponseModel> GetFullDetailById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse> UpdateActiveStatus(UpdateStudentActiveStatusRequestModel model)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse> UpdateAddress(UpdateStudentAddressRequestModel model)
    {
        throw new NotImplementedException();
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

    public Task<BaseResponse> UpdatePassword(UpdateStudentPasswordRequestModel model)
    {
        throw new NotImplementedException();
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