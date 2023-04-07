using EasyLearn.Models.DTOs.ModeratorDTOs;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Controllers
{
    public class ModeratorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly IModeratorService _moderatorService;

        public ModeratorController(IModeratorService moderatorService)
        {
            _moderatorService = moderatorService;
        }


        public IActionResult Create()
        {
            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["failed"] = "Invalid inputs...";
                return View(model);
            }

            var create = await _moderatorService.Create(model);
            if (!create.Status)
            {
                TempData["failed"] = create.Message;
                //return RedirectToAction(nameof(Index));
                return View(model);
            }

            TempData["success"] = create.Message;
            return RedirectToAction("GetAllActive");

        }

        public async Task<IActionResult> Detail(string id)
        {
            var moderator = await _moderatorService.GetById(id);
            if (!moderator.Status)
            {
                TempData["failed"] = moderator.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["success"] = moderator.Message;
            return View(moderator);
        }


        public async Task<IActionResult> DynamicDetail(string id)
        {
            //var instructor = await _instructorService.GetById(id);
            var moderator = TempData["instructor"] as ModeratorResponseModel;
            if (moderator == null)
            {
                TempData["failed"] = moderator.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["success"] = moderator.Message;
            return View(moderator);
        }



        public async Task<IActionResult> GetByName(string name)
        {
            var moderator = await _moderatorService.GetByName(name);
            if (!moderator.Status)
            {
                TempData["failed"] = moderator.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            //TempData["instructor"] = instructor;
            TempData["success"] = moderator.Message;
            //return RedirectToAction(nameof(DynamicDetail));
            return View(moderator);
        }




        public async Task<IActionResult> GetByEmail(string email)
        {
            var moderator = await _moderatorService.GetByEmail(email);
            if (!moderator.Status)
            {
                TempData["failed"] = moderator.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            //TempData["instructor"] = instructor;
            TempData["success"] = moderator.Message;
            //return RedirectToAction(nameof(DynamicDetail));
            return View(moderator);
        }



        //public async Task<IActionResult> GetByEmail(string email)
        //{
        //    var instructor = await _instructorService.GetByEmail(email);
        //    if (!instructor.Status)
        //    {
        //        TempData["failed"] = instructor.Message;
        //        return RedirectToAction(nameof(Index), "Home");
        //    }

        //    TempData["instructor"] = instructor;
        //    TempData["success"] = instructor.Message;
        //    return RedirectToAction(nameof(DynamicDetail));

        //    //return View(instructor);
        //}



        public async Task<IActionResult> DeletePreview(string id)
        {
            var moderator = await _moderatorService.GetById(id);
            if (moderator.Status) return View(moderator);
            TempData["failed"] = moderator.Message;
            return RedirectToAction(nameof(Index), "Home");
        }


        public async Task<IActionResult> Delete(string id)
        {
            var moderator = await _moderatorService.Delete(id);
            if (moderator.Status)
            {
                TempData["success"] = moderator.Message;
                return RedirectToAction(nameof(GetAll));
            }

            TempData["failed"] = moderator.Message;
            return RedirectToAction(nameof(GetAll));
        }


        public async Task<IActionResult> GetAll()
        {
            var moderator = await _moderatorService.GetAll();
            if (!moderator.Status)
            {
                TempData["failed"] = moderator.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            TempData["success"] = moderator.Message;
            return View(moderator);
        }


        //public async Task<IActionResult> PaginatedSample(int page)
        //{
        //    // page = 1;
        //    int recordPerPage = 5;
        //    var instructors = await _instructorService.PaginatedSample();
        //    var pagedList = instructors.ToPagedList(page, recordPerPage);
        //    //if (!instructors.Status)
        //    //{
        //    //    TempData["failed"] = instructors.Message;
        //    //    return RedirectToAction(nameof(Index), "Home");
        //    //}

        //    //TempData["success"] = instructors.Message;
        //    return View(pagedList);
        //}



        //public async Task<IActionResult> customerDatatable(int page)
        //{

        //    var instructors = await _instructorService.PaginatedSample();

        //    return View(instructors);
        //}


        //public async Task<IActionResult> GetAllActive()
        //{
        //    var instructors = await _instructorService.GetAllActive();
        //    if (!instructors.Status)
        //    {
        //        TempData["failed"] = instructors.Message;
        //        return RedirectToAction(nameof(Index), "Home");
        //    }

        //    TempData["success"] = instructors.Message;
        //    return View(instructors);
        //}


        //public async Task<IActionResult> GetAllInActive()
        //{
        //    var instructors = await _instructorService.GetAllInActive();
        //    if (!instructors.Status)
        //    {
        //        TempData["failed"] = instructors.Message;
        //        return RedirectToAction(nameof(Index), "Home");
        //    }

        //    TempData["success"] = instructors.Message;
        //    return View(instructors);
        //}


        ////public async Task<IActionResult> Update(string id)
        ////{
        ////    var instructors = await _instructorService.GetById(id);
        ////    if (instructors.Status) return View(instructors);
        ////    TempData["failed"] = instructors.Message;
        ////    return RedirectToAction(nameof(Index), "Home");
        ////}


        ////[ValidateAntiForgeryToken]
        ////[HttpPost]
        ////public async Task<IActionResult> Update(UpdateInstructorProfileRequestModel model)
        ////{
        ////    if (!ModelState.IsValid)
        ////    {
        ////        TempData["failed"] = "Invalid detail...";

        ////        return View();
        ////    }

        ////    var update = await _instructorService.UpdateProfile(model);
        ////    if (update.Status)
        ////    {
        ////        TempData["success"] = update.Message;
        ////        return RedirectToAction(nameof(Index), "Home");
        ////    }

        ////    TempData["failed"] = update.Message;
        ////    return RedirectToAction(nameof(Index), "Home");
        ////}



        //public async Task<IActionResult> UpdateProfile(string id)
        //{
        //    var instructors = await _instructorService.GetById(id);
        //    if (instructors.Status) return View(instructors);
        //    TempData["failed"] = instructors.Message;
        //    return RedirectToAction(nameof(Index), "Home");
        //}


        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public async Task<IActionResult> UpdateProfile(UpdateInstructorProfileRequestModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        TempData["failed"] = "Invalid detail...";
        //        return View();
        //    }

        //    var updateProfile = await _instructorService.UpdateProfile(model);
        //    if (updateProfile.Status)
        //    {
        //        TempData["success"] = updateProfile.Message;
        //        return RedirectToAction(nameof(Index), "Home");
        //    }

        //    TempData["failed"] = updateProfile.Message;
        //    return RedirectToAction(nameof(Index), "Home");
        //}


        //public async Task<IActionResult> ListOfBankDetail(string id)
        //{
        //    var instructor = await _instructorService.GetListOfInstructorBankDetails(id);
        //    if (instructor.Status)
        //    {
        //        TempData["Success"] = instructor.Message;
        //        return View(instructor);
        //    }
        //    TempData["failed"] = instructor.Message;
        //    return RedirectToAction(nameof(Index), "Home");
        //    //TempData["success"] = admin.Message;
        //}


        //public async Task<IActionResult> UpdateBankDetail(string id)
        //{
        //    var admin = await _instructorService.GetByPaymentDetail(id);
        //    if (admin.Status) return View(admin);
        //    TempData["failed"] = admin.Message;
        //    return RedirectToAction(nameof(Index), "Home");
        //}


        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public async Task<IActionResult> UpdateBankDetail(UpdateInstructorBankDetailRequestModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        TempData["failed"] = "Invalid detail...";
        //        return View();
        //    }

        //    var admin = await _instructorService.UpdateBankDetail(model);

        //    return RedirectToAction(nameof(Index), "Home");
        //}

        //public IActionResult UpdateAddressDetail()
        //{
        //    throw new NotImplementedException();
        //}

    }
}
