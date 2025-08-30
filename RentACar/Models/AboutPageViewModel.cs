using RentACar.DataContext.Entities;
using RentACar.DataContext.Entities.AboutPage;
using Stripe.Entitlements;

namespace RentACar.Models
{
    public class AboutPageViewModel
    {
        public AboutIntro Intro { get; set; } = new();
        public List<Statistic> Statistics { get; set; } = new();
        public List<TeamMember> TeamMembers { get; set; } = new();
        public List<QualityTabItem> QualityTabItems { get; set; } = new();
        public CallToAction CallToAction { get; set; } = new();
        public DirectorsBoard Board { get; set; } = new();
        public SubHeader SubHeader { get; set; } = new();
        public List<HomeFeature> Features { get; set; } = new();
        public HomeFeatureSection HomeFeatureSection { get; set; } = new();

    }
}
