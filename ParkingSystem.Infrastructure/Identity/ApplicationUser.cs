using Microsoft.AspNetCore.Identity;

namespace ParkingSystem.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public required string VehicleLicensePlate { get; set; }
        public required string FullName { get; set; }
    }
}
