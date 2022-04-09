using System;
using System.Collections.Generic;
using Model;

namespace Services
{
    public interface IServices
    {
        void Login(Agency agency, IObserver observer);
        void Logout(Agency agency, IObserver observer);

        IEnumerable<Agency> GetAgencies();
        IEnumerable<Trip> GetTrips(string destination, TimeSpan startTime, TimeSpan endTime);
        IEnumerable<Reservation> GetReservations();
        void SaveReservation(Reservation reservation);
    }
}
