using EasyLearn.Models.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Controllers
{
    public partial class AdminController : Controller
    {

        public async Task<IActionResult> UpdateProfile(string id)
        {
            var admin = await _adminService.GetFullDetailById(id);
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

            var updateProfile = await _adminService.UpdateProfile(model);
            if (updateProfile.Status)
            {
                TempData["success"] = updateProfile.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["failed"] = updateProfile.Message;
            return RedirectToAction(nameof(Index), "Home");
        }


        public async Task<IActionResult> ListOfBankDetail(string id)
        {
            var admin = await _adminService.GetListOfAdminBankDetails(id);
            if (admin.Status) return View(admin);
            TempData["failed"] = admin.Message;
            return RedirectToAction(nameof(Index), "Home");
            //TempData["success"] = admin.Message;
        }


        public async Task<IActionResult> UpdateBankDetail(string id)
        {
            var admin = await _adminService.GetBankDetail(id);
            if (admin.Status) return View(admin);
            TempData["failed"] = admin.Message;
            return RedirectToAction(nameof(Index), "Home");
            //TempData["success"] = admin.Message;
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdateBankDetail(UpdateUserBankDetailRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Invalid detail...";

                return View();
            }

            var admin = await _adminService.UpdateBankDetail(model);

            return RedirectToAction(nameof(Index), "Home");
        }

        public async Task<IActionResult> UpdateAddressDetail(string id)
        {
            var admin = await _adminService.GetFullDetailById(id);
            if (admin.Status) return View(admin);
            TempData["failed"] = admin.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdateAddressDetail(UpdateUserAddressRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Invalid detail...";

                return View();
            }

            var admin = await _adminService.UpdateAddress(model);

            return RedirectToAction(nameof(Index), "Home");
        }


    }
}
