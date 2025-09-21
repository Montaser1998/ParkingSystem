using ParkingSystem.Domain.Enums;

namespace ParkingSystem.Infrastructure.Services
{
    public class ToastService
    {
        public event Action<string, ToastLevel> OnShow;
        public event Action OnHide;

        public void ShowToast(string message, ToastLevel level)
        {
            OnShow?.Invoke(message, level);
        }

        public void HideToast()
        {
            OnHide?.Invoke();
        }
    }
}
