using EasyLearn.GateWays.Payments;
using EasyLearn.Models.DTOs.EnrolmentDTOs;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EasyLearn.Controllers
{
    public class EnrolmentController : Controller
    {
        private readonly IEnrolmentService _enrolmentService;
        private readonly IPayStackService _payStackService;
        private readonly ICourseService _courseService;


        public EnrolmentController(IEnrolmentService enrolmentService, ICourseService courseService, IPayStackService payStackService)
        {
            _enrolmentService = enrolmentService;
            _courseService = courseService;
            _payStackService = payStackService;
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

            var enrol = await _enrolmentService.Create(model, studentId, userId);
            if (enrol.Status)
            {
                TempData["failed"] = enrol.Message;
                return RedirectToAction("Index", "Course");
            }
            var baseUrl = $"https://{Request.Host}/Enrolment/verifypayment";

            enrol.PaymentRequest.Email = User.FindFirstValue(ClaimTypes.Email);
            enrol.PaymentRequest.CallbackUrl = baseUrl;
            var proceedToPay = await _payStackService.InitializePayment(enrol.PaymentRequest);
            TempData["success"] = enrol.Message;
            return Redirect(proceedToPay.data.authorization_url);
            //return RedirectToAction("ListAllCourses", "Course");
        }

        public IActionResult GenerateReceipt()
        {

            return View();
        }


    }
}
