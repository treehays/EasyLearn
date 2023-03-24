using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Controllers
{
    public class ModeratorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
