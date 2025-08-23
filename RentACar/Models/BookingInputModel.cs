namespace RentACar.Models
{
    public class BookingInputModel
    {
        public required string VehicleType { get; set; }
        public required string PickupLocation { get; set; }
        public required string Destination { get; set; }
        public DateTime PickupDate { get; set; } = DateTime.UtcNow;
        public string PickupTime { get; set; } = "09:00";
        public DateTime ReturnDate { get; set; } = DateTime.UtcNow.AddDays(1);
        public string ReturnTime { get; set; }  = "09:00";
        public required string Name { get; set; }
        public string? Email { get; set; }
        public required string Phone { get; set; }
        public string? Message { get; set; }
    }
}
