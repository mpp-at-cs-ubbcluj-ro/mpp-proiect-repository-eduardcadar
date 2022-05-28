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

        public TripDbOrmRepo(AgenciesContext context)
        {
            _context = context;
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
            var tripEntity = _context.Trips.SingleOrDefault(t => t.Id.Equals(id));
            if (tripEntity == null)
                return null;
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

        public Trip Save(Trip elem)
        {
            var tripEntity = EntityUtils.TripToTripEntity(elem);
            _context.Trips.Add(tripEntity);
            _context.SaveChanges();
            return EntityUtils.TripEntityToTrip(tripEntity);
        }

        public Trip Update(int id, Trip elem)
        {
            var tripEntity = _context.Trips.SingleOrDefault(t => t.Id.Equals(id));
            if (tripEntity == null)
                return null;
            tripEntity.TouristAttraction = elem.TouristAttraction;
            tripEntity.TransportCompany = elem.TransportCompany;
            tripEntity.DepartureTime = elem.DepartureTime;
            tripEntity.Price = elem.Price;
            tripEntity.Seats = elem.Seats;
            _context.SaveChanges();
            return EntityUtils.TripEntityToTrip(tripEntity);
        }

        public Trip Delete(int id)
        {
            var tripEntity = _context.Trips.SingleOrDefault(t => t.Id.Equals(id));
            if (tripEntity == null)
                return null;
            _context.Trips.Remove(tripEntity);
            _context.SaveChanges();
            return EntityUtils.TripEntityToTrip(tripEntity);
        }
    }
}
