using JuanJoseHernandez.Constants;
using JuanJoseHernandez.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace JuanJoseHernandez.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            // Seed Roles
            await SeedRoleAsync(roleManager, Roles.Admin);
            await SeedRoleAsync(roleManager, Roles.User);

            // Seed Admin User
            var adminEmail = "admin@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var user = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    // Business Properties
                    Names = "Super",
                    LastNames = "Admin",
                    Document = "0000000000",
                    Direction = "System ADmin Address",
                    Telephone = "5555555555",
                    Salary = 0,
                    Profile = "System Administrator",
                    DateEntry = DateOnly.FromDateTime(DateTime.UtcNow)
                };

                var result = await userManager.CreateAsync(user, "admin123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Roles.Admin);
                }
            }
        }

        private static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
