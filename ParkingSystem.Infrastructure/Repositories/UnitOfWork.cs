using ParkingSystem.Application.Interfaces;
using ParkingSystem.Domain.Entities;
using ParkingSystem.Infrastructure.Data;

namespace ParkingSystem.Infrastructure.Repositories
{
    public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
    {
        private readonly ApplicationDbContext _context = context;
        public IGenericRepository<ParkingSlots> ParkingSlots { get; } = new GenericRepository<ParkingSlots>(context);
        public IBookingRepository Bookings { get; } = new BookingRepository(context);
        public IGenericRepository<Fines> Fines { get; } = new GenericRepository<Fines>(context);
        public IGenericRepository<Payments> Payments { get; } = new GenericRepository<Payments>(context);

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
