using EasyLearn.Models.Enums;

namespace EasyLearn.Models.DTOs.AdminDTOs;

public class AdminDtos
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ProfilePicture { get; set; }
    public string Biography { get; set; }
    public string Skill { get; set; }
    public string Interest { get; set; }
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }
    public  StudentshipStatus StudentshipStatus { get; set; }
    public string RoleId { get; set; }
    public bool IsActive { get; set; }

    //Bank Detail
    public string BankName { get; set; }
    public string AccountNumber { get; set; }
    public string AccountName { get; set; }
    public string AccountType { get; set; }
    //Address
    public string Country { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Language { get; set; }
}

public class CreateAdminRequestModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public Gender Gender { get; set; }
    public  StudentshipStatus StudentshipStatus { get; set; }
}


public class UpdateAdminProfileRequestModel
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePicture { get; set; }
    public string Biography { get; set; }
    public string Skill { get; set; }
    public string Interest { get; set; }
    public string PhoneNumber { get; set; }
    public  StudentshipStatus StudentshipStatus { get; set; }
    
    //Address
    public string Country { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Language { get; set; }
}

public class UpdateAdminBankDetailRequestModel
{
    public string BankName { get; set; }
    public string AccountNumber { get; set; }
    public string AccountName { get; set; }
    public string AccountType { get; set; }
    public string Id { get; set; }
  }
  
  
public class UpdateAdminAddressRequestModel
{
    public string Id { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Language { get; set; }
  }
  
  
public class UpdateAdminPasswordRequestModel
{
    public string Id { get; set; }
    public string Password { get; set; }
}

  
public class UpdateAdminActiveStatusRequestModel
{
    public string Id { get; set; }
    public int IsActive { get; set; }

}


public class AdminResponseModel : BaseResponse
{
    public AdminDtos Data { get; set; }
}



public class AdminsResponseModel : BaseResponse
{
    public IEnumerable<AdminDtos> Data { get; set; }
    //public AdminDtos Data { get; set; }
}

