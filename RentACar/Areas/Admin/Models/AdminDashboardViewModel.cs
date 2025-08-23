namespace RentACar.Areas.Admin.Models
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalBookings { get; set; }
        public int TotalCars { get; set; }
        public int TotalCategories { get; set; }
        public List<RecentBookingAdminViewModel> RecentBookings { get; set; } = new();
    }

}
