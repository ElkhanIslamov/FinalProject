using RentACar.DataContext.Entities;

namespace RentACar.Areas.Admin.Models
{
    public class UserWithRolesViewModel
    {
        public required AppUser User { get; set; }
        public List<string> Roles { get; set; } = [];
    }
}
