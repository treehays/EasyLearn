using EasyLearn.Models.DTOs.AdminDTOs;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Controllers;

public class AdminController : Controller
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
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

    [HttpPost]
    public async Task<IActionResult> Create(CreateAdminRequestModel model)
    {
        var createAdmin = await _adminService.Create(model);
        if (!createAdmin.Status)
        {
            TempData["failed"] = createAdmin.Message;
        return RedirectToAction(nameof(Index),"Home");
            //return View(model);
        }
        TempData["success"] = createAdmin.Message;
        return RedirectToAction(nameof(Index),"Home");
    }

    public async Task<IActionResult> GetAllAdmin()
    {
        var admins = await _adminService.GetAll();
        if (!admins.Status)
        {
            TempData["failed"] = admins.Message;
            return RedirectToAction(nameof(Index),"Home");
        }
        TempData["success"] = admins.Message;
        return View(admins);
    }

    public async Task<IActionResult> Detail(string id)
    {
        var admin = await _adminService.GetById(id);
        return View(admin);
    }
}