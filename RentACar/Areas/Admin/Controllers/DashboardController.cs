using Microsoft.AspNetCore.Mvc;
using RentACar.Areas.Admin.Data;
using RentACar.Areas.Admin.Models;

namespace RentACar.Areas.Admin.Controllers
{
    public class DashboardController : AdminController
    {
     
        public IActionResult Index()
        {
            // Burada real verilənlər bazasından məlumatları çəkməlisiniz.
            // Sadəcə nümunə üçün statik data istifadə olunur.

            var model = new DashboardViewModel
            {
                TotalCars = 120,
                RentedCars = 45,
                TodaysBookings = 10,
                TotalRevenue = 35000m,
                RecentBookings = new List<BookingInfo>
        {
            new BookingInfo { CustomerName="Elxan", CarName="Toyota Camry", BookingDate=DateTime.Today.AddDays(-2), ReturnDate=DateTime.Today.AddDays(3), Price=300, Status="Active" },
            new BookingInfo { CustomerName="Aysel", CarName="BMW X5", BookingDate=DateTime.Today.AddDays(-5), ReturnDate=DateTime.Today.AddDays(-1), Price=500, Status="Completed" },
            new BookingInfo { CustomerName="Ramin", CarName="Audi A6", BookingDate=DateTime.Today, ReturnDate=DateTime.Today.AddDays(2), Price=400, Status="Active" },
        }
            };

            return View(model);
        }

    }
}
