using RentACar.DataContext.Entities;

namespace RentACar.Models
{
    public class HomeIndexViewModel
    {
        public HomeHeroSection HeroSection { get; set; } = null!;
        public List<HomeFeature> Features { get; set; } = new List<HomeFeature>();
        public List<HomeVehicle> Vehicles { get; set; } = new();
        public List<TimelineStep> TimelineSteps { get; set; } = new();
        public List<HomeFaq> Faqs { get; set; } = new();
        public HomeFeatureSection FeatureSection { get; set; } = null!;
        public List<Car> Cars { get; set; } = new();
        public VehicleFleetSetting VehicleFleetSetting { get; set; } = null!;


    }
}
