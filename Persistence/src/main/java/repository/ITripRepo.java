package repository;

import model.domain.Trip;

import java.sql.Time;
import java.util.Collection;

public interface ITripRepo extends IRepo<Trip, Integer> {
    Collection<Trip> getTouristAttractionTrips(String touristAttraction, Time startTime, Time endTime);
}
