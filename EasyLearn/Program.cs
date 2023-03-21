using EasyLearn.Data;
using EasyLearn.Repositories.Implementations;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Implementations;
using EasyLearn.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EasyLearn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            var configuration = builder.Configuration.GetConnectionString("EasyLearnDbConnectionString");
            builder.Services.AddDbContext<EasyLearnDbContext>(options => options.UseMySql(configuration, ServerVersion.AutoDetect(configuration)));
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
            builder.Services.AddScoped<IInstructorService, InstructorService>();
            builder.Services.AddScoped<IModeratorRepository, ModeratorRepository>();
            builder.Services.AddScoped<IModeratorService, ModeratorService>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            // EasyLearnDbInitializer.Seed(app);

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}