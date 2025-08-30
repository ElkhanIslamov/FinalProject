namespace RentACar.DataContext.Entities.AboutPage
{
    public class QualityTabItem
    {
        public int Id { get; set; }

        // Başlıq hissəsi (ümumi başlıq "Only Quality For Clients")
        public string HeadTitle { get; set; } = null!;

        // Tab başlığı (Luxury, Comfort və s.)
        public string Title { get; set; } = null!;

        // Tab mətni
        public string Content { get; set; } = null!;

        // İlk açılan tab üçün (default olaraq false)
        public bool IsActive { get; set; } = false;
    }

}
