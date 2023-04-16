using EasyLearn.Models;
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


        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            // ViewData["Title"] = "Light Sidebar";
            // ViewData["pTitle"] = "Light Sidebar";
            return View();
        }

        public async Task<IActionResult> ConfirmEmail(string emailToken)
        {
            await _userService.EmailVerification(emailToken);
            return RedirectToAction("Index");
        }

        //[Route("v{version:apiVersion}/[controller]")]
        //[Route("{Login}")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index");
            }
            return View();
        }

        [HttpPost]
        // [Route("{Account/login}")]
        //[Route("{Login}")]
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

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role,user.RoleId.ToLower()),
                new Claim(ClaimTypes.NameIdentifier,user.UserId),
                new Claim(ClaimTypes.Actor, user.Id),
                new Claim(ClaimTypes.Name,user.FirstName),
                new Claim(ClaimTypes.UserData,user.ProfilePicture),
            };
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authenticationProperties = new AuthenticationProperties();
            var principal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

            if (user.RoleId == "admin")
            {
                TempData["success"] = "Login successful";
                return RedirectToAction("Index", "Admin");
            }
            else if (user.RoleId == "instructor")
            {
                TempData["success"] = "Login successful";
                return RedirectToAction("GetAllActive", "Instructor");
            }
            else if (user.RoleId == "moderator")
            {
                TempData["success"] = "Login successful";
                return RedirectToAction("GetAllActive", "Student");
            }  else if (user.RoleId == "student")
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


        // [Route("Account/login")]
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