using Microsoft.AspNetCore.Mvc;
using RentACar.Areas.Admin.Models;
using RentACar.DataContext;
using RentACar.DataContext.Entities;

namespace RentACar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeFeatureController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeFeatureController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // INDEX
        public IActionResult Index()
        {
            var features = _context.HomeFeatures.ToList();
            return View(features);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View(new HomeFeatureViewModel());
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HomeFeatureViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            string? imageUrl = null;
            if (model.ImageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                var path = Path.Combine(_env.WebRootPath, "uploads/features", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                using var stream = new FileStream(path, FileMode.Create);
                model.ImageFile.CopyTo(stream);
                imageUrl = "/uploads/features/" + fileName;
            }

            var feature = new HomeFeature
            {
                Title = model.Title,
                Description = model.Description,
                Icon = model.Icon,
                ImageUrl = imageUrl
            };

            _context.HomeFeatures.Add(feature);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // EDIT GET
        public IActionResult Edit(int id)
        {
            var feature = _context.HomeFeatures.Find(id);
            if (feature == null) return NotFound();

            var model = new HomeFeatureViewModel
            {
                Id = feature.Id,
                Title = feature.Title,
                Description = feature.Description,
                Icon = feature.Icon,
                ExistingImageUrl = feature.ImageUrl
            };

            return View(model);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HomeFeatureViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var feature = _context.HomeFeatures.Find(model.Id);
            if (feature == null) return NotFound();

            feature.Title = model.Title;
            feature.Description = model.Description;
            feature.Icon = model.Icon;

            if (model.ImageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                var path = Path.Combine(_env.WebRootPath, "uploads/features", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                using var stream = new FileStream(path, FileMode.Create);
                model.ImageFile.CopyTo(stream);
                feature.ImageUrl = "/uploads/features/" + fileName;
            }

            _context.HomeFeatures.Update(feature);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var feature = _context.HomeFeatures.Find(id);
            if (feature != null)
            {
                _context.HomeFeatures.Remove(feature);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
