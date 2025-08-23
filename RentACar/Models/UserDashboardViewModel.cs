
namespace RentACar.Models
{
    public class UserDashboardViewModel
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ProfileImageUrl { get; set; } = "/images/profile/1.jpg";
        public int UpcomingOrdersCount { get; set; }
        public int CouponsCount { get; set; }
        public int TotalOrdersCount { get; set; }
        public int CancelOrdersCount { get; set; }
        public List<BookingViewModel> RecentBookings { get; set; } = new();
        public List<FavoriteCarViewModel> FavoriteCars { get; set; } = new();
    }
}
