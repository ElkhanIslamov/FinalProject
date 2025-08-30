namespace RentACar.DataContext.Entities.AboutPage
{
    public class DirectorsBoard
    {
        public int Id { get; set; }

        // Frontenddə başlıq/description üçün
        public string Description { get; set; } = string.Empty;

        // Arxa plan şəkli
        public string BackgroundImageUrl { get; set; } = string.Empty;
    }
}
