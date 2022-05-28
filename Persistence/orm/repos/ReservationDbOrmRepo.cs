using Model;
using Persistence.orm.entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.orm.repos
{
    public class ReservationDbOrmRepo : IReservationRepo
    {
        private readonly AgenciesContext _context;

        public ReservationDbOrmRepo(AgenciesContext context)
        {
            _context = context;
        }

        public IEnumerable<Reservation> GetAll()
        {
            var reservationEntities = _context.Reservations.ToArray();
            foreach (var r in reservationEntities) {
                r.Trip = _context.Trips.Single(t => t.Id == r.TripId);
                r.Agency = _context.Agencies.Single(a => a.Name == r.AgencyId);
            }
            IEnumerable<Reservation> reservations = reservationEntities
                .Select(r => EntityUtils.ReservationEntityToReservation(r))
                .ToList();
            return reservations;
        }

        public Reservation GetById(Tuple<string, Trip> id)
        {
            var reservationEntity = _context.Reservations
                .SingleOrDefault(r => r.TripId == id.Item2.Id
                && r.Client == id.Item1);
            var reservation = EntityUtils.ReservationEntityToReservation(reservationEntity);
            return reservation;
        }

        public Reservation Save(Reservation elem)
        {
            var reservationEntity = EntityUtils.ReservationToReservationEntity(elem);
            _context.Reservations.Add(reservationEntity);
            _context.SaveChanges();
            return EntityUtils.ReservationEntityToReservation(reservationEntity);
        }
    }
}
