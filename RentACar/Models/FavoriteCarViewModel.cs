namespace RentACar.Models
{
    public class FavoriteCarViewModel
    {
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public decimal PricePerDay { get; set; }
        public int Seats { get; set; }
        public int Luggage { get; set; }
        public int Doors { get; set; }
        public string Fuel { get; set; } = null!;
        public int Horsepower { get; set; }
        public int Engine { get; set; }
        public string Drive { get; set; } = null!;
        public string Type { get; set; } = null!;
    }
}
