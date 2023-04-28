using EasyLearn.Models.DTOs.InstructorDTOs;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Services.Implementations;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

namespace EasyLearn.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
    private readonly IRoleService _roleService;
        private readonly INigerianBankService _nigerianBankService;

        public UserController(IUserService userService, INigerianBankService nigerianBankService, IRoleService roleService)
        {
            _userService = userService;
            _nigerianBankService = nigerianBankService;
            _roleService = roleService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ResetPasswordPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email)
        {
            var baseUrl = $"https://{Request.Host}";
            var user = await _userService.ResetPassword(email, baseUrl);
            TempData["success"] = user.Message;
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ConfirmPasswordReset(string emailToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.PasswordRessetConfirmation(emailToken, userId);
            if (!user.Status)
            {
                TempData["failed"] = user.Message;
                return RedirectToAction("Index","Home");
            }
            TempData["success"] = user.Message;
            return View(user);
        }
       
        [HttpPost]
        public async Task<IActionResult> UpdateUserPassword(UpdateUserPasswordRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["success"] = "Enter a valid password,must be at least 8 characters long and contains uppercase letter, lowercase letter, number and symbol";
                return View(model);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.UpdateUserPassword(model, userId);
            if (!user.Status)
            {
                TempData["success"] = user.Message;
                return RedirectToAction("Index", "Home");
            }
            TempData["success"] = user.Message;
            return RedirectToAction("Login","Home");
        }
        public async Task<IActionResult> ConfirmEmail(string emailToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _userService.EmailVerification(emailToken, userId);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UpdateLoggedinUserProfile()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.GetByIdAsync(userid);
            if (!user.Status)
            {
                TempData["failed"] = user.Message;
                return RedirectToAction("index", "home");
            }
            TempData["sucess"] = user.Message;
            return View(user);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdateLoggedinUserProfile(UpdateUserProfileRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Invalid detail...";
                return View();
            }
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var updateProfile = await _userService.UpdateProfile(model, userid);
            if (updateProfile.Status)
            {
                TempData["success"] = updateProfile.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["failed"] = updateProfile.Message;
            return RedirectToAction(nameof(Index), "Home");
        }
        public async Task<IActionResult> ListOfLoggedinUserBankDetail(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.ListOfUserBankDetails(userId);
            if (!user.Status)
            {
                TempData["failed"] = user.Message;
                return RedirectToAction("index", "Home");
            }
            TempData["Success"] = user.Message;
            return View(user);

        }
        public async Task<IActionResult> UpdateBankDetail(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.GetPaymentDetailPaymentId(id, userId);
            if (!user.Status)
            {
                TempData["Success"] = user.Message;
                return RedirectToAction("index", "Home");
            }
            var listOfBanks = await _nigerianBankService.FetchAllNigerianBanks();
            ViewData["ListOfBanks"] = new SelectList(listOfBanks, "BankCode", "BankName");
            TempData["Success"] = user.Message;
            return View(user);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdateBankDetail(UpdateUserBankDetailRequestModel model)
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Invalid detail...";
                return View();
            }

            var user = await _userService.UpdateBankDetail(model, userid);
            if (user.Status)
            {
                TempData["Success"] = user.Message;
                return View(user);
            }
            TempData["failed"] = user.Message;
            return RedirectToAction("index", "Home");
        }
        public async Task<IActionResult> UpdateAddressDetail(string id)
        {
            var user = await _userService.GetFullDetailById(id);
            if (user.Status) return View(user);
            TempData["failed"] = user.Message;
            return RedirectToAction("Index", "home");
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdateAddressDetail(UpdateUserAddressRequestModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var instructor = await _userService.UpdateAddress(model, userId);
            if (instructor.Status)
            {
                TempData["Success"] = instructor.Message;
                return View(instructor);
            }
            TempData["failed"] = instructor.Message;
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> FindUser(string emailOrname)
        {
            var admin = await _userService.GetByUsersNameOrEmail(emailOrname);
            if (!admin.Status)
            {
                TempData["failed"] = admin.Message;
                return RedirectToAction("index", "Home");
            }

            TempData["success"] = admin.Message;
            return View(admin);
        }


        public async Task<IActionResult> UpgradeUser(string userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            if (!user.Status)
            {
                TempData["failed"] = user.Message;
                return RedirectToAction("index","Home");
            }
            var roles = await _roleService.GetAll();
            ViewData["roles"] = new SelectList(roles.Data, "Id", "RoleName");
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpgradeUser(UserUpgradeRequestModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.UpgradeUser(model, userId);
            if (!user.Status)
            {
                TempData["failed"] = user.Message;
                return RedirectToAction("Index","Home");
            }

            TempData["success"] = user.Message;
            return RedirectToAction("Index", "Home");
        }
    }
}
