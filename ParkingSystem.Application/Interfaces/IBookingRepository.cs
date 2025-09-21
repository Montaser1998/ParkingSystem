using ParkingSystem.Domain.Entities;

namespace ParkingSystem.Application.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Bookings>
    {
        Task<IEnumerable<Bookings>> FindExpiredBookingsAsync();
    }
}
