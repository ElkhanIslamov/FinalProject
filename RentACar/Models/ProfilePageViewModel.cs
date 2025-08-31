using RentACar.DataContext.Entities.ProfilePage;

namespace RentACar.Models
{
    public class ProfilePageViewModel
    {
        public ProfileSubHeader SubHeader { get; set; } = new();
        public List<UserBookingViewModel> UserBookings { get; set; } = new();
        public string FullName { get; set; } = string.Empty;
    }
}
