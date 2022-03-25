using System;
using System.Collections.Generic;
using AgentiiDeTurism.src.domain;
using AgentiiDeTurism.src.repository;

namespace AgentiiDeTurism.src.service
{
    public class TripService
    {
        private ITripRepo repo;
        public TripService(ITripRepo repo)
        {
            this.repo = repo;
        }
        public ICollection<Trip> getTouristAttractionTrips(string touristAttraction, TimeSpan startTime, TimeSpan endTime)
        {
            return repo.getTouristAttractionTrips(touristAttraction, startTime, endTime);
        }

        public ICollection<Trip> getAllTrips()
        {
            return repo.getAll();
        }

        public Trip getTrip(int id)
        {
            return repo.getById(id);
        }
    }
}