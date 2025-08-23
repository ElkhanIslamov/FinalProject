using Microsoft.AspNetCore.Mvc;
using RentACar.DataContext;
using RentACar.DataContext.Entities;
using RentACar.Areas.Admin.Models;

namespace RentACar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeFeatureSectionController : Controller
    {
        private readonly AppDbContext _context;

        public HomeFeatureSectionController(AppDbContext context)
        {
            _context = context;
        }

        // INDEX
        public IActionResult Index()
        {
            var sections = _context.HomeFeatureSections
                .Select(s => new HomeFeatureSectionViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description
                })
                .ToList();

            return View(sections);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View(new HomeFeatureSectionViewModel());
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HomeFeatureSectionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new HomeFeatureSection
                {
                    Title = model.Title,
                    Description = model.Description
                };

                _context.HomeFeatureSections.Add(entity);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // EDIT GET
        public IActionResult Edit(int id)
        {
            var section = _context.HomeFeatureSections.Find(id);
            if (section == null) return NotFound();

            var model = new HomeFeatureSectionViewModel
            {
                Id = section.Id,
                Title = section.Title,
                Description = section.Description
            };

            return View(model);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HomeFeatureSectionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _context.HomeFeatureSections.Find(model.Id);
                if (entity == null) return NotFound();

                entity.Title = model.Title;
                entity.Description = model.Description;

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var section = _context.HomeFeatureSections.Find(id);
            if (section == null) return NotFound();

            _context.HomeFeatureSections.Remove(section);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
