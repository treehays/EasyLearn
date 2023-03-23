using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Controllers
{
    public class InstructorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
