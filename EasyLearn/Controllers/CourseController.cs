using EasyLearn.Models.DTOs.CourseDTOs;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace EasyLearn.Controllers;

public class CourseController : Controller
{
    private readonly ICourseService _courseService;
    private readonly ICategoryService _categoryService;


    public CourseController(ICourseService courseService, ICategoryService categoryService)
    {
        _courseService = courseService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var course = await _courseService.GetAllCourse();
        if (!course.Status)
        {
            TempData["failed"] = course.Message;
            return RedirectToAction("Index", "Home");
        }

        //TempData["success"] = course.Message;
        return View(course);
    }


    public IActionResult Dashboard()
    {

        return View();
    }


    public async Task<IActionResult> CreateCourse()
    {
        var categoryList = await _categoryService.GetAll();

        var multiSelectList = new MultiSelectList(categoryList.Data, "Id", "Name");

        ViewData["Category"] = new SelectList(categoryList.Data, "Id", "Name");


        var createDetail = new CreateCourseRequestModel
        {
            multiSelectList = multiSelectList,
        };
        return View(createDetail);
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> CreateCourse(CreateCourseRequestModel model)
    {
        var instructorId = User.FindFirstValue(ClaimTypes.Actor);
        if (!ModelState.IsValid)
        {
            TempData["failed"] = "Invalid inputs...";
            return View(model);
        }

        var createCourse = await _courseService.Create(model, instructorId);
        if (!createCourse.Status)
        {
            TempData["failed"] = createCourse.Message;
            return View(model);
        }

        TempData["success"] = createCourse.Message;
        return RedirectToAction(nameof(GetAllCoursesByInstructorId));
    }

    public async Task<IActionResult> Detail(string courseId)
    {
        var course = await _courseService.GetFullDetailOfCourseById(courseId);
        if (!course.Status)
        {
            TempData["failed"] = course.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = course.Message;
        return View(course);
    }

    public async Task<IActionResult> DeletePreview(string courseId)
    {
        var course = await _courseService.GetById(courseId);
        if (course.Status) return View(course);
        TempData["failed"] = course.Message;
        return RedirectToAction("Index", "Home");

    }

    public async Task<IActionResult> Delete(string courseId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var course = await _courseService.Delete(courseId, userId);
        if (course.Status)
        {
            TempData["success"] = course.Message;
            return RedirectToAction("Index", "Home");

        }

        TempData["failed"] = course.Message;
        return RedirectToAction("Index", "Home");

    }

    [Route("[controller]/ListAllCourses")]
    public async Task<IActionResult> GetAll()
    {
        //var instructorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var course = await _courseService.GetAllCourse();
        if (!course.Status)
        {
            TempData["failed"] = course.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = course.Message;
        return View(course);
    }


    //[Route("[controller]/ListAllCourses")]
    public async Task<IActionResult> GetAllCoursesByInstructorId()
    {
        var instructorId = User.FindFirst(ClaimTypes.Actor)?.Value;
        var course = await _courseService.GetAllCoursesByAnInstructor(instructorId);
        if (!course.Status)
        {
            TempData["failed"] = course.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = course.Message;
        return View(course);
    }

    public async Task<IActionResult> GetActiveCoursesOfTheAuthInstructor()
    {
        var instructorId = User.FindFirst(ClaimTypes.Actor)?.Value;
        var course = await _courseService.GetActiveCoursesOfAnInstructor(instructorId);
        if (!course.Status)
        {
            TempData["failed"] = course.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = course.Message;
        return View(course);
    }




    public async Task<IActionResult> GetAllInActiveCoursesOfAuthInstructor()
    {
        var instructorId = User.FindFirst(ClaimTypes.Actor)?.Value;

        var courses = await _courseService.GetInActiveCoursesOfAnInstructor(instructorId);
        if (!courses.Status)
        {
            TempData["failed"] = courses.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = courses.Message;
        return View(courses);
    }


    public async Task<IActionResult> GetAllUnverifiedCoursesOfAuthInstructor()
    {
        var instructorId = User.FindFirst(ClaimTypes.Actor)?.Value;

        var courses = await _courseService.GetUnverifiedCoursesOfAnInstructor(instructorId);
        if (!courses.Status)
        {
            TempData["failed"] = courses.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = courses.Message;
        return View(courses);
    }



    public async Task<IActionResult> GetEnrolledCourses()
    {
        var studentId = User.FindFirst(ClaimTypes.Actor)?.Value;
        var courses = await _courseService.GetEnrolledCourses(studentId);
        if (!courses.Status)
        {
            TempData["failed"] = courses.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = courses.Message;
        return View(courses);
    }

    public async Task<IActionResult> UnpaidCourse()
    {
        var studentId = User.FindFirst(ClaimTypes.Actor)?.Value;
        var courses = await _courseService.UnpaidCourse(studentId);
        if (!courses.Status)
        {
            TempData["failed"] = courses.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = courses.Message;
        return View(courses);
    }



    public async Task<IActionResult> StudentActiveCourses()
    {
        var studentId = User.FindFirst(ClaimTypes.Actor)?.Value;
        var courses = await _courseService.StudentActiveCourses(studentId);
        if (!courses.Status)
        {
            TempData["failed"] = courses.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = courses.Message;
        return View(courses);
    }



    public async Task<IActionResult> GetCompletedCourses()
    {
        var studentId = User.FindFirst(ClaimTypes.Actor)?.Value;
        var courses = await _courseService.GetCompletedCourses(studentId);
        if (!courses.Status)
        {
            TempData["failed"] = courses.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = courses.Message;
        return View(courses);
    }



    public async Task<IActionResult> GetAllActive()
    {
        //var instructorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //var courses = await _courseService.GetAllActiveInstructorCourse(instructorId);
        var courses = await _courseService.GetAllActiveCourse();

        if (!courses.Status)
        {
            TempData["failed"] = courses.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = courses.Message;
        return View(courses);
    }

    public async Task<IActionResult> GetAllInActive()
    {
        //var instructorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var courses = await _courseService.GetAllInActiveCourse();
        if (!courses.Status)
        {
            TempData["failed"] = courses.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = courses.Message;
        return View(courses);
    }


    public async Task<IActionResult> GetAllUnVerifiedCourse()
    {
        //var instructorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var courses = await _courseService.GetAllUnVerifiedCourse();
        if (!courses.Status)
        {
            TempData["failed"] = courses.Message;
            return View();
        }
        TempData["success"] = courses.Message;
        return View(courses);
    }

    public async Task<IActionResult> Update(string courseId)
    {
        var course = await _courseService.GetById(courseId);
        if (course.Status) return View(course);
        TempData["failed"] = course.Message;
        return RedirectToAction(nameof(Index));

    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Update(UpdateCourseRequestModel model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!ModelState.IsValid)
        {
            TempData["failed"] = "Invalid detail...";

            return View();
        }

        var update = await _courseService.Update(model, userId);
        if (update.Status)
        {
            TempData["success"] = update.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["failed"] = update.Message;
        return View();

    }






    public async Task<IActionResult> ApproveCourse(string courseId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var course = await _courseService.ApproveCourse(courseId, userId);
        if (!course.Status)
        {
            TempData["failed"] = course.Message;
            return RedirectToAction(nameof(Index), "Home");
        }
        TempData["success"] = course.Message;
        return RedirectToAction("GetAllUnVerifiedCourse", "Course");

    }


    public async Task<IActionResult> RejectCourse(string courseId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var course = await _courseService.RejectCourse(courseId, userId);
        if (!course.Status)
        {
            TempData["failed"] = course.Message;
            return RedirectToAction(nameof(Index), "Home");
        }
        TempData["success"] = course.Message;
        return RedirectToAction("GetAllUnVerifiedCourse", "Course");

    }

    public async Task<IActionResult> UpdateActive(string courseId)
    {
        var course = await _courseService.GetById(courseId);
        if (course.Status) return View(course);
        TempData["failed"] = course.Message;
        return RedirectToAction(nameof(Index), "Home");

    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> UpdateActive(UpdateCourseActiveStatusRequestModel model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!ModelState.IsValid)
        {
            TempData["failed"] = "Invalid detail...";

            return View();
        }

        var update = await _courseService.UpdateActiveStatus(model, userId);
        if (update.Status)
        {
            TempData["success"] = update.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["failed"] = update.Message;
        return RedirectToAction(nameof(Index), "Home");
    }




    public async Task<IActionResult> GlobalSearch(string name)
    {

        var globalResult = await _courseService.GlobalSearch(name);
        if (globalResult == null)
        {
            TempData["failed"] = "No result found";
            return RedirectToAction(nameof(Index), "Home");
        }

        return View(globalResult);
        //return View();
    }



}
