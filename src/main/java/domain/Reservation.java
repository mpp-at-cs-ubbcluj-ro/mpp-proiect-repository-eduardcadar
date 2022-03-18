package domain;

import utils.Pair;

public class Reservation implements Identifiable<Pair<String, Trip>> {
    private String client;
    private Trip trip;
    private String phoneNumber;
    private int seats;

    public Reservation(Pair<String, Trip> reservationID, String phoneNumber, int seats) {
        this.client = reservationID.getFirst();
        this.trip = reservationID.getSecond();
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

    public void setClient(String client) {
        this.client = client;
    }

    public Trip getTrip() {
        return trip;
    }

    public void setTrip(Trip trip) {
        this.trip = trip;
    }

    public String getPhoneNumber() {
        return phoneNumber;
    }

    public int getSeats() {
        return seats;
    }

    public void setPhoneNumber(String phoneNumber) {
        this.phoneNumber = phoneNumber;
    }

    public void setSeats(int seats) {
        this.seats = seats;
    }

    @Override
    public String toString() {
        return "Reservation by " + this.client + " on trip to " + this.trip.getTouristAttraction()
                + " for " + this.seats + " seats";
    }
}
