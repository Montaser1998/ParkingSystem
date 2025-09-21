namespace ParkingSystem.Domain.Entities
{
    public class Fines
    {
        public int Id { get; set; }
        public int ParkingSlotId { get; set; }
        public required string VehicleLicensePlate { get; set; }
        public required string Reason { get; set; }
        public decimal Amount { get; set; }
        public DateTime IssueDate { get; set; }
        public bool IsPaid { get; set; }
        public Guid IssuedByUserId { get; set; }
        public ParkingSlots? ParkingSlot { get; set; }
    }
}
