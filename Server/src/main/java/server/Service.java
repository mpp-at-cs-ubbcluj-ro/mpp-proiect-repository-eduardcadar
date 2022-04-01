package server;

import model.domain.Agency;
import model.domain.Reservation;
import model.domain.Trip;
import model.utils.MyException;
import services.IObserver;
import services.IServices;
import services.ServiceException;

import java.sql.Time;
import java.util.Collection;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class Service implements IServices {
    private final AgencyService agencySrv;
    private final TripService tripSrv;
    private final ReservationService resSrv;
    private final Map<String, IObserver> loggedAgencies;
    private final int defaultThreadsNo = 5;

    public Service(AgencyService agencySrv, TripService tripSrv, ReservationService resSrv) {
        this.agencySrv = agencySrv;
        this.tripSrv = tripSrv;
        this.resSrv = resSrv;
        loggedAgencies = new ConcurrentHashMap<>();
    }

    @Override
    public synchronized void saveReservation(Reservation reservation) throws ServiceException {
        try {
            resSrv.saveReservation(reservation);
        } catch (MyException e) {
            throw new ServiceException("Client already has a reservation to this trip");
        }
        ExecutorService executor = Executors.newFixedThreadPool(defaultThreadsNo);

        loggedAgencies.forEach((a, o) -> {
            if (!a.equals(reservation.getAgency()))
                executor.execute(() -> {
                    try {
                        System.out.println("Notifying [" + a + "] - new reservation saved");
                        o.ReservationSaved(reservation);
                    } catch (ServiceException e) {
                        System.out.println("Error notifying agency " + a);
                    }
                });
        });
    }

    @Override
    public synchronized void login(Agency agency, IObserver observer) throws ServiceException {
        Agency agency2 = agencySrv.getAgency(agency);
        if (agency2 != null) {
            if (loggedAgencies.get(agency.getName()) != null)
                throw new ServiceException("Agency already logged in");
            loggedAgencies.put(agency.getName(), observer);
        } else
            throw new ServiceException("Authentication failed");
    }

    @Override
    public synchronized void logout(Agency agency, IObserver observer) throws ServiceException{
        IObserver localObserver = loggedAgencies.remove(agency.getName());
        if (localObserver == null)
            throw new ServiceException("Agency " + agency.getName() + "is not logged in");
    }

    @Override
    public synchronized Collection<Agency> getAgencies() {
        return agencySrv.getAllAgencies();
    }

    @Override
    public synchronized Collection<Trip> getTrips(String destination, Time startTime, Time endTime) {
        return tripSrv.getTouristAttractionTrips(destination, startTime, endTime);
    }

    @Override
    public synchronized Collection<Reservation> getReservations() { return resSrv.getAllReservations(); }

}
