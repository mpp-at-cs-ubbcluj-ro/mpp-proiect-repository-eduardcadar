package service;

import domain.Agency;
import domain.Reservation;
import domain.Trip;

import java.sql.Time;
import java.util.Collection;

public class Service {
    AgencyService agencySrv;
    TripService tripSrv;
    ReservationService resSrv;

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
}
