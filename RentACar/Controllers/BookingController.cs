using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RentACar.DataContext;
using RentACar.DataContext.Entities;
using RentACar.Models;
using Stripe;
using Stripe.Checkout;

namespace RentACar.Controllers
{
    public class BookingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly StripeSettings _stripeSettings;

        public BookingController(
            AppDbContext context,
            UserManager<AppUser> userManager,
            IOptions<StripeSettings> stripeOptions)
        {
            _context = context;
            _userManager = userManager;
            _stripeSettings = stripeOptions.Value; // Stripe config burda set olunur
        }

        // GET: QuickBooking
        [HttpGet]
        public async Task<IActionResult> QuickBooking(int? carId, string? carType)
        {
            var subHeader = await _context.BookingSubHeaders.FirstOrDefaultAsync();

            var model = new BookingViewModel
            {                
                CarId = carId,
                CarType = carType ?? string.Empty,
                SubHeader = subHeader ?? new BookingSubHeader()
            };

            return View(model);
        }

        // POST: QuickBooking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuickBooking(BookingViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            var car = await _context.Cars.FindAsync(model.CarId);
            if (car == null)
                return NotFound();

            var booking = new Booking
            {
                CarId = car.Id,
                CarType = car.Name,
                UserId = user.Id,
                PickupDate = model.PickupDate,
                ReturnDate = model.ReturnDate,
                PickupTime = model.PickupTime,
                ReturnTime = model.ReturnTime,
                PickupLocation = model.PickupLocation,
                DropoffLocation = model.DropoffLocation,
                CustomerName = model.CustomerName,
                Email = model.Email,
                Phone = model.Phone,
                CreatedAt = DateTime.Now,
                Description = ""
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return RedirectToAction("Checkout", new { bookingId = booking.Id });
        }

        // GET: Checkout
        [HttpGet]
        public async Task<IActionResult> Checkout(int bookingId)
        {
            var booking = await _context.Bookings
                .Include(b => b.Car)
                .FirstOrDefaultAsync(b => b.Id == bookingId);

            if (booking == null)
                return NotFound();

            var days = (booking.ReturnDate - booking.PickupDate).Days;
            if (days == 0) days = 1;

            var totalPrice = days * (booking.Car?.PricePerDay ?? 0);

            var checkoutVm = new CheckoutViewModel
            {
                BookingId = booking.Id,
                CarType = booking.CarType,
                TotalPrice = totalPrice
            };

            return View(checkoutVm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCheckoutSession(int bookingId)
        {
            var booking = await _context.Bookings
                .Include(b => b.Car)
                .FirstOrDefaultAsync(b => b.Id == bookingId);

            if (booking == null)
                return NotFound();

            // ümumi qiyməti hesablamaq
            var days = (booking.ReturnDate - booking.PickupDate).Days;
            if (days <= 0) days = 1;
            var totalPrice = booking.Car.PricePerDay * days;

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
        {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(totalPrice * 100), // Stripe sentlə
                    Currency = "azn",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = booking.Car?.Name ?? "Car Booking"
                    }
                },
                Quantity = 1,
            }
        },
                Mode = "payment",
                SuccessUrl = Url.Action("Success", "Booking", new { bookingId = booking.Id }, Request.Scheme)!,
                CancelUrl = Url.Action("Cancel", "Booking", null, Request.Scheme)!,
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Redirect(session.Url);
        }

        public IActionResult Success(int bookingId)
        {
            // burada DB-də booking-in statusunu "Paid" edə bilərsən
            // booking.PaymentStatus = "Paid";  await _dbContext.SaveChangesAsync();
            return View();
        }

        public IActionResult Cancel()
        {
            return View();
        }       
    }
}
