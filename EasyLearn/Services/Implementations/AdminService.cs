using System.Security.Claims;
using BCrypt.Net;
using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.AdminDTOs;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Models.Entities;
using EasyLearn.Models.Enums;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;
using Microsoft.Extensions.Hosting;

namespace EasyLearn.Services.Implementations;

public class AdminService : IAdminService
{
    private readonly IUserRepository _userRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly IPaymentDetailsRepository _paymentDetailsRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAddressRepository _addressRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;


    public AdminService(IAdminRepository adminRepository, IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor, IPaymentDetailsRepository paymentDetailsRepository, IAddressRepository addressRepository, IWebHostEnvironment webHostEnvironment)
    {
        _adminRepository = adminRepository;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _paymentDetailsRepository = paymentDetailsRepository;
        _addressRepository = addressRepository;
        _webHostEnvironment = webHostEnvironment;
    }


    public async Task<BaseResponse> Create(CreateUserRequestModel model)
    {


        //var basePath = Path.Combine(_webHostEnvironment.WebRootPath, "Files", "Images", "ProfilPictures");
        //Directory.CreateDirectory(basePath);
        //var contentType = model.formFile.ContentType.Split('/')[1];
        //var fileName = $"{Guid.NewGuid()}-{contentType}";
        //var fullPath = Path.Combine(basePath, fileName);
        //using (var fileStream = new FileStream(fullPath, FileMode.Create))
        //{
        //    model.formFile.CopyTo(fileStream);

        //}
        //model.formFile;
        if (model.formFile == null || model.formFile.Length == 0)
        {
            return new BaseResponse
            {
                Status = false,
                Message = "No image uploaded.",
            };
        }

        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "profilePictures");//ppop
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }



        //var fileName1 = Path.GetFileName(model.formFile.FileName);
        //var fileExtension = Path.GetExtension(model.formFile.FileName);




        var fileName = Guid.NewGuid().ToString() + Path.GetFileName(model.formFile.FileName);

        var fileRelativePathx = "/uploads/profilePictures/" + fileName;




        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await model.formFile.CopyToAsync(stream);
        }



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
            ProfilePicture = fileRelativePathx,
            RoleId = "Admin",
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
        var admin = await _userRepository.GetAsync(x => x.Id == id);
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


        admin.FirstName = model.FirstName;
        admin.LastName = model.LastName;
        admin.ProfilePicture = model.ProfilePicture;
        admin.Biography = model.Biography;
        admin.Skill = model.Skill;
        admin.Interest = model.Interest;
        admin.PhoneNumber = model.PhoneNumber;
        admin.StudentshipStatus = model.StudentshipStatus == 0 ? StudentshipStatus.Graduate : model.StudentshipStatus;
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
        var admin = await _paymentDetailsRepository.GetAsync(x => x.UserId == model.Id && !x.IsDeleted);
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

        admin.Country = model.Country;
        admin.State = model.State;
        admin.City = model.City;
        admin.Language = model.Language;
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

        admin.Password = model.Password;
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

        admin.IsActive = model.IsActive == (int)boolOption.IsTrue;
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
        var admin = await _userRepository.GetAsync(x => x.Id == id && x.IsActive && !x.IsDeleted);
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
        var admin = await _userRepository.GetAsync(x => x.Email == email && x.IsActive && !x.IsDeleted);
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
        var admins = await _userRepository.GetListAsync(x => x.FirstName == name || x.LastName == name && x.IsActive && !x.IsDeleted);
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
        var admins = await _userRepository.GetListAsync(x => x.IsActive && !x.IsDeleted);
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
        var admins = await _userRepository.GetListAsync(x => !x.IsActive && !x.IsDeleted);
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