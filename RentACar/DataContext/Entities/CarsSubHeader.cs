namespace RentACar.DataContext.Entities
{
    public class CarsSubHeader
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;   // Məsələn: "Cars"
        public string? BackgroundImage { get; set; }        // Şəkil yolu (upload və ya url)
    }
}
