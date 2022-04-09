using System;
using Model;

namespace Persistence
{
    public interface IReservationRepo : IRepoInterface<Reservation, Tuple<string, Trip>>
    {
        int getAvailableSeatsForTrip(Trip trip);
    }
}
