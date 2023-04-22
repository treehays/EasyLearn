using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.AdminDTOs;
using EasyLearn.Models.DTOs.PaymentDetailDTOs;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Models.Entities;
using EasyLearn.Models.Enums;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;
using System.Security.Claims;

namespace EasyLearn.Services.Implementations;

public class AdminService : IAdminService
{
    private readonly IUserRepository _userRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly IPaymentDetailRepository _paymentDetailsRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;


    public AdminService(IAdminRepository adminRepository, IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor, IPaymentDetailRepository paymentDetailsRepository,
        IAddressRepository addressRepository, IUserService userService)
    {
        _adminRepository = adminRepository;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _paymentDetailsRepository = paymentDetailsRepository;
        _addressRepository = addressRepository;
        _userService = userService;
    }


    public async Task<BaseResponse> AdminRegistration(CreateUserRequestModel model, string baseUrl)
    {

        var admin = await _userService.UserRegistration(model, baseUrl);
        if (admin == null)
        {
            return new BaseResponse
            {
                Status = false,
                Message = "Email already exist.",
            };
        }

        var userAdmin = new Admin
        {
            Id = Guid.NewGuid().ToString(),
            UserId = admin.Id,
            CreatedBy = admin.CreatedBy,
            CreatedOn = admin.CreatedOn,
        };
        admin.Admin = userAdmin;
        admin.RoleId = "Admin";
        await _userRepository.AddAsync(admin);
        await _userRepository.SaveChangesAsync();


        return new BaseResponse
        {
            Status = true,
            Message = "Account successfully created.",
        };
    }

    public async Task<BaseResponse> Delete(string id)
    {
        var admin = await _adminRepository.GetFullDetailByIdAsync(x => x.Id == id && !x.IsDeleted && x.IsActive);
        if (admin == null)
        {
            return new BaseResponse
            {
                Message = "User not Found...",
                Status = false,
            };
        }

        var date = DateTime.Now;
        var deletedby = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        admin.IsDeleted = true;
        admin.DeletedOn = date;
        admin.DeletedBy = deletedby;
        admin.Address.IsDeleted = true;
        admin.Address.DeletedOn = date;
        admin.Address.DeletedBy = deletedby;
        admin.Admin.IsDeleted = true;
        admin.Admin.DeletedOn = date;
        admin.Admin.DeletedBy = deletedby;

        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "Admin successfully deleted...",
            Status = true,
        };
    }

    public async Task<BaseResponse> UpdateProfile(UpdateUserProfileRequestModel model)
    {
        var admin = await _userRepository.GetAsync(x => x.Id == model.Id && x.IsActive && !x.IsDeleted);
        if (admin == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }


        admin.FirstName = model.FirstName ?? admin.FirstName;
        admin.LastName = model.LastName ?? admin.LastName;
        admin.ProfilePicture = model.ProfilePicture ?? admin.ProfilePicture;
        admin.Biography = model.Biography ?? admin.Biography;
        admin.Skill = model.Skill ?? admin.Skill;
        admin.Interest = model.Interest ?? admin.Interest;
        admin.PhoneNumber = model.PhoneNumber ?? admin.PhoneNumber;
        admin.StudentshipStatus = model.StudentshipStatus != 0 ? StudentshipStatus.Student : StudentshipStatus.Graduate;
        admin.ModifiedOn = DateTime.Now;
        admin.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };
    }

    public async Task<BaseResponse> UpdateBankDetail(UpdateUserBankDetailRequestModel model)
    {
        var admin = await _paymentDetailsRepository.GetAsync(x => x.Id == model.Id && !x.IsDeleted);
        if (admin == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        admin.BankName = model.BankName ?? admin.BankName;
        admin.AccountNumber = model.AccountNumber ?? admin.AccountNumber;
        admin.AccountName = model.AccountName ?? admin.AccountName;
        admin.AccountType = model.AccountType ?? admin.AccountType;
        admin.ModifiedOn = DateTime.Now;
        admin.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };
    }

    public async Task<BaseResponse> UpdateAddress(UpdateUserAddressRequestModel model)
    {
        var admin = await _addressRepository.GetAsync(x => x.UserId == model.Id && !x.IsDeleted);
        if (admin == null)
        {

            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        admin.Country = model.Country ?? admin.Country;
        admin.State = model.State ?? admin.State;
        admin.City = model.City ?? admin.City;
        admin.Language = model.Language ?? admin.Language;
        admin.ModifiedOn = DateTime.Now;
        admin.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };
    }

    public async Task<BaseResponse> UpdatePassword(UpdateUserPasswordRequestModel model)
    {
        var admin = await _userRepository.GetAsync(x => x.Id == model.Id && x.IsActive && !x.IsDeleted);
        if (admin == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        admin.Password = model.Password ?? admin.Password;
        admin.ModifiedOn = DateTime.Now;
        admin.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };
    }

    public async Task<BaseResponse> UpdateActiveStatus(UpdateUserActiveStatusRequestModel model)
    {
        var admin = await _userRepository.GetAsync(x => x.Id == model.Id && x.IsActive && !x.IsDeleted);
        if (admin == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        admin.IsActive = model.IsActive;
        admin.ModifiedOn = DateTime.Now;
        admin.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };
    }

    public async Task<AdminResponseModel> GetById(string id)
    {
        var admin = await _userRepository.GetAsync(x => x.Id == id && x.IsActive && !x.IsDeleted && x.RoleId == "Admin");

        if (admin == null)
        {
            return new AdminResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var adminModel = new AdminResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = ConvertToAdminResponseModel(admin),
        };
        return adminModel;
    }


    public async Task<AdminResponseModel> GetFullDetailById(string id)
    {
        var admin = await _adminRepository.GetFullDetailByIdAsync(x => x.Id == id && x.IsActive && !x.IsDeleted && x.RoleId == "Admin");
        if (admin == null)
        {
            return new AdminResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var adminModel = new AdminResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = new AdminDto
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                Password = admin.Password,
                ProfilePicture = admin.ProfilePicture,
                Biography = admin.Biography,
                Skill = admin.Skill,
                Interest = admin.Interest,
                PhoneNumber = admin.PhoneNumber,
                Gender = admin.Gender,
                StudentshipStatus = admin.StudentshipStatus,
                RoleId = admin.RoleId,

                Language = admin.Address?.Language,
                City = admin.Address?.City,
                State = admin.Address?.State,
                Country = admin.Address?.Country,
                PaymentDetailData = admin.PaymentDetails?.Select(x => new PaymentDetailDTO
                {
                    AccountName = x.AccountName,
                    BankName = x.BankName,
                    AccountNumber = x.AccountNumber,
                    AccountType = x.AccountType,
                }),
            }
        };
        return adminModel;
    }

    public async Task<AdminResponseModel> GetByEmail(string email)
    {
        // throw new NotImplementedException();
        var admin = await _userRepository.GetAsync(x => x.Email == email && x.RoleId == "Admin" && x.IsActive && !x.IsDeleted);

        if (admin == null)
        {
            return new AdminResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var adminModel = new AdminResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = ConvertToAdminResponseModel(admin),
        };
        return adminModel;
    }




    public async Task<PaymentDetailRequestModel> GetByPaymentDetail(string id)
    {
        var instructor = await _paymentDetailsRepository.GetAsync(x => x.Id == id && !x.IsDeleted);

        if (instructor == null)
        {
            return new PaymentDetailRequestModel
            {
                Status = false,
                Message = "No bank account found",
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



    public async Task<AdminsResponseModel> GetByName(string name)
    {
        var admins = await _userRepository.GetListAsync(x =>
           (x.FirstName == name || x.LastName == name || (x.FirstName + x.LastName) == name) && !x.IsActive && x.RoleId == "Admin" && !x.IsDeleted);

        if (admins.Count() == 0)
        {
            return new AdminsResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var adminModel = new AdminsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = admins.Select(x => ConvertToAdminResponseModel(x)),
        };
        return adminModel;
    }

    public async Task<PaymentsDetailRequestModel> GetListOfAdminBankDetails(string userId)
    {
        var admins = await _paymentDetailsRepository.GetListAsync(x => x.UserId == userId && !x.IsDeleted);

        if (admins.Count() == 0)
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

    public async Task<AdminsResponseModel> GetAll()
    {
        var admins = await _userRepository.GetListAsync(x => x.RoleId == "Admin");

        if (admins.Count() == 0)
        {
            return new AdminsResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var adminModel = new AdminsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = admins.Select(x => ConvertToAdminResponseModel(x)),
        };
        return adminModel;
    }

    public async Task<AdminsResponseModel> GetAllActive()
    {
        var admins = await _userRepository.GetListAsync(x => x.RoleId == "Admin" && x.IsActive && !x.IsDeleted);

        if (admins.Count() == 0)
        {
            return new AdminsResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var adminModel = new AdminsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = admins.Select(x => ConvertToAdminResponseModel(x)),
        };
        return adminModel;
    }

    public async Task<AdminsResponseModel> GetAllInActive()
    {
        var admins = await _userRepository.GetListAsync(x => x.RoleId == "Admin" && !x.IsActive && !x.IsDeleted);

        if (admins.Count() == 0)
        {
            return new AdminsResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }

        var adminModel = new AdminsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = admins.Select(x => ConvertToAdminResponseModel(x)),
        };
        return adminModel;
    }




    public AdminDto ConvertToAdminResponseModel(User user)
    {
        var adminModel = new AdminDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Password = user.Password,
            ProfilePicture = user.ProfilePicture,
            Biography = user.Biography,
            Skill = user.Skill,
            Interest = user.Interest,
            PhoneNumber = user.PhoneNumber,
            Gender = user.Gender,
            StudentshipStatus = user.StudentshipStatus,
            RoleId = user.RoleId,
        };
        return adminModel;
    }
}

