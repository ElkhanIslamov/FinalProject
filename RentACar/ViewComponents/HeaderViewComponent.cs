using Microsoft.AspNetCore.Mvc;
using RentACar.DataContext;
using RentACar.Areas.Admin.Models;

namespace RentACar.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public HeaderViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var entity = _context.HeaderInfos.FirstOrDefault();

            if (entity == null)
            {
                return View(new HeaderInfoViewModel
                {
                    Phone = "",
                    Email = "",
                    WorkingTime = DateTime.UtcNow.AddHours(4)
                });
            }

            var model = new HeaderInfoViewModel
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

            return View(model);
        }
    }
}
