using System;
using System.Collections.Generic;
using System.Data;
using AgentiiDeTurism.src.domain;
using log4net;

namespace AgentiiDeTurism.src.repository
{
    class ReservationDBRepo : IReservationRepo
    {
        private static readonly ILog log = LogManager.GetLogger("ReservationDBRepo");
        private readonly string tableName = "reservations";
        IDictionary<string, string> props;
        private readonly ITripRepo tripRepo;
        public ReservationDBRepo(IDictionary<string, string> props, ITripRepo tripRepo)
        {
            log.Info("Creating ReservationDBRepo");
            this.props = props;
            this.tripRepo = tripRepo;
        }
        public void clear()
        {
            log.Info("Entering clear");
            IDbConnection con = DBUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from " + tableName;
                int result = comm.ExecuteNonQuery();
                log.InfoFormat("Deleted {0} reservations", result);
            }
            log.Info("Exiting clear");
        }

        public void delete(Reservation elem)
        {
            deleteById(elem.getId());
        }

        public void deleteById(Tuple<string, Trip> id)
        {
            log.InfoFormat("Entering deleteById with value {0}", id);
            IDbConnection con = DBUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from " + tableName + " where client = @client and id_trip = @idTrip";
               
                IDbDataParameter paramClient = comm.CreateParameter();
                paramClient.ParameterName = "@client";
                paramClient.Value = id.Item1;
                comm.Parameters.Add(paramClient);
                
                IDbDataParameter paramIdTrip = comm.CreateParameter();
                paramIdTrip.ParameterName = "@idTrip";
                paramIdTrip.Value = id.Item2.id;
                comm.Parameters.Add(paramIdTrip);

                int result = comm.ExecuteNonQuery();
                log.InfoFormat("Deleted {0} reservations", result);
            }
            log.Info("Exiting deleteById");
        }

        public ICollection<Reservation> getAll()
        {
            log.Info("Entering getAll");
            IDbConnection con = DBUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from " + tableName;

                using (var dataR = comm.ExecuteReader())
                {
                    List<Reservation> reservations = new List<Reservation>();
                    while (dataR.Read())
                    {
                        String client = dataR.GetString(0);
                        int id_trip = dataR.GetInt32(1);
                        Trip trip = tripRepo.getById(id_trip);
                        String phoneNumber = dataR.GetString(2);
                        int seats = dataR.GetInt32(3);

                        Reservation reservation = new Reservation(new Tuple<string, Trip>(client, trip), phoneNumber, seats);
                        reservations.Add(reservation);
                    }
                    log.InfoFormat("Exiting getAll with {0} results", reservations.Count);
                    return reservations;
                }
            }
        }

        public Reservation getById(Tuple<string, Trip> id)
        {
            log.InfoFormat("Entering getById with value {0}", id);
            IDbConnection con = DBUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from " + tableName + " where client = @client and id_trip = @idTrip";
                
                IDbDataParameter paramClient = comm.CreateParameter();
                paramClient.ParameterName = "@client";
                paramClient.Value = id.Item1;
                comm.Parameters.Add(paramClient);
                
                IDbDataParameter paramIdTrip = comm.CreateParameter();
                paramIdTrip.ParameterName = "@idTrip";
                paramIdTrip.Value = id.Item2.id;
                comm.Parameters.Add(paramIdTrip);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        String client = dataR.GetString(0);
                        int id_trip = dataR.GetInt32(1);
                        Trip trip = tripRepo.getById(id_trip);
                        String phoneNumber = dataR.GetString(2);
                        int seats = dataR.GetInt32(3);

                        Reservation reservation = new Reservation(new Tuple<string, Trip>(client, trip), phoneNumber, seats);
                        log.InfoFormat("Exiting getById with value {0}", trip);
                        return reservation;
                    }
                }
            }
            log.InfoFormat("Exiting getById with value {0}", null);
            return null;
        }

        public void save(Reservation elem)
        {
            log.InfoFormat("Entering save with value {0}", elem);
            IDbConnection con = DBUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into " + tableName +
                    " (client, id_trip, phone_number, seats) values (@client, @idTrip, @phoneNumber, @seats)";

                IDbDataParameter paramClient = comm.CreateParameter();
                paramClient.ParameterName = "@client";
                paramClient.Value = elem.client;
                comm.Parameters.Add(paramClient);

                IDbDataParameter paramIdTrip = comm.CreateParameter();
                paramIdTrip.ParameterName = "@idTrip";
                paramIdTrip.Value = elem.trip.id;
                comm.Parameters.Add(paramIdTrip);

                IDbDataParameter paramPhoneNumber = comm.CreateParameter();
                paramPhoneNumber.ParameterName = "@phoneNumber";
                paramPhoneNumber.Value = elem.phoneNumber;
                comm.Parameters.Add(paramPhoneNumber);

                IDbDataParameter paramSeats = comm.CreateParameter();
                paramSeats.ParameterName = "@seats";
                paramSeats.Value = elem.seats;
                comm.Parameters.Add(paramSeats);

                int result = comm.ExecuteNonQuery();
                log.InfoFormat("Saved {0} trips", result);
            }
            log.Info("Exiting save");
        }

        public void update(Reservation elem, Tuple<string, Trip> id)
        {
            log.InfoFormat("Entering update with values {0}, id = {1}", elem, id);
            IDbConnection con = DBUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "update " + tableName + " set phone_number = @phoneNumber," +
                    " seats = @seats where client = @client and id_trip = @idTrip";

                IDbDataParameter paramPhoneNumber = comm.CreateParameter();
                paramPhoneNumber.ParameterName = "@phoneNumber";
                paramPhoneNumber.Value = elem.phoneNumber;
                comm.Parameters.Add(paramPhoneNumber);

                IDbDataParameter paramSeats = comm.CreateParameter();
                paramSeats.ParameterName = "@seats";
                paramSeats.Value = elem.seats;
                comm.Parameters.Add(paramSeats);

                IDbDataParameter paramClien = comm.CreateParameter();
                paramClien.ParameterName = "@client";
                paramClien.Value = elem.client;
                comm.Parameters.Add(paramClien);

                IDbDataParameter paramIdTrip = comm.CreateParameter();
                paramIdTrip.ParameterName = "@idTrip";
                paramIdTrip.Value = elem.trip.id;
                comm.Parameters.Add(paramIdTrip);

                int result = comm.ExecuteNonQuery();
                log.InfoFormat("Updated {0} reservations", result);
            }
            log.Info("Exiting update");
        }
    }
}
