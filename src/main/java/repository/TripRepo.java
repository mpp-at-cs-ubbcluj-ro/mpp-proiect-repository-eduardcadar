package repository;

import domain.Trip;

import java.time.LocalTime;
import java.util.Collection;

public interface TripRepo extends RepoInterface<Trip, Integer> {
    Collection<Trip> getTouristAttractionTrips(String touristAttraction, LocalTime startTime, LocalTime endTime);
}
