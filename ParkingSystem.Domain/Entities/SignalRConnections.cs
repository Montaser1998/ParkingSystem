namespace ParkingSystem.Domain.Entities
{
    public class SignalRConnection
    {
        public int Id { get; set; }
        public required string ConnectionId { get; set; }
        public required string UserId { get; set; }
        public DateTime ConnectedAt { get; set; }
    }
}
