using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Controllers;

public partial class AdminController : Controller
{
    private readonly IAdminService _adminService;
    private readonly IUserService _userService;

    public AdminController(IAdminService adminService, IUserService userService)
    {
        _adminService = adminService;
        _userService = userService;
    }

    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {

        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["failed"] = "Invalid inputs...";
            return View(model);
        }
        var baseUrl = $"https://{Request.Host}";
        var createAdmin = await _adminService.AdminRegistration(model,baseUrl);
        if (!createAdmin.Status)
        {
            TempData["failed"] = createAdmin.Message;
            return RedirectToAction(nameof(GetAllActive));
            //return View(model);
        }

        TempData["success"] = createAdmin.Message;
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Detail(string id)
    {
        var admin = await _adminService.GetById(id);
        if (!admin.Status)
        {
            TempData["failed"] = admin.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = admin.Message;
        return View(admin);
    }

    public async Task<IActionResult> DeletePreview(string id)
    {
        var admin = await _adminService.GetById(id);
        if (admin.Status) return View(admin);
        TempData["failed"] = admin.Message;
        return RedirectToAction(nameof(Index), "Home");
        //TempData["success"] = admin.Message;
    }

    public async Task<IActionResult> Delete(string id)
    {
        var admin = await _adminService.Delete(id);
        if (admin.Status)
        {
            TempData["success"] = admin.Message;
            return RedirectToAction(nameof(GetAll));
        }

        TempData["failed"] = admin.Message;
        return RedirectToAction(nameof(GetAll));
    }


    public async Task<IActionResult> GetAll()
    {
        var admins = await _adminService.GetAll();
        if (!admins.Status)
        {
            TempData["failed"] = admins.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = admins.Message;
        return View(admins);
    }

    public async Task<IActionResult> GetAllActive()
    {
        var admins = await _adminService.GetAllActive();
        if (!admins.Status)
        {
            TempData["failed"] = admins.Message;
            return RedirectToAction(nameof(Index), "Home");
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
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = admins.Message;
        return View(admins);
    }

}