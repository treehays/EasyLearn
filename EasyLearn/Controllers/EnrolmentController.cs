using EasyLearn.Models.DTOs.EnrolmentDTOs;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EasyLearn.Controllers
{
    public class EnrolmentController : Controller
    {
        private readonly IEnrolmentService _enrolmentService;
        private readonly ICourseService _courseService;


        public EnrolmentController(IEnrolmentService enrolmentService, ICourseService courseService)
        {
            _enrolmentService = enrolmentService;
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(string courseId)
        {
            var course = await _courseService.GetById(courseId);
            if (course == null)
            {
                TempData["failed"] = "No course found..";
                return View();
            }
            var courseDetail = new CreateEnrolmentRequestModel
            {
                AmountPaid = course.Data.Price,
                CourseName = course.Data.Title,
                CourseId = course.Data.Id,
            };
            //ViewData["enrolCourse"] = course;
            return View(courseDetail);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEnrolmentRequestModel model)
        {
            var studentId = User.FindFirstValue(ClaimTypes.Actor);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Try again..";
                return View();
            }
            var enrol = await _enrolmentService.Create(model, studentId, userId);
            if (!enrol.Status)
            {
                TempData["failed"] = enrol.Message;
                return View(enrol);
            }
            TempData["success"] = enrol.Message;
            return RedirectToAction("ListAllCourses", "Course");
        }

        public IActionResult GenerateReceipt()
        {

            return View();
        }


    }
}
