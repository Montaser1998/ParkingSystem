using ParkingSystem.Application.Interfaces;
using ParkingSystem.Domain.Entities;

namespace ParkingSystem.Infrastructure.Services
{
    public class FineService(IUnitOfWork unitOfWork, IUserService userService, NotificationHubClient notification) : IFineService
    {
        public async Task<Fines> CreateFineAndNotifyAsync(int slotId, string licensePlate, string reason, decimal amount, Guid issuerUserId)
        {
            var fine = new Fines
            {
                ParkingSlotId = slotId,
                VehicleLicensePlate = licensePlate,
                Reason = reason,
                Amount = amount,
                IssueDate = DateTime.UtcNow,
                IssuedByUserId = issuerUserId,
                IsPaid = false
            };

            await unitOfWork.Fines.AddAsync(fine);
            await unitOfWork.CompleteAsync();

            var carOwner = await userService.FindUserByLicensePlateAsync(licensePlate);

            if (carOwner != null)
            {
                var ownerMessage = $"A fine of {amount:C} was issued for your vehicle ({licensePlate}) for: {reason}.";
                await notification.SendNotificationAsync(ownerMessage);
            }

            return fine;
        }
    }
}
