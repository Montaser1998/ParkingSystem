using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ParkingSystem.Application.Interfaces;

namespace ParkingSystem.Infrastructure.Services
{
    public class BookingExpirationService : BackgroundService
    {
        private readonly ILogger<BookingExpirationService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public BookingExpirationService(IServiceProvider serviceProvider, ILogger<BookingExpirationService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Booking Expiration Service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Checking for expired bookings...");
                    await CheckForExpiredBookingsAsync(stoppingToken);
                    _logger.LogInformation("Finished checking for expired bookings. Waiting for next cycle.");
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An unexpected error occurred while checking for expired bookings.");
                }

                try
                {
                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }

            _logger.LogInformation("Booking Expiration Service is stopping.");
        }
        private async Task CheckForExpiredBookingsAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Checking for expired bookings...");

            using (var scope = _serviceProvider.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var notificationClient = scope.ServiceProvider.GetRequiredService<NotificationHubClient>();
                var expiredBookings = await unitOfWork.Bookings.FindExpiredBookingsAsync();

                foreach (var booking in expiredBookings)
                {
                    await unitOfWork.CompleteAsync();

                    await notificationClient.SendNotificationAsync("Your booking has expired.");
                }
            }

            _logger.LogInformation("Finished checking for expired bookings.");
        }
    }
}
