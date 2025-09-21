using ParkingSystem.Domain.Entities;

namespace ParkingSystem.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<ParkingSlots> ParkingSlots { get; }
        IBookingRepository Bookings { get; }
        IGenericRepository<Payments> Payments { get; }
        IGenericRepository<Fines> Fines { get; }
        Task<int> CompleteAsync();
    }
}
