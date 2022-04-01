package rpc;

import dto.TripFilterDTO;
import model.domain.Agency;
import model.domain.Reservation;
import model.domain.Trip;
import services.IObserver;
import services.IServices;
import services.ServiceException;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.sql.Time;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

public class ServicesRpcProxy implements IServices {
    private final String host;
    private final int port;

    private IObserver client;

    private ObjectInputStream input;
    private ObjectOutputStream output;
    private Socket connection;

    private final BlockingQueue<Response> qresponses;
    private volatile boolean finished;

    public ServicesRpcProxy(String host, int port) {
        this.host = host;
        this.port = port;
        qresponses = new LinkedBlockingQueue<>();
    }

    @Override
    public void login(Agency agency, IObserver observer) throws ServiceException {
        initializeConnection();

        Request request = new Request.Builder().type(RequestType.LOGIN).data(agency).build();
        sendRequest(request);
        Response response = readResponse();
        if (response.type() == ResponseType.OK) {
            this.client = observer;
            return;
        }
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            closeConnection();
            throw new ServiceException(err);
        }
    }

    @Override
    public void logout(Agency agency, IObserver observer) throws ServiceException {
        Request request = new Request.Builder().type(RequestType.LOGOUT).data(agency).build();
        sendRequest(request);
        Response response = readResponse();
        closeConnection();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            throw new ServiceException(err);
        }
    }

    @Override
    public Collection<Agency> getAgencies() throws ServiceException {
        Request request = new Request.Builder().type(RequestType.GET_AGENCIES).build();
        sendRequest(request);
        Response response = readResponse();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            throw new ServiceException(err);
        }
        Agency[] agencies = (Agency[]) response.data();
        return new ArrayList<>(Arrays.asList(agencies));
    }

    @Override
    public Collection<Trip> getTrips(String destination, Time startTime, Time endTime) throws ServiceException {
        TripFilterDTO filterDTO = new TripFilterDTO(destination, startTime, endTime);
        Request request = new Request.Builder().type(RequestType.GET_TRIPS).data(filterDTO).build();
        sendRequest(request);
        Response response = readResponse();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            throw new ServiceException(err);
        }
        Trip[] trips = (Trip[]) response.data();
        return new ArrayList<>(Arrays.asList(trips));
    }

    @Override
    public Collection<Reservation> getReservations() throws ServiceException {
        Request request = new Request.Builder().type(RequestType.GET_RESERVATIONS).build();
        sendRequest(request);
        Response response = readResponse();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            throw new ServiceException(err);
        }
        Reservation[] reservations = (Reservation[]) response.data();
        return new ArrayList<>(Arrays.asList(reservations));
    }

    @Override
    public void saveReservation(Reservation reservation) throws ServiceException {
        Request request = new Request.Builder().type(RequestType.SAVE_RESERVATION).data(reservation).build();
        sendRequest(request);
        Response response = readResponse();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            throw new ServiceException(err);
        }
    }

    private void closeConnection() {
        finished = true;
        try {
            input.close();
            output.close();
            connection.close();
            client = null;
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void sendRequest(Request request) throws ServiceException {
        try {
            output.writeObject(request);
            output.flush();
        } catch (IOException ex) {
            throw new ServiceException("Error sending object " + ex);
        }
    }

    private Response readResponse() {
        Response response = null;
        try {
            response = qresponses.take();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        return response;
    }

    private void initializeConnection() {
        try {
            connection = new Socket(host, port);
            output = new ObjectOutputStream(connection.getOutputStream());
            output.flush();
            input = new ObjectInputStream(connection.getInputStream());
            finished = false;
            startReader();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void startReader() {
        Thread tw = new Thread(new ReaderThread());
        tw.start();
    }

    private void handleUpdate(Response response) {
        if (response.type() == ResponseType.NEW_RESERVATION) {
            Reservation reservation = (Reservation) response.data();
            try {
                client.ReservationSaved(reservation);
            } catch (ServiceException e) {
                e.printStackTrace();
            }
        }
    }

    private boolean isUpdate(Response response) {
        return response.type() == ResponseType.NEW_RESERVATION;
    }

    private class ReaderThread implements Runnable {
        @Override
        public void run() {
            while (!finished) {
                try {
                    Object response = input.readObject();
                    System.out.println("Response received: " + response);
                    if (isUpdate((Response) response))
                        handleUpdate((Response) response);
                    else {
                        try {
                            qresponses.put((Response) response);
                        } catch (InterruptedException ex) {
                            ex.printStackTrace();
                        }
                    }
                } catch (IOException | ClassNotFoundException ex) {
                    System.out.println("Reading error: " + ex);
                }
            }
        }
    }
}
