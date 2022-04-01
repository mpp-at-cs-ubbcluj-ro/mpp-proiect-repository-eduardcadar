package repository;

import model.domain.Trip;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import utils.JdbcUtils;

import java.sql.*;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.Properties;

public class TripDBRepo implements ITripRepo {

    private final JdbcUtils dbUtils;
    private static final Logger logger = LogManager.getLogger();
    private final String tableName = "trips";

    public TripDBRepo(Properties props) {
        logger.info("Initializing TripDBRepo with properties: {} ", props);
        dbUtils = new JdbcUtils(props);
    }

    @Override
    public void save(Trip elem) {
        logger.traceEntry("Saving {}", elem);
        Connection con = dbUtils.getConnection();
        String command = "insert into " + tableName +
                " (tourist_attraction, transport_company, departure_time, price, seats)" +
                " values (?, ?, ?, ?, ?)";
        try (PreparedStatement ps = con.prepareStatement(command)) {
            ps.setString(1, elem.getTouristAttraction());
            ps.setString(2, elem.getTransportCompany());
            ps.setString(3, elem.getDepartureTime().toString());
            ps.setDouble(4, elem.getPrice());
            ps.setInt(5, elem.getSeats());
            int result = ps.executeUpdate();
            logger.trace("Saved {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
    }

    @Override
    public Trip getById(Integer id) {
        logger.traceEntry();
        Connection con = dbUtils.getConnection();
        String command = "select * from " + tableName + " where id = ?";
        Trip trip = null;
        try (PreparedStatement ps = con.prepareStatement(command)) {
            ps.setInt(1, id);
            ResultSet res = ps.executeQuery();
            res.next();
            String touristAttraction = res.getString("tourist_attraction");
            String transportCompany = res.getString("transport_company");
            String departureTime = res.getString("departure_time");
            double price = res.getDouble("price");
            int seats = res.getInt("seats");
            trip = new Trip(id, touristAttraction, transportCompany, Time.valueOf(departureTime), price, seats);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
        return trip;
    }

    @Override
    public Collection<Trip> getAll() {
        logger.traceEntry();
        Connection con = dbUtils.getConnection();
        String command = "select * from " + tableName;
        List<Trip> trips = new ArrayList<>();
        try (PreparedStatement ps = con.prepareStatement(command)) {
            ResultSet res = ps.executeQuery();
            while (res.next()) {
                int id = res.getInt("id");
                String touristAttraction = res.getString("tourist_attraction");
                String transportCompany = res.getString("transport_company");
                String departureTime = res.getString("departure_time");
                double price = res.getDouble("price");
                int seats = res.getInt("seats");
                trips.add(new Trip(id, touristAttraction, transportCompany, Time.valueOf(departureTime), price, seats));
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
        return trips;
    }

    @Override
    public Collection<Trip> getTouristAttractionTrips(String touristAttraction, Time startTime, Time endTime) {
        logger.traceEntry();
        Connection con = dbUtils.getConnection();
        String command = "select * from " + tableName +
                " where tourist_attraction like ?";
        List<Trip> trips = new ArrayList<>();
        try (PreparedStatement ps = con.prepareStatement(command)) {
            ps.setString(1, "%" + touristAttraction + "%");
            ResultSet res = ps.executeQuery();
            while (res.next()) {
                int id = res.getInt("id");
                String destination = res.getString("tourist_attraction");
                String transportCompany = res.getString("transport_company");
                String departureTime = res.getString("departure_time");
                double price = res.getDouble("price");
                int seats = res.getInt("seats");
                trips.add(new Trip(id, destination, transportCompany, Time.valueOf(departureTime), price, seats));
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
        return trips.stream()
                .filter(t -> t.getDepartureTime().after(startTime)
                        && t.getDepartureTime().before(endTime)).toList();
    }
}
