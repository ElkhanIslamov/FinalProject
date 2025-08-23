using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.DataContext;
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
            HeroSection = _context.HomeHeroSections.FirstOrDefault()!,
            FeatureSection = _context.HomeFeatureSections.FirstOrDefault(),
            VehicleFleetSetting = _context.VehicleFleetSettings.FirstOrDefault()!,
            Features = _context.HomeFeatures.ToList(),
            Vehicles = _context.HomeVehicles.ToList(),
            TimelineSteps = _context.TimelineSteps.ToList(),
            Faqs = _context.HomeFaqs.ToList(),
            Cars = _context.Cars
                    .Include(c => c.Category)
                    .ToList()
            .OrderByDescending(c => c.Id) // ?n son ?lav? olunanlar? g?st?rm?k ???n
            .Take(6) // m?s?l?n, yaln?z 6 ma??n g?st?r
            .ToList()
        };

        return View(vm);
    }
}
