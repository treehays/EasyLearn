using EasyLearn.Models;
using EasyLearn.Models.DTOs.EmailSenderDTOs;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace EasyLearn.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;


        public HomeController(ILogger<HomeController> logger, IUserService userService, IEmailService emailService)
        {
            _logger = logger;
            _userService = userService;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Testing()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Testing(EmailSenderAttachmentDTO model)
        {

            await _emailService.SendEmailAttachment(model);
            return View();
        }

        //[Route("v{version:apiVersion}/[controller]")]
        [Route("{Login}")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("{Login}")]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userService.Login(model);

            if (!user.Status)
            {
                TempData["failed"] = user.Message;
                return View();
            }
            //var verifyPassword = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

            //if (!verifyPassword)
            //{
            //    TempData["failed"] = user.Message;
            //    return View();
            //}
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role,user.RoleId),
                new Claim(ClaimTypes.NameIdentifier,user.UserId),
                new Claim(ClaimTypes.Actor, user.Id),
                new Claim(ClaimTypes.Name,user.FirstName),
            };
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authenticationProperties = new AuthenticationProperties();
            var principal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

            if (user.RoleId == "Admin")
            {
                TempData["success"] = "Login successful";
                return RedirectToAction("GetAllActive", "Admin");
            }
            else if (user.RoleId == "Instructor")
            {
                TempData["success"] = "Login successful";
                return RedirectToAction("GetAllActive", "Instructor");
            }
            else if (user.RoleId == "Student")
            {
                TempData["success"] = "Login successful";
                return RedirectToAction("GetAllActive", "Student");
            }
            else
            {
                TempData["success"] = "Account has not been activated";
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}