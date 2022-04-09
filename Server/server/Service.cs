using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using Services;

namespace Server.server
{
    public class Service : IServices
    {
        private readonly AgencyService _agencySrv;
        private readonly TripService _tripSrv;
        private readonly ReservationService _resSrv;
        private readonly IDictionary<string, IObserver> _loggedAgencies;

        public Service(AgencyService agencySrv, TripService tripSrv, ReservationService resSrv)
        {
            _agencySrv = agencySrv;
            _tripSrv = tripSrv;
            _resSrv = resSrv;
            _loggedAgencies = new Dictionary<string, IObserver>();
        }

        public void Login(Agency agency, IObserver observer)
        {
            Agency agency2 = _agencySrv.Get(agency);
            if (agency2 != null)
            {
                if (_loggedAgencies.ContainsKey(agency.Name))
                    throw new ServiceException("Agency already logged in");
                _loggedAgencies[agency.Name] = observer;
            }
            else
                throw new ServiceException("Authentication failed");
        }

        public void Logout(Agency agency, IObserver observer)
        {
            IObserver localObserver = _loggedAgencies[agency.Name];
            if (localObserver == null)
                throw new ServiceException("Agency " + agency.Name + "is not logged in");
            _loggedAgencies.Remove(agency.Name);
        }

        public IEnumerable<Agency> GetAgencies() => _agencySrv.GetAll();

        public IEnumerable<Trip> GetTrips(string destination, TimeSpan startTime, TimeSpan endTime) =>
            _tripSrv.GetTouristAttractionTrips(destination, startTime, endTime);

        public IEnumerable<Reservation> GetReservations() => _resSrv.GetAllReservations();

        public void SaveReservation(Reservation reservation)
        {
            try
            {
                _resSrv.SaveReservation(reservation);
            }
            catch (ArgumentException e)
            {
                throw new ServiceException(e.Message);
            }

            foreach (KeyValuePair<string, IObserver> entry in _loggedAgencies)
                if (entry.Key != reservation.Agency)
                    Task.Run(() => entry.Value.ReservationSaved(reservation));
        }
    }
}
