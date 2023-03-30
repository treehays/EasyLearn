using EasyLearn.Models.DTOs.CategoryDTOs;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Services.Implementations;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
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
        public async Task<IActionResult> Create(CreateCategoryRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Invalid inputs...";
                return View(model);
            }

            var createCategory = await _categoryService.Create(model);
            if (!createCategory.Status)
            {
                TempData["failed"] = createCategory.Message;
                return RedirectToAction(nameof(Index), "Home");
                //return View(model);
            }

            TempData["success"] = createCategory.Message;
            return RedirectToAction(nameof(GetAll));
        }

        public async Task<IActionResult> GetAll()
        {
            var category = await _categoryService.GetAll();
            if (!category.Status)
            {
                TempData["failed"] = category.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["success"] = category.Message;
            return View(category);
        }



        public async Task<IActionResult> Detail(string id)
        {
            var admin = await _categoryService.GetById(id);
            if (!admin.Status)
            {
                TempData["failed"] = admin.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["success"] = admin.Message;
            return View(admin);
        }



        public async Task<IActionResult> Update(string id)
        {
            var admin = await _categoryService.GetById(id);
            if (admin.Status) return View(admin);
            TempData["failed"] = admin.Message;
            return RedirectToAction(nameof(Index), "Home");
            //TempData["success"] = admin.Message;
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Invalid detail...";

                return View();
            }

            await _categoryService.UpdateCategory(model);

            return RedirectToAction(nameof(Index), "Home");
        }


    }
}
