package utils;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.Properties;

public class JdbcUtils {

    private final Properties jdbcProps;
    private static final Logger logger = LogManager.getLogger();
    private Connection instance = null;

    public JdbcUtils(Properties props){
        jdbcProps = props;
    }

    private Connection getNewConnection(){
        logger.traceEntry();

        String url = jdbcProps.getProperty("jdbc.url");
        String user = jdbcProps.getProperty("jdbc.user");
        String pass = jdbcProps.getProperty("jdbc.pass");
        logger.info("trying to connect to database ... {}",url);
        logger.info("user: {}",user);
        logger.info("pass: {}", pass);
        try {
            if (user != null && pass != null)
                return DriverManager.getConnection(url, user, pass);
            else
                return DriverManager.getConnection(url);
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error getting connection: " + e);
        }
        return null;
    }

    public Connection getConnection(){
        logger.traceEntry();
        try {
            if (instance == null || instance.isClosed())
                instance = getNewConnection();
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB: " + e);
        }
        logger.traceExit(instance);
        return instance;
    }
}
