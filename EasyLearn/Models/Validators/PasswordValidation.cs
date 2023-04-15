using Org.BouncyCastle.Bcpg;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EasyLearn.Models.Validators;

public class PasswordValidation : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        var password = value as string;
        if (string.IsNullOrWhiteSpace(password)) return false;
        if (password.Length < 8) return false;
        if (!Regex.IsMatch(password,@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")) return false;
            return true;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"The {name} field must be at least 8 characters long and contains uppercase letter, lowercase letter, number and symbol.";
    }
}
