package client.dtos;

import model.domain.Trip;

import java.sql.Time;

public class TripDTO {
    private int id;
    private final Trip trip;
    private final String touristAttraction;
    private final String transportCompany;
    private final Time departureTime;
    private final double price;
    private int availableSeats;

    public TripDTO(int id, Trip trip, String touristAttraction, String transportCompany, Time departureTime,
                   double price, int availableSeats) {
        this.id = id;
        this.trip = trip;
        this.touristAttraction = touristAttraction;
        this.transportCompany = transportCompany;
        this.departureTime = departureTime;
        this.price = price;
        this.availableSeats = availableSeats;
    }

    public Trip getTrip() {
        return trip;
    }

    public int getAvailableSeats() {
        return availableSeats;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getTouristAttraction() {
        return touristAttraction;
    }

    public String getTransportCompany() {
        return transportCompany;
    }

    public Time getDepartureTime() {
        return departureTime;
    }

    public double getPrice() {
        return price;
    }

    public void setAvailableSeats(int availableSeats) {
        this.availableSeats = availableSeats;
    }
}
