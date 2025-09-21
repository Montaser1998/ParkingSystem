using ParkingSystem.Application.Interfaces;
using ParkingSystem.Domain.Entities;

namespace ParkingSystem.Infrastructure.Services
{
    public class BookingService(IUnitOfWork unitOfWork) : IBookingService
    {

        public async Task<Bookings> CreateOrExtendBookingAsync(int slotId, Guid userId, int durationInHours)
        {
            // Rule 1: Validate that the slot is not currently booked by another user.
            IEnumerable<Bookings> activeBookingsForSlot = await unitOfWork.Bookings.FindAsync(b =>
                b.ParkingSlotId == slotId &&
                b.IsPaid &&
                b.EndTime > DateTime.UtcNow);

            Bookings? bookingByAnotherUser = activeBookingsForSlot.FirstOrDefault(b => b.UserId != userId);
            if (bookingByAnotherUser != null)
            {
                // This is a critical business rule!
                throw new InvalidOperationException("This parking slot is currently booked by another user.");
            }

            // Rule 2: Check if the current user is extending their own booking.
            Bookings? existingBooking = activeBookingsForSlot.FirstOrDefault(b => b.UserId == userId);

            if (existingBooking != null)
            {
                // --- Use Case: Extend Booking ---
                existingBooking.EndTime = existingBooking.EndTime.AddHours(durationInHours);
                unitOfWork.Bookings.Update(existingBooking);
                await unitOfWork.CompleteAsync();
                return existingBooking;
            }
            else
            {
                // --- Use Case: Create New Booking ---
                var newBooking = new Bookings
                {
                    ParkingSlotId = slotId,
                    UserId = userId,
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddHours(durationInHours),
                    IsPaid = false
                };
                await unitOfWork.Bookings.AddAsync(newBooking);
                await unitOfWork.CompleteAsync();
                return newBooking;
            }
        }
    }
}
