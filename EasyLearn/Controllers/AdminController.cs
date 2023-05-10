using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Models.ViewModels;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Controllers;

public partial class AdminController : Controller
{
    private readonly IAdminService _adminService;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IEnrolmentService _enrolmentService;
    private readonly ICourseService _courseService;

    public AdminController(IAdminService adminService, IUserService userService, IRoleService roleService, IEnrolmentService enrolmentService, ICourseService courseService)
    {
        _adminService = adminService;
        _userService = userService;
        _roleService = roleService;
        _enrolmentService = enrolmentService;
        _courseService = courseService;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var topEnrollement = await _enrolmentService.RecentEnrollments(10);
        var unApprovedCourses = await _courseService.GetAllInActiveCourse();
        var adminIndexViewModel = new AdminIndexViewModel
        {
            Enrolments = topEnrollement,
            Courses = unApprovedCourses,
        };

        return View(adminIndexViewModel);
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

    //public async Task<IActionResult> FindUser(string emailOrname)
    //{
    //    var admin = await _adminService.GetByUsersNameOrEmail(emailOrname);
    //    if (!admin.Status)
    //    {
    //        TempData["failed"] = admin.Message;
    //        return RedirectToAction("index");
    //    }

    //    TempData["success"] = admin.Message;
    //    return View(admin);
    //    }


    [Route("User/UserDetail")]
    public async Task<IActionResult> UserDetail(string id)
    {
        var admin = await _userService.GetByIdAsync(id);
        if (!admin.Status)
        {
            TempData["failed"] = admin.Message;
            return RedirectToAction("index", "course");
        }
        //TempData["success"] = admin.Message;
        return View(admin);
    }

    //public async Task<IActionResult> UpgradeUser(string userId)
    //{
    //    var user = await _userService.GetByIdAsync(userId);
    //    if (!user.Status)
    //    {
    //        TempData["failed"] = user.Message;
    //        return RedirectToAction("index");
    //    }
    //    var roles = await _roleService.GetAll();
    //    ViewData["roles"] = new SelectList(roles.Data, "Id", "RoleName");
    //    return View(user);
    //}

    //[HttpPost]
    //public async Task<IActionResult> UpgradeUser(UserUpgradeRequestModel model)
    //{
    //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    //    var user = await _userService.UpgradeUser(model, userId);
    //    if (!user.Status)
    //    {
    //        TempData["failed"] = user.Message;
    //        return RedirectToAction("Index");
    //    }

    //    TempData["success"] = user.Message;
    //    return RedirectToAction("Index");
    //}

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