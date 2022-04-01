package server;

import model.domain.Reservation;
import model.domain.Trip;
import model.utils.MyException;
import repository.IReservationRepo;

import java.util.Collection;

public class ReservationService {
    private final IReservationRepo repo;

    public ReservationService(IReservationRepo repo) {
        this.repo = repo;
    }

    public void saveReservation(Reservation reservation) throws MyException { repo.save(reservation); }

    public Collection<Reservation> getAllReservations() { return repo.getAll(); }

    public int getAvailableSeatsForTrip(Trip trip) { return repo.getAvailableSeatsForTrip(trip); }
}
