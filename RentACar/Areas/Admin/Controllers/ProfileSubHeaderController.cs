using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Areas.Admin.Models;
using RentACar.DataContext;
using RentACar.DataContext.Entities.ProfilePage;

namespace RentACar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProfileSubHeaderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProfileSubHeaderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Admin/ProfileSubHeader
        public async Task<IActionResult> Index()
        {
            var model = await _context.ProfileSubHeaders.ToListAsync();
            return View(model);
        }

        // GET: Admin/ProfileSubHeader/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProfileSubHeader/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProfileSubHeaderViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var entity = new ProfileSubHeader
                {
                    Title = vm.Title
                };

                if (vm.BackgroundImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/profile");
                    Directory.CreateDirectory(uploadsFolder);
                    string fileName = Guid.NewGuid() + Path.GetExtension(vm.BackgroundImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await vm.BackgroundImageFile.CopyToAsync(stream);
                    entity.BackgroundImage = "/uploads/profile/" + fileName;
                }

                _context.ProfileSubHeaders.Add(entity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Admin/ProfileSubHeader/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _context.ProfileSubHeaders.FindAsync(id);
            if (entity == null) return NotFound();

            var vm = new ProfileSubHeaderViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                BackgroundImage = entity.BackgroundImage
            };

            return View(vm);
        }

        // POST: Admin/ProfileSubHeader/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProfileSubHeaderViewModel vm)
        {
            if (id != vm.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var entity = await _context.ProfileSubHeaders.FindAsync(id);
                if (entity == null) return NotFound();

                entity.Title = vm.Title;

                if (vm.BackgroundImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/profile");
                    Directory.CreateDirectory(uploadsFolder);
                    string fileName = Guid.NewGuid() + Path.GetExtension(vm.BackgroundImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await vm.BackgroundImageFile.CopyToAsync(stream);
                    entity.BackgroundImage = "/uploads/profile/" + fileName;
                }

                _context.Update(entity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }
        // GET: Admin/ProfileSubHeader/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var subHeader = await _context.ProfileSubHeaders
                .FirstOrDefaultAsync(m => m.Id == id);

            if (subHeader == null)
                return NotFound();

            var vm = new ProfileSubHeaderViewModel
            {
                Id = subHeader.Id,
                Title = subHeader.Title,
                BackgroundImage = subHeader.BackgroundImage
            };

            return View(vm);
        }

        // POST: Admin/ProfileSubHeader/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subHeader = await _context.ProfileSubHeaders.FindAsync(id);
            if (subHeader != null)
            {
                // Fayl varsa sil
                if (!string.IsNullOrEmpty(subHeader.BackgroundImage))
                {
                    var filePath = Path.Combine(_env.WebRootPath, subHeader.BackgroundImage.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);
                }

                _context.ProfileSubHeaders.Remove(subHeader);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
