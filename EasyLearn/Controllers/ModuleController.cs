using EasyLearn.GateWays.FileManager;
using EasyLearn.Models.DTOs.FIleManagerDTOs;
using EasyLearn.Models.DTOs.ModuleDTOs;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace EasyLearn.Controllers;

public class ModuleController : Controller
{
    private readonly IModuleService _moduleService;
    private readonly ICourseService _courseService;
    private readonly IFileManagerService _fileManagerService;

    public ModuleController(IModuleService moduleService, ICourseService courseService, IFileManagerService fileManagerService)
    {
        _moduleService = moduleService;
        _courseService = courseService;
        _fileManagerService = fileManagerService;
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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var module = await _moduleService.Delete(id, userId);
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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var update = await _moduleService.Update(model, userId);
        if (update.Status)
        {
            TempData["success"] = update.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["failed"] = update.Message;
        return View();

    }

    public async Task<IActionResult> UploadCSVFile(IFormFile formFile, string courseId)
    {

        var instructorId = User.FindFirstValue(ClaimTypes.Actor);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = User.FindFirstValue(ClaimTypes.Email);
        var fileName = await _fileManagerService.GetFileName(formFile, "CsvFolder", "Uploads");
        var uploadd = await _moduleService.ModuleUploaderTemplate(fileName, userId, email, courseId);
        return RedirectToAction("GenerateCSVFile");
    }

    public async Task<IActionResult> GenerateCSVFile()
    {
        var instructorId = User.FindFirstValue(ClaimTypes.Actor);
        var instructorCourse = await _courseService.GetActiveCoursesOfAnInstructor(instructorId);
        ViewData["Courses"] = new SelectList(instructorCourse.Data, "Id", "Title");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> GenerateCSVFile(CSVFileRequestModel model)
    {
        var gg = AppDomain.CurrentDomain.BaseDirectory;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = User.FindFirstValue(ClaimTypes.Email);
        var generate = await _moduleService.GenerateModuleUploaderTemplate(model, userId, email);
        var baseUrl = $"https://{Request.Host}/CsvFolder/{generate.FileName}";
        //return Redirect(generate.FullFilePath);
        var filename = $"~/CsvFolder/{generate.FileName}";
        return File(filename, "application/force-download", Path.GetFileName(filename));
    }


}
