package services;

import model.domain.Reservation;

public interface IObserver {
    void ReservationSaved(Reservation reservation) throws ServiceException;
}
