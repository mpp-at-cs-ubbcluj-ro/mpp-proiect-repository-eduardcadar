using Model;
using System;
using System.Collections.Generic;

namespace Services.ams
{
    public interface IServicesAMS
    {
        void Login(Agency agency);
        void Logout(Agency agency);

        IEnumerable<Agency> GetAgencies();
        IEnumerable<Trip> GetTrips(string destination, TimeSpan startTime, TimeSpan endTime);
        IEnumerable<Reservation> GetReservations();
        void SaveReservation(Reservation reservation);
    }
}
