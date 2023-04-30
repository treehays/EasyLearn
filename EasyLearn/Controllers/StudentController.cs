using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Controllers;

public class StudentController : Controller
{

    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult RegisterStudent()
    {
        return View();
    }
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> RegisterStudent(CreateUserRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["failed"] = "Invalid inputs...";
            return View(model);
        }
        var baseUrl = $"https://{Request.Host}";
        var create = await _studentService.StudentRegistration(model, baseUrl);
        if (!create.Status)
        {
            TempData["failed"] = create.Message;
            //return RedirectToAction(nameof(Index));
            return View(model);
        }

        TempData["success"] = create.Message;
        return RedirectToAction("index", "Course");

    }
    public async Task<IActionResult> Detail(string id)
    {
        var student = await _studentService.GetById(id);
        if (!student.Status)
        {
            TempData["failed"] = student.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = student.Message;
        return View(student);
    }
    public async Task<IActionResult> DeletePreview(string id)
    {
        var student = await _studentService.GetById(id);
        if (student.Status) return View(student);
        TempData["failed"] = student.Message;
        return RedirectToAction(nameof(Index), "Home");
    }
    public async Task<IActionResult> Delete(string id)
    {
        var student = await _studentService.Delete(id);
        if (student.Status)
        {
            TempData["success"] = student.Message;
            return RedirectToAction("index", "Home");
        }

        TempData["failed"] = student.Message;
        return RedirectToAction("index", "Home");
    }
    public async Task<IActionResult> GetAllStudents()
    {
        var students = await _studentService.GetAllStudent();
        if (!students.Status)
        {
            TempData["failed"] = students.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = students.Message;
        return View(students);
    }
    public async Task<IActionResult> GetAllActiveStudent()
    {
        var students = await _studentService.GetAllActive();
        if (!students.Status)
        {
            TempData["failed"] = students.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = students.Message;
        return View(students);
    }
    public async Task<IActionResult> GetAllInActiveStudent()
    {
        var students = await _studentService.GetAllInActive();
        if (!students.Status)
        {
            TempData["failed"] = students.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = students.Message;
        return View(students);
    }
    public async Task<IActionResult> GetAllUnverifiedStudent()
    {
        var students = await _studentService.GetAllInActive();
        if (!students.Status)
        {
            TempData["failed"] = students.Message;
            return RedirectToAction("Index", "Home");
        }

        TempData["success"] = students.Message;
        return View(students);
    }



}
