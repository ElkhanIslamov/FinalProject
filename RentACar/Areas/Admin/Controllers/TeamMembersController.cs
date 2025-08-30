using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Areas.Admin.Models;
using RentACar.DataContext;
using RentACar.DataContext.Entities.AboutPage;

namespace RentACar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamMembersController : Controller
    {
        private readonly AppDbContext _context;

        public TeamMembersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TeamMembers
        public async Task<IActionResult> Index()
        {
            var members = await _context.TeamMembers
                .Select(x => new TeamMemberVM
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Position = x.Position,
                    FacebookUrl = x.FacebookUrl,
                    TwitterUrl = x.TwitterUrl,
                    LinkedinUrl = x.LinkedinUrl,
                    PinterestUrl = x.PinterestUrl,
                    ImageUrl = x.ImageUrl
                }).ToListAsync();

            return View(members);
        }

        // GET: Admin/TeamMembers/Create
        public IActionResult Create() => View();

        // POST: Admin/TeamMembers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamMemberVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var entity = new TeamMember
            {
                FullName = vm.FullName,
                Position = vm.Position,
                FacebookUrl = vm.FacebookUrl,
                TwitterUrl = vm.TwitterUrl,
                LinkedinUrl = vm.LinkedinUrl,
                PinterestUrl = vm.PinterestUrl
            };

            if (vm.ImageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(vm.ImageFile.FileName);
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/team");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var path = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await vm.ImageFile.CopyToAsync(stream);
                }

                // yalnız fayl adı DB-də
                entity.ImageUrl = fileName;
            }



            _context.TeamMembers.Add(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/TeamMembers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _context.TeamMembers.FindAsync(id);
            if (entity == null) return NotFound();

            var vm = new TeamMemberVM
            {
                Id = entity.Id,
                FullName = entity.FullName,
                Position = entity.Position,
                FacebookUrl = entity.FacebookUrl,
                TwitterUrl = entity.TwitterUrl,
                LinkedinUrl = entity.LinkedinUrl,
                PinterestUrl = entity.PinterestUrl,
                ImageUrl = entity.ImageUrl
            };

            return View(vm);
        }

        // POST: Admin/TeamMembers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeamMemberVM vm)
        {
            if (id != vm.Id) return NotFound();
            if (!ModelState.IsValid) return View(vm);

            var entity = await _context.TeamMembers.FindAsync(id);
            if (entity == null) return NotFound();

            entity.FullName = vm.FullName;
            entity.Position = vm.Position;
            entity.FacebookUrl = vm.FacebookUrl;
            entity.TwitterUrl = vm.TwitterUrl;
            entity.LinkedinUrl = vm.LinkedinUrl;
            entity.PinterestUrl = vm.PinterestUrl;

            if (vm.ImageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(vm.ImageFile.FileName);
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/team");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var path = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await vm.ImageFile.CopyToAsync(stream);
                }

                // yalnız fayl adı DB-də
                entity.ImageUrl = fileName;
            }


            _context.Update(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/TeamMembers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.TeamMembers.FindAsync(id);
            if (entity == null) return NotFound();

            var vm = new TeamMemberVM
            {
                Id = entity.Id,
                FullName = entity.FullName,
                Position = entity.Position,
                FacebookUrl = entity.FacebookUrl,
                TwitterUrl = entity.TwitterUrl,
                LinkedinUrl = entity.LinkedinUrl,
                PinterestUrl = entity.PinterestUrl,
                ImageUrl = entity.ImageUrl
            };

            return View(vm);
        }

        // POST: Admin/TeamMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entity = await _context.TeamMembers.FindAsync(id);
            if (entity != null)
            {
                _context.TeamMembers.Remove(entity);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
