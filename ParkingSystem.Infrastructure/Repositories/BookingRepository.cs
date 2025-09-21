using Microsoft.EntityFrameworkCore;
using ParkingSystem.Application.Interfaces;
using ParkingSystem.Domain.Entities;
using ParkingSystem.Infrastructure.Data;

namespace ParkingSystem.Infrastructure.Repositories
{
    public class BookingRepository : GenericRepository<Bookings>, IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bookings>> FindExpiredBookingsAsync()
        {
            var now = DateTime.UtcNow;
            return await _context.Bookings
                    .Where(b => b.EndTime < now && b.IsPaid == false)
                    .ToListAsync();
        }
    }
}
