using System.Security.Claims;
using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.AdminDTOs;
using EasyLearn.Models.Entities;
using EasyLearn.Models.Enums;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;

namespace EasyLearn.Services.Implementations;

public class AdminService : IAdminService
{
    private readonly IUserRepository _userRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly IPaymentDetailsRepository _paymentDetailsRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAddressRepository _addressRepository;

    public AdminService(IAdminRepository adminRepository, IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor, IPaymentDetailsRepository paymentDetailsRepository, IAddressRepository addressRepository)
    {
        _adminRepository = adminRepository;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _paymentDetailsRepository = paymentDetailsRepository;
        _addressRepository = addressRepository;
    }


    public async Task<BaseResponse> Create(CreateAdminRequestModel model)
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

        var user = new User
        {
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Password = model.Password,
            Gender = model.Gender,
            StudentshipStatus = model.StudentshipStatus,
            RoleId = "Admin",
            UserName = userName,
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
        var admin = await _adminRepository.GetAsync(x => x.Id == id);
        if (admin == null)
        {
            return new BaseResponse
            {
                Message = "User not Found...",
                Status = false,
            };
        }

        admin.IsDeleted = true;
        admin.DeletedOn = DateTime.Now;
        admin.DeletedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "Admin successfully deleted...",
            Status = true,
        };
    }

    public async Task<BaseResponse> UpdateProfile(UpdateAdminProfileRequestModel model)
    {
        var admin = await _userRepository.GetAsync(x => x.Id == model.Id);
        if (admin == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        admin.FirstName = model.FirstName;
        admin.LastName = model.LastName;
        admin.ProfilePicture = model.ProfilePicture;
        admin.Biography = model.Biography;
        admin.Skill = model.Skill;
        admin.Interest = model.Interest;
        admin.PhoneNumber = model.PhoneNumber;
        admin.StudentshipStatus = model.StudentshipStatus;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };
    }

    public async Task<BaseResponse> UpdateBankDetail(UpdateAdminBankDetailRequestModel model)
    {
        var admin = await _paymentDetailsRepository.GetAsync(x => x.UserId == model.Id);
        if (admin == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        admin.BankName = model.BankName;
        admin.AccountNumber = model.AccountNumber;
        admin.AccountName = model.AccountName;
        admin.AccountType = model.AccountType;

        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };
    }

    public async Task<BaseResponse> UpdateAddress(UpdateAdminAddressRequestModel model)
    {
        var admin = await _addressRepository.GetAsync(x => x.UserId == model.Id);
        if (admin == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        admin.Country = model.Country;
        admin.State = model.State;
        admin.City = model.City;
        admin.Language = model.Language;

        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };
    }

    public async Task<BaseResponse> UpdatePassword(UpdateAdminPasswordRequestModel model)
    {
        var admin = await _userRepository.GetAsync(x => x.Id == model.Id);
        if (admin == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        admin.Password = model.Password;

        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };
    }

    public async Task<BaseResponse> UpdateActiveStatus(UpdateAdminActiveStatusRequestModel model)
    {
        var admin = await _userRepository.GetAsync(x => x.Id == model.Id);
        if (admin == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        admin.IsActive = model.IsActive == (int)boolOption.IsTrue;
     
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };
    }

    public async Task<AdminResponseModel> GetById(string id)
    {
        var admin = await _userRepository.GetAsync(x => x.Id == id);
        var adminModel = new AdminResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = new AdminDtos
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
            }
        };
        return adminModel;
    }

    public async Task<AdminResponseModel> GetFullDetailById(string id)
    {
        var admin = await _adminRepository.GetFullDetailByIdAsync(id);
        var adminModel = new AdminResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = new AdminDtos
            {
                Id = admin.Id,
                FirstName = admin.User.FirstName,
                LastName = admin.User.LastName,
                Email = admin.User.Email,
                Password = admin.User.Password,
                ProfilePicture = admin.User.ProfilePicture,
                Biography = admin.User.Biography,
                Skill = admin.User.Skill,
                Interest = admin.User.Interest,
                PhoneNumber = admin.User.PhoneNumber,
                Gender = admin.User.Gender,
                StudentshipStatus = admin.User.StudentshipStatus,
                RoleId = admin.User.RoleId,
                Language = admin.User.Address.Language,
                City = admin.User.Address.City,
                State = admin.User.Address.State,
                Country = admin.User.Address.Country,
            }
        };
        return adminModel;
    }

    public async Task<AdminResponseModel> GetByEmail(string email)
    {
        // throw new NotImplementedException();
        var admin = await _userRepository.GetAsync(x => x.Email == email);
        var adminModel = new AdminResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = new AdminDtos
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
            }
        };
        return adminModel;
    }


    public async Task<AdminsResponseModel> GetByName(string name)
    {
        //throw new NotImplementedException();
        // throw new NotImplementedException();
        var admins = await _userRepository.GetListAsync(x => x.FirstName == name || x.LastName == name);
        var adminModel = new AdminsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = admins.Select(x => new AdminDtos
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
        return adminModel;
    }

    public async Task<AdminsResponseModel> GetAll()
    {
        //throw new NotImplementedException();
        var admins = await _userRepository.GetAllAsync();
        var adminModel = new AdminsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = admins.Select(x => new AdminDtos
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
        return adminModel;
    }

    public async Task<AdminsResponseModel> GetAllActive()
    {
        var admins = await _userRepository.GetListAsync(x => x.IsActive);
        var adminModel = new AdminsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = admins.Select(x => new AdminDtos
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
        return adminModel;
    }

    public async Task<AdminsResponseModel> GetAllInActive()
    {
        var admins = await _userRepository.GetListAsync(x => !x.IsActive);
        var adminModel = new AdminsResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = admins.Select(x => new AdminDtos
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
        return adminModel;
    }
}