using Microsoft.AspNetCore.Mvc;
using RentACar.DataContext;
using RentACar.Models;
using System.Linq;

namespace RentACar.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var vm = new AboutPageViewModel
            {
                Intro = _context.AboutIntros.FirstOrDefault() ?? new(),
                Statistics = _context.Statistics.ToList(),
                TeamMembers = _context.TeamMembers.ToList(),
                QualityTabs = _context.QualityTabs.ToList(),
                CallToAction = _context.CallToActions.FirstOrDefault() ?? new(),
                Board = _context.DirectorsBoards.FirstOrDefault() ?? new(),
                SubHeader = _context.SubHeaders.FirstOrDefault() ?? new()
            };

            return View(vm);
        }
    }
}
