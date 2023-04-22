using EasyLearn.Models.DTOs.CourseDTOs;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace EasyLearn.Controllers
{
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
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Invalid inputs...";
                return View(model);
            }

            var createCourse = await _courseService.Create(model);
            if (!createCourse.Status)
            {
                TempData["failed"] = createCourse.Message;
                return View(model);
            }

            TempData["success"] = createCourse.Message;
            return RedirectToAction(nameof(GetAllCoursesByInstructorId));
        }

        public async Task<IActionResult> Detail(string id)
        {
            var course = await _courseService.GetById(id);
            if (!course.Status)
            {
                TempData["failed"] = course.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["success"] = course.Message;
            return View(course);
        }

        public async Task<IActionResult> DeletePreview(string id)
        {
            var course = await _courseService.GetById(id);
            if (course.Status) return View(course);
            TempData["failed"] = course.Message;
            return RedirectToAction(nameof(Index), "Home");

        }

        public async Task<IActionResult> Delete(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var course = await _courseService.Delete(id, userId);
            if (course.Status)
            {
                TempData["success"] = course.Message;
                return RedirectToAction(nameof(GetAll));
            }

            TempData["failed"] = course.Message;
            return RedirectToAction(nameof(GetAll));
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
            var course = await _courseService.GetAllInstructorCourse(instructorId);
            if (!course.Status)
            {
                TempData["failed"] = course.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["success"] = course.Message;
            return View(course);
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


        public async Task<IActionResult> GetByStatus(bool status)
        {
            var instructorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var courses = await _courseService.GetAllInActiveInstructorCourse(instructorId);
            if (!courses.Status)
            {
                TempData["failed"] = courses.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["success"] = courses.Message;
            return View(courses);
        }


        public async Task<IActionResult> GetByCategorys(bool status)
        {
            var instructorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var courses = await _courseService.GetAllActiveInstructorCourse(instructorId);
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
            var instructorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var courses = await _courseService.GetAllActiveInstructorCourse(instructorId);
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
            var instructorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var courses = await _courseService.GetAllInActiveInstructorCourse(instructorId);
            if (!courses.Status)
            {
                TempData["failed"] = courses.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["success"] = courses.Message;
            return View(courses);
        }

        public async Task<IActionResult> Update(string id)
        {
            var course = await _courseService.GetById(id);
            if (course.Status) return View(course);
            TempData["failed"] = course.Message;
            return RedirectToAction(nameof(Index));

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCourseRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Invalid detail...";

                return View();
            }

            var update = await _courseService.Update(model);
            if (update.Status)
            {
                TempData["success"] = update.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["failed"] = update.Message;
            return View();

        }




        public async Task<IActionResult> UpdateActive(string id)
        {
            var course = await _courseService.GetById(id);
            if (course.Status) return View(course);
            TempData["failed"] = course.Message;
            return RedirectToAction(nameof(Index), "Home");

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdateActive(UpdateCourseActiveStatusRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Invalid detail...";

                return View();
            }

            var update = await _courseService.UpdateActiveStatus(model);
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
}
