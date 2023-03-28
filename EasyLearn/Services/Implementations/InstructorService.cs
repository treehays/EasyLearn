using BCrypt.Net;
using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.AdminDTOs;
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
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IAddressRepository _addressRepository;

    public InstructorService(IInstructorRepository instructorRepository, IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor, IPaymentDetailsRepository paymentDetailsRepository, IAddressRepository addressRepository, IWebHostEnvironment webHostEnvironment = null)
    {
        _instructorRepository = instructorRepository;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _paymentDetailsRepository = paymentDetailsRepository;
        _addressRepository = addressRepository;
        _webHostEnvironment = webHostEnvironment;
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
        string fileRelativePathx = null;

        if (model.formFile != null || model.formFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "abdullahpicture", "profilePictures"); //ppop
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
            RoleId = "Instructor",
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

        var instrDetail = new Instructor
        {
            Id = Guid.NewGuid().ToString(),
            UserId = user.Id,
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
        
        await _instructorRepository.AddAsync(instrDetail);
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
        var instructor = await _instructorRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
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

    public async Task<InstructorsResponseModel> GetAllActive()
    {
        var instructor = await _userRepository.GetListAsync(x => x.IsActive && !x.IsDeleted);

        if (instructor == null)
        {
            return new InstructorsResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var adminModel = new InstructorsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = instructor.Select(x => new InstructorDtos
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
        return adminModel;
    }

    public async Task<InstructorsResponseModel> GetAllInActive()
    {

        var instructor = await _userRepository.GetListAsync(x => !x.IsActive && !x.IsDeleted);

        if (instructor == null)
        {
            return new InstructorsResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var adminModel = new InstructorsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = instructor.Select(x => new InstructorDtos
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
        return adminModel;
    }

    public async Task<InstructorResponseModel> GetByEmail(string email)
    {
        var instructor = await _userRepository.GetAsync(x => x.Email == email && x.IsActive && !x.IsDeleted);
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

    public async Task<InstructorResponseModel> GetById(string id)
    {
        var instructor = await _userRepository.GetAsync(x => x.Id == id && x.IsActive && !x.IsDeleted);
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

    public async Task<InstructorsResponseModel> GetByName(string name)
    {
        var instructor = await _userRepository.GetListAsync(x =>
           (x.FirstName == name || x.LastName == name || (x.FirstName + x.LastName) == name) && !x.IsActive && !x.IsDeleted);

        if (instructor == null)
        {
            return new InstructorsResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var adminModel = new InstructorsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = instructor.Select(x => new InstructorDtos
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
        return adminModel;
    }

    public async Task<InstructorResponseModel> GetFullDetailById(string id)
    {
        var instructor = await _userRepository.GetAsync(x => x.Id == id && x.IsActive && !x.IsDeleted);
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

    public async Task<BaseResponse> UpdateActiveStatus(UpdateInstructorActiveStatusRequestModel model)
    {
        var instructor = await _userRepository.GetAsync(x => x.Id == model.Id && x.IsActive && !x.IsDeleted);
        if (instructor == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        instructor.IsActive = model.IsActive;
        instructor.ModifiedOn = DateTime.Now;
        instructor.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();

        return new BaseResponse
        {
            Message = "User status changed successfully...",
            Status = true,
        };

    }

    public async Task<BaseResponse> UpdateAddress(UpdateInstructorAddressRequestModel model)
    {
        var instructor = await _addressRepository.GetAsync(x => x.Id == model.Id && !x.IsDeleted);
        if (instructor == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        instructor.Country = model.Country;
        instructor.State = model.State;
        instructor.City = model.City;
        instructor.Language = model.Language;
        instructor.ModifiedOn = DateTime.Now;
        instructor.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();

        return new BaseResponse
        {
            Message = "User status changed successfully...",
            Status = true,
        };
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

    public async Task<BaseResponse> UpdatePassword(UpdateInstructorPasswordRequestModel model)
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

        instructor.Password = model.Password;
        instructor.ModifiedOn = DateTime.Now;
        instructor.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };

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