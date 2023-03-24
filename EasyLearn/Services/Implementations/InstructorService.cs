using BCrypt.Net;
using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.InstructorDTOs;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Implementations;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;
using System.Security.Claims;

namespace EasyLearn.Services.Implementations;


public class InstructorService : IInstructorService
{
    private readonly IUserRepository _userRepository;
    private readonly IInstructorRepository _instructorRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPaymentDetailsRepository _paymentDetailsRepository;
    private readonly IAddressRepository _addressRepository;

    public InstructorService(IInstructorRepository instructorRepository, IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor, IPaymentDetailsRepository paymentDetailsRepository, IAddressRepository addressRepository)
    {
        _instructorRepository = instructorRepository;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _paymentDetailsRepository = paymentDetailsRepository;
        _addressRepository = addressRepository;
    }
    public async Task<BaseResponse> Create(CreateInstructorRequestModel model)
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
            RoleId = "Instructor",
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
        var instructor = await _instructorRepository.GetAsync(x => x.Id == id);
        if (instructor == null)
        {
            return new BaseResponse
            {
                Message = "User not Found...",
                Status = false,
            };
        }

        instructor.IsDeleted = true;
        instructor.DeletedOn = DateTime.Now;
        instructor.DeletedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "Instructor successfully deleted...",
            Status = true,
        };

    }

    public async Task<InstructorsResponseModel> GetAll()
    {
        var instructors = await _userRepository.GetAllAsync();
        var instructorModel = new InstructorsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = instructors.Select(x => new InstructorDtos
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
        return instructorModel;

    }

    public Task<InstructorsResponseModel> GetAllActive()
    {
        throw new NotImplementedException();
    }

    public Task<InstructorsResponseModel> GetAllInActive()
    {
        throw new NotImplementedException();
    }

    public Task<InstructorResponseModel> GetByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<InstructorResponseModel> GetById(string id)
    {
        var instructor = await _userRepository.GetAsync(x => x.Id == id);
        var instructorModel = new InstructorResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = new InstructorDtos
            {
                Id = instructor.Id,
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Email = instructor.Email,
                Password = instructor.Password,
                ProfilePicture = instructor.ProfilePicture,
                Biography = instructor.Biography,
                Skill = instructor.Skill,
                Interest = instructor.Interest,
                PhoneNumber = instructor.PhoneNumber,
                Gender = instructor.Gender,
                StudentshipStatus = instructor.StudentshipStatus,
                RoleId = instructor.RoleId,
            }
        };
        return instructorModel;

    }

    public Task<InstructorsResponseModel> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<InstructorResponseModel> GetFullDetailById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse> UpdateActiveStatus(UpdateInstructorActiveStatusRequestModel model)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse> UpdateAddress(UpdateInstructorAddressRequestModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse> UpdateBankDetail(UpdateInstructorBankDetailRequestModel model)
    {
        var instructor = await _paymentDetailsRepository.GetAsync(x => x.UserId == model.Id);
        if (instructor == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        instructor.BankName = model.BankName;
        instructor.AccountNumber = model.AccountNumber;
        instructor.AccountName = model.AccountName;
        instructor.AccountType = model.AccountType;
        instructor.ModifiedOn = DateTime.Now;
        instructor.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };

    }

    public Task<BaseResponse> UpdatePassword(UpdateInstructorPasswordRequestModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse> UpdateProfile(UpdateInstructorProfileRequestModel model)
    {
        var instructor = await _userRepository.GetAsync(x => x.Id == model.Id);
        if (instructor == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        instructor.FirstName = model.FirstName;
        instructor.LastName = model.LastName;
        instructor.ProfilePicture = model.ProfilePicture;
        instructor.Biography = model.Biography;
        instructor.Skill = model.Skill;
        instructor.Interest = model.Interest;
        instructor.PhoneNumber = model.PhoneNumber;
        instructor.StudentshipStatus = model.StudentshipStatus;
        instructor.ModifiedOn = DateTime.Now;
        instructor.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };
    }
}