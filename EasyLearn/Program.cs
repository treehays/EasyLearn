using EasyLearn.Data;
using EasyLearn.GateWays.Email;
using EasyLearn.GateWays.FileManager;
using EasyLearn.Repositories.Implementations;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Implementations;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
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

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ICourseService, CourseService>();

            builder.Services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();

            builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
            builder.Services.AddScoped<IInstructorService, InstructorService>();

            builder.Services.AddScoped<IModeratorRepository, ModeratorRepository>();
            builder.Services.AddScoped<IModeratorService, ModeratorService>();

            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IStudentService, StudentService>();

            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IRoleService, RoleService>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IAddressRepository, AddressRepository>();

            builder.Services.AddScoped<IPaymentDetailRepository, PaymentDetailRepository>();
            builder.Services.AddScoped<IPaymentDetailService, PaymentDetailService>();

            builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
            builder.Services.AddScoped<IModuleService, ModuleService>();

            builder.Services.AddScoped<ISendInBlueEmailService, SendInBlueEmailService>();
            builder.Services.AddScoped<IFileManagerService, FileManagerService>();

            builder.Services.AddScoped<IEnrolmentRepository, EnrolmentRepository>();
            builder.Services.AddScoped<IEnrolmentService, EnrolmentService>();

            //builder.Services.AddFluentEmail("katelynn3@ethereal.email")
            //    .AddMailKitSender(new FluentEmail.MailKitSmtp.SmtpClientOptions
            //    {
            //        Server = "smtp.ethereal.email",
            //        Port = 587,
            //        Password = "qhdb6KQeKn49ameSj5",
            //        RequiresAuthentication = true,
            //        User = "katelynn3@ethereal.email",
            //        SocketOptions = MailKit.Security.SecureSocketOptions.StartTls
            //    });


            // Set the execution timeout
            builder.Services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequestSizeLimitAttribute(int.MaxValue));
            });

            //// Add Kestrel server options
            //builder.Services.Configure<KestrelServerOptions>(options =>
            //{
            //    options.Limits.MaxRequestBodySize = int.MaxValue;
            //});

            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = int.MaxValue;
            });


            //builder.Services.AddHttpClient("MyHttpClient", client =>
            //{
            //    // Set the maximum request timeout to 5 minutes
            //    client.Timeout = TimeSpan.FromMinutes(5);
            //});

            /*Thi is working*/



            /*

            This 2 doesnt work
                        // Set the maximum request length (in bytes)
                        builder.Services.Configure<IISServerOptions>(options =>
                        {
                            options.MaxRequestBodySize = int.MaxValue;
                        });




                        builder.Services.Configure<FormOptions>(options =>
                        {
                            // Set the limit to 128 MB
                            options.MultipartBodyLengthLimit = 134217728;
                        });

            */











            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                configuration =>
                {
                    configuration.LoginPath = "/Home/Login";
                    configuration.LogoutPath = "/Home/Login";
                    configuration.Cookie.Name = "EasyLearn2.0";
                    //configuration.Cookie.Expiration = TimeSpan.FromHours(18);
                    configuration.Cookie.MaxAge = TimeSpan.FromDays(1);
                });
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

            app.UseAuthentication();
            app.UseAuthorization();


            //seeding into databse secondmethos
            //EasyLearnDbSeedingData.InitializeDb(app.Services.CreateScope().ServiceProvider);
            //seeding into databse first methos

            EasyLearnDbInitializer.Seed(app);

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}