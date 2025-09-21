namespace ParkingSystem.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationToUserAsync(string message);
    }
}
