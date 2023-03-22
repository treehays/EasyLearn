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
