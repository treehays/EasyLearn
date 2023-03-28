using EasyLearn.Models.DTOs.CourseDTOs;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
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
        public async Task<IActionResult> Create(CreateCourseRequestModel model)
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
                return RedirectToAction(nameof(Index));
            }

            TempData["success"] = createCourse.Message;
            return RedirectToAction(nameof(Index));
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
            var course = await _courseService.Delete(id);
            if (course.Status)
            {
                TempData["success"] = course.Message;
                return RedirectToAction(nameof(GetAll));
            }

            TempData["failed"] = course.Message;
            return RedirectToAction(nameof(GetAll));
        }


        public async Task<IActionResult> GetAll()
        {
            var course = await _courseService.GetAll();
            if (!course.Status)
            {
                TempData["failed"] = course.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["success"] = course.Message;
            return View(course);
        }


        public async Task<IActionResult> GetAllActive()
        {
            var courses = await _courseService.GetAllActive();
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
            var courses = await _courseService.GetAllInActive();
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
            return RedirectToAction(nameof(Index), "Home");
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




    }
}
