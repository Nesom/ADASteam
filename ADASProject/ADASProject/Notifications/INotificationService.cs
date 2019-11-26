using ADASProject.Order;
using System.Threading.Tasks;

namespace ADASProject.Notifications
{
    public interface INotificationService
    {
        Task SendOrderNotificationAsync(string email, OrderInfo order, Status type);
        Task SendRegisterNotificationAsync(string email, string password, RegisterMailType type);
    }

    public enum RegisterMailType { Registered }
}
