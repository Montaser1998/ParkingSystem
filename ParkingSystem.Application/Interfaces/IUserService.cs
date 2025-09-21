using ParkingSystem.Application.DTOs;

namespace ParkingSystem.Application.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Finds a user by their registered vehicle license plate.
        /// </summary>
        /// <param name="licensePlate">The license plate to search for.</param>
        /// <returns>A UserDto if found; otherwise, null.</returns>
        Task<UserDto?> FindUserByLicensePlateAsync(string licensePlate);
    }
}
