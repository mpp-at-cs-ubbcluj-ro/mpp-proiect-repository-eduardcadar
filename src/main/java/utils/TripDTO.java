package utils;

import java.sql.Time;

public class TripDTO {
    private int id;
    private final String touristAttraction;
    private final String transportCompany;
    private final Time departureTime;
    private final double price;
    private final int availableSeats;

    public TripDTO(int id, String touristAttraction, String transportCompany, Time departureTime, double price, int availableSeats) {
        this.id = id;
        this.touristAttraction = touristAttraction;
        this.transportCompany = transportCompany;
        this.departureTime = departureTime;
        this.price = price;
        this.availableSeats = availableSeats;
    }

    public TripDTO(String touristAttraction, String transportCompany, Time departureTime, double price, int availableSeats) {
        this.touristAttraction = touristAttraction;
        this.transportCompany = transportCompany;
        this.departureTime = departureTime;
        this.price = price;
        this.availableSeats = availableSeats;
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

    public int getAvailableSeats() {
        return availableSeats;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }
}
