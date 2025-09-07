using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Areas.Admin.Models;
using RentACar.DataContext;

namespace RentACar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _dbContext;

        public DashboardController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            // Ümumi maşın sayı
            var totalCars = await _dbContext.Cars.CountAsync();

            // Ümumi bookinglərin sayı
            var rentedCars = await _dbContext.Bookings.CountAsync();

            // Bugünkü rezervasiyalar
            var todaysBookings = await _dbContext.Bookings
                .CountAsync(b => b.PickupDate.Date == DateTime.Today);

            // Ümumi gəlir (icarə günləri * PricePerDay)
            var totalRevenue = await _dbContext.Bookings
                .Where(b => b.Car != null)
                .SumAsync(b =>
                    (EF.Functions.DateDiffDay(b.PickupDate, b.ReturnDate) <= 0 ? 1 :
                     EF.Functions.DateDiffDay(b.PickupDate, b.ReturnDate))
                     * b.Car.PricePerDay
                );

            // Son 5 rezervasiya
            var recentBookings = await _dbContext.Bookings
                .Include(b => b.Car)
                .OrderByDescending(b => b.CreatedAt)
                .Take(5)
                .Select(b => new RecentBookingViewModel
                {
                    CustomerName = b.CustomerName,
                    CarName = b.Car != null ? b.Car.Name : b.CarType,
                    BookingDate = b.PickupDate,
                    ReturnDate = b.ReturnDate,
                    Price = b.Car != null
                        ? ((EF.Functions.DateDiffDay(b.PickupDate, b.ReturnDate) <= 0 ? 1 :
                            EF.Functions.DateDiffDay(b.PickupDate, b.ReturnDate))
                           * b.Car.PricePerDay)
                        : 0,
                    Status = b.Status
                })
                .ToListAsync();

            var viewModel = new DashboardViewModel
            {
                TotalCars = totalCars,
                RentedCars = rentedCars,     // bütün bookinglərin sayı
                TodaysBookings = todaysBookings,
                TotalRevenue = totalRevenue, // qiymətlərin cəmi
                RecentBookings = recentBookings
            };

            return View(viewModel);
        }


    }
}
