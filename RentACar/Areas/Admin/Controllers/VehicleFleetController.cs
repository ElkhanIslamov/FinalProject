using Microsoft.AspNetCore.Mvc;
using RentACar.DataContext;
using RentACar.DataContext.Entities;
using RentACar.Areas.Admin.Models;

namespace RentACar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VehicleFleetController : Controller
    {
        private readonly AppDbContext _context;

        public VehicleFleetController(AppDbContext context)
        {
            _context = context;
        }

        // Index (hamısını göstərir)
        public IActionResult Index()
        {
            var list = _context.VehicleFleetSettings
                .Select(x => new VehicleFleetViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description
                }).ToList();

            return View(list); // -> BURADA List qaytarırsan
        }


        // GET: Create
        public IActionResult Create()
        {
            return View(new VehicleFleetViewModel());
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VehicleFleetViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var entity = new VehicleFleetSetting
            {
                Title = model.Title,
                Description = model.Description
            };

            _context.VehicleFleetSettings.Add(entity);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Edit
        public IActionResult Edit(int id)
        {
            var entity = _context.VehicleFleetSettings.Find(id);
            if (entity == null) return NotFound();

            var model = new VehicleFleetViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description
            };

            return View(model);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(VehicleFleetViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var entity = _context.VehicleFleetSettings.Find(model.Id);
            if (entity == null) return NotFound();

            entity.Title = model.Title;
            entity.Description = model.Description;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var entity = _context.VehicleFleetSettings.Find(id);
            if (entity == null) return NotFound();

            var vm = new VehicleFleetViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description
            };

            return View(vm); // Confirm səhifəsinə göndəririk
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var entity = _context.VehicleFleetSettings.Find(id);
            if (entity == null) return NotFound();

            _context.VehicleFleetSettings.Remove(entity);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
