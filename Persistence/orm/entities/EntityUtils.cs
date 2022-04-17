using Model;
using System;

namespace Persistence.orm.entities
{
    public static class EntityUtils
    {
        public static Agency AgencyEntityToAgency(AgencyEntity agencyEntity)
        {
            var agency = new Agency(agencyEntity.Name, agencyEntity.Password);
            return agency;
        }

        public static AgencyEntity AgencyToAgencyEntity(Agency elem)
        {
            var agencyEntity = new AgencyEntity { Name = elem.Name, Password = elem.Password };
            return agencyEntity;
        }

        public static Trip TripEntityToTrip(TripEntity t)
        {
            var trip = new Trip(t.Id, t.TouristAttraction, t.TransportCompany, t.DepartureTime, t.Price, t.Seats);
            return trip;
        }

        public static Reservation ReservationEntityToReservation(ReservationEntity r)
        {
            var trip = TripEntityToTrip(r.Trip);
            var agency = AgencyEntityToAgency(r.Agency);
            var reservation = new Reservation(
                r.Client, trip, agency.Name, r.PhoneNumber, r.Seats);
            return reservation;
        }

        public static TripEntity TripToTripEntity(Trip elem)
        {
            var tripEntity = new TripEntity
            {
                Id = elem.Id,
                TouristAttraction = elem.TouristAttraction,
                TransportCompany = elem.TransportCompany,
                DepartureTime = elem.DepartureTime,
                Price = elem.Price,
                Seats = elem.Seats
            };
            return tripEntity;
        }

        public static ReservationEntity ReservationToReservationEntity(Reservation elem)
        {
            var tripEntity = TripToTripEntity(elem.Trip);
            var agencyEntity = new AgencyEntity { Name = elem.Agency };
            var reservationEntity = new ReservationEntity
            {
                Trip = tripEntity,
                Agency = agencyEntity,
                Client = elem.Client,
                PhoneNumber = elem.PhoneNumber,
                Seats = elem.Seats
            };
            return reservationEntity;
        }
    }
}
