using Model;
using Services;
using Services.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.server
{
    public class ServerAMS : IServicesAMS
    {
        private readonly AgencyService _agencySrv;
        private readonly TripService _tripSrv;
        private readonly ReservationService _resSrv;
        private readonly INotificationService _notificationSrv;
        private readonly ICollection<string> _loggedAgencies;

        public ServerAMS(AgencyService agencySrv, TripService tripSrv,
            ReservationService resSrv, INotificationService notificationSrv)
        {
            _agencySrv = agencySrv;
            _tripSrv = tripSrv;
            _resSrv = resSrv;
            _notificationSrv = notificationSrv;
            _loggedAgencies = new List<string>();
        }

        public void Login(Agency agency)
        {
            Agency agency2 = _agencySrv.Get(agency);
            if (agency2 != null)
            {
                if (_loggedAgencies.Contains(agency.Name))
                    throw new ServiceException("Agency already logged in");
                _loggedAgencies.Add(agency.Name);
            }
            else
                throw new ServiceException("Authentication failed");
        }

        public void Logout(Agency agency)
        {
            if (!_loggedAgencies.Contains(agency.Name))
                throw new ServiceException("Agency " + agency.Name + "is not logged in");
            _loggedAgencies.Remove(agency.Name);
        }

        public IEnumerable<Agency> GetAgencies() => _agencySrv.GetAll();

        public IEnumerable<Trip> GetTrips(string destination, TimeSpan startTime, TimeSpan endTime)
            => _tripSrv.GetTouristAttractionTrips(destination, startTime, endTime);

        public IEnumerable<Reservation> GetReservations() => _resSrv.GetAllReservations();

        public void SaveReservation(Reservation reservation)
        {
            try
            {
                _resSrv.SaveReservation(reservation);
                _notificationSrv.NewReservation(reservation);
            }
            catch (ArgumentException e)
            {
                throw new ServiceException(e.Message);
            }
            catch (InvalidOperationException e)
            {
                throw new ServiceException(e.Message);
            }
        }
    }
}
