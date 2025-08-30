using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.DataContext;
using RentACar.DataContext.Entities;
using RentACar.Models;

namespace RentACar.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var vm = new HomeIndexViewModel
        {
            HeroSection = _context.HomeHeroSections.FirstOrDefault() ?? new HomeHeroSection { MainTitle = "Welcome", SmallTitle = "Default Hero" },
            FeatureSection = _context.HomeFeatureSections.FirstOrDefault() ?? new HomeFeatureSection { Title = "Our Features" },
            VehicleFleetSetting = _context.VehicleFleetSettings.FirstOrDefault() ?? new VehicleFleetSetting { Title = "Our Fleet", Description = "Default Fleet Description" },
            Features = _context.HomeFeatures.ToList(),
            Vehicles = _context.HomeVehicles.ToList(),
            TimelineSteps = _context.TimelineSteps.ToList(),
            Faqs = _context.HomeFaqs.ToList(),
            Cars = _context.Cars
                    .Include(c => c.Category)
                    .ToList()
                    .OrderByDescending(c => c.Id)
                    .Take(6)
                    .ToList()
        };

        return View(vm);
    }
}
