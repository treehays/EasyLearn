using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.InstructorDTOs;
using EasyLearn.Models.DTOs.PaymentDetailDTOs;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;
using System.Security.Claims;
using X.PagedList;

namespace EasyLearn.Services.Implementations;


public class InstructorService : IInstructorService
{
    private readonly IUserRepository _userRepository;
    private readonly IInstructorRepository _instructorRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPaymentDetailRepository _paymentDetailsRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IFileManagerService _fileManagerService;
    private readonly IAddressRepository _addressRepository;
    private readonly IEmailService _emailService;
    private readonly IUserService _userService;



    public InstructorService(IInstructorRepository instructorRepository, IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor, IPaymentDetailRepository paymentDetailsRepository, IAddressRepository addressRepository, IWebHostEnvironment webHostEnvironment, IFileManagerService fileManagerService, IEmailService emailService, IUserService userService)
    {
        _instructorRepository = instructorRepository;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _paymentDetailsRepository = paymentDetailsRepository;
        _addressRepository = addressRepository;
        _webHostEnvironment = webHostEnvironment;
        _fileManagerService = fileManagerService;
        _emailService = emailService;
        _userService = userService;
    }
    public async Task<BaseResponse> InstructorRegistration(CreateUserRequestModel model, string baseUrl)
    {

        var instructor = await _userService.UserRegistration(model, baseUrl);
        if (instructor == null)
        {
            return new BaseResponse
            {
                Status = false,
                Message = "Email already exist.",
            };
        }
        var userInstructor = new Instructor
        {
            Id = Guid.NewGuid().ToString(),
            UserId = instructor.Id,
            CreatedBy = instructor.CreatedBy,
            CreatedOn = instructor.CreatedOn,

        };
        instructor.RoleId = "Instructor";
        instructor.Instructor = userInstructor;
        await _userRepository.AddAsync(instructor);
        await _userRepository.SaveChangesAsync();

        return new BaseResponse
        {
            Status = true,
            Message = "Account successfully created.",
        };
    }

    public async Task<BaseResponse> Delete(string id)
    {
        var instructor = await _instructorRepository.GetFullDetailByIdAsync(x => x.Id == id && !x.IsDeleted);
        if (instructor == null)
        {
            return new BaseResponse
            {
                Message = "User not Found...",
                Status = false,
            };
        }
        var date = DateTime.Now;
        var deletedby = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        instructor.IsDeleted = true;
        instructor.DeletedOn = date;
        instructor.DeletedBy = deletedby;
        instructor.Address.IsDeleted = true;
        instructor.Address.DeletedOn = date;
        instructor.Address.DeletedBy = deletedby;
        instructor.Instructor.IsDeleted = true;
        instructor.Instructor.DeletedOn = date;
        instructor.Instructor.DeletedBy = deletedby;

        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "Instructor successfully deleted...",
            Status = true,
        };
    }


    public async Task<PaymentsDetailRequestModel> GetListOfInstructorBankDetails(string userId)
    {
        var admins = await _paymentDetailsRepository.GetListAsync(x => x.UserId == userId && !x.IsDeleted);

        if (admins == null)
        {
            return new PaymentsDetailRequestModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var adminModel = new PaymentsDetailRequestModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = admins.Select(x => new PaymentDetailDTO
            {
                Id = x.Id,
                BankName = x.BankName,
                AccountName = x.AccountName,
                AccountNumber = x.AccountNumber,
                AccountType = x.AccountType,
            }),
        };
        return adminModel;
    }


    public async Task<IList<User>> PaginatedSample()
    {
        var instructor = await _userRepository.GetListAsync(x => x.RoleId == "Instructor");
        var instructors = instructor.ToList();
        /*
                var pagedList = instructors.ToPagedList(instructors.Count(), 5);
                var instructorModel = new InstructorsResponseModel
                {
                    Status = true,
                    Message = "Details successfully retrieved...",
                    Data = instructors.Select(x => new InstructorDto
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
                };*/
        return instructors;

    }


    public async Task<InstructorsResponseModel> GetAll()
    {
        var instructors = await _userRepository.GetListAsync(x => x.RoleId == "Instructor");
        var pagedList = instructors.ToPagedList(instructors.Count(), 5);
        var instructorModel = new InstructorsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = instructors.Select(x => new InstructorDto
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
        var instructor = await _userRepository.GetListAsync(x => x.RoleId == "Instructor" && x.IsActive && !x.IsDeleted);

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
            Data = instructor.Select(x => new InstructorDto
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

        var instructor = await _userRepository.GetListAsync(x => x.RoleId == "Instructor" && !x.IsActive && !x.IsDeleted);

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
            Data = instructor.Select(x => new InstructorDto
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
        var instructor = await _userRepository.GetAsync(x => x.RoleId == "Instructor" && x.Email == email && x.IsActive && !x.IsDeleted);
        var instructorModel = new InstructorResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = new InstructorDto
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
        var instructor = await _userRepository.GetAsync(x => x.RoleId == "Instructor" && x.Id == id && x.IsActive && !x.IsDeleted);
        var instructorModel = new InstructorResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = new InstructorDto
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



    public async Task<PaymentDetailRequestModel> GetByPaymentDetail(string id)
    {
        var instructor = await _paymentDetailsRepository.GetAsync(x => x.Id == id && !x.IsDeleted);

        if (instructor == null)
        {
            return new PaymentDetailRequestModel
            {
                Message = "User not found..",
                Status = false,
            };
        }
        var instructorModel = new PaymentDetailRequestModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = new PaymentDetailDTO
            {
                Id = instructor.Id,
                AccountName = instructor.AccountName,
                AccountNumber = instructor.AccountNumber,
                AccountType = instructor.AccountType,
                BankName = instructor.BankName,
                UserId = instructor.UserId,
            }
        };
        return instructorModel;

    }

    public async Task<InstructorsResponseModel> GetByName(string name)
    {
        var instructor = await _userRepository.GetListAsync(x =>
          x.RoleId == "Instructor" 
          && x.IsActive 
          && !x.IsDeleted
          && x.FirstName.ToUpper().Contains(name.ToUpper())
          && x.LastName.ToUpper().Contains(name.ToUpper())
          && x.UserName.ToUpper().Contains(name.ToUpper())
          && x.Interest.ToUpper().Contains(name.ToUpper()));

        if (instructor.Count() == 0)
        {
            return new InstructorsResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var instructorModel = new InstructorsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = instructor.Select(x => new InstructorDto
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
        return instructorModel;
    }

    public async Task<InstructorResponseModel> GetFullDetailById(string id)
    {
        var instructor = await _userRepository.GetAsync(x => x.RoleId == "Instructor" && x.Id == id && x.IsActive && !x.IsDeleted);
        var instructorModel = new InstructorResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = new InstructorDto
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
        var instructor = await _userRepository.GetAsync(x => x.RoleId == "Instructor" && x.Id == model.Id && x.IsActive && !x.IsDeleted);
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
        var instructor = await _userRepository.GetAsync(x => x.RoleId == "Instructor" && x.Id == model.Id);
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
        var instructor = await _userRepository.GetAsync(x => x.RoleId == "Instructor" && x.Id == model.Id);
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

    //public async Task<BaseResponse> EmailVerification(string emailToken)
    //{

    //    var user = await _userRepository.GetAsync(x => x.EmailToken == emailToken);

    //    if (user == null)
    //    {
    //        return new BaseResponse
    //        {
    //            Message = "Wrong verification code...",
    //            Status = false,
    //        };
    //    }

    //    if (user.EmailConfirmed)
    //    {
    //        return new BaseResponse
    //        {
    //            Message = "Account already verified, proceed to login...",
    //            Status = false,
    //        };
    //    }

    //    var date = DateTime.Now;
    //    var modifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    //    user.EmailConfirmed = true;
    //    user.ModifiedOn = date;
    //    user.ModifiedBy = modifiedBy;

    //    await _userRepository.SaveChangesAsync();
    //    return new BaseResponse
    //    {
    //        Message = "Account activated...",
    //        Status = true,
    //    };
    //}
}