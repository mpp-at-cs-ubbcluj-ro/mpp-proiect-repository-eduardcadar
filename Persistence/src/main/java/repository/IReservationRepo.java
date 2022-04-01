package repository;

import model.utils.Pair;
import model.domain.Reservation;
import model.domain.Trip;

public interface IReservationRepo extends IRepo<Reservation, Pair<String, Trip>> {
    int getAvailableSeatsForTrip(Trip trip);
}
