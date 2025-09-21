namespace ParkingSystem.Domain.Entities
{
    public class Bookings
    {
        public int Id { get; set; }
        public int ParkingSlotId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsPaid { get; set; }
        public ParkingSlots? ParkingSlot { get; set; }
        public Payments? Payment { get; set; }
    }
}
