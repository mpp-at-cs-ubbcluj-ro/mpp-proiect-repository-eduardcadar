using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using Model;

namespace Persistence
{
    public class TripDBRepo : ITripRepo
    {
        private static readonly ILog log = LogManager.GetLogger("TripDBRepo");
        private readonly string tableName = "trips";
        readonly IDictionary<string, string> props;
        public TripDBRepo(IDictionary<string, string> props)
        {
            log.Info("Creating TripDBRepo");
            this.props = props;
        }
        public void Clear()
        {
            log.Info("Entering clear");
            IDbConnection con = DBUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from " + tableName;
                int result = comm.ExecuteNonQuery();
                log.InfoFormat("Deleted {0} trips", result);
            }
            log.Info("Exiting clear");
        }

        public void Delete(Trip elem)
        {
            DeleteById(elem.Id);
        }

        public void DeleteById(int id)
        {
            log.InfoFormat("Entering deleteById with value {0}", id);
            IDbConnection con = DBUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from " + tableName + " where id = @id";
                IDbDataParameter paramID = comm.CreateParameter();
                paramID.ParameterName = "@id";
                paramID.Value = id;
                comm.Parameters.Add(paramID);

                int result = comm.ExecuteNonQuery();
                log.InfoFormat("Deleted {0} trips", result);
            }
            log.Info("Exiting deleteById");
        }

        public IEnumerable<Trip> GetAll()
        {
            log.Info("Entering getAll");
            IDbConnection con = DBUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from " + tableName;

                using (var dataR = comm.ExecuteReader())
                {
                    List<Trip> trips = new List<Trip>();
                    while (dataR.Read())
                    {
                        int idTrip = dataR.GetInt32(0);
                        string touristAttraction = dataR.GetString(1);
                        string transportCompany = dataR.GetString(2);
                        TimeSpan departureTime = dataR.GetDateTime(3).TimeOfDay;
                        float price = dataR.GetFloat(4);
                        int seats = dataR.GetInt32(5);
                        Trip trip = new Trip(idTrip, touristAttraction, transportCompany, departureTime, price, seats);
                        trips.Add(trip);
                    }
                    log.InfoFormat("Exiting getAll with {0} results", trips.Count);
                    return trips;
                }
            }
        }

        public Trip GetById(int id)
        {
            log.InfoFormat("Entering getById with value {0}", id);
            IDbConnection con = DBUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from " + tableName + " where id = @id";
                IDbDataParameter paramID = comm.CreateParameter();
                paramID.ParameterName = "@id";
                paramID.Value = id;
                comm.Parameters.Add(paramID);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int idTrip = dataR.GetInt32(0);
                        string touristAttraction = dataR.GetString(1);
                        string transportCompany = dataR.GetString(2);
                        TimeSpan departureTime = dataR.GetDateTime(3).TimeOfDay;
                        float price = dataR.GetFloat(4);
                        int seats = dataR.GetInt32(5);
                        Trip trip = new Trip(idTrip, touristAttraction, transportCompany, departureTime, price, seats);
                        log.InfoFormat("Exiting getById with value {0}", trip);
                        return trip;
                    }
                }
            }
            log.InfoFormat("Exiting getById with value {0}", null);
            return null;
        }

        public IEnumerable<Trip> GetTouristAttractionTrips(string touristAttraction, TimeSpan startTime, TimeSpan endTime)
        {
            log.InfoFormat("Entering getTouristAttractionTrips");
            IDbConnection con = DBUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from " + tableName +
                    " where tourist_attraction like @touristAttraction and departure_time between @startTime and @endTime";

                IDbDataParameter paramTouristAttraction = comm.CreateParameter();
                paramTouristAttraction.ParameterName = "@touristAttraction";
                paramTouristAttraction.Value = "%" + touristAttraction + "%";
                comm.Parameters.Add(paramTouristAttraction);
                
                IDbDataParameter paramStartTime = comm.CreateParameter();
                paramStartTime.ParameterName = "@startTime";
                paramStartTime.Value = startTime;
                comm.Parameters.Add(paramStartTime);
                
                IDbDataParameter paramEndTime = comm.CreateParameter();
                paramEndTime.ParameterName = "@endTime";
                paramEndTime.Value = endTime;
                comm.Parameters.Add(paramEndTime);

                using (var dataR = comm.ExecuteReader())
                {
                    List<Trip> trips = new List<Trip>();
                    while (dataR.Read())
                    {
                        int idTrip = dataR.GetInt32(0);
                        string destination = dataR.GetString(1);
                        string transportCompany = dataR.GetString(2);
                        TimeSpan departureTime = dataR.GetDateTime(3).TimeOfDay;
                        float price = dataR.GetFloat(4);
                        int seats = dataR.GetInt32(5);
                        Trip trip = new(idTrip, destination, transportCompany, departureTime, price, seats);
                        trips.Add(trip);
                    }
                    log.InfoFormat("Exiting getTouristAttractionTrips with {0} results", trips.Count);
                    return trips;
                }
            }
        }

        public Trip Save(Trip elem)
        {
            log.InfoFormat("Entering save with value {0}", elem);
            IDbConnection con = DBUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into " + tableName +
                    " (tourist_attraction, transport_company, departure_time, price, seats)" +
                    "values (@touristAttraction, @transportCompany, @departureTime, @price, @seats)";

                IDbDataParameter paramTouristAttraction = comm.CreateParameter();
                paramTouristAttraction.ParameterName = "@touristAttraction";
                paramTouristAttraction.Value = elem.TouristAttraction;
                comm.Parameters.Add(paramTouristAttraction);

                IDbDataParameter paramTransportCompany = comm.CreateParameter();
                paramTransportCompany.ParameterName = "@transportCompany";
                paramTransportCompany.Value = elem.TransportCompany;
                comm.Parameters.Add(paramTransportCompany);

                IDbDataParameter paramDepartureTime = comm.CreateParameter();
                paramDepartureTime.ParameterName = "@departureTime";
                paramDepartureTime.Value = elem.DepartureTime;
                comm.Parameters.Add(paramDepartureTime);

                IDbDataParameter paramPrice = comm.CreateParameter();
                paramPrice.ParameterName = "@price";
                paramPrice.Value = elem.Price;
                comm.Parameters.Add(paramPrice);

                IDbDataParameter paramSeats = comm.CreateParameter();
                paramSeats.ParameterName = "@seats";
                paramSeats.Value = elem.Seats;
                comm.Parameters.Add(paramSeats);

                int result = comm.ExecuteNonQuery();
                log.InfoFormat("Saved {0} trips", result);
            }
            log.Info("Exiting save");
            return elem;
        }

        public void Update(Trip elem, int id)
        {
            log.InfoFormat("Entering update with values {0}, id = {1}", elem, id);
            IDbConnection con = DBUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "update " + tableName +" set tourist_attraction = @touristAttraction," +
                    " transport_company = @transportCompany, departure_time = @departureTime, price = @price, seats = @seats where id = @id";

                IDbDataParameter paramTouristAttraction = comm.CreateParameter();
                paramTouristAttraction.ParameterName = "@touristAttraction";
                paramTouristAttraction.Value = elem.TouristAttraction;
                comm.Parameters.Add(paramTouristAttraction);

                IDbDataParameter paramTransportCompany = comm.CreateParameter();
                paramTransportCompany.ParameterName = "@transportCompany";
                paramTransportCompany.Value = elem.TransportCompany;
                comm.Parameters.Add(paramTransportCompany);

                IDbDataParameter paramDepartureTime = comm.CreateParameter();
                paramDepartureTime.ParameterName = "@departureTime";
                paramDepartureTime.Value = elem.DepartureTime;
                comm.Parameters.Add(paramDepartureTime);

                IDbDataParameter paramPrice = comm.CreateParameter();
                paramPrice.ParameterName = "@price";
                paramPrice.Value = elem.Price;
                comm.Parameters.Add(paramPrice);

                IDbDataParameter paramSeats = comm.CreateParameter();
                paramSeats.ParameterName = "@seats";
                paramSeats.Value = elem.Seats;
                comm.Parameters.Add(paramSeats);

                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                int result = comm.ExecuteNonQuery();
                log.InfoFormat("Updated {0} trips", result);
            }
            log.Info("Exiting update");
        }
    }
}
