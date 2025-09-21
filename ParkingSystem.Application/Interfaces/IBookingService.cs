using ParkingSystem.Domain.Entities;

namespace ParkingSystem.Application.Interfaces
{
    public interface IBookingService
    {
        /// <summary>
        /// Creates a new booking or extends an existing one for a user and slot.
        /// </summary>
        /// <param name="slotId">The ID of the parking slot.</param>
        /// <param name="userId">The ID of the user making the booking.</param>
        /// <param name="durationInHours">The number of hours to book or extend by.</param>
        /// <returns>The created or updated Booking entity.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the slot is unavailable.</exception>
        Task<Bookings> CreateOrExtendBookingAsync(int slotId, Guid userId, int durationInHours);
    }
}
