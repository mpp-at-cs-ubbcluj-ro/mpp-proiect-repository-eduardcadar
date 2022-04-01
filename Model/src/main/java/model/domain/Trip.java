package model.domain;

import java.sql.Time;

public class Trip implements Identifiable<Integer> {
    private int id;
    private final String touristAttraction;
    private final String transportCompany;
    private final Time departureTime;
    private final double price;
    private final int seats;

    public Trip(String touristAttraction, String transportCompany, Time departureTime, double price, int seats) {
        this.touristAttraction = touristAttraction;
        this.transportCompany = transportCompany;
        this.departureTime = departureTime;
        this.price = price;
        this.seats = seats;
    }

    public Trip(int id, String touristAttraction, String transportCompany, Time departureTime, double price, int seats) {
        this.id = id;
        this.touristAttraction = touristAttraction;
        this.transportCompany = transportCompany;
        this.departureTime = departureTime;
        this.price = price;
        this.seats = seats;
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

    public int getSeats() {
        return seats;
    }

    @Override
    public Integer getId() {
        return id;
    }

    @Override
    public void setId(Integer id) {
        this.id = id;
    }

    @Override
    public String toString() {
        return "Trip to " + this.touristAttraction + " by " + this.transportCompany +
                " at " + this.departureTime + ", price: " + this.price + " seats: " + this.seats;
    }
}
