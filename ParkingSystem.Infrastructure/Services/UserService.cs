using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Application.DTOs;
using ParkingSystem.Application.Interfaces;
using ParkingSystem.Infrastructure.Identity;


namespace ParkingSystem.Infrastructure.Services
{
    public class UserService(UserManager<ApplicationUser> userManager) : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<UserDto?> FindUserByLicensePlateAsync(string licensePlate)
        {
            // Find the user using the UserManager
            ApplicationUser? user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.VehicleLicensePlate == licensePlate);

            if (user == null)
            {
                return null;
            }

            // Map the ApplicationUser entity to a UserDto to safely return it
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                VehicleLicensePlate = user.VehicleLicensePlate
            };
        }
    }
}
