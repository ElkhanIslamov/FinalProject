namespace RentACar.DataContext.Entities
{
    public class BookingSubHeader
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;          // Məsələn: "Quick Booking"
        public string? BackgroundImage { get; set; }               // Şəkil yolu
    }
}
