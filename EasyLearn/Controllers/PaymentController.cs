using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Controllers;

public class PaymentController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
