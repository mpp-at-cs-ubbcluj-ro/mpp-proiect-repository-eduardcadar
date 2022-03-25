using System;
using System.Collections.Generic;
using AgentiiDeTurism.src.domain;
using AgentiiDeTurism.src.repository;

namespace AgentiiDeTurism.src.service
{
    public class ReservationService
    {
        private IReservationRepo repo;

        public ReservationService(IReservationRepo repo)
        {
            this.repo = repo;
        }

        public void saveReservation(String client, Trip trip, String phoneNumber, int seats)
        {
            repo.save(new Reservation(new Tuple<string, Trip>(client, trip), phoneNumber, seats));
        }

        public ICollection<Reservation> getAllReservations() { return repo.getAll(); }

        public int getAvailableSeatsForTrip(Trip trip) { return repo.getAvailableSeatsForTrip(trip); }
    }
}
