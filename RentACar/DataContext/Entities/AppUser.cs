using Microsoft.AspNetCore.Identity;

namespace RentACar.DataContext.Entities
{
    public class AppUser:IdentityUser
    {
        public required string FullName { get; set; }
    }
}
