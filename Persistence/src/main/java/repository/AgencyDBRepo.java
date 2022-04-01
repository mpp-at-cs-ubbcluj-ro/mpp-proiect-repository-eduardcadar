package repository;

import model.domain.Agency;
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

public class AgencyDBRepo implements IAgencyRepo {

    private final JdbcUtils dbUtils;
    private static final Logger logger = LogManager.getLogger();
    private final String tableName = "agencies";

    public AgencyDBRepo(Properties props) {
        logger.info("Initializing AgencyDBRepo with properties: {} ", props);
        dbUtils = new JdbcUtils(props);
    }

    @Override
    public void save(Agency elem) {
        logger.traceEntry("Saving {}", elem);
        Connection con = dbUtils.getConnection();
        String command = "insert into " + tableName + " (name, password) values (?, ?)";
        try (PreparedStatement ps = con.prepareStatement(command)) {
            ps.setString(1, elem.getName());
            ps.setString(2, elem.getPassword());
            int result = ps.executeUpdate();
            logger.trace("Saved {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
    }

    @Override
    public Agency getById(String id) {
        logger.traceEntry();
        Connection con = dbUtils.getConnection();
        String command = "select * from " + tableName + " where name = ?";
        Agency agency = null;
        try (PreparedStatement ps = con.prepareStatement(command)) {
            ps.setString(1, id);
            ResultSet res = ps.executeQuery();
            res.next();
            String password = res.getString("password");
            agency = new Agency(id, password);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
        return agency;
    }

    @Override
    public Collection<Agency> getAll() {
        logger.traceEntry();
        Connection con = dbUtils.getConnection();
        String command = "select * from " + tableName;
        List<Agency> agencies = new ArrayList<>();
        try (PreparedStatement ps = con.prepareStatement(command)) {
            ResultSet res = ps.executeQuery();
            while (res.next()) {
                String name = res.getString("name");
                String password = res.getString("password");
                agencies.add(new Agency(name, password));
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
        return agencies;
    }

    @Override
    public Agency getAgency(String name, String password) {
        logger.traceEntry();
        Connection con = dbUtils.getConnection();
        String command = "select * from " + tableName + " where name = ? and password = ?";
        Agency agency = null;
        try (PreparedStatement ps = con.prepareStatement(command)) {
            ps.setString(1, name);
            ps.setString(2, password);
            ResultSet res = ps.executeQuery();
            if (res.next()) {
                agency = new Agency(name, password);
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
        return agency;
    }
}
