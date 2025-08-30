using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.DataContext;
using RentACar.DataContext.Entities;
using RentACar.DataContext.Entities.AboutPage;

namespace RentACar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QualityTabItemsController : Controller
    {
        private readonly AppDbContext _context;

        public QualityTabItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/QualityTabItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.QualityTabs.ToListAsync());
        }

        // GET: Admin/QualityTabItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/QualityTabItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QualityTabItem item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Admin/QualityTabItems/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.QualityTabs.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: Admin/QualityTabItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, QualityTabItem item)
        {
            if (id != item.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.QualityTabs.Any(e => e.Id == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Admin/QualityTabItems/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.QualityTabs.FirstOrDefaultAsync(m => m.Id == id);
            if (item == null) return NotFound();

            return View(item);
        }

        // POST: Admin/QualityTabItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.QualityTabs.FindAsync(id);
            if (item != null)
            {
                _context.QualityTabs.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
