package services;

import model.domain.Agency;
import model.domain.Reservation;
import model.domain.Trip;

import java.sql.Time;
import java.util.Collection;

public interface IServices {
    void login(Agency agency, IObserver observer) throws ServiceException;
    void logout(Agency agency, IObserver observer) throws ServiceException;

    Collection<Agency> getAgencies() throws ServiceException;
    Collection<Trip> getTrips(String destination, Time startTime, Time endTime) throws ServiceException;
    Collection<Reservation> getReservations() throws ServiceException;
    void saveReservation(Reservation reservation) throws ServiceException;
}
