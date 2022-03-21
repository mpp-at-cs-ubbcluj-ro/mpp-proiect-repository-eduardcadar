package service;

import domain.Agency;
import domain.Reservation;
import domain.Trip;
import utils.TripDTO;

import java.sql.Time;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

public class Service {
    private AgencyService agencySrv;
    private TripService tripSrv;
    private ReservationService resSrv;

    public Service(AgencyService agencySrv, TripService tripSrv, ReservationService resSrv) {
        this.agencySrv = agencySrv;
        this.tripSrv = tripSrv;
        this.resSrv = resSrv;
    }

    public Collection<Trip> getTouristAttractionTrips(String touristAttraction, Time startTime, Time endTime) {
        return tripSrv.getTouristAttractionTrips(touristAttraction, startTime, endTime);
    }

    public void saveReservation(String client, Trip trip, String phoneNumber, int seats) {
        resSrv.saveReservation(client, trip, phoneNumber, seats);
    }

    public Collection<Agency> getAllAgencies() {
        return agencySrv.getAllAgencies();
    }

    public Collection<Trip> getAllTrips() { return tripSrv.getAllTrips(); }

    public Collection<Reservation> getAllReservations() { return resSrv.getAllReservations(); }

    public Trip getTrip(int id) { return tripSrv.getTrip(id); }

    public Agency getAgencyByName(String name) { return agencySrv.getByName(name); }

    public int getAvailableSeatsForTrip(Trip trip) { return resSrv.getAvailableSeatsForTrip(trip); }

    public Collection<TripDTO> getTripDTOs(Collection<Trip> trips) {
        List<TripDTO> tripDTOs = new ArrayList<>();
        trips.forEach(t -> {
                    int availableSeats = resSrv.getAvailableSeatsForTrip(t);
                    TripDTO tripDTO = new TripDTO(t.getId(), t.getTouristAttraction(), t.getTransportCompany(),
                            t.getDepartureTime(), t.getPrice(), availableSeats);
                    tripDTOs.add(tripDTO);
                });
        return tripDTOs;
    }
}
