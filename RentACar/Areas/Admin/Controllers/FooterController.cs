using Microsoft.AspNetCore.Mvc;
using RentACar.DataContext;
using RentACar.DataContext.Entities;
using System.Linq;

namespace RentACar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FooterController : Controller
    {
        private readonly AppDbContext _context;

        public FooterController(AppDbContext context)
        {
            _context = context;
        }

        // READ - Index
        public IActionResult Index()
        {
            var footers = _context.FooterInfos.ToList();
            return View(footers);
        }

        // CREATE - GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FooterInfo model)
        {
            if (ModelState.IsValid)
            {
                _context.FooterInfos.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // EDIT - GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var footer = _context.FooterInfos.Find(id);
            if (footer == null) return NotFound();

            return View(footer);
        }

        // EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FooterInfo model)
        {
            if (ModelState.IsValid)
            {
                _context.FooterInfos.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // DELETE - GET
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var footer = _context.FooterInfos.Find(id);
            if (footer == null) return NotFound();

            return View(footer);
        }

        // DELETE - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var footer = _context.FooterInfos.Find(id);
            if (footer == null) return NotFound();

            _context.FooterInfos.Remove(footer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
