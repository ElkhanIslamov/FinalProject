using Microsoft.AspNetCore.Mvc;
using RentACar.DataContext;
using RentACar.DataContext.Entities;
using RentACar.Models;
using Microsoft.AspNetCore.Identity;

namespace RentACar.Controllers
{
    public class BookingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BookingController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Booking/QuickBooking
        [HttpGet]
        public IActionResult QuickBooking(int? carId, string? carType)
        {
            var model = new BookingViewModel
            {
                CarType = carType ?? string.Empty // Ensure required property is set
            };
            if (carId.HasValue)
            {
                model.CarId = carId.Value;
            }
            return View(model);
        }
    }
}
