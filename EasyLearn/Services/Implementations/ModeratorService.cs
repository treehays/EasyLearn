using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.ModeratorDTOs;
using EasyLearn.Models.DTOs.PaymentDetailDTOs;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;
using System.Security.Claims;

namespace EasyLearn.Services.Implementations;


public class ModeratorService : IModeratorService
{
    private readonly IUserRepository _userRepository;
    private readonly IModeratorRepository _moderatorRepository;
    private readonly IPaymentDetailRepository _paymentDetailsRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAddressRepository _addressRepository;
    private readonly IFileManagerService _fileManagerService;
    private readonly IUserService _userService;


    public ModeratorService(IModeratorRepository moderatorRepository, IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor, IPaymentDetailRepository paymentDetailsRepository, IAddressRepository addressRepository, IFileManagerService fileManagerService, IUserService userService)
    {
        _moderatorRepository = moderatorRepository;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _paymentDetailsRepository = paymentDetailsRepository;
        _addressRepository = addressRepository;
        _fileManagerService = fileManagerService;
        _userService = userService;
    }

    public async Task<BaseResponse> ModeratorRegistration(CreateUserRequestModel model, string baseUrl)
    {

        var moderator = await _userService.UserRegistration(model, baseUrl);
        if (moderator == null)
        {
            return new BaseResponse
            {
                Status = false,
                Message = "Email already exist.",
            };
        }
        var userModerator = new Moderator
        {
            Id = Guid.NewGuid().ToString(),
            UserId = moderator.Id,
            CreatedBy = moderator.CreatedBy,
            CreatedOn = moderator.CreatedOn,
        };
        moderator.Moderator = userModerator;
        moderator.RoleId = "Moderator";
        await _userRepository.AddAsync(moderator);
        await _userRepository.SaveChangesAsync();


        return new BaseResponse
        {
            Status = true,
            Message = "Account successfully created.",
        };
    }

    public async Task<BaseResponse> Delete(string id)
    {
        var moderator = await _moderatorRepository.GetFullDetailByIdAsync(x => x.Id == id && !x.IsDeleted && x.IsActive);
        if (moderator == null)
        {
            return new BaseResponse
            {
                Message = "User not Found...",
                Status = false,
            };
        }

        var date = DateTime.Now;
        var deletedby = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        moderator.IsDeleted = true;
        moderator.DeletedOn = date;
        moderator.DeletedBy = deletedby;
        moderator.Address.IsDeleted = true;
        moderator.Address.DeletedOn = date;
        moderator.Address.DeletedBy = deletedby;
        moderator.Admin.IsDeleted = true;
        moderator.Admin.DeletedOn = date;
        moderator.Admin.DeletedBy = deletedby;
        await _userRepository.SaveChangesAsync();

        return new BaseResponse
        {
            Message = "Moderator successfully deleted...",
            Status = true,
        };

    }




    public async Task<PaymentsDetailRequestModel> GetListOfModeratorBankDetails(string userId)
    {
        var moderator = await _paymentDetailsRepository.GetListAsync(x => x.UserId == userId && !x.IsDeleted);

        if (moderator == null)
        {
            return new PaymentsDetailRequestModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var moderatorModel = new PaymentsDetailRequestModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = moderator.Select(x => new PaymentDetailDTO
            {
                Id = x.Id,
                BankName = x.BankCode,
                AccountName = x.AccountName,
                AccountNumber = x.AccountNumber,
                AccountType = x.AccountType,
            }),
        };
        return moderatorModel;
    }




    public async Task<ModeratorsResponseModel> GetAll()
    {
        var moderators = await _userRepository.GetListAsync(x => !x.IsDeleted && x.RoleId == "Moderator");

        if (moderators.Count() == 0)
        {
            return new ModeratorsResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var moderatorModel = new ModeratorsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = moderators.Select(x => new ModeratorDto
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
        return moderatorModel;

    }

    public async Task<ModeratorsResponseModel> GetAllActive()
    {
        var moderators = await _userRepository.GetListAsync(x => x.RoleId == "Moderator" && x.IsActive && !x.IsDeleted);

        if (moderators.Count() == 0)
        {
            return new ModeratorsResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var moderatorModel = new ModeratorsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = moderators.Select(x => new ModeratorDto
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
        return moderatorModel;

    }

    public async Task<ModeratorsResponseModel> GetAllInActive()
    {
        var moderators = await _userRepository.GetListAsync(x => x.RoleId == "Admin" && !x.IsActive && !x.IsDeleted);

        if (moderators.Count() == 0)
        {
            return new ModeratorsResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var moderatorModel = new ModeratorsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = moderators.Select(x => new ModeratorDto
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
        return moderatorModel;
    }

    public async Task<ModeratorResponseModel> GetByEmail(string email)
    {
        var moderator = await _userRepository.GetAsync(x => x.Email == email && x.RoleId == "Moderator" && x.IsActive && !x.IsDeleted);

        if (moderator == null)
        {
            return new ModeratorResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var moderatorModel = new ModeratorResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = new ModeratorDto
            {
                Id = moderator.Id,
                FirstName = moderator.FirstName,
                LastName = moderator.LastName,
                Email = moderator.Email,
                Password = moderator.Password,
                ProfilePicture = moderator.ProfilePicture,
                Biography = moderator.Biography,
                Skill = moderator.Skill,
                Interest = moderator.Interest,
                PhoneNumber = moderator.PhoneNumber,
                Gender = moderator.Gender,
                StudentshipStatus = moderator.StudentshipStatus,
                RoleId = moderator.RoleId,
            }
        };
        return moderatorModel;
    }

    public async Task<ModeratorResponseModel> GetById(string id)
    {
        var moderator = await _userRepository.GetAsync(x => x.Id == id && x.IsActive && !x.IsDeleted);

        if (moderator == null)
        {
            return new ModeratorResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var moderatorModel = new ModeratorResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = new ModeratorDto
            {
                Id = moderator.Id,
                FirstName = moderator.FirstName,
                LastName = moderator.LastName,
                Email = moderator.Email,
                Password = moderator.Password,
                ProfilePicture = moderator.ProfilePicture,
                Biography = moderator.Biography,
                Skill = moderator.Skill,
                Interest = moderator.Interest,
                PhoneNumber = moderator.PhoneNumber,
                Gender = moderator.Gender,
                StudentshipStatus = moderator.StudentshipStatus,
                RoleId = moderator.RoleId,
            }
        };
        return moderatorModel;

    }

    public async Task<ModeratorsResponseModel> GetByName(string name)
    {
        var moderators = await _userRepository.GetListAsync(x =>
         (x.FirstName == name || x.LastName == name || (x.FirstName + x.LastName) == name) && !x.IsActive && x.RoleId == "Moderator" && !x.IsDeleted);

        if (moderators.Count() == 0)
        {
            return new ModeratorsResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var moderatorsModel = new ModeratorsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = moderators.Select(x => new ModeratorDto
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
        return moderatorsModel;

    }

    public async Task<ModeratorResponseModel> GetFullDetailById(string userId)
    {
        var moderator = await _userRepository.GetFullDetails(x => x.Id == userId && x.IsActive && !x.IsDeleted && x.RoleId == "Moderator");
        if (moderator == null)
        {
            return new ModeratorResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var moderatorModel = new ModeratorResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = new ModeratorDto
            {
                Id = moderator.Id,
                FirstName = moderator.FirstName,
                LastName = moderator.LastName,
                Email = moderator.Email,
                Password = moderator.Password,
                ProfilePicture = moderator.ProfilePicture,
                Biography = moderator.Biography,
                Skill = moderator.Skill,
                Interest = moderator.Interest,
                PhoneNumber = moderator.PhoneNumber,
                Gender = moderator.Gender,
                StudentshipStatus = moderator.StudentshipStatus,
                RoleId = moderator.RoleId,
            }
        };
        return moderatorModel;

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
                BankName = instructor.BankCode,
                UserId = instructor.UserId,
            }
        };
        return instructorModel;

    }



    public async Task<BaseResponse> UpdateActiveStatus(UpdateModeratorActiveStatusRequestModel model)
    {
        var moderator = await _userRepository.GetAsync(x => x.Id == model.Id && x.IsActive && !x.IsDeleted);
        if (moderator == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        moderator.IsActive = model.IsActive;
        moderator.ModifiedOn = DateTime.Now;
        moderator.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };

    }

    public async Task<BaseResponse> UpdateAddress(UpdateModeratorAddressRequestModel model)
    {
        var moderator = await _addressRepository.GetAsync(x => x.UserId == model.Id && !x.IsDeleted);
        if (moderator == null)
        {

            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        moderator.Country = model.Country ?? moderator.Country;
        moderator.State = model.State ?? moderator.State;
        moderator.City = model.City ?? moderator.City;
        moderator.Language = model.Language ?? moderator.Language;
        moderator.ModifiedOn = DateTime.Now;
        moderator.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };
    }

    public async Task<BaseResponse> UpdateBankDetail(UpdateModeratorBankDetailRequestModel model)
    {
        var moderator = await _paymentDetailsRepository.GetAsync(x => x.UserId == model.Id);
        if (moderator == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        moderator.BankCode = model.BankName;
        moderator.AccountNumber = model.AccountNumber;
        moderator.AccountName = model.AccountName;
        moderator.AccountType = model.AccountType;
        moderator.ModifiedOn = DateTime.Now;
        moderator.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };

    }

    public async Task<BaseResponse> UpdatePassword(UpdateModeratorPasswordRequestModel model)
    {
        var moderator = await _userRepository.GetAsync(x => x.Id == model.Id && x.IsActive && !x.IsDeleted);
        if (moderator == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        moderator.Password = model.Password ?? moderator.Password;
        moderator.ModifiedOn = DateTime.Now;
        moderator.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };
    }

    public async Task<BaseResponse> UpdateProfile(UpdateModeratorProfileRequestModel model)
    {
        var moderator = await _userRepository.GetAsync(x => x.Id == model.Id);
        if (moderator == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        moderator.FirstName = model.FirstName;
        moderator.LastName = model.LastName;
        moderator.ProfilePicture = model.ProfilePicture;
        moderator.Biography = model.Biography;
        moderator.Skill = model.Skill;
        moderator.Interest = model.Interest;
        moderator.PhoneNumber = model.PhoneNumber;
        moderator.StudentshipStatus = model.StudentshipStatus;
        moderator.ModifiedOn = DateTime.Now;
        moderator.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "Successfully Updated..",
            Status = true,
        };
    }
}