package repository;

import domain.Agency;
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

public class AgencyDBRepo implements AgencyRepo {

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
    public void delete(Agency elem) {
        deleteById(elem.getId());
    }

    @Override
    public void deleteById(Integer id) {
        logger.traceEntry("Deleting agency with id = {}", id);
        Connection con = dbUtils.getConnection();
        String command = "delete from " + tableName + " where id = ?";
        try (PreparedStatement ps = con.prepareStatement(command)) {
            ps.setInt(1, id);
            int result = ps.executeUpdate();
            logger.trace("Deleted {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
    }

    @Override
    public void update(Agency elem, Integer id) {
        logger.traceEntry();
        Connection con = dbUtils.getConnection();
        String command = "update " + tableName + " set name = ?, password = ? where id = ?";
        try (PreparedStatement ps = con.prepareStatement(command)) {
            ps.setString(1, elem.getName());
            ps.setString(2, elem.getPassword());
            ps.setInt(3, id);
            int result = ps.executeUpdate();
            logger.trace("Updated {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
    }

    @Override
    public Agency getById(Integer id) {
        logger.traceEntry();
        Connection con = dbUtils.getConnection();
        String command = "select * from " + tableName + " where id = ?";
        Agency agency = null;
        try (PreparedStatement ps = con.prepareStatement(command)) {
            ps.setInt(1, id);
            ResultSet res = ps.executeQuery();
            res.next();
            String name = res.getString("name");
            String password = res.getString("password");
            agency = new Agency(id, name, password);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
        return agency;
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
    public Collection<Agency> getAll() {
        logger.traceEntry();
        Connection con = dbUtils.getConnection();
        String command = "select * from " + tableName;
        List<Agency> agencies = new ArrayList<>();
        try (PreparedStatement ps = con.prepareStatement(command)) {
            ResultSet res = ps.executeQuery();
            while (res.next()) {
                int id = res.getInt("id");
                String name = res.getString("name");
                String password = res.getString("password");
                agencies.add(new Agency(id, name, password));
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        logger.traceExit();
        return agencies;
    }

    @Override
    public Agency getByName(String name) {
        logger.traceEntry();
        Connection con = dbUtils.getConnection();
        String command = "select * from " + tableName + " where name = ?";
        Agency agency = null;
        try (PreparedStatement ps = con.prepareStatement(command)) {
            ps.setString(1, name);
            ResultSet res = ps.executeQuery();
            res.next();
            int id = res.getInt("id");
            String password = res.getString("password");
            agency = new Agency(id, name, password);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB: " + e);
        }
        if (agency == null)
            throw new MyException("No agency with this name!");
        logger.traceExit();
        return agency;
    }
}
