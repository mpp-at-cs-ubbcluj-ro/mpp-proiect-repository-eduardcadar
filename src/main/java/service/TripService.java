package service;

import domain.Trip;
import repository.TripRepo;

import java.sql.Time;
import java.util.Collection;

public class TripService {
    TripRepo repo;

    public TripService(TripRepo repo) {
        this.repo = repo;
    }

    public Collection<Trip> getTouristAttractionTrips(String touristAttraction, Time startTime, Time endTime) {
        return repo.getTouristAttractionTrips(touristAttraction, startTime, endTime);
    }

    public Collection<Trip> getAllTrips() {
        return repo.getAll();
    }

    public Trip getTrip(int id) {
        return repo.getById(id);
    }
}
