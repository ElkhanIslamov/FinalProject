using Microsoft.AspNetCore.Mvc;
using RentACar.DataContext;
using RentACar.Areas.Admin.Models;
using System.Linq;

namespace RentACar.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public FooterViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var footerEntity = _context.FooterInfos.FirstOrDefault();
            var links = _context.FooterLinks.ToList(); // FooterLinks cədvəlindən çəkirik

            FooterInfoViewModel model;

            if (footerEntity == null)
            {
                model = new FooterInfoViewModel
                {
                    AboutText = "",
                    Address = "",
                    Phone = "",
                    Email = "",
                    FooterLinks = new List<FooterLinkViewModel>()
                };
            }
            else
            {
                model = new FooterInfoViewModel
                {
                    Id = footerEntity.Id,
                    AboutText = footerEntity.AboutText,
                    Address = footerEntity.Address,
                    Phone = footerEntity.Phone,
                    Email = footerEntity.Email,
                    FacebookUrl = footerEntity.FacebookUrl,
                    TwitterUrl = footerEntity.TwitterUrl,
                    LinkedInUrl = footerEntity.LinkedInUrl,
                    PinterestUrl = footerEntity.PinterestUrl,
                    RssUrl = footerEntity.RssUrl,
                    FooterLinks = links.Select(fl => new FooterLinkViewModel
                    {
                        Id = fl.Id,
                        Title = fl.Title,
                        Url = fl.Url
                    }).ToList()
                };
            }

            return View(model);
        }

    }
}
