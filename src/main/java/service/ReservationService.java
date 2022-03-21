package service;

import domain.Reservation;
import domain.Trip;
import repository.ReservationRepo;
import utils.Pair;

import java.util.Collection;

public class ReservationService {
    private ReservationRepo repo;

    public ReservationService(ReservationRepo repo) {
        this.repo = repo;
    }

    public void saveReservation(String client, Trip trip, String phoneNumber, int seats) {
        repo.save(new Reservation(new Pair<>(client, trip), phoneNumber, seats));
    }

    public Collection<Reservation> getAllReservations() { return repo.getAll(); }

    public int getAvailableSeatsForTrip(Trip trip) { return repo.getAvailableSeatsForTrip(trip); }
}
