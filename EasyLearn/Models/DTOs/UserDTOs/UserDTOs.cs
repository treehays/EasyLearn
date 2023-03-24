using EasyLearn.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyLearn.Models.DTOs.UserDTOs;

public class UserDTOs
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ProfilePicture { get; set; }
    public string RoleId { get; set; }
}


public class CreateUserRequestModel
{
    [DisplayName("First Name")] public string FirstName { get; set; }
    [DisplayName("Last Name")] public string LastName { get; set; }

    [EmailAddress(ErrorMessage = "Enter a valid email..")]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [DisplayName("Re-enter Password")]
    [Compare(nameof(Password), ErrorMessage = "Password not match")]
    public string ConfirmPassword { get; set; }
    public Gender Gender { get; set; }
    public StudentshipStatus StudentshipStatus { get; set; }
    public IFormFile formFile { get; set; }
}


public class UpdateUserProfileRequestModel
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePicture { get; set; }
    public string Biography { get; set; }
    public string Skill { get; set; }
    public string Interest { get; set; }
    public string PhoneNumber { get; set; }
    public StudentshipStatus StudentshipStatus { get; set; }

    //Address
    public string Country { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Language { get; set; }
}




public class UpdateUserBankDetailRequestModel
{
    public string BankName { get; set; }
    public string AccountNumber { get; set; }
    public string AccountName { get; set; }
    public string AccountType { get; set; }
    public string Id { get; set; }
}


public class UpdateUserAddressRequestModel
{
    public string Id { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Language { get; set; }
}




public class UpdateUserPasswordRequestModel
{
    public string Id { get; set; }
    public string Password { get; set; }
}


public class UpdateUserActiveStatusRequestModel
{
    public string Id { get; set; }
    public int IsActive { get; set; }
}


public class UserRequestModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ProfilePicture { get; set; }
    // public string RoleId { get; set; }   
}

public class UserResponseModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ProfilePicture { get; set; }
    public string RoleId { get; set; }
}

public class UserLoginResponseModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string RoleId { get; set; }
}
