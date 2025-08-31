using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.DataContext;
using RentACar.DataContext.Entities.AboutPage;
using RentACar.Areas.Admin.Models;

namespace RentACar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CallToActionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CallToActionController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var callToActions = await _context.CallToActions.ToListAsync();
            return View(callToActions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CallToActionViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var entity = new CallToAction
            {
                Heading = model.Heading,
                Phone = model.Phone,
                ButtonText = model.ButtonText,
                ButtonUrl = model.ButtonUrl,
                Icon = model.Icon
            };

            if (model.BackgroundImageFile != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(model.BackgroundImageFile.FileName);
                string path = Path.Combine(_env.WebRootPath, "uploads/calltoaction", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                using var stream = new FileStream(path, FileMode.Create);
                await model.BackgroundImageFile.CopyToAsync(stream);

                entity.BackgroundImage = "/uploads/calltoaction/" + fileName;
            }

            _context.Add(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _context.CallToActions.FindAsync(id);
            if (entity == null) return NotFound();

            var vm = new CallToActionViewModel
            {
                Id = entity.Id,
                Heading = entity.Heading,
                Phone = entity.Phone,
                ButtonText = entity.ButtonText,
                ButtonUrl = entity.ButtonUrl,
                Icon = entity.Icon,
                BackgroundImage = entity.BackgroundImage
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CallToActionViewModel model)
        {
            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);

            var entity = await _context.CallToActions.FindAsync(id);
            if (entity == null) return NotFound();

            entity.Heading = model.Heading;
            entity.Phone = model.Phone;
            entity.ButtonText = model.ButtonText;
            entity.ButtonUrl = model.ButtonUrl;
            entity.Icon = model.Icon;

            if (model.BackgroundImageFile != null)
            {
                // köhnə şəkili silmək (əgər varsa)
                if (!string.IsNullOrEmpty(entity.BackgroundImage))
                {
                    var oldPath = Path.Combine(_env.WebRootPath, entity.BackgroundImage.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                string fileName = Guid.NewGuid() + Path.GetExtension(model.BackgroundImageFile.FileName);
                string path = Path.Combine(_env.WebRootPath, "uploads/calltoaction", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                using var stream = new FileStream(path, FileMode.Create);
                await model.BackgroundImageFile.CopyToAsync(stream);

                entity.BackgroundImage = "/uploads/calltoaction/" + fileName;
            }

            _context.Update(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.CallToActions.FindAsync(id);
            if (entity == null) return NotFound();

            return View(entity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entity = await _context.CallToActions.FindAsync(id);
            if (entity == null) return NotFound();

            // şəkili sil
            if (!string.IsNullOrEmpty(entity.BackgroundImage))
            {
                var oldPath = Path.Combine(_env.WebRootPath, entity.BackgroundImage.TrimStart('/'));
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }

            _context.CallToActions.Remove(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
