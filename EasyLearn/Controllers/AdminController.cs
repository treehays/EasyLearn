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
            return View(model);
        }
        TempData["success"] = createAdmin.Message;
        return View();
    }
}