using Microsoft.AspNetCore.Mvc;
using RentACar.Areas.Admin.Models;
using RentACar.DataContext;
using RentACar.DataContext.Entities;

namespace RentACar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeHeroSectionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeHeroSectionController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // Index
        public IActionResult Index()
        {
            var heroSections = _context.HomeHeroSections.ToList();
            return View(heroSections);
        }

        // Create GET
        public IActionResult Create()
        {
            return View();
        }

        // Create POST
        // Create POST
        [HttpPost]
        public async Task<IActionResult> Create(HomeHeroSectionViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            string fileName = "";
            if (vm.ImageFile != null)
            {
                fileName = Guid.NewGuid() + Path.GetExtension(vm.ImageFile.FileName);

                string uploadPath = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath); // qovluq yoxdursa yarat

                string path = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await vm.ImageFile.CopyToAsync(stream);
                }
            }

            var entity = new HomeHeroSection
            {
                BackgroundImageUrl = "/uploads/" + fileName,
                SmallTitle = vm.SmallTitle,
                MainTitle = vm.MainTitle,
                Description = vm.Description
            };

            _context.HomeHeroSections.Add(entity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // Edit GET
        public IActionResult Edit(int id)
        {
            var hero = _context.HomeHeroSections.Find(id);
            if (hero == null) return NotFound();

            var vm = new HomeHeroSectionViewModel
            {
                Id = hero.Id,
                BackgroundImageUrl = hero.BackgroundImageUrl,
                SmallTitle = hero.SmallTitle,
                MainTitle = hero.MainTitle,
                Description = hero.Description
            };

            return View(vm);
        }

        // Edit POST
        // Edit POST
        [HttpPost]
        public async Task<IActionResult> Edit(HomeHeroSectionViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var hero = _context.HomeHeroSections.Find(vm.Id);
            if (hero == null) return NotFound();

            if (vm.ImageFile != null)
            {
                // köhnə şəkli sil
                if (!string.IsNullOrEmpty(hero.BackgroundImageUrl))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, hero.BackgroundImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                string fileName = Guid.NewGuid() + Path.GetExtension(vm.ImageFile.FileName);

                string uploadPath = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                string path = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await vm.ImageFile.CopyToAsync(stream);
                }
                hero.BackgroundImageUrl = "/uploads/" + fileName;
            }

            hero.SmallTitle = vm.SmallTitle;
            hero.MainTitle = vm.MainTitle;
            hero.Description = vm.Description;

            _context.Update(hero);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            var hero = _context.HomeHeroSections.Find(id);
            if (hero == null) return NotFound();

            if (!string.IsNullOrEmpty(hero.BackgroundImageUrl))
            {
                string oldPath = Path.Combine(_env.WebRootPath, hero.BackgroundImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }

            _context.HomeHeroSections.Remove(hero);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }

}
