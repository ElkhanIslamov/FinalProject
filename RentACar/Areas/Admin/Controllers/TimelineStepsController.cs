using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Areas.Admin.Models;
using RentACar.DataContext;
using RentACar.DataContext.Entities;
using RentACar.Models;

namespace RentACar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TimelineStepsController : Controller
    {
        private readonly AppDbContext _context;

        public TimelineStepsController(AppDbContext context)
        {
            _context = context;
        }

        // Index
        public async Task<IActionResult> Index()
        {
            var steps = await _context.TimelineSteps.OrderBy(s => s.Order).ToListAsync();
            return View(steps);
        }

        // Create GET
        public IActionResult Create()
        {
            return View();
        }

        // Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TimelineStepVM vm)
        {
            if (ModelState.IsValid)
            {
                string? fileName = null;

                if (vm.ImageFile != null)
                {
                    // wwwroot/uploads/timeline/ qovluğuna yazırıq
                    string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/timeline");
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    fileName = Guid.NewGuid() + Path.GetExtension(vm.ImageFile.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await vm.ImageFile.CopyToAsync(stream);
                    }
                }

                var step = new TimelineStep
                {
                    Title = vm.Title,
                    Description = vm.Description,
                    Order = vm.Order,
                    ImageUrl = fileName != null ? "/uploads/timeline/" + fileName : null
                };

                _context.Add(step);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // Edit GET
        public async Task<IActionResult> Edit(int id)
        {
            var step = await _context.TimelineSteps.FindAsync(id);
            if (step == null) return NotFound();
            return View(step);
        }

        // Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TimelineStepVM vm)
        {
            var step = await _context.TimelineSteps.FindAsync(id);
            if (step == null) return NotFound();

            if (ModelState.IsValid)
            {
                step.Title = vm.Title;
                step.Description = vm.Description;
                step.Order = vm.Order;

                if (vm.ImageFile != null)
                {
                    // köhnə şəkli silmək olar (optional)
                    if (!string.IsNullOrEmpty(step.ImageUrl))
                    {
                        string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", step.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldPath))
                            System.IO.File.Delete(oldPath);
                    }

                    string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/timeline");
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    string fileName = Guid.NewGuid() + Path.GetExtension(vm.ImageFile.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await vm.ImageFile.CopyToAsync(stream);
                    }

                    step.ImageUrl = "/uploads/timeline/" + fileName;
                }

                _context.Update(step);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // Delete GET
        public async Task<IActionResult> Delete(int id)
        {
            var step = await _context.TimelineSteps.FindAsync(id);
            if (step == null) return NotFound();
            return View(step);
        }

        // Delete POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var step = await _context.TimelineSteps.FindAsync(id);
            if (step != null)
            {
                _context.TimelineSteps.Remove(step);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
