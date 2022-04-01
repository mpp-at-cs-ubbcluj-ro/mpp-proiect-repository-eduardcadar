package model.domain;

import model.utils.Pair;

public class Reservation implements Identifiable<Pair<String, Trip>> {
    private String client;
    private Trip trip;
    private final String agency;
    private final String phoneNumber;
    private final int seats;

    public Reservation(Pair<String, Trip> reservationID, String agency, String phoneNumber, int seats) {
        this.client = reservationID.getFirst();
        this.trip = reservationID.getSecond();
        this.agency = agency;
        this.phoneNumber = phoneNumber;
        this.seats = seats;
    }

    @Override
    public Pair<String, Trip> getId() {
        return new Pair<>(client, trip);
    }

    @Override
    public void setId(Pair<String, Trip> id) {
        client = id.getFirst();
        trip = id.getSecond();
    }

    public String getClient() {
        return client;
    }

    public Trip getTrip() {
        return trip;
    }

    public String getPhoneNumber() {
        return phoneNumber;
    }

    public int getSeats() {
        return seats;
    }

    public String getAgency() {
        return agency;
    }

    @Override
    public String toString() {
        return "Reservation by " + this.client + " on trip to " + this.trip.getTouristAttraction()
                + " by " + this.agency + " for " + this.seats + " seats";
    }
}
