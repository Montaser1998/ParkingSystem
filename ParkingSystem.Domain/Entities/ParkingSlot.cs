namespace ParkingSystem.Domain.Entities
{
    public class ParkingSlots
    {
        public int Id { get; set; }
        public required string SlotNumber { get; set; }
        public ICollection<Bookings> Bookings { get; set; } = [];
    }
}
