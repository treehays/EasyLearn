using EasyLearn.Models.DTOs.ModuleDTOs;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EasyLearn.Controllers;

public class ModuleController : Controller
{
    private readonly IModuleService _moduleService;
    private readonly ICourseService _courseService;

    public ModuleController(IModuleService moduleService, ICourseService courseService)
    {
        _moduleService = moduleService;
        _courseService = courseService;
    }

    public IActionResult Index()
    {
        return View();
    }


    public async Task<IActionResult> Create(string courseId)
    {

        var instructorId = User.FindFirstValue(ClaimTypes.Actor);
        var course = await _courseService.GetById(courseId);
        if (course.Data?.InstructorId != instructorId)
        {
            TempData["failed"] = course.Message;
            return RedirectToAction("Index", "Home");
        }
        var courseIdModel = new CreateModuleRequestModel
        {
            CourseId = courseId,
        };
        return View(courseIdModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateModuleRequestModel model)
    {
        var instructorId = User.FindFirstValue(ClaimTypes.Actor);
        if (!ModelState.IsValid)
        {
            TempData["failed"] = "Invalid inputs...";
            return View(model);
        }

        var createModule = await _moduleService.Create(model, instructorId);
        if (createModule.Status)
        {
            TempData["success"] = createModule.Message;
            return RedirectToAction("GetActiveCoursesOfTheAuthInstructor", "Course");
        }
        TempData["failed"] = createModule.Message;
        return View(model);
    }
    /// <summary>
    /// previous name GetAllByCourseId
    /// </summary>
    /// <param name="courseId"></param>
    /// <returns></returns>
    public async Task<IActionResult> GetCourseModulesByCourseInstructor(string courseId)
    {
        var instructorId = User.FindFirstValue(ClaimTypes.Actor);
        var module = await _moduleService.GetCourseContentsByCourseInstructor(courseId, instructorId);
        if (!module.Status)
        {
            TempData["failed"] = module.Message;
            return RedirectToAction("Index", "Home");
        }

        module.CourseId = courseId;
        TempData["failed"] = module.Message;
        return View(module);
    }

    public async Task<IActionResult> GetAll()
    {
        var module = await _moduleService.GetAll();
        if (!module.Status)
        {
            TempData["failed"] = module.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = module.Message;
        return View(module);
    }


    public async Task<IActionResult> GetNotDeleted()
    {
        var module = await _moduleService.GetNotDeleted();
        if (!module.Status)
        {
            TempData["failed"] = module.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = module.Message;
        return View(module);
    }


    public async Task<IActionResult> GetById(string id)
    {
        var module = await _moduleService.GetById(id);
        if (!module.Status)
        {
            TempData["failed"] = module.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = module.Message;
        return View(module);
    }


    public async Task<IActionResult> GetByVideoSequesnce(string videoSequence)
    {
        var module = await _moduleService.GetByVideoSequesnce(videoSequence);
        if (!module.Status)
        {
            TempData["failed"] = module.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = module.Message;
        return View(module);
    }


    public async Task<IActionResult> Detail(string id)
    {
        var module = await _moduleService.GetById(id);
        if (!module.Status)
        {
            TempData["failed"] = module.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = module.Message;
        return View(module);
    }


    public async Task<IActionResult> DeletePreview(string id)
    {
        var module = await _moduleService.GetById(id);
        if (module.Status) return View(module);
        TempData["failed"] = module.Message;
        return RedirectToAction(nameof(Index), "Home");

    }



    public async Task<IActionResult> Delete(string id)
    {
        var module = await _moduleService.Delete(id);
        if (module.Status)
        {
            TempData["success"] = module.Message;
            return RedirectToAction(nameof(GetAll));
        }

        TempData["failed"] = module.Message;
        return RedirectToAction(nameof(GetAll));
    }



    public async Task<IActionResult> Update(string id)
    {
        var module = await _moduleService.GetById(id);
        if (module.Status) return View(module);
        TempData["failed"] = module.Message;
        return RedirectToAction(nameof(Index));

    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Update(UpdateModuleRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["failed"] = "Invalid detail...";

            return View();
        }

        var update = await _moduleService.Update(model);
        if (update.Status)
        {
            TempData["success"] = update.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["failed"] = update.Message;
        return View();

    }



}
