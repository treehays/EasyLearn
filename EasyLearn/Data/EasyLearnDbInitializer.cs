using EasyLearn.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyLearn.Data
{
    public class EasyLearnDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<EasyLearnDbContext>();

                context.Database.Migrate();
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();
/*
                //Create Role
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(new List<Role>()
                    {
                        new Role()
                        {

                        }
                    });
                    context.SaveChanges();
                }

                //Create User
                if (!context.Users.Any())
                {
                    context.Users.AddRange(new List<User>()
                    {
                        new User()
                        {

                        }
                    });
                    context.SaveChanges();
                }

                //Create Admin
                if (!context.Admins.Any())
                {
                    context.Admins.AddRange(new List<Admin>()
                    {
                        new Admin()
                        {

                        }
                    });
                    context.SaveChanges();
                }
                        */
            }
        }
    }
}