using EasyLearn.Data;
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
            EasyLearnDbInitializer.Seed(app);

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}