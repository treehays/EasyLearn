using EasyLearn.Models.DTOs.EnrolmentDTOs;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Controllers
{
    public class EnrolmentController : Controller
    {
        private readonly IEnrolmentService _enrolmentService;

        public EnrolmentController(IEnrolmentService enrolmentService)
        {
            _enrolmentService = enrolmentService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEnrolmentRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Try again..";
                return View();
            }
            var enrol = await _enrolmentService.Create(model);
            if (!enrol.Status)
            {
                TempData["failed"] = enrol.Message;
                return View(enrol);
            }
            TempData["success"] = enrol.Message;
            return RedirectToAction("GenerateReciept");
        }

        public IActionResult GenerateReciept()
        {

            return View();
        }


    }
}
