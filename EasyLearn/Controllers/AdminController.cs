using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Controllers;

public class AdminController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create()
    {
        return View();
    }
}