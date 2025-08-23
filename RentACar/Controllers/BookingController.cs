using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.DataContext;
using RentACar.DataContext.Entities;
using RentACar.Models;
using Stripe.Checkout;

namespace RentACar.Controllers;

[Authorize]
public class BookingController : Controller
{
    private readonly AppDbContext _context;

    public BookingController(AppDbContext context)
    {
        _context = context;
    }

    // ------------------------
    // QuickBooking GET
    // ------------------------
    [HttpGet]
    public async Task<IActionResult> QuickBooking(int? carId)
    {
        var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == carId);
        if (car == null) return NotFound();

        var vm = new BookingViewModel
        {
            CarId = car.Id,
            CarType = car.Name,
            CustomerName = User.Identity?.Name ?? "Guest",
            Email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? "",
            Phone = User.FindFirst(System.Security.Claims.ClaimTypes.MobilePhone)?.Value ?? "N/A",
            PickupLocation = "Default Pickup Location",
            DropoffLocation = "Default Dropoff Location",
            PickupDate = DateTime.UtcNow,
            ReturnDate = DateTime.UtcNow.AddDays(1)
        };

        return View(vm);
    }

    // ------------------------
    // QuickBooking POST
    // ------------------------
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> QuickBooking(BookingViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var booking = new Booking
        {
            CarId = model.CarId,
            CarType = model.CarType,
            CustomerName = model.CustomerName,
            Email = model.Email,
            Phone = model.Phone,
            PickupLocation = model.PickupLocation,
            DropoffLocation = model.DropoffLocation,
            PickupDate = model.PickupDate,
            PickupTime = model.PickupTime,
            ReturnDate = model.ReturnDate,
            ReturnTime = model.ReturnTime,
            Description = model.Message ?? "No description", // <- default dəyər
            Status = "Pending",
            CreatedAt = DateTime.Now,
            UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        // Checkout səhifəsinə yönləndir
        return RedirectToAction("Checkout", new { bookingId = booking.Id });
    }

    // ------------------------
    // Checkout GET
    // ------------------------
    [HttpGet]
    public async Task<IActionResult> Checkout(int bookingId)
    {
        var booking = await _context.Bookings.Include(b => b.Car)
            .FirstOrDefaultAsync(b => b.Id == bookingId);

        if (booking == null) return NotFound();

        int days = Math.Max(1, (booking.ReturnDate.Date - booking.PickupDate.Date).Days);
        decimal totalPrice = booking.Car != null ? booking.Car.PricePerDay * days : 0;

        var vm = new CheckoutViewModel
        {
            BookingId = booking.Id,
            CarType = booking.CarType,
            TotalPrice = totalPrice
        };

        return View(vm);
    }

    // ------------------------
    // Stripe Hosted Checkout POST
    // ------------------------
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCheckoutSession(int bookingId)
    {
        var booking = await _context.Bookings.Include(b => b.Car)
            .FirstOrDefaultAsync(b => b.Id == bookingId);
        if (booking == null) return NotFound();

        int days = Math.Max(1, (booking.ReturnDate.Date - booking.PickupDate.Date).Days);
        decimal totalPrice = booking.Car != null ? booking.Car.PricePerDay * days : 0;

        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = totalPrice * 100, // qəpiklə
                        Currency = "azn",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = booking.CarType
                        }
                    },
                    Quantity = 1
                }
            },
            Mode = "payment",
            SuccessUrl = Url.Action("Confirmation", "Booking", new { id = booking.Id }, Request.Scheme),
            CancelUrl = Url.Action("Checkout", "Booking", new { bookingId = booking.Id }, Request.Scheme)
        };

        var service = new SessionService();
        Session session = service.Create(options);

        return Redirect(session.Url); // Stripe Hosted Checkout açılır
    }

    // ------------------------
    // Confirmation
    // ------------------------
    [HttpGet]
    public async Task<IActionResult> Confirmation(int id)
    {
        var booking = await _context.Bookings.Include(b => b.Car)
            .FirstOrDefaultAsync(b => b.Id == id);
        if (booking == null) return NotFound();

        booking.Status = "Paid";
        await _context.SaveChangesAsync();

        return View(booking);
    }
}
