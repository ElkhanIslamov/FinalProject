namespace RentACar.Areas.Admin.Models
{
    public class RecentBookingAdminViewModel
    {
        public string CustomerName { get; set; } = null!;
        public string CarName { get; set; } = null!;
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Status { get; set; } = "Pending";
        public decimal TotalPrice { get; set; }
    }

}
