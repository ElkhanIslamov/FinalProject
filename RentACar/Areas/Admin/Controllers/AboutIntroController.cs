using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.DataContext;
using RentACar.DataContext.Entities.AboutPage;

namespace RentACar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutIntroController : Controller
    {
        private readonly AppDbContext _context;

        public AboutIntroController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AboutIntro
        public async Task<IActionResult> Index()
        {
            var intro = await _context.AboutIntros.ToListAsync();
            return View(intro);
        }

        // GET: Admin/AboutIntro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AboutIntro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutIntro model)
        {
            if (!ModelState.IsValid) return View(model);

            _context.AboutIntros.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/AboutIntro/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var intro = await _context.AboutIntros.FindAsync(id);
            if (intro == null) return NotFound();

            return View(intro);
        }

        // POST: Admin/AboutIntro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AboutIntro model)
        {
            if (!ModelState.IsValid) return View(model);

            var intro = await _context.AboutIntros.FindAsync(model.Id);
            if (intro == null) return NotFound();

            // yalnız lazımlı sahələri güncəlləyirik
            intro.Title = model.Title;
            intro.Description = model.Description;
            intro.CompletedOrdersLabel = model.CompletedOrdersLabel;
            intro.CompletedOrdersValue = model.CompletedOrdersValue;
            intro.HappyCustomersLabel = model.HappyCustomersLabel;
            intro.HappyCustomersValue = model.HappyCustomersValue;
            intro.VehiclesFleetLabel = model.VehiclesFleetLabel;
            intro.VehiclesFleetValue = model.VehiclesFleetValue;
            intro.YearsExperienceLabel = model.YearsExperienceLabel;
            intro.YearsExperienceValue = model.YearsExperienceValue;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/AboutIntro/Delete/5       
        public async Task<IActionResult> Delete(int id)
        {
            var intro = await _context.AboutIntros.FindAsync(id);
            if (intro == null) return NotFound();

            return View(intro); // Delete.cshtml səhifəsini göstər
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var intro = await _context.AboutIntros.FindAsync(id);
            if (intro == null) return NotFound();

            _context.AboutIntros.Remove(intro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
