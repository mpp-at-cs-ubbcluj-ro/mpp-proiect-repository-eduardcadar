package repository;

import domain.Agency;
import domain.Pair;
import domain.Reservation;
import domain.Trip;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import utils.JdbcUtils;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.Properties;

public class ReservationDBRepo implements ReservationRepo {

    private final JdbcUtils dbUtils;
    private static final Logger logger = LogManager.getLogger();
    private final String tableName = "reservations";
    private final TripRepo tripRepo;

    public ReservationDBRepo(Properties props, TripRepo tripRepo) {
        logger.info("Initializing ReservationDBRepo with properties: {} ", props);
        dbUtils = new JdbcUtils(props);
        this.tripRepo = tripRepo;
    }

    @Override
    public void save(Reservation elem) {
        logger.traceEntry("Saving {}", elem);
        Connection con = dbUtils.getConnection();
        String command = "insert into " + tableName + " (client, id_trip, phone_number, seats) values (?, ?, ?, ?)";
        try (PreparedStatement ps = con.prepareStatement(command)) {
            ps.setString(1, elem.getClient());
            ps.setInt(2, elem.getTrip().getId());
            ps.setString(3, elem.getPhoneNumber());
            ps.setInt(4, elem.getSeats());
            int result = ps.executeUpdate();
            logger.trace("Saved {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
    }

    @Override
    public void delete(Reservation elem) {
        deleteById(elem.getId());
    }

    @Override
    public void deleteById(Pair<String, Trip> id) {
        logger.traceEntry("Deleting reservation with id = {}", id);
        Connection con = dbUtils.getConnection();
        String command = "delete from " + tableName + " where client = ? and id_trip = ?";
        try (PreparedStatement ps = con.prepareStatement(command)) {
            ps.setString(1, id.getFirst());
            ps.setInt(2, id.getSecond().getId());
            int result = ps.executeUpdate();
            logger.trace("Deleted {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
    }

    @Override
    public void update(Reservation elem, Pair<String, Trip> id) {
        logger.traceEntry();
        Connection con = dbUtils.getConnection();
        String command = "update " + tableName + " set phone_number = ?, seats = ? where client = ? and id_trip = ?";
        try (PreparedStatement ps = con.prepareStatement(command)) {
            ps.setString(1, elem.getPhoneNumber());
            ps.setInt(2, elem.getSeats());
            ps.setString(3, id.getFirst());
            ps.setInt(4, id.getSecond().getId());
            int result = ps.executeUpdate();
            logger.trace("Updated {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
    }

    @Override
    public Reservation getById(Pair<String, Trip> id) {
        logger.traceEntry();
        Connection con = dbUtils.getConnection();
        String command = "select * from " + tableName + " where client = ? and id_trip = ?";
        Reservation reservation = null;
        try (PreparedStatement ps = con.prepareStatement(command)) {
            ps.setString(1, id.getFirst());
            ps.setInt(2, id.getSecond().getId());
            ResultSet res = ps.executeQuery();
            res.next();
            String phoneNumber = res.getString("phone_number");
            int seats = res.getInt("seats");
            reservation = new Reservation(id, phoneNumber, seats);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
        return reservation;
    }

    @Override
    public void clear() {
        logger.traceEntry();
        Connection con = dbUtils.getConnection();
        String command = "delete from " + tableName;
        try (PreparedStatement ps = con.prepareStatement(command)) {
            int result = ps.executeUpdate();
            logger.trace("Deleted {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
    }

    @Override
    public Collection<Reservation> getAll() {
        logger.traceEntry();
        Connection con = dbUtils.getConnection();
        String command = "select * from " + tableName;
        List<Reservation> reservations = new ArrayList<>();
        try (PreparedStatement ps = con.prepareStatement(command)) {
            ResultSet res = ps.executeQuery();
            while (res.next()) {
                String client = res.getString("client");
                int id_trip = res.getInt("id_trip");
                Trip trip = tripRepo.getById(id_trip);
                String phoneNumber = res.getString("phone_number");
                int seats = res.getInt("seats");
                reservations.add(new Reservation(new Pair<>(client, trip), phoneNumber, seats));
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
        return reservations;
    }
}
