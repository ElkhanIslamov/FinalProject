using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.DataContext;
using RentACar.Models;
using RentACar.Extensions; // Session üçün lazımdır
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.Controllers
{
    public class CarController : Controller
    {
        private readonly AppDbContext _dbContext;
        private const string FavoritesSessionKey = "Favorites";

        public CarController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            var cars = await _dbContext.Cars
                                       .Include(c => c.Category)
                                       .ToListAsync();

            // ⭐ Session-dan favoritləri götür
            var favorites = HttpContext.Session.GetObjectFromJson<List<int>>(FavoritesSessionKey) ?? new List<int>();

            // ⭐ Favoritləri işarələ
            foreach (var car in cars)
            {
                car.IsFavorite = favorites.Contains(car.Id);
            }

            decimal minPrice = 0;
            decimal maxPrice = 0;

            if (cars.Count > 0)
            {
                minPrice = cars.Min(c => c.PricePerDay);
                maxPrice = cars.Max(c => c.PricePerDay);
            }

            var model = new CarsViewModel
            {
                Categories = categories,
                Cars = cars,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Filter([FromBody] CarFilterRequest filter)
        {
            var query = _dbContext.Cars
                .Include(c => c.Category)
                .AsQueryable();

            if (filter.BodyTypes != null && filter.BodyTypes.Any())
            {
                query = query.Where(c => filter.BodyTypes.Contains(c.CategoryId));
            }

            if (!string.IsNullOrEmpty(filter.SearchKeyword))
            {
                var keyword = filter.SearchKeyword.ToLower();
                query = query.Where(c =>
                    c.Name.ToLower().Contains(keyword) ||
                    c.Category.Name.ToLower().Contains(keyword)
                );
            }

            query = query.Where(c =>
                c.PricePerDay >= filter.MinPrice &&
                c.PricePerDay <= filter.MaxPrice);

            var filteredCars = await query.ToListAsync();

            // ⭐ Filter olunanda da favoritləri işarələ
            var favorites = HttpContext.Session.GetObjectFromJson<List<int>>(FavoritesSessionKey) ?? new List<int>();
            foreach (var car in filteredCars)
            {
                car.IsFavorite = favorites.Contains(car.Id);
            }

            return PartialView("_CarCardsPartial", filteredCars);
        }

        public async Task<IActionResult> Details(int id)
        {
            var car = await _dbContext.Cars
                .Include(c => c.Images)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null) return NotFound();

            var viewModel = new CarDetailsViewModel
            {
                Name = car.Name,
                ImageUrl = car.ImageUrl,
                PricePerDay = car.PricePerDay,
                Description = car.Description,
                AdditionalImageUrls = car.Images?.Select(img => img.ImageUrl).ToList() ?? new List<string>()
            };

            return View(viewModel);
        }
    }
}
