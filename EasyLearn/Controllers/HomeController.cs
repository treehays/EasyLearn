using EasyLearn.Models;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using sib_api_v3_sdk.Client;
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
            return View();
        }


        public IActionResult Testing(string email, string OTPKey)
        {

            var verify = _userService.Testing(email, OTPKey);
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var getEmail = await _userService.Login(model.Email);

            if (!getEmail.Status)
            {
                TempData["failed"] = getEmail.Message;
                return View();
            }
            var verifyPassword = BCrypt.Net.BCrypt.Verify(model.Password, getEmail.Password);

            if (!verifyPassword)
            {
                TempData["failed"] = getEmail.Message;
                return View();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role,getEmail.RoleId),
                new Claim(ClaimTypes.NameIdentifier,getEmail.Id),
                new Claim(ClaimTypes.Actor, getEmail.Id),
                new Claim(ClaimTypes.Name,getEmail.FirstName),
            };
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authenticationProperties = new AuthenticationProperties();
            var principal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

            if (getEmail.RoleId == "Admin")
            {
                TempData["success"] = "Login successful";
                return RedirectToAction("GetAllActive", "Admin");
            }
            else if (getEmail.RoleId == "Instructor")
            {
                TempData["success"] = "Login successful";
                return RedirectToAction("GetAllActive", "Instructor");
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