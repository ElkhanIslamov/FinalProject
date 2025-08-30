using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Areas.Admin.Models;
using RentACar.DataContext;
using RentACar.DataContext.Entities;
using RentACar.DataContext.Entities.AboutPage;

namespace RentACar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubHeaderController : Controller
    {
        private readonly AppDbContext _context;

        public SubHeaderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/SubHeader
        public async Task<IActionResult> Index()
        {
            var headers = await _context.SubHeaders.ToListAsync();
            return View(headers);
        }

        // GET: Admin/SubHeader/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/SubHeader/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubHeaderViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            string fileName = string.Empty;

            if (model.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/subheader");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }
            }

            var subHeader = new SubHeader
            {
                BackgroundImage = fileName, // ✅ yalnız fayl adı saxlanır
                Title = model.Title
            };

            _context.SubHeaders.Add(subHeader);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Edit GET
        public IActionResult Edit(int id)
        {
            var entity = _context.SubHeaders.FirstOrDefault(x => x.Id == id);
            if (entity == null) return NotFound();

            var vm = new SubHeaderViewModel
            {
                Title = entity.Title, // JSON parse edə bilərsən əgər lazım olsa
                ImageFile = null
            };

            // Mövcud şəkili ViewBag ilə göndəririk
            ViewBag.BackgroundImage = entity.BackgroundImage;
            ViewBag.Id = entity.Id;

            return View(vm);
        }

        // Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubHeaderViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var entity = await _context.SubHeaders.FindAsync(id);
            if (entity == null) return NotFound();

            entity.Title = vm.Title;

            if (vm.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/subheader");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileName = Guid.NewGuid() + Path.GetExtension(vm.ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.ImageFile.CopyToAsync(stream);
                }

                entity.BackgroundImage = fileName; // ✅ yalnız fayl adı saxlanır
            }


            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        // GET: Admin/SubHeader/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var subHeader = await _context.SubHeaders.FirstOrDefaultAsync(x => x.Id == id);
            if (subHeader == null) return NotFound();

            return View(subHeader);
        }

        // POST: Admin/SubHeader/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subHeader = await _context.SubHeaders.FindAsync(id);
            if (subHeader == null) return NotFound();

            if (!string.IsNullOrEmpty(subHeader.BackgroundImage))
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/subheader");
                string filePath = Path.Combine(uploadsFolder, subHeader.BackgroundImage);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.SubHeaders.Remove(subHeader);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
