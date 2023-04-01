using EasyLearn.Models.DTOs.ModulesDTOs;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Controllers
{
    public class ModuleController : Controller
    {
        private readonly IModuleService _moduleService;

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create(string CourseId)
        {
            var courseId = new CreateModuleRequestModel
            {
                CourseId = CourseId,
            };
            return View(courseId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateModuleRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Invalid inputs...";
                return View(model);
            }

            var createModule = await _moduleService.Create(model);
            if (createModule.Status)
            {
                TempData["success"] = createModule.Message;
                return RedirectToAction("GetAll");
            }
            TempData["failed"] = createModule.Message;
            return View(model);
        }

        public async Task<IActionResult> GetAllByCourseId(string courseId)
        {
            if (courseId == null)
            {
                TempData["failed"] = "Empty course";
                return RedirectToAction(nameof(Index), "Home");
            }
            var module = await _moduleService.GetByCourse(courseId);
            if (!module.Status)
            {
                module = new ModulesResponseModel
                {
                    CourseId = courseId,
                };
                TempData["failed"] = module.Message;
                return View(module);
                //return RedirectToAction(nameof(Index), "Home");
            }

            TempData["success"] = module.Message;
            return View(module);
        }

        public async Task<IActionResult> GetAll()
        {
            var module = await _moduleService.GetAll();
            if (!module.Status)
            {
                TempData["failed"] = module.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["success"] = module.Message;
            return View(module);
        }


        public async Task<IActionResult> GetNotDeleted()
        {
            var module = await _moduleService.GetNotDeleted();
            if (!module.Status)
            {
                TempData["failed"] = module.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["success"] = module.Message;
            return View(module);
        }


        public async Task<IActionResult> GetById(string id)
        {
            var module = await _moduleService.GetById(id);
            if (!module.Status)
            {
                TempData["failed"] = module.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["success"] = module.Message;
            return View(module);
        }


        public async Task<IActionResult> Detail(string id)
        {
            var module = await _moduleService.GetById(id);
            if (!module.Status)
            {
                TempData["failed"] = module.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["success"] = module.Message;
            return View(module);
        }


        //public async Task<IActionResult> GetByCourse(string moduleId, string courseId)
        //{
        //    var module = await _moduleService.GetByCourse(courseId, moduleId);
        //    if (!module.Status)
        //    {
        //        TempData["failed"] = module.Message;
        //        return RedirectToAction(nameof(Index), "Home");
        //    }

        //    TempData["success"] = module.Message;
        //    return View(module);
        //}



        public async Task<IActionResult> DeletePreview(string id)
        {
            var module = await _moduleService.GetById(id);
            if (module.Status) return View(module);
            TempData["failed"] = module.Message;
            return RedirectToAction(nameof(Index), "Home");

        }



        public async Task<IActionResult> Delete(string id)
        {
            var module = await _moduleService.Delete(id);
            if (module.Status)
            {
                TempData["success"] = module.Message;
                return RedirectToAction(nameof(GetAll));
            }

            TempData["failed"] = module.Message;
            return RedirectToAction(nameof(GetAll));
        }



        public async Task<IActionResult> Update(string id)
        {
            var module = await _moduleService.GetById(id);
            if (module.Status) return View(module);
            TempData["failed"] = module.Message;
            return RedirectToAction(nameof(Index));

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateModuleRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Invalid detail...";

                return View();
            }

            var update = await _moduleService.Update(model);
            if (update.Status)
            {
                TempData["success"] = update.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["failed"] = update.Message;
            return View();

        }



    }
}
