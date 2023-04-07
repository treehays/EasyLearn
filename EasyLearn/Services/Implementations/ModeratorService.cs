﻿using BCrypt.Net;
using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.ModeratorDTOs;
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
    private readonly IWebHostEnvironment _webHostEnvironment;


    public ModeratorService(IModeratorRepository moderatorRepository, IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor, IPaymentDetailRepository paymentDetailsRepository, IAddressRepository addressRepository, IFileManagerService fileManagerService, IWebHostEnvironment webHostEnvironment)
    {
        _moderatorRepository = moderatorRepository;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _paymentDetailsRepository = paymentDetailsRepository;
        _addressRepository = addressRepository;
        _fileManagerService = fileManagerService;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<BaseResponse> Create(CreateUserRequestModel model)
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
        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "profilePictures"); //ppop

        var filePath = await _fileManagerService.GetFileName(model.FormFile, uploadsFolder);

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
            RoleId = "Moderator",
            UserName = userName,
            CreatedOn = DateTime.Now,
            IsActive = true,
            ProfilePicture = filePath,
            CreatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
        };


        var userAddress = new Address
        {
            Id = Guid.NewGuid().ToString(),
            CreatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            UserId = user.Id,
        };


        var userPayment = new List<PaymentDetails>
        {
            new PaymentDetails
            {
                Id = Guid.NewGuid().ToString(),
                UserId = user.Id,
                CreatedBy = user.CreatedBy,
                CreatedOn = user.CreatedOn,
            }
        };

        var userModerator = new Moderator
        {
            Id = Guid.NewGuid().ToString(),
            UserId = user.Id,
            CreatedBy = user.CreatedBy,
            CreatedOn = user.CreatedOn,

        };
        user.Address = userAddress;
        user.Moderator = userModerator;
        user.PaymentDetails = userPayment;

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
        var moderator = await _moderatorRepository.GetAsync(x => x.Id == id);
        if (moderator == null)
        {
            return new BaseResponse
            {
                Message = "User not Found...",
                Status = false,
            };
        }

        moderator.IsDeleted = true;
        moderator.DeletedOn = DateTime.Now;
        moderator.DeletedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "Moderator successfully deleted...",
            Status = true,
        };

    }

    public async Task<ModeratorsResponseModel> GetAll()
    {
        var moderators = await _userRepository.GetListAsync(x => !x.IsDeleted && x.RoleId == "Moderator");
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
            }).AsEnumerable(),
        };
        return moderatorModel;

    }

    public Task<ModeratorsResponseModel> GetAllActive()
    {
        throw new NotImplementedException();
    }

    public Task<ModeratorsResponseModel> GetAllInActive()
    {
        throw new NotImplementedException();
    }

    public Task<ModeratorResponseModel> GetByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<ModeratorResponseModel> GetById(string id)
    {
        var moderator = await _userRepository.GetAsync(x => x.Id == id);
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

    public Task<ModeratorsResponseModel> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<ModeratorResponseModel> GetFullDetailById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse> UpdateActiveStatus(UpdateModeratorActiveStatusRequestModel model)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse> UpdateAddress(UpdateModeratorAddressRequestModel model)
    {
        throw new NotImplementedException();
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

        moderator.BankName = model.BankName;
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

    public Task<BaseResponse> UpdatePassword(UpdateModeratorPasswordRequestModel model)
    {
        throw new NotImplementedException();
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