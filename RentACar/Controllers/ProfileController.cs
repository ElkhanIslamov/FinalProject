using Microsoft.AspNetCore.Mvc;

namespace RentACar.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
