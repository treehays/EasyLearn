using EasyLearn.Data;
using EasyLearn.Models;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Security.Claims;

namespace EasyLearn.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserService _userService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly CompanyInfoOption _companyInfoOption;


    public HomeController(ILogger<HomeController> logger, IUserService userService, IOptions<CompanyInfoOption> companyInfoOption, IWebHostEnvironment webHostEnvironment)
    {
        _logger = logger;
        _userService = userService;
        _companyInfoOption = companyInfoOption.Value;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
        {
        ////var ff = new VerifyTransactionRequestModel { ReferenceNumber = "5cb18b3ay6e0ay442dyb89dy17e78e7596b0", };
        ////var pp = await _payStackService.VerifyTransaction(ff);
        //var sdnnm = _companyInfoOption.CompanyName;
        //var sdnnmew = _companyInfoOption.CompanyName;

        //var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "CsvFolder");
        //if (!Directory.Exists(filePath))
        //{
        //    Directory.CreateDirectory(filePath);
        //}
        //var fileName = "test.csv";
        //var fullPath = Path.Combine(filePath, fileName);

        //var readFromCsv = new List<ReadFromCsvFile>();
        //using (var reader = new StreamReader(fullPath))
        //using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        //{
        //    var records = csv.GetRecords<ReadFromCsvFile>();
        //    readFromCsv = records.ToList();
        //}
        //var m = readFromCsv;


        //var writeToCsv = new List<WriteToCsvFile>
        //{

        //    new WriteToCsvFile
        //    {
        //        Description = Guid.NewGuid().ToString(),
        //        Objective= Guid.NewGuid().ToString(),
        //         Prerequisites= Guid.NewGuid().ToString(),
        //         Resources= Guid.NewGuid().ToString(),
        //         Title= Guid.NewGuid().ToString(),
        //         VideoName = Guid.NewGuid().ToString() ,
        //    },

        //    new WriteToCsvFile
        //    {
        //        Description = Guid.NewGuid().ToString()
        //    },

        //    new WriteToCsvFile
        //    {
        //        Description = Guid.NewGuid().ToString(),

        //    },

        //    new WriteToCsvFile
        //    {
        //        Description = Guid.NewGuid().ToString(),

        //    },

        //};

        //var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "CsvFolder");
        //if (!Directory.Exists(filePath))
        //{
        //    Directory.CreateDirectory(filePath);
        //}
        //var fileName = $"{Guid.NewGuid().ToString().Replace('-', 's')}.csv";
        //var fullPath = Path.Combine(filePath, fileName);

        //using (var writer = new StreamWriter(fullPath))
        //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        //{
        //    csv.WriteRecords(writeToCsv);
        //}



        return View();
    }
    //[Route("v{version:apiVersion}/[controller]")]
    //[Route("{Login}")]
    public IActionResult Login()
    {
        //var baseUrl = $"https://{Request.Host}";
        //var c = $"https://{Request.Host.Value}";
        //var d = $"https://{Request.GetEncodedPathAndQuery}";
        //var a = Request.GetDisplayUrl();
        //var b = Request.PathBase.ToString();
        //// ViewData["Title"] = "Light Sidebar";
        //// ViewData["pTitle"] = "Light Sidebar";

        //var model = new InitializePaymentRequestModel
        //{
        //    CallbackUrl = baseUrl,
        //    CoursePrice = 44000,
        //    Email = "treehays90@gmail.com",
        //};
        //var initializee = await _payStackService.InitializePayment(model);


        //var model = new VerifyAccountNumberRequestModel
        //{
        //    AccountNumber = "5004279517",
        //    BankCode = "058",
        //};
        //var varifyaccout = await _payStackService.VerifyAccountNumber(model);


        //var model = new CreateTransferRecipientRequestModel
        //{
        //    AccountNumber = "0159192507",
        //    BankCode="058",
        //    Description = "Testing payment",
        //    Name = "Abdulsalam Ahmad Ayoola",

        //};
        //var createrec = await _payStackService.CreateTransferRecipient(model);
        //var trans = await _payStackService.TransferMoneyToUser(createrec);

        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("index");
        }
        return View();
    }

    [HttpPost]
    // [Route("{Account/login}")]
    //[Route("{Login}")]
    public async Task<IActionResult> Login(LoginRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var user = await _userService.Login(model);

        if (!user.Status)
        {
            TempData["failed"] = user.Message;
            return View();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role,user.RoleId.ToLower()),
            new Claim(ClaimTypes.NameIdentifier,user.UserId),
            new Claim(ClaimTypes.Actor, user.Id),
            new Claim(ClaimTypes.Name,user.FirstName),
            new Claim(ClaimTypes.UserData,user.ProfilePicture),
            new Claim(ClaimTypes.Email,model.Email),
        };
        var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authenticationProperties = new AuthenticationProperties();
        var principal = new ClaimsPrincipal(claimIdentity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

        if (user.RoleId == "Admin")
        {
            TempData["success"] = "Login successful";
            return RedirectToAction("Index", "Admin");
        }
        else if (user.RoleId == "Instructor")
        {
            TempData["success"] = "Login successful";
            return RedirectToAction("Index", "Instructor");
        }
        else if (user.RoleId == "Moderator")
        {
            TempData["success"] = "Login successful";
            return RedirectToAction("index", "Moderator");
        }
        else if (user.RoleId == "Student")
        {
            TempData["success"] = "Login successful";
            return RedirectToAction("index", "course");
        }
        else
        {
            //var user = 
            TempData["success"] = "Account has not been activated";
            return RedirectToAction("Index", "Home");
        }
    }


    // [Route("Account/login")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}