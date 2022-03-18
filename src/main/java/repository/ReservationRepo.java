package repository;

import utils.Pair;
import domain.Reservation;
import domain.Trip;

public interface ReservationRepo extends RepoInterface<Reservation, Pair<String, Trip>> {
}
