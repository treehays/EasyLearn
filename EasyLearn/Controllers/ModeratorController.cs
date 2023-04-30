using EasyLearn.Models.DTOs.ModeratorDTOs;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Controllers;

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


    public IActionResult RegisterModerator()
    {
        return View();
    }


    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> RegisterModerator(CreateUserRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["failed"] = "Invalid inputs...";
            return View(model);
        }
        var baseUrl = $"https://{Request.Host}";

        var create = await _moderatorService.ModeratorRegistration(model, baseUrl);
        if (!create.Status)
        {
            TempData["failed"] = create.Message;
            //return RedirectToAction(nameof(Index));
            return View(model);
        }

        TempData["success"] = create.Message;
        return RedirectToAction("Login", "Home");

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


    //public async Task<IActionResult> DynamicDetail(string id)
    //{
    //    //var instructor = await _instructorService.GetById(id);
    //    var moderator = TempData["instructor"] as ModeratorResponseModel;
    //    if (moderator == null)
    //    {
    //        TempData["failed"] = moderator.Message;
    //        return RedirectToAction(nameof(Index), "Home");
    //    }

    //    TempData["success"] = moderator.Message;
    //    return View(moderator);
    //}



    public async Task<IActionResult> GetByName(string name)
    {
        var moderator = await _moderatorService.GetByName(name);
        if (!moderator.Status)
        {
            TempData["failed"] = moderator.Message;
            return RedirectToAction(nameof(GetAllActiveModerators));
        }

        TempData["success"] = moderator.Message;
        return View(moderator);
    }




    public async Task<IActionResult> GetAllActiveModerators()
    {
        var moderator = await _moderatorService.GetAllActive();
        if (!moderator.Status)
        {
            TempData["failed"] = moderator.Message;
            return View();
        }

        TempData["success"] = moderator.Message;
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

        TempData["success"] = moderator.Message;
        return View(moderator);
    }


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



    //public async Task<IActionResult> customerDatatable(int page)
    //{

    //    var instructors = await _instructorService.PaginatedSample();

    //    return View(instructors);
    //}



    public async Task<IActionResult> GetAllInActive()
    {
        var moderator = await _moderatorService.GetAllInActive();
        if (!moderator.Status)
        {
            TempData["failed"] = moderator.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = moderator.Message;
        return View(moderator);
    }


    public async Task<IActionResult> UpdateProfile(string id)
    {
        var moderator = await _moderatorService.GetById(id);
        if (moderator.Status) return View(moderator);
        TempData["failed"] = moderator.Message;
        return RedirectToAction(nameof(Index), "Home");
    }


    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> UpdateProfile(UpdateModeratorProfileRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["failed"] = "Invalid detail...";

            return View();
        }

        var moderator = await _moderatorService.UpdateProfile(model);
        if (moderator.Status)
        {
            TempData["success"] = moderator.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["failed"] = moderator.Message;
        return RedirectToAction(nameof(Index), "Home");
    }




    public async Task<IActionResult> ListOfBankDetail(string id)
    {
        var moderator = await _moderatorService.GetListOfModeratorBankDetails(id);
        if (moderator.Status)
        {
            TempData["Success"] = moderator.Message;
            return View(moderator);
        }
        TempData["failed"] = moderator.Message;
        return RedirectToAction(nameof(Index), "Home");
        //TempData["success"] = admin.Message;
    }


    public async Task<IActionResult> UpdateBankDetail(string id)
    {
        var moderator = await _moderatorService.GetByPaymentDetail(id);
        if (moderator.Status) return View(moderator);
        TempData["failed"] = moderator.Message;
        return RedirectToAction(nameof(Index), "Home");
    }


    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> UpdateBankDetail(UpdateModeratorBankDetailRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["failed"] = "Invalid detail...";
            return View();
        }

        var admin = await _moderatorService.UpdateBankDetail(model);

        return RedirectToAction(nameof(Index), "Home");
    }

    public IActionResult UpdateAddressDetail()
    {
        throw new NotImplementedException();
    }

}
