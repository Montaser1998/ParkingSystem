namespace ParkingSystem.Domain.Entities
{
    public class Payments
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public required string PaymentMethod { get; set; }
        public Bookings? Booking { get; set; }
    }
}
