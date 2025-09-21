using ParkingSystem.Domain.Entities;

namespace ParkingSystem.Application.Interfaces
{
    public interface IFineService
    {
        /// <summary>
        /// Creates a new fine and sends a confirmation notification to the issuer.
        /// </summary>
        /// <param name="slotId">The ID of the parking slot.</param>
        /// <param name="licensePlate">The vehicle's license plate.</param>
        /// <param name="reason">The reason for the fine.</param>
        /// <param name="amount">The amount of the fine.</param>
        /// <param name="issuerUserId">The ID of the security user issuing the fine.</param>
        /// <returns>The newly created Fine entity.</returns>
        Task<Fines> CreateFineAndNotifyAsync(int slotId, string licensePlate, string reason, decimal amount, Guid issuerUserId);
    }
}
