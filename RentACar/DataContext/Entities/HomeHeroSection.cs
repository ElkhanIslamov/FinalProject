namespace RentACar.DataContext.Entities
{
    public class HomeHeroSection
    {
        public int Id { get; set; }
        public string BackgroundImageUrl { get; set; } = null!;
        public string SmallTitle { get; set; } = null!;
        public string MainTitle { get; set; } = null!;
        public string Description { get; set; } = null!;
    }

}
