using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.DataContext;
using RentACar.DataContext.Entities;

namespace RentACar.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public FavoritesController(AppDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var favorites = await _dbContext.FavoriteCars
                .Include(f => f.Car)
                .Where(f => f.UserId == user.Id)
                .ToListAsync();

            return View(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int carId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var exists = await _dbContext.FavoriteCars
                .AnyAsync(f => f.CarId == carId && f.UserId == user.Id);

            if (!exists)
            {
                var favorite = new FavoriteCar
                {
                    CarId = carId,
                    UserId = user.Id
                };

                _dbContext.FavoriteCars.Add(favorite);
                await _dbContext.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int carId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var favorite = await _dbContext.FavoriteCars
                .FirstOrDefaultAsync(f => f.CarId == carId && f.UserId == user.Id);

            if (favorite != null)
            {
                _dbContext.FavoriteCars.Remove(favorite);
                await _dbContext.SaveChangesAsync();
            }

            return Ok();
        }
    }
}
