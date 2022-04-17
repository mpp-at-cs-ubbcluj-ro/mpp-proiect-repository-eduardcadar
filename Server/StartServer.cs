using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using log4net.Config;
using Microsoft.EntityFrameworkCore;
using Networking.server;
using Persistence;
using Persistence.orm;
using Persistence.orm.repos;
using Server.server;

namespace Server
{
    public static class StartServer
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("log4net.xml"));

            //string connectionString = ConfigurationManager.AppSettings["connectionString"]; // ORM
            string connectionString = GetConnectionStringByName("agenciesDB");
            Console.WriteLine("Configuration Settings for agenciesDB {0}", connectionString);
            IDictionary<string, string> props = new SortedList<string, string>
            {
                { "ConnectionString", connectionString }
            };

            //var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

            //var dbContextOptionsBuilder = new DbContextOptionsBuilder<AgenciesContext>()
            //    .UseSqlServer(sqlConnectionStringBuilder.ConnectionString,
            //    options => options.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null));

            //AgenciesContext context = new(dbContextOptionsBuilder.Options);
            //context.Database.Migrate();

            IAgencyRepo agencyRepo = new AgencyDBRepo(props);
            ITripRepo tripRepo = new TripDBRepo(props);
            IReservationRepo reservationRepo = new ReservationDBRepo(props, tripRepo);

            //IAgencyRepo agencyRepo = new AgencyDbOrmRepo(connectionString);
            //ITripRepo tripRepo = new TripDbOrmRepo(connectionString);
            //IReservationRepo reservationRepo = new ReservationDbOrmRepo(connectionString);

            AgencyService agencyService = new(agencyRepo);
            TripService tripService = new(tripRepo);
            ReservationService reservationService = new(reservationRepo);

            Service service = new(agencyService, tripService, reservationService);

            //string host = "127.0.0.1";
            //int port = 55553;

            string host = ConfigurationManager.AppSettings.Get("host");
            int port = int.Parse(ConfigurationManager.AppSettings.Get("port"));
            Console.WriteLine("Port " + port);

            AbstractServer server;
            //server = new RpcConcurrentServer(host, port, service);
            server = new ProtoServer(host, port, service);

            server.Start();
        }

        private static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
}
