using Model;
using Persistence.orm.entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.orm.repos
{
    public class TripDbOrmRepo : ITripRepo
    {
        private readonly AgenciesContext _context;

        public TripDbOrmRepo(string connectionString)
        {
            _context = DBUtils.GetDbContext(connectionString);
        }

        public IEnumerable<Trip> GetAll()
        {
            var tripEntities = _context.Trips.ToArray();
            IEnumerable<Trip> trips = tripEntities
                .Select(t => EntityUtils.TripEntityToTrip(t));
            return trips;
        }

        public Trip GetById(int id)
        {
            var tripEntity = _context.Trips.Find(id);
            var trip = EntityUtils.TripEntityToTrip(tripEntity);
            return trip;
        }

        public IEnumerable<Trip> GetTouristAttractionTrips(string touristAttraction, TimeSpan startTime, TimeSpan endTime)
        {
            var tripEntities = _context.Trips
                .Where(t => t.TouristAttraction.Contains(touristAttraction)
                && t.DepartureTime >= startTime
                && t.DepartureTime <= endTime);
            IEnumerable<Trip> trips = tripEntities
                .Select(t => EntityUtils.TripEntityToTrip(t));
            return trips;
        }

        public void Save(Trip elem)
        {
            var tripEntity = EntityUtils.TripToTripEntity(elem);
            _context.Trips.Add(tripEntity);
            _context.SaveChanges();
        }
    }
}
