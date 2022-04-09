using System;
using System.Collections.Generic;
using Model;
using Persistence;

namespace Server.server
{
    public class ReservationService
    {
        private readonly IReservationRepo _repo;

        public ReservationService(IReservationRepo repo)
        {
            _repo = repo;
        }

        public void SaveReservation(Reservation reservation) =>
            _repo.Save(reservation);

        public IEnumerable<Reservation> GetAllReservations() => _repo.GetAll();

        public int GetAvailableSeatsForTrip(Trip trip) => _repo.getAvailableSeatsForTrip(trip);
    }
}
