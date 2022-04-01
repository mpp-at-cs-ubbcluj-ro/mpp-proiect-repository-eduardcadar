package repository;

import utils.JdbcUtils;
import model.utils.Pair;
import model.domain.Reservation;
import model.domain.Trip;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import model.utils.MyException;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.Properties;

public class ReservationDBRepo implements IReservationRepo {

    private final JdbcUtils dbUtils;
    private static final Logger logger = LogManager.getLogger();
    private final String tableName = "reservations";
    private final ITripRepo tripRepo;

    public ReservationDBRepo(Properties props, ITripRepo tripRepo) {
        logger.info("Initializing ReservationDBRepo with properties: {} ", props);
        dbUtils = new JdbcUtils(props);
        this.tripRepo = tripRepo;
    }

    @Override
    public void save(Reservation elem) throws MyException {
        logger.traceEntry("Saving {}", elem);
        Connection con = dbUtils.getConnection();
        String command = "insert into " + tableName +
                " (client, id_trip, agency, phone_number, seats) values (?, ?, ?, ?, ?)";
        try (PreparedStatement ps = con.prepareStatement(command)) {
            ps.setString(1, elem.getClient());
            ps.setInt(2, elem.getTrip().getId());
            ps.setString(3, elem.getAgency());
            ps.setString(4, elem.getPhoneNumber());
            ps.setInt(5, elem.getSeats());
            int result = ps.executeUpdate();
            logger.trace("Saved {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
            throw new MyException("DB Error" + e);
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
            String agency = res.getString("agency");
            String phoneNumber = res.getString("phone_number");
            int seats = res.getInt("seats");
            reservation = new Reservation(id, agency, phoneNumber, seats);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
        return reservation;
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
                String agency = res.getString("agency");
                String phoneNumber = res.getString("phone_number");
                int seats = res.getInt("seats");
                reservations.add(new Reservation(new Pair<>(client, trip), agency, phoneNumber, seats));
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
        return reservations;
    }

    @Override
    public int getAvailableSeatsForTrip(Trip trip) {
        logger.traceEntry();
        int reservedSeats = 0;
        Connection con = dbUtils.getConnection();
        String command = "select sum(seats) as reserved_seats from " + tableName + " where id_trip = ?";
        try (PreparedStatement ps = con.prepareStatement(command)) {
            ps.setInt(1, trip.getId());
            ResultSet res = ps.executeQuery();
            res.next();
            reservedSeats = res.getInt("reserved_seats");
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
        return trip.getSeats() - reservedSeats;
    }
}
