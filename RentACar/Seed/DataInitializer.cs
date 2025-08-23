using Microsoft.AspNetCore.Identity;
using RentACar.DataContext.Entities;

namespace RentACar.Seed
{
    public static class DataInitializer
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            string[] roleNames = { "Admin", "Moderator", "Customer" };

            foreach (var roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminUser = await userManager.FindByEmailAsync("admin@rentaly.com");
            if (adminUser == null)
            {
                var user = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@rentaly.com",
                    FullName = "Admin",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "admin123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
