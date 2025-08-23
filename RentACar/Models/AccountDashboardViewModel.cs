namespace RentACar.Models
{
    public class AccountDashboardViewModel
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<UserBookingViewModel> RecentBookings { get; set; } = new();
    }

}
