using Microsoft.AspNetCore.SignalR;
using ParkingSystem.Application.Interfaces;

namespace ParkingSystem.Infrastructure.Services
{
    public class NotificationHub() : Hub, INotificationService
    {
        public async Task SendNotificationToUserAsync(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
