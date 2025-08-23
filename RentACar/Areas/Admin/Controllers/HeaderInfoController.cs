using Microsoft.AspNetCore.Mvc;
using RentACar.DataContext;
using RentACar.DataContext.Entities;
using RentACar.Areas.Admin.Models;

namespace RentACar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HeaderInfoController : Controller
    {
        private readonly AppDbContext _context;

        public HeaderInfoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/HeaderInfo
        public IActionResult Index()
        {
            var list = _context.HeaderInfos.ToList();
            return View(list);
        }

        // GET: Admin/HeaderInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/HeaderInfo/Create
        [HttpPost]
        public IActionResult Create(HeaderInfoViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var entity = new HeaderInfo
            {
                Phone = model.Phone,
                Email = model.Email,
                WorkingTime = model.WorkingTime,
                FacebookUrl = model.FacebookUrl,
                TwitterUrl = model.TwitterUrl,
                YoutubeUrl = model.YoutubeUrl,
                PinterestUrl = model.PinterestUrl,
                InstagramUrl = model.InstagramUrl
            };

            _context.HeaderInfos.Add(entity);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Admin/HeaderInfo/Edit/5
        public IActionResult Edit(int id)
        {
            var entity = _context.HeaderInfos.Find(id);
            if (entity == null) return NotFound();

            var vm = new HeaderInfoViewModel
            {
                Id = entity.Id,
                Phone = entity.Phone,
                Email = entity.Email,
                WorkingTime = entity.WorkingTime,
                FacebookUrl = entity.FacebookUrl,
                TwitterUrl = entity.TwitterUrl,
                YoutubeUrl = entity.YoutubeUrl,
                PinterestUrl = entity.PinterestUrl,
                InstagramUrl = entity.InstagramUrl
            };

            return View(vm);
        }

        // POST: Admin/HeaderInfo/Edit/5
        [HttpPost]
        public IActionResult Edit(HeaderInfoViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var entity = _context.HeaderInfos.Find(model.Id);
            if (entity == null) return NotFound();

            entity.Phone = model.Phone;
            entity.Email = model.Email;
            entity.WorkingTime = model.WorkingTime;
            entity.FacebookUrl = model.FacebookUrl;
            entity.TwitterUrl = model.TwitterUrl;
            entity.YoutubeUrl = model.YoutubeUrl;
            entity.PinterestUrl = model.PinterestUrl;
            entity.InstagramUrl = model.InstagramUrl;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Admin/HeaderInfo/Delete/5
        public IActionResult Delete(int id)
        {
            var entity = _context.HeaderInfos.Find(id);
            if (entity == null) return NotFound();

            _context.HeaderInfos.Remove(entity);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
