using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.Notifications
{
    public static class NotificationService
    {
        public static INotificationService Service { get; }

        static NotificationService()
        {
            Service = new EmailNotification();
        }
    }
}
