using Microsoft.AspNetCore.Mvc;
using RentACar.DataContext;
using RentACar.DataContext.Entities;

namespace RentACar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeFaqController : Controller
    {
        private readonly AppDbContext _context;

        public HomeFaqController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var faqs = _context.HomeFaqs.ToList();
            return View(faqs);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(HomeFaq faq)
        {
            if (ModelState.IsValid)
            {
                _context.HomeFaqs.Add(faq);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(faq);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var faq = _context.HomeFaqs.Find(id);
            if (faq == null) return NotFound();
            return View(faq);
        }

        [HttpPost]
        public IActionResult Edit(HomeFaq faq)
        {
            if (ModelState.IsValid)
            {
                _context.HomeFaqs.Update(faq);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(faq);
        }

        public IActionResult Delete(int id)
        {
            var faq = _context.HomeFaqs.Find(id);
            if (faq == null) return NotFound();

            _context.HomeFaqs.Remove(faq);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
