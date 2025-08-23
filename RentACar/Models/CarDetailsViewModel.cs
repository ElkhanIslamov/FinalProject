namespace RentACar.Models
{
    public class CarDetailsViewModel
    {
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public List<string> AdditionalImageUrls { get; set; } = new();

        public decimal PricePerDay { get; set; }
        public string? Description { get; set; }

        // Aşağıdakıları əlavə et ki, .cshtml səhifəsində göstərilə bilsin:
        public int? Seats { get; set; }
        public int? Doors { get; set; }
        public int? Luggage { get; set; }

        public string? VehicleTypeName { get; set; }
        public string? BodyTypeName { get; set; }
    }
}
