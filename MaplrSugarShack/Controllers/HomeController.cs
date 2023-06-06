using Microsoft.AspNetCore.Mvc;

namespace MaplrSugarSnack.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult About()
        {
            ViewBag.Title = "About";
            return View();
        }
    }
}
