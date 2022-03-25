using System;
using System.Collections.Generic;
using AgentiiDeTurism.src.domain;
using AgentiiDeTurism.src.utils;

namespace AgentiiDeTurism.src.service
{
    public class Service
    {
        private AgencyService agencySrv;
        private TripService tripSrv;
        private ReservationService resSrv;

        public Service(AgencyService agencySrv, TripService tripSrv, ReservationService resSrv)
        {
            this.agencySrv = agencySrv;
            this.tripSrv = tripSrv;
            this.resSrv = resSrv;
        }

        public ICollection<Trip> getTouristAttractionTrips(String touristAttraction, TimeSpan startTime, TimeSpan endTime)
        {
            return tripSrv.getTouristAttractionTrips(touristAttraction, startTime, endTime);
        }

        public void saveReservation(String client, Trip trip, String phoneNumber, int seats)
        {
            resSrv.saveReservation(client, trip, phoneNumber, seats);
        }

        public ICollection<Agency> getAllAgencies()
        {
            return agencySrv.getAllAgencies();
        }

        public ICollection<Trip> getAllTrips() { return tripSrv.getAllTrips(); }

        public ICollection<Reservation> getAllReservations() { return resSrv.getAllReservations(); }

        public Trip getTrip(int id) { return tripSrv.getTrip(id); }

        public Agency getAgencyByName(String name) { return agencySrv.getByName(name); }

        public int getAvailableSeatsForTrip(Trip trip) { return resSrv.getAvailableSeatsForTrip(trip); }

        public ICollection<TripDTO> getTripDTOs(ICollection<Trip> trips)
        {
            List<TripDTO> tripDTOs = new List<TripDTO>();
            foreach (Trip t in trips)
            {
                int availableSeats = resSrv.getAvailableSeatsForTrip(t);
                TripDTO tripDTO = new TripDTO(t.getId(), t.TouristAttraction, t.TransportCompany,
                        t.DepartureTime, t.Price, availableSeats);
                tripDTOs.Add(tripDTO);
            }

            return tripDTOs;
        }
    }
}
