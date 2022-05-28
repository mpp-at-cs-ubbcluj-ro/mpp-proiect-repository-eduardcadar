namespace Services.ams
{
    public interface INotificationReceiver
    {
        void Start(INotificationSubscriber subscriber);
        void Stop();
    }
}
