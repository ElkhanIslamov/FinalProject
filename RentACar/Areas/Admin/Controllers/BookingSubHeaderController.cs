using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.DataContext;
using RentACar.DataContext.Entities;
using RentACar.Areas.Admin.Models;
using System.Threading.Tasks;
using System.IO;

namespace RentACar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingSubHeaderController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _env;

        public BookingSubHeaderController(AppDbContext dbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _dbContext.BookingSubHeaders.ToListAsync();

            // Entity -> ViewModel
            var model = list.Select(b => new BookingSubHeaderViewModel
            {
                Id = b.Id,
                Title = b.Title,
                BackgroundImagePath = b.BackgroundImage
            }).ToList();

            return View(model);
        }


        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(BookingSubHeaderViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var entity = new BookingSubHeader { Title = model.Title };

            if (model.BackgroundImageFile != null)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.BackgroundImageFile.FileName)}";
                var path = Path.Combine(_env.WebRootPath, "images/background", fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await model.BackgroundImageFile.CopyToAsync(stream);

                entity.BackgroundImage = $"/images/background/{fileName}";
            }

            _dbContext.BookingSubHeaders.Add(entity);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _dbContext.BookingSubHeaders.FindAsync(id);
            if (entity == null) return NotFound();

            var model = new BookingSubHeaderViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                BackgroundImagePath = entity.BackgroundImage
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookingSubHeaderViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var entity = await _dbContext.BookingSubHeaders.FindAsync(model.Id);
            if (entity == null) return NotFound();

            entity.Title = model.Title;

            if (model.BackgroundImageFile != null)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.BackgroundImageFile.FileName)}";
                var path = Path.Combine(_env.WebRootPath, "images/background", fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await model.BackgroundImageFile.CopyToAsync(stream);

                entity.BackgroundImage = $"/images/background/{fileName}";
            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _dbContext.BookingSubHeaders.FindAsync(id);
            if (entity == null) return NotFound();

            _dbContext.BookingSubHeaders.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
