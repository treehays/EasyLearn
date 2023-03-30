using EasyLearn.Models.DTOs.RoleDTOs;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Services.Implementations;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
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
        public async Task<IActionResult> Create(CreateRoleRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Invalid inputs...";
                return View(model);
            }

            var createAdmin = await _roleService.Create(model);
            if (!createAdmin.Status)
            {
                TempData["failed"] = createAdmin.Message;
                return RedirectToAction(nameof(Index), "Home");
                //return View(model);
            }

            TempData["success"] = createAdmin.Message;
            return RedirectToAction(nameof(GetAll));
        }


        public async Task<IActionResult> GetAll()
        {
            var admins = await _roleService.GetAll();
            if (!admins.Status)
            {
                TempData["failed"] = admins.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["success"] = admins.Message;
            return View(admins);
        }

    }


}
