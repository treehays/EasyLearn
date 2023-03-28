﻿using EasyLearn.Models.DTOs.InstructorDTOs;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorService _instructorService;

        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

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
        public async Task<IActionResult> Create(CreateInstructorRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Invalid inputs...";
                return View(model);
            }

            var create = await _instructorService.Create(model);
            if (!create.Status)
            {
                TempData["failed"] = create.Message;
                return RedirectToAction(nameof(Index));
                //return View(model);
            }

            TempData["success"] = create.Message;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(string id)
        {
            var instructor = await _instructorService.GetById(id);
            if (!instructor.Status)
            {
                TempData["failed"] = instructor.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["success"] = instructor.Message;
            return View(instructor);
        }


        public async Task<IActionResult> DeletePreview(string id)
        {
            var instructor = await _instructorService.GetById(id);
            if (instructor.Status) return View(instructor);
            TempData["failed"] = instructor.Message;
            return RedirectToAction(nameof(Index), "Home");
        }


        public async Task<IActionResult> Delete(string id)
        {
            var instructor = await _instructorService.Delete(id);
            if (instructor.Status)
            {
                TempData["success"] = instructor.Message;
                return RedirectToAction(nameof(GetAll));
            }

            TempData["failed"] = instructor.Message;
            return RedirectToAction(nameof(GetAll));
        }


        public async Task<IActionResult> GetAll()
        {
            var instructors = await _instructorService.GetAll();
            if (!instructors.Status)
            {
                TempData["failed"] = instructors.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["success"] = instructors.Message;
            return View(instructors);
        }


        public async Task<IActionResult> GetAllActive()
        {
            var instructors = await _instructorService.GetAllActive();
            if (!instructors.Status)
            {
                TempData["failed"] = instructors.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["success"] = instructors.Message;
            return View(instructors);
        }


        public async Task<IActionResult> GetAllInActive()
        {
            var instructors = await _instructorService.GetAllInActive();
            if (!instructors.Status)
            {
                TempData["failed"] = instructors.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["success"] = instructors.Message;
            return View(instructors);
        }


        public async Task<IActionResult> Update(string id)
        {
            var instructors = await _instructorService.GetById(id);
            if (instructors.Status) return View(instructors);
            TempData["failed"] = instructors.Message;
            return RedirectToAction(nameof(Index), "Home");
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateInstructorProfileRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Invalid detail...";

                return View();
            }

            var update = await _instructorService.UpdateProfile(model);
            if (update.Status)
            {
                TempData["success"] = update.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["failed"] = update.Message;
            return RedirectToAction(nameof(Index), "Home");
        }



        public async Task<IActionResult> UpdateProfile(string id)
        {
            var instructors = await _instructorService.GetById(id);
            if (instructors.Status) return View(instructors);
            TempData["failed"] = instructors.Message;
            return RedirectToAction(nameof(Index), "Home");
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateInstructorProfileRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Invalid detail...";
                return View();
            }

            var updateProfile = await _instructorService.UpdateProfile(model);
            if (updateProfile.Status)
            {
                TempData["success"] = updateProfile.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["failed"] = updateProfile.Message;
            return RedirectToAction(nameof(Index), "Home");
        }


        public async Task<IActionResult> UpdateBankDetail(string id)
        {
            var admin = await _instructorService.GetById(id);
            if (admin.Status) return View(admin);
            TempData["failed"] = admin.Message;
            return RedirectToAction(nameof(Index), "Home");
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdateBankDetail(UpdateInstructorBankDetailRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Invalid detail...";
                return View();
            }

            var admin = await _instructorService.UpdateBankDetail(model);

            return RedirectToAction(nameof(Index), "Home");
        }

    }
}