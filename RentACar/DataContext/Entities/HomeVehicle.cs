namespace RentACar.DataContext.Entities
{
    public class HomeVehicle
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public int Seats { get; set; }
        public int Doors { get; set; }
        public int Luggage { get; set; }
        public decimal PricePerDay { get; set; }
        public int Likes { get; set; }
    }

}
