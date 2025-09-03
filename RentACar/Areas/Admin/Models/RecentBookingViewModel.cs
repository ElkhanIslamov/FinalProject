namespace RentACar.Areas.Admin.Models
{
    public class RecentBookingViewModel
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CarName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
