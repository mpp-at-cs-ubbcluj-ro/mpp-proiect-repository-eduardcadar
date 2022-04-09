using System;
using System.Collections.Generic;
using Model;
using Persistence;

namespace Server.server
{
    public class TripService
    {
        private readonly ITripRepo _repo;
        public TripService(ITripRepo repo)
        {
            _repo = repo;
        }
        public IEnumerable<Trip> GetTouristAttractionTrips(string touristAttraction, TimeSpan startTime, TimeSpan endTime) =>
            _repo.GetTouristAttractionTrips(touristAttraction, startTime, endTime);
    }
}