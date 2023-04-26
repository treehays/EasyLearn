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
            var role = User.FindFirstValue(ClaimTypes.Role).ToString();
            var email = User.FindFirstValue(ClaimTypes.Email);
            var baseUrl = $"https://{Request.Host}/Enrolment/verifypayment";
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Try again..";
                return View();
            }

            if (role.ToLower() != "student")
            {
                TempData["failed"] = "Only student's can enroll for courses.";
                return RedirectToAction("Index", "Course");
            }

            var enrol = await _enrolmentService.Create(model, studentId, userId, email, baseUrl);
            if (!enrol.status)
            {
                TempData["failed"] = enrol.message;
                return RedirectToAction("GetEnrolledCourses", "Course");
            }

            TempData["success"] = enrol.message;
            return Redirect(enrol.data.authorization_url);
        }

        /*
                public async Task<IActionResult> RecentEnrollments(string trxref)
                {
                    var paymentStatus = await _enrolmentService.VerifyPayment(trxref);
                    if (!paymentStatus.Status)
                    {
                        TempData["failed"] = paymentStatus.Message;
                        //return RedirectToAction("UnPaidCourse", "Course");
                        return RedirectToAction("index", "Course");
                    }
                    TempData["success"] = paymentStatus.Message;
                    return RedirectToAction("index", "Course");
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

        */

        public IActionResult GenerateReceipt()
        {

            return View();
        }


    }
}
