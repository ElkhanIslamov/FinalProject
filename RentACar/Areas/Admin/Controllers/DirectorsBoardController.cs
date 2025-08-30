using Microsoft.AspNetCore.Mvc;
using RentACar.DataContext;
using RentACar.DataContext.Entities.AboutPage;

namespace RentACar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DirectorsBoardController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DirectorsBoardController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Index
        public IActionResult Index()
        {
            var boards = _context.DirectorsBoards.ToList();
            return View(boards);
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DirectorsBoard model, IFormFile BackgroundImageFile)
        {
            if (ModelState.IsValid)
            {
                if (BackgroundImageFile != null)
                {
                    var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "team");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var fileName = Guid.NewGuid() + Path.GetExtension(BackgroundImageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await BackgroundImageFile.CopyToAsync(stream);

                    model.BackgroundImageUrl = fileName;
                }

                _context.DirectorsBoards.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Edit
        public IActionResult Edit(int id)
        {
            var board = _context.DirectorsBoards.Find(id);
            if (board == null) return NotFound();
            return View(board);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DirectorsBoard model, IFormFile? BackgroundImageFile)
        {
            var board = _context.DirectorsBoards.Find(id);
            if (board == null) return NotFound();

            if (ModelState.IsValid)
            {
                board.Description = model.Description;

                if (BackgroundImageFile != null)
                {
                    var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "team");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var fileName = Guid.NewGuid() + Path.GetExtension(BackgroundImageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await BackgroundImageFile.CopyToAsync(stream);

                    // köhnə faylı silmək istəsən burada əlavə edə bilərsən
                    board.BackgroundImageUrl = fileName;
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Delete
        public IActionResult Delete(int id)
        {
            var board = _context.DirectorsBoards.Find(id);
            if (board == null) return NotFound();
            return View(board);
        }

        // POST: DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var board = _context.DirectorsBoards.Find(id);
            if (board == null) return NotFound();

            // köhnə şəkli silmək istəsən burada əlavə edə bilərsən
            _context.DirectorsBoards.Remove(board);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
