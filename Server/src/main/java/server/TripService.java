package server;

import model.domain.Trip;
import repository.ITripRepo;

import java.sql.Time;
import java.util.Collection;

public class TripService {
    private final ITripRepo repo;

    public TripService(ITripRepo repo) {
        this.repo = repo;
    }

    public Collection<Trip> getTouristAttractionTrips(String touristAttraction, Time startTime, Time endTime) {
        return repo.getTouristAttractionTrips(touristAttraction, startTime, endTime);
    }

    public Trip getTrip(int id) {
        return repo.getById(id);
    }
}
