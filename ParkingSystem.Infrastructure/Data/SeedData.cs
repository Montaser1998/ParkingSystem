using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ParkingSystem.Domain.Entities;
using ParkingSystem.Infrastructure.Identity;

namespace ParkingSystem.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            context.Database.Migrate();

            // Seed Roles
            if (!await roleManager.RoleExistsAsync("Security"))
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = "Security" });
            }
            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = "User" });
            }

            // Seed Security User
            if (await userManager.FindByNameAsync("security@parking.com") == null)
            {
                var securityUser = new ApplicationUser { UserName = "security@parking.com", Email = "security@parking.com", EmailConfirmed = true, FullName = "security", VehicleLicensePlate = "ABC-2036" };
                await userManager.CreateAsync(securityUser, "Password123!");
                await userManager.AddToRoleAsync(securityUser, "Security");
            }

            // Seed Parking Slots
            if (!context.ParkingSlots.Any())
            {
                for (var i = 1; i <= 20; i++)
                {
                    context.ParkingSlots.Add(new ParkingSlots { SlotNumber = $"A-{i:00}" });
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
