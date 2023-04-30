using BCrypt.Net;
using EasyLearn.Models.Entities;
using EasyLearn.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EasyLearn.Data;

public class EasyLearnDbSeedingData
{
    private readonly CompanyInfoOption _companyInfoOption;

    public EasyLearnDbSeedingData(IOptions<CompanyInfoOption> companyInfoOption)
    {
        _companyInfoOption = companyInfoOption.Value;
    }

    public static async void InitializeDb(IServiceProvider serviceProvider)
    {
        var userId = "USERID53D4EB9E-1C51-44A4-A012-25C98042E32A";
        var AdminId = "ADMINID8726B43F-83F4-4586-AF5D-DF9DFF91ADBD";
        var admin = new Admin()
        {
            Id = AdminId,
            IsDeleted = false,
            CreatedBy = "Auto Create",
            CreatedOn = DateTime.Now,
            UserId = userId,
        };

        var user = new List<User>
        {
          new User ()
          {
            Id = userId,
            FirstName = "Abdulsalam",
            LastName = "Ahmad",
            Password = BCrypt.Net.BCrypt.HashPassword("Admin", SaltRevision.Revision2B),
            RoleId = "Admin",
            Email = "aymoneyay@gmail.com",
            PhoneNumber = "08066117783",
            IsActive = true,
            IsDeleted = false,
            UserName = "Admin",
            CreatedBy = "Auto Create",
            CreatedOn = DateTime.Now,
            Gender = Gender.Male,
            Admin = admin,
          }
        };

        using (var context = new EasyLearnDbContext(serviceProvider.GetRequiredService<DbContextOptions<EasyLearnDbContext>>()))
        {
            await context.Database.MigrateAsync();

            if (!context.Roles.Any())
            {
                var listOfRoles = new List<Role>
                {
                      new Role()
                    {
                        RoleName = "Admin",
                        Description= "Admin",
                        CreatedOn= DateTime.Now,
                        Id= "Admin",
                        CreatedBy= "Auto Create",
                        User = user,
                    },

                    new Role()
                    {
                        RoleName = "Instructor",
                        Description= "Instructor",
                        CreatedOn= DateTime.Now,
                        CreatedBy= "Auto Create",
                        Id= "Instructor",
                    },

                    new Role()
                    {
                        RoleName = "Moderator",
                        Description= "Moderator",
                        CreatedOn= DateTime.Now,
                        CreatedBy= "Auto Create",
                        Id= "Moderator",
                    },

                    new Role()
                    {
                        RoleName = "Student",
                        Description= "Student",
                        CreatedOn= DateTime.Now,
                        CreatedBy= "Auto Create",
                        Id= "Student",
                    }
                };
                context.Roles.AddRange(listOfRoles);
                context.SaveChanges();
            }
        }
    }
}
