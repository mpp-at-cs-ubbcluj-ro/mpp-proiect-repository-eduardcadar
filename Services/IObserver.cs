using Model;

namespace Services
{
    public interface IObserver
    {
        void ReservationSaved(Reservation reservation);
    }
}
