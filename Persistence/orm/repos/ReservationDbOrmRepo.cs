using Model;
using Persistence.orm.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.orm.repos
{
    public class ReservationDbOrmRepo : IReservationRepo
    {
        private readonly AgenciesContext _context;

        public ReservationDbOrmRepo(string connectionString)
        {
            _context = DBUtils.GetDbContext(connectionString);
        }

        public IEnumerable<Reservation> GetAll()
        {
            var reservationEntities = _context.Reservations.ToArray();
            IEnumerable<Reservation> reservations = reservationEntities
                .Select(r => EntityUtils.ReservationEntityToReservation(r));
            return reservations;
        }

        public int getAvailableSeatsForTrip(Trip trip)
        {
            throw new NotImplementedException();
        }

        public Reservation GetById(Tuple<string, Trip> id)
        {
            var reservationEntity = _context.Reservations
                .SingleOrDefault(r => r.TripId == id.Item2.Id
                && r.Client == id.Item1);
            var reservation = EntityUtils.ReservationEntityToReservation(reservationEntity);
            return reservation;
        }

        public void Save(Reservation elem)
        {
            var reservationEntity = EntityUtils.ReservationToReservationEntity(elem);
            _context.Reservations.Add(reservationEntity);
            _context.SaveChanges();
        }
    }
}
