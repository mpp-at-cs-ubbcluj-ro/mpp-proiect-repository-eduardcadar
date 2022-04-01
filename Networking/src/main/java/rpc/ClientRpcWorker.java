package rpc;

import dto.TripFilterDTO;
import model.domain.Agency;
import model.domain.Reservation;
import model.domain.Trip;
import model.utils.MyException;
import services.IObserver;
import services.IServices;
import services.ServiceException;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.util.Collection;

public class ClientRpcWorker implements Runnable, IObserver {
    private final IServices server;
    private final Socket connection;

    private ObjectInputStream input;
    private ObjectOutputStream output;
    private volatile boolean connected;

    private static final Response okResponse = new Response.Builder().type(ResponseType.OK).build();

    public ClientRpcWorker(IServices server, Socket connection) {
        this.server = server;
        this.connection = connection;
        try {
            output = new ObjectOutputStream(connection.getOutputStream());
            output.flush();
            input = new ObjectInputStream(connection.getInputStream());
            connected = true;
        } catch (IOException ex) {
            ex.printStackTrace();
        }
    }

    @Override
    public void run() {
        while (connected) {
            try {
                Object request = input.readObject();
                Response response = handleRequest((Request)request);
                if (response != null)
                    sendResponse(response);
            } catch (IOException | ClassNotFoundException ex) {
                ex.printStackTrace();
            }
            try {
                Thread.sleep(500);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
        try {
            input.close();
            output.close();
            connection.close();
        } catch (IOException ex) {
            System.out.println("Error " + ex);
        }
    }

    private Response handleRequest(Request request) {
        if (request.type() == RequestType.LOGIN) return handleLogin(request);
        if (request.type() == RequestType.LOGOUT) return handleLogout(request);
        if (request.type() == RequestType.GET_AGENCIES) return handleGetAgencies(request);
        if (request.type() == RequestType.GET_TRIPS) return handleGetTrips(request);
        if (request.type() == RequestType.GET_RESERVATIONS) return handleGetReservations(request);
        if (request.type() == RequestType.SAVE_RESERVATION) return handleSaveReservation(request);

        return null;
    }

    private Response handleSaveReservation(Request request) {
        System.out.println("Save reservation request.." + request.type());
        Reservation reservation = (Reservation) request.data();
        try {
            server.saveReservation(reservation);
            return okResponse;
        } catch (ServiceException e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    private Response handleGetReservations(Request request) {
        System.out.println("Get reservations request.." + request.type());
        try {
            Collection<Reservation> reservationsCol = server.getReservations();
            Reservation[] reservations = reservationsCol.toArray(new Reservation[0]);
            return new Response.Builder().type(ResponseType.GET_RESERVATIONS).data(reservations).build();
        } catch (ServiceException ex) {
            connected = false;
            return new Response.Builder().type(ResponseType.ERROR).data(ex.getMessage()).build();
        }
    }

    private Response handleGetTrips(Request request) {
        System.out.println("Get trips request.." + request.type());
        TripFilterDTO fDTO = (TripFilterDTO) request.data();
        try {
            Collection<Trip> tripsCol = server.getTrips(fDTO.destination(), fDTO.startTime(), fDTO.endTime());
            Trip[] trips = tripsCol.toArray(new Trip[0]);
            return new Response.Builder().type(ResponseType.GET_TRIPS).data(trips).build();
        } catch (ServiceException ex) {
            connected = false;
            return new Response.Builder().type(ResponseType.ERROR).data(ex.getMessage()).build();
        }
    }

    private Response handleGetAgencies(Request request) {
        System.out.println("Get agencies request.." + request.type());
        try {
            Collection<Agency> agenciesCol = server.getAgencies();
            Agency[] agencies = agenciesCol.toArray(new Agency[0]);
            return new Response.Builder().type(ResponseType.GET_AGENCIES).data(agencies).build();
        } catch (ServiceException ex) {
            connected = false;
            return new Response.Builder().type(ResponseType.ERROR).data(ex.getMessage()).build();
        }
    }

    private Response handleLogout(Request request) {
        System.out.println("Logout request.." + request.type());
        Agency agency = (Agency) request.data();
        try {
            server.logout(agency, this);
            connected = false;
            return okResponse;
        } catch (ServiceException ex) {
            return new Response.Builder().type(ResponseType.ERROR).data(ex.getMessage()).build();
        }
    }

    private Response handleLogin(Request request) {
        System.out.println("Login request.." + request.type());
        Agency agency = (Agency) request.data();
        try {
            server.login(agency, this);
            return okResponse;
        } catch (ServiceException ex) {
            connected = false;
            return new Response.Builder().type(ResponseType.ERROR).data(ex.getMessage()).build();
        }
    }

    private void sendResponse(Response response) throws IOException {
        System.out.println("Sending response " + response);
        output.writeObject(response);
        output.flush();
    }

    @Override
    public void ReservationSaved(Reservation reservation) throws ServiceException {
        Response response = new Response.Builder().type(ResponseType.NEW_RESERVATION).data(reservation).build();
        System.out.println("Reservation saved: " + reservation);
        try {
            sendResponse(response);
        } catch (IOException e) {
            throw new ServiceException("Reservation saved error: ", e);
        }
    }
}
