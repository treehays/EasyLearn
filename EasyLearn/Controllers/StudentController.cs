using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
