namespace RentACar.Models
{
    public class UserBookingViewModel
    {
        public string CarName { get; set; } = null!;
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string PickupLocation { get; set; } = null!;
        public string DropoffLocation { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending"; // Optional
    }

}
