using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.DataContext; // Sənin DbContext namespace-inə uyğun düzəlt
using RentACar.DataContext.Entities; // Car entity üçün namespace
using RentACar.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace RentACar.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly AppDbContext _context;
        private const string FavoritesSessionKey = "Favorites";

        public FavoritesController(AppDbContext context)
        {
            _context = context;
        }

        // Seçilmişlər səhifəsi
        public IActionResult Index()
        {
            var favorites = HttpContext.Session.GetObjectFromJson<List<int>>(FavoritesSessionKey) ?? new List<int>();
            var cars = _context.Cars
                               .Where(c => favorites.Contains(c.Id))
                               .ToList();

            return View(cars);
        }

        // Like düyməsinə klik zamanı çağırılacaq
        [HttpPost]
        public IActionResult Toggle(int id)
        {
            var favorites = HttpContext.Session.GetObjectFromJson<List<int>>(FavoritesSessionKey) ?? new List<int>();

            if (favorites.Contains(id))
            {
                favorites.Remove(id);
            }
            else
            {
                favorites.Add(id);
            }

            HttpContext.Session.SetObjectAsJson(FavoritesSessionKey, favorites);

            return Json(new { success = true, count = favorites.Count });
        }
    }
}
