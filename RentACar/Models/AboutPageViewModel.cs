using RentACar.DataContext.Entities.AboutPage;

namespace RentACar.Models
{
    public class AboutPageViewModel
    {
        public AboutIntro Intro { get; set; } = new();
        public List<Statistic> Statistics { get; set; } = new();
        public List<TeamMember> TeamMembers { get; set; } = new();
        public List<QualityTabItem> QualityTabs { get; set; } = new();
        public CallToAction CallToAction { get; set; } = new();
        public DirectorsBoard Board { get; set; } = new();
        public SubHeader SubHeader { get; set; } = new();   
    }
}
