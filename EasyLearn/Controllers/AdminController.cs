using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Services.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace EasyLearn.Controllers;

public partial class AdminController : Controller
{
    private readonly IAdminService _adminService;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public AdminController(IAdminService adminService, IUserService userService, IRoleService roleService)
    {
        _adminService = adminService;
        _userService = userService;
        _roleService = roleService;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var admins = await _adminService.GetAll();
        if (!admins.Status)
        {
            TempData["failed"] = admins.Message;
            return RedirectToAction(nameof(GetAllActive));

        }

        TempData["success"] = admins.Message;
        return View(admins);
    }

    public IActionResult RegisterAdmin()
    {

        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> RegisterAdmin(CreateUserRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["failed"] = "Invalid inputs...";
            return View(model);
        }
        var baseUrl = $"https://{Request.Host}";
        var registerAdmin = await _adminService.AdminRegistration(model, baseUrl);
        if (!registerAdmin.Status)
        {
            TempData["failed"] = registerAdmin.Message;
            return View(model);
        }

        TempData["success"] = registerAdmin.Message;
        return RedirectToAction("", "Login");
    }

    public async Task<IActionResult> Detail(string id)
    {
        var admin = await _adminService.GetById(id);
        if (!admin.Status)
        {
            TempData["failed"] = admin.Message;
            return RedirectToAction(nameof(GetAllActive));
        }

        TempData["success"] = admin.Message;
        return View(admin);
    }

    public async Task<IActionResult> UpgradeUser(string userId)
    {

        var user = await _userService.GetByIdAsync("43c31652-50cc-491b-9c45-99b27a1dfb2b");
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
            return RedirectToAction("Index");
        }

        TempData["success"] = user.Message;
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> DeletePreview(string id)
    {
        var admin = await _adminService.GetById(id);
        if (admin.Status) return View(admin);

        TempData["failed"] = admin.Message;
        return RedirectToAction(nameof(GetAllActive));
    }

    public async Task<IActionResult> Delete(string id)
    {
        var admin = await _adminService.Delete(id);
        if (admin.Status)
        {
            TempData["failed"] = admin.Message;
            return RedirectToAction(nameof(GetAllActive));
        }
        TempData["success"] = admin.Message;
        return View(admin);
    }


    public async Task<IActionResult> GetAll()
    {
        var admins = await _adminService.GetAll();
        if (!admins.Status)
        {
            TempData["failed"] = admins.Message;
            return RedirectToAction(nameof(GetAllActive));

        }

        TempData["success"] = admins.Message;
        return View(admins);
    }

    public async Task<IActionResult> GetAllActive()
    {
        var admins = await _adminService.GetAllActive();
        if (!admins.Status)
        {
            return RedirectToAction(nameof(GetAllActive));

        }

        TempData["success"] = admins.Message;
        return View(admins);
    }

    public async Task<IActionResult> GetAllInActive()
    {
        var admins = await _adminService.GetAllInActive();
        if (!admins.Status)
        {
            TempData["failed"] = admins.Message;
            return RedirectToAction(nameof(GetAllActive));
        }

        TempData["success"] = admins.Message;
        return View(admins);
    }

}