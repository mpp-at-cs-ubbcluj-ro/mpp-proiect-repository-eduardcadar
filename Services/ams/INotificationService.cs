using Model;

namespace Services.ams
{
    public interface INotificationService
    {
        void NewReservation(Reservation reservation);
    }
}
