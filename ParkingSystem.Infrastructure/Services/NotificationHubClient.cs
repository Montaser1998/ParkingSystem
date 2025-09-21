using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using ParkingSystem.Domain.Enums;
using ParkingSystem.Infrastructure.Services;

public class NotificationHubClient
{
    private readonly HubConnection _hubConnection;
    private readonly ILogger<NotificationHubClient> _logger;
    private readonly NavigationManager _navigationManager;
    private bool _isConnectionStarted = false;

    public NotificationHubClient(ILogger<NotificationHubClient> logger, NavigationManager navigationManager, ToastService toastService)
    {
        _logger = logger;
        _navigationManager = navigationManager;


        var hubUrl = _navigationManager.ToAbsoluteUri("/notificationHub");
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(hubUrl, options =>
            {
                options.UseDefaultCredentials = true;
            })
            .WithAutomaticReconnect()
            .ConfigureLogging(logging => logging.AddConsole())
            .Build();


        _hubConnection.On<string>("ReceiveNotification", (message) =>
        {
            toastService.ShowToast(message, ToastLevel.Danger);
        });
    }

    public async Task SendNotificationAsync(string message)
    {
        if (!await EnsureConnectionAsync())
        {
            _logger.LogWarning("Cannot send notification, SignalR connection is not available.");
            return;
        }

        try
        {
            await _hubConnection.InvokeAsync("SendNotificationToUserAsync", message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error invoking SendNotificationToUserAsync on the hub.");
        }
    }
    private async Task<bool> EnsureConnectionAsync()
    {
        if (_hubConnection.State == HubConnectionState.Connected)
        {
            return true;
        }

        if (_isConnectionStarted)
        {
            return false;
        }

        try
        {
            _logger.LogInformation("Attempting to start SignalR connection...");
            await _hubConnection.StartAsync();
            _isConnectionStarted = true;
            _logger.LogInformation("SignalR connection started successfully.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to start SignalR connection. This is expected if the user is not authenticated.");
            _isConnectionStarted = true;
            return false;
        }
    }
}
