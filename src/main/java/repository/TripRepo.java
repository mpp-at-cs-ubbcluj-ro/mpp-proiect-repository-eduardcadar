package repository;

import domain.Trip;

import java.sql.Time;
import java.util.Collection;

public interface TripRepo extends RepoInterface<Trip, Integer> {
    Collection<Trip> getTouristAttractionTrips(String touristAttraction, Time startTime, Time endTime);
}
