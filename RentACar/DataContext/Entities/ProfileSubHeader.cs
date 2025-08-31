namespace RentACar.DataContext.Entities.ProfilePage
{
    public class ProfileSubHeader
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;               // Dashboard və ya başlıq
        public string? BackgroundImage { get; set; }                  // Şəkil url
    }
}
