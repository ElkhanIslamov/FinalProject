using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.DataContext;
using RentACar.DataContext.Entities;

namespace RentACar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FooterLinksController : Controller
    {
        private readonly AppDbContext _context;

        public FooterLinksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/FooterLinks
        public async Task<IActionResult> Index()
        {
            var links = await _context.FooterLinks.ToListAsync();
            return View(links);
        }

        // GET: Admin/FooterLinks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/FooterLinks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FooterLink model)
        {
            if (ModelState.IsValid)
            {
                _context.FooterLinks.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Admin/FooterLinks/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var link = await _context.FooterLinks.FindAsync(id);
            if (link == null) return NotFound();

            return View(link);
        }

        // POST: Admin/FooterLinks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FooterLink model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Admin/FooterLinks/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var link = await _context.FooterLinks.FindAsync(id);
            if (link == null) return NotFound();

            return View(link);
        }

        // POST: Admin/FooterLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var link = await _context.FooterLinks.FindAsync(id);
            if (link != null)
            {
                _context.FooterLinks.Remove(link);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
