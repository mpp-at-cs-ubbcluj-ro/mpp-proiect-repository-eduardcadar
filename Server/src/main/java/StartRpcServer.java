import repository.*;
import server.*;
import services.IServices;

import java.io.IOException;
import java.util.Properties;

public class StartRpcServer {
    private static final int defaultPort = 55554;

    public static void main(String[] args) {
        Properties serverProps = new Properties();
        try {
            serverProps.load(StartRpcServer.class.getResourceAsStream("/server.properties"));
            System.out.println("Server properties set");
            serverProps.list(System.out);
        } catch (IOException ex) {
            System.err.println("Cannot find servet.properties: " + ex);
            return;
        }
        IAgencyRepo agencyRepo = new AgencyDBRepo(serverProps);
        ITripRepo tripRepo = new TripDBRepo(serverProps);
        IReservationRepo resRepo = new ReservationDBRepo(serverProps, tripRepo);
        AgencyService agencyService = new AgencyService(agencyRepo);
        TripService tripService = new TripService(tripRepo);
        ReservationService reservationService = new ReservationService(resRepo);

        IServices serverImpl = new Service(agencyService, tripService, reservationService);
        int serverPort = defaultPort;
        try {
            serverPort = Integer.parseInt(serverProps.getProperty("server.port"));
        } catch (NumberFormatException ex) {
            System.err.println("Wrong port number: " + ex.getMessage());
            System.err.println("Using default port: " + defaultPort);
        }
        System.out.println("Starting server on port: " + serverPort);
        AbstractServer server = new RpcConcurrentServer(serverPort, serverImpl);
        try {
            server.start();
        } catch (ServerException ex) {
            System.err.println("Error starting server: " + ex.getMessage());
        } finally {
            try {
                server.stop();
            } catch (ServerException ex) {
                System.err.println("Error stopping server: " + ex.getMessage());
            }
        }
    }
}
