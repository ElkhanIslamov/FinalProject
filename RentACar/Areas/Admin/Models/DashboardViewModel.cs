using RentACar.Areas.Admin.Data;

namespace RentACar.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int TotalCars { get; set; }
        public int RentedCars { get; set; }
        public int TodaysBookings { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<BookingInfo> RecentBookings { get; set; }
    }
}
