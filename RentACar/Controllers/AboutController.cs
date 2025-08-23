using Microsoft.AspNetCore.Mvc;

namespace RentACar.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
