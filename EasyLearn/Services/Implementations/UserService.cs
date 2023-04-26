using BCrypt.Net;
using EasyLearn.GateWays.Email;
using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.EmailSenderDTOs;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;
using System.Security.Claims;

namespace EasyLearn.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IFileManagerService _fileManagerService;
    private readonly ISendInBlueEmailService _emailService;
    private readonly IModeratorRepository _moderatorRepository;
    private readonly IInstructorRepository _instructorRepository;
    private readonly IStudentRepository _studentRepository;


    public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IFileManagerService fileManagerService, ISendInBlueEmailService emailService, IModeratorRepository moderatorRepository, IInstructorRepository instructorRepository, IStudentRepository studentRepository)
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _fileManagerService = fileManagerService;
        _emailService = emailService;
        _moderatorRepository = moderatorRepository;
        _instructorRepository = instructorRepository;
        _studentRepository = studentRepository;
    }


    public async Task<User> UserRegistration(CreateUserRequestModel model, string baseUrl)
    {
        var emailExist = await _userRepository.ExistByEmailAsync(model.Email);
        if (emailExist)
        {
            return null;
        }

        var filePath = await _fileManagerService.GetFileName(model.FormFile, "uploads", "images", "profilePictures");
        var userName = model.Email.Remove(model.Email.IndexOf('@'));
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

            UserName = $"{userName}{new Random().Next(100, 999)}",
            CreatedOn = DateTime.Now,
            IsActive = true,
            EmailToken = Guid.NewGuid().ToString().Replace('-', '0'),
            ProfilePicture = filePath,
            CreatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
        };

        var senderDetail = new EmailSenderDetails
        {
            EmailToken = $"{baseUrl}/Home/ConfirmEmail?emailToken={user.EmailToken}",
            ReceiverEmail = user.Email,
            ReceiverName = model.FirstName,
        };
        var emailSender = _emailService.EmailVerificationTemplate(senderDetail, baseUrl);

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


        user.Address = userAddress;
        user.PaymentDetails = userPayment;
        return user;

    }


    public async Task<LoginRequestModel> Login(LoginRequestModel model)
    {
        var user = await _userRepository.GetFullDetails(x => x.UserName.ToUpper() == model.Email.ToUpper() || x.Email.ToUpper() == model.Email.ToUpper());


        if (user == null)
        {
            return new LoginRequestModel
            {
                Message = "invalid login details",
                Status = false,
            };
        }

        if (!user.EmailConfirmed)
        {
            return new LoginRequestModel
            {
                Message = "kindly Confirm your email...",
                Status = false,
            };
        }

        var verifyPassword = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

        if (!verifyPassword)
        {
            return new LoginRequestModel
            {
                Message = "incorrect login detail...",
                Status = false,
            };
        }

        var instructorId = user.Instructor != null ? user.Instructor.Id : null;
        var moderatorId = user.Moderator != null ? user.Moderator.Id : null;
        var adminId = user.Admin != null ? user.Admin.Id : null;
        var studentId = user.Student != null ? user.Student.Id : null;
        var loginModel = new LoginRequestModel
        {
            Message = "You’ve successfully logged in to EasyLearn. Wellcome back soon!..",
            Status = true,
            Email = user.Email,
            Password = user.Password,
            RoleId = user.RoleId,
            LastName = user.LastName,
            FirstName = user.FirstName,
            ProfilePicture = user.ProfilePicture ?? "Default.jpg",
            Id = instructorId ?? moderatorId ?? adminId ?? studentId,
            UserId = user.Id,
        };
        return loginModel;
    }

    //public async Task<BaseResponse> EmailReVerification(string emailToken)
    //{
    //    var user = await _userRepository.GetUserByTokenAsync(emailToken);
    //    if (user == null)
    //    {
    //        return new BaseResponse
    //        {
    //            Message = "Wrong verification code...",
    //            Status = false,
    //        };
    //    }
    //}


    public async Task<BaseResponse> EmailVerification(string emailToken)
    {
        var user = await _userRepository.GetUserByTokenAsync(emailToken);
        if (user == null)
        {
            return new BaseResponse
            {
                Message = "Wrong verification code...",
                Status = false,
            };
        }

        if (user.EmailConfirmed)
        {
            return new BaseResponse
            {
                Message = "Account already verified, proceed to login...",
                Status = false,
            };
        }

        var date = DateTime.Now;
        var modifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        user.EmailConfirmed = true;
        user.ModifiedOn = date;
        user.ModifiedBy = modifiedBy;
        await _userRepository.UpdateAsync(user);
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "Account activated...",
            Status = true,
        };
    }


    public async Task<UserResponseModel> GetByIdAsync(string userId)
    {
        var user = await _userRepository.GetAsync(x => x.Id == userId && !x.IsDeleted);
        if (user == null)
        {
            return new UserResponseModel
            {
                Status = false,
                Message = "User not found..."
            };
        }
        var userResponseModel = new UserResponseModel
        {
            Status = true,
            Message = "Details successfully retrieved...",
            Data = new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                ProfilePicture = user.ProfilePicture,
                RoleId = user.RoleId,
                UserName = user.UserName,
            }
        };
        return userResponseModel;
    }



    public async Task<BaseResponse> UpgradeUser(UserUpgradeRequestModel model, string userId)
    {
        var user = await _userRepository.GetAsync(x => x.Id == model.UserId);
        if (user == null)
        {
            return new BaseResponse
            {
                Message = "User not found...",
                Status = false,
            };
        }

        if (user.RoleId.ToLower() == "student")
        {
            var student = await _studentRepository.GetAsync(x => x.UserId == model.UserId);
            if (model.RoleId.ToLower() == "moderator")
            {
                var moderatorUser = new Moderator
                {
                    Id = student.Id,
                    UserId = student.UserId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.Now,
                };
                user.RoleId = "Moderator";
                await _moderatorRepository.AddAsync(moderatorUser);
                await _moderatorRepository.SaveChangesAsync();
                return new BaseResponse
                {
                    Message = "User successfully upgraded to a moderator...",
                    Status = true,
                };
            }

            if (model.RoleId.ToLower() == "instructor")
            {
                var instructorUser = new Instructor
                {
                    Id = student.Id,
                    UserId = student.UserId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.Now,
                };
                user.RoleId = "Moderator";
                await _instructorRepository.AddAsync(instructorUser);
                await _instructorRepository.SaveChangesAsync();
                return new BaseResponse
                {
                    Message = "User successfully upgraded to an instructor...",
                    Status = true,
                };
            }
            return new BaseResponse
            {
                Message = "User can not be downgraded...",
                Status = false,
            };
        }
        if (user.RoleId.ToLower() == "instructor" && model.RoleId.ToLower() == "moderator")
        {
            var instructor = await _instructorRepository.GetAsync(x => x.UserId == model.UserId);

            var moderatorUser = new Moderator
            {
                Id = instructor.Id,
                UserId = instructor.UserId,
                CreatedBy = userId,
                CreatedOn = DateTime.Now,
            };
            user.RoleId = "Moderator";
            await _moderatorRepository.AddAsync(moderatorUser);
            await _moderatorRepository.SaveChangesAsync();
            return new BaseResponse
            {
                Message = "User successfully upgraded to a moderator...",
                Status = true,
            };
        }


        return new BaseResponse
        {
            Message = "User can not be downgraded...",
            Status = false,
        };

    }
}
