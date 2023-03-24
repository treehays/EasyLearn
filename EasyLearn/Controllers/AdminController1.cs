using EasyLearn.Models.DTOs.AdminDTOs;
using EasyLearn.Models.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Controllers
{
    public partial class AdminController : Controller
    {

        public async Task<IActionResult> UpdateProfile(string id)
        {
            var admin = await _adminService.GetById(id);
            if (admin.Status) return View(admin);
            TempData["failed"] = admin.Message;
            return RedirectToAction(nameof(Index), "Home");
            //TempData["success"] = admin.Message;
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateUserProfileRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Invalid detail...";

                return View();
            }

            await _adminService.UpdateProfile(model);

            return RedirectToAction(nameof(Index), "Home");
        }


        public async Task<IActionResult> UpdateAdminBankDetail(string id)
        {
            var admin = await _adminService.GetById(id);
            if (admin.Status) return View(admin);
            TempData["failed"] = admin.Message;
            return RedirectToAction(nameof(Index), "Home");
            //TempData["success"] = admin.Message;
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdateAdminBankDetail(UpdateUserBankDetailRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Invalid detail...";

                return View();
            }

            await _adminService.UpdateBankDetail(model);

            return RedirectToAction(nameof(Index), "Home");
        }


    }
}
