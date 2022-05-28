using Model.notification;

namespace Services.ams
{
    public interface INotificationSubscriber
    {
        void NotificationReceived(Notification notif);
    }
}