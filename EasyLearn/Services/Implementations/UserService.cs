using BCrypt.Net;
using EasyLearn.GateWays.Email;
using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.EmailSenderDTOs;
using EasyLearn.Models.DTOs.InstructorDTOs;
using EasyLearn.Models.DTOs.PaymentDetailDTOs;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Implementations;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;
using System.Security.Claims;

namespace EasyLearn.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IFileManagerService _fileManagerService;
    private readonly ISendInBlueEmailService _emailService;
    private readonly IPaymentDetailRepository _paymentDetailsRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IAddressRepository _addressRepository;

    public UserService(IUserRepository userRepository, IFileManagerService fileManagerService, ISendInBlueEmailService emailService, IHttpContextAccessor contextAccessor, IPaymentDetailRepository paymentDetailsRepository, IAddressRepository addressRepository)
    {
        _userRepository = userRepository;
        _fileManagerService = fileManagerService;
        _emailService = emailService;
        _contextAccessor = contextAccessor;
        _paymentDetailsRepository = paymentDetailsRepository;
        _addressRepository = addressRepository;
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
            CreatedBy = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
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
            CreatedBy = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
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
            Message = "You’ve successfully logged in to EasyLearn. Wellcome back!..",
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
    public async Task<BaseResponse> ResetPassword(string email, string baseUrl)
    {
        var user = await _userRepository.GetAsync(x => x.Email.ToLower() == email.ToLower());
        if (user == null)
        {
            return new BaseResponse
            {
                Status = false,
                Message = "Link has been sent to the registerd email..",
            };
        }

        user.EmailToken = Guid.NewGuid().ToString().Replace('-', 's');
        user.EmailConfirmed = false;
        user.ModifiedOn = DateTime.Now;

        var senderDetail = new EmailSenderDetails
        {
            EmailToken = $"{baseUrl}/User/ConfirmPasswordReset?emailToken={user.EmailToken}",
            ReceiverEmail = user.Email,
            ReceiverName = user.FirstName,
        };
        var emailSender = _emailService.EmailVerificationTemplate(senderDetail, baseUrl);
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Status = true,
            Message = "Link has been sent to the registerd email..",
        };
    }

    public async Task<BaseResponse> PasswordRessetConfirmation(string emailToken, string userId)
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
        var resetDate = Convert.ToDateTime(user.ModifiedOn);
        var expiryDate = resetDate.AddDays(1);

        if (expiryDate < DateTime.Now)
        {
            return new BaseResponse
            {
                Message = "Link has expired kindly request for new reset link...",
                Status = false,
            };
        }

        //if (user.EmailConfirmed)
        //{
        //    return new BaseResponse
        //    {
        //        Message = "Account already verified, proceed to login...",
        //        Status = false,
        //    };
        //}

        //var date = DateTime.Now;
        //var modifiedBy = userId;
        //user.EmailConfirmed = true;
        //user.ModifiedOn = date;
        //user.ModifiedBy = modifiedBy;
        //await _userRepository.UpdateAsync(user);
        //await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "Token confirmed activated...",
            Status = true,
        };
    }

    public async Task<BaseResponse> EmailVerification(string emailToken, string userId)
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
        var modifiedBy = userId;
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
        var user = await _userRepository.GetAsync(x => x.Id == userId && !x.IsDeleted && x.IsActive);
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
        var user = await _userRepository.GetFullDetails(x => x.Id == model.UserId);
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
            //var student = await _studentRepository.GetAsync(x => x.UserId == model.UserId);
            if (model.RoleId.ToLower() == "moderator")
            {
                var moderatorUser = new Moderator
                {
                    Id = user.Student.Id,
                    UserId = user.Id,
                    CreatedBy = userId,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedBy = userId,
                };
                user.RoleId = "Moderator";
                user.Moderator = moderatorUser;
                //await _userRepository.AddAsync(moderatorUser);
                await _userRepository.SaveChangesAsync();
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
                    Id = user.Student.Id,
                    UserId = user.Id,
                    CreatedBy = userId,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedBy = userId,
                };
                user.RoleId = "Moderator";
                user.Instructor = instructorUser;
                //await _instructorRepository.AddAsync(instructorUser);
                await _userRepository.SaveChangesAsync();
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
            //var instructor = await _instructorRepository.GetAsync(x => x.UserId == model.UserId);
            var moderatorUser = new Moderator
            {
                Id = user.Instructor.Id,
                UserId = user.Id,
                CreatedBy = userId,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedBy = userId,
            };
            user.RoleId = "Moderator";
            user.Moderator = moderatorUser;
            await _userRepository.SaveChangesAsync();
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

    public async Task<BaseResponse> UpdateProfile(UpdateUserProfileRequestModel model, string userId)
    {
        var user = await _userRepository.GetAsync(x => x.Id == model.Id);
        if (user == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }
        user.FirstName = model.FirstName ?? user.FirstName;
        user.LastName = model.LastName ?? user.LastName;
        user.ProfilePicture = model.ProfilePicture ?? user.ProfilePicture;
        user.Biography = model.Biography ?? user.Biography;
        user.Skill = model.Skill ?? user.Skill;
        user.Interest = model.Interest ?? user.Interest;
        user.PhoneNumber = model.PhoneNumber ?? user.PhoneNumber;
        user.StudentshipStatus = model.StudentshipStatus;
        user.ModifiedOn = DateTime.Now;
        user.ModifiedBy = userId;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };
    }

    public async Task<PaymentsDetailRequestModel> ListOfUserBankDetails(string userId)
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
                BankName = x.BankCode,
                AccountName = x.AccountName,
                AccountNumber = x.AccountNumber,
                AccountType = x.AccountType,
            }),
        };
        return adminModel;
    }

    public async Task<PaymentDetailRequestModel> GetPaymentDetailPaymentId(string paymentId, string userId)
    {

        var user = await _paymentDetailsRepository.GetAsync(x => x.Id == paymentId && !x.IsDeleted);

        if (user == null)
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
                Id = user.Id,
                AccountName = user.AccountName,
                AccountNumber = user.AccountNumber,
                AccountType = user.AccountType,
                BankName = user.BankCode,
                UserId = user.UserId,

            }
        };
        return instructorModel;
    }

    public async Task<BaseResponse> UpdateBankDetail(UpdateUserBankDetailRequestModel model, string userId)
    {
        var user = await _paymentDetailsRepository.GetAsync(x => x.UserId == model.Id);
        if (user == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        user.BankCode = model.BankName;
        user.AccountNumber = model.AccountNumber;
        user.AccountName = model.AccountName;
        user.AccountType = model.AccountType;
        user.ModifiedOn = DateTime.Now;
        user.ModifiedBy = userId;
        await _userRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Message = "User updated successfully..",
            Status = true,
        };
    }

    public async Task<UserResponseModel> GetFullDetailById(string id)
    {
        var user = await _userRepository.GetAsync(x => x.RoleId == "Instructor" && x.Id == id && x.IsActive && !x.IsDeleted);
        if (user == null)
        {
            return new UserResponseModel
            {
                Message = "User not found..",
                Status = false,
            };
        }
        var instructorModel = new UserResponseModel
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
                Biography = user.Biography,
                Skill = user.Skill,
                Interest = user.Interest,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                StudentshipStatus = user.StudentshipStatus,
                RoleId = user.RoleId,

            }
        };
        return instructorModel;

    }

    public async Task<BaseResponse> UpdateAddress(UpdateUserAddressRequestModel model, string userId)
    {
        var user = await _addressRepository.GetAsync(x => x.Id == model.Id && !x.IsDeleted);
        if (user == null)
        {
            return new BaseResponse
            {
                Message = "User not found.",
                Status = false,
            };
        }

        user.Country = model.Country;
        user.State = model.State;
        user.City = model.City;
        user.Language = model.Language;
        user.ModifiedOn = DateTime.Now;
        user.ModifiedBy = userId;
        await _userRepository.SaveChangesAsync();

        return new BaseResponse
        {
            Message = "User status changed successfully...",
            Status = true,
        };
    }
}
