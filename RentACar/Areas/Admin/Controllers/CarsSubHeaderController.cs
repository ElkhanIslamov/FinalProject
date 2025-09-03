using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Areas.Admin.Models;
using RentACar.DataContext;
using RentACar.DataContext.Entities;
using RentACar.DataContext.Entities.ProfilePage;

namespace RentACar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarsSubHeaderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CarsSubHeaderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // Index
        public async Task<IActionResult> Index()
        {
            var list = await _context.CarsSubHeaders
                .Select(x => new CarsSubHeaderViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    BackgroundImage = x.BackgroundImage
                }).ToListAsync();

            return View(list);
        }

        // Create GET
        public IActionResult Create() => View();

        // Create POST
        [HttpPost]
        public async Task<IActionResult> Create(CarsSubHeaderViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            string? filePath = null;
            if (model.ImageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                filePath = "/uploads/" + fileName;
                var savePath = Path.Combine(_env.WebRootPath, "uploads", fileName);

                using var stream = new FileStream(savePath, FileMode.Create);
                await model.ImageFile.CopyToAsync(stream);
            }

            var entity = new CarsSubHeader
            {
                Title = model.Title,
                BackgroundImage = filePath
            };

            _context.CarsSubHeaders.Add(entity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Edit GET
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _context.CarsSubHeaders.FindAsync(id);
            if (entity == null) return NotFound();

            var vm = new CarsSubHeaderViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                BackgroundImage = entity.BackgroundImage
            };

            return View(vm);
        }

        // Edit POST
        [HttpPost]
        public async Task<IActionResult> Edit(CarsSubHeaderViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var entity = await _context.CarsSubHeaders.FindAsync(model.Id);
            if (entity == null) return NotFound();

            entity.Title = model.Title;

            if (model.ImageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = "/uploads/" + fileName;
                var savePath = Path.Combine(_env.WebRootPath, "uploads", fileName);

                using var stream = new FileStream(savePath, FileMode.Create);
                await model.ImageFile.CopyToAsync(stream);

                entity.BackgroundImage = filePath;
            }

            _context.CarsSubHeaders.Update(entity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Delete GET
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.CarsSubHeaders.FindAsync(id);
            if (entity == null) return NotFound();

            var vm = new CarsSubHeaderViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                BackgroundImage = entity.BackgroundImage
            };

            return View(vm);
        }

        // Delete POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entity = await _context.CarsSubHeaders.FindAsync(id);
            if (entity == null) return NotFound();

            _context.CarsSubHeaders.Remove(entity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
