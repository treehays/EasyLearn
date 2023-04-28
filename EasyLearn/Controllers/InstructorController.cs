using EasyLearn.Models.DTOs.InstructorDTOs;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace EasyLearn.Controllers;

public class InstructorController : Controller
{
    private readonly IInstructorService _instructorService;
    private readonly INigerianBankService _nigerianBankService;
    //private readonly IPaymentDetailService _paymentDetailService;


    public InstructorController(IInstructorService instructorService, INigerianBankService nigerianBankService)
    {
        _instructorService = instructorService;
        _nigerianBankService = nigerianBankService;
        //_paymentDetailService = paymentDetailService;
    }

    public IActionResult Index()
    {
        return View();
    }


    public IActionResult RegisterInstructor()
    {
        return View();
    }


    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> RegisterInstructor(CreateUserRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["failed"] = "Invalid inputs...";
            return View(model);
        }

        var baseUrl = $"https://{Request.Host}";
        var registerAdmin = await _instructorService.InstructorRegistration(model, baseUrl);
        if (!registerAdmin.Status)
        {
            TempData["failed"] = registerAdmin.Message;
            return View(model);
        }

        TempData["success"] = registerAdmin.Message;
        return RedirectToAction("Login", "Home");
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


    public async Task<IActionResult> InstructorDetail(string id)
    {
        var instructor = await _instructorService.InstructorDetail(id);
        if (!instructor.Status)
        {
            TempData["failed"] = instructor.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = instructor.Message;
        return View(instructor);
    }


    //public async Task<IActionResult> DynamicDetail(string id)
    //{
    //    //var instructor = await _instructorService.GetById(id);
    //    var instructor = TempData["instructor"] as InstructorResponseModel;
    //    if (instructor == null)
    //    {
    //        TempData["failed"] = instructor.Message;
    //        return RedirectToAction(nameof(Index), "Home");
    //    }

    //    TempData["success"] = instructor.Message;
    //    return View(instructor);
    //}



    public async Task<IActionResult> GetByName(string name)
    {
        var instructor = await _instructorService.GetByName(name);
        if (!instructor.Status)
        {
            TempData["failed"] = instructor.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        TempData["success"] = instructor.Message;
        return View(instructor);
    }




    public async Task<IActionResult> GetByEmail(string email)
    {
        var instructor = await _instructorService.GetByEmail(email);
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
        return RedirectToAction(nameof(GetAllActive));
    }


    public async Task<IActionResult> Delete(string id)
    {
        var instructor = await _instructorService.Delete(id);
        if (instructor.Status)
        {
            TempData["success"] = instructor.Message;
            return RedirectToAction(nameof(GetAllActive));
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


    public async Task<IActionResult> PaginatedSample(int page)
    {
        // page = 1;
        int recordPerPage = 5;
        var instructors = await _instructorService.PaginatedSample();
        var pagedList = instructors.ToPagedList(page, recordPerPage);
        //if (!instructors.Status)
        //{
        //    TempData["failed"] = instructors.Message;
        //    return RedirectToAction(nameof(Index), "Home");
        //}

        //TempData["success"] = instructors.Message;
        return View(pagedList);
    }



    public async Task<IActionResult> customerDatatable(int page)
    {

        var instructors = await _instructorService.PaginatedSample();

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

    public async Task<IActionResult> UpdateProfile(string id)
    {
        var instructors = await _instructorService.GetById(id);
        if (instructors.Status) return View(instructors);
        TempData["failed"] = instructors.Message;
        return RedirectToAction(nameof(GetAllActive));
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


    public async Task<IActionResult> ListOfBankDetail(string id)
    {
        var instructor = await _instructorService.GetListOfInstructorBankDetails(id);
        if (instructor.Status)
        {
            TempData["Success"] = instructor.Message;
            return View(instructor);
        }
        TempData["failed"] = instructor.Message;
        return RedirectToAction(nameof(Index), "Home");

    }


    public async Task<IActionResult> UpdateBankDetail(string id)
    {
        var instructor = await _instructorService.GetByPaymentDetail(id);
        var listOfBanks = await _nigerianBankService.FetchAllNigerianBanks();

        ViewData["ListOfBanks"] = new SelectList(listOfBanks, "BankCode", "BankName");
        if (instructor.Status)
        {
            TempData["Success"] = instructor.Message;
            return View(instructor);
        }
        TempData["failed"] = instructor.Message;
        return RedirectToAction(nameof(GetAllActive));
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

        var instructor = await _instructorService.UpdateBankDetail(model);
        if (instructor.Status)
        {
            TempData["Success"] = instructor.Message;
            return View(instructor);
        }
        TempData["failed"] = instructor.Message;
        return RedirectToAction(nameof(GetAllActive));
    }



    public async Task<IActionResult> UpdateAddressDetail(string id)
    {
        var instructors = await _instructorService.GetFullDetailById(id);
        if (instructors.Status) return View(instructors);
        TempData["failed"] = instructors.Message;
        return RedirectToAction(nameof(GetAllActive));
    }




    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> UpdateAddressDetail(UpdateInstructorAddressRequestModel model)
    {
        var instructor = await _instructorService.UpdateAddress(model);
        if (instructor.Status)
        {
            TempData["Success"] = instructor.Message;
            return View(instructor);
        }
        TempData["failed"] = instructor.Message;
        return RedirectToAction(nameof(GetAllActive));
    }
}
