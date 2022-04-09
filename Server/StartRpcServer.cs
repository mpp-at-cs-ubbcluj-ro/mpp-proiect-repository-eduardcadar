using System;
using System.Collections.Generic;
using System.Configuration;
using log4net.Config;
using Networking.server;
using Persistence;
using Server.server;

namespace Server
{
    public static class StartRpcServer
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("log4net.xml"));
            Console.WriteLine("Configuration Settings for agenciesDB {0}", GetConnectionStringByName("agenciesDB"));
            IDictionary<string, string> props = new SortedList<string, string>
            {
                { "ConnectionString", GetConnectionStringByName("agenciesDB") }
            };

            IAgencyRepo agencyRepo = new AgencyDBRepo(props);
            ITripRepo tripRepo = new TripDBRepo(props);
            IReservationRepo reservationRepo = new ReservationDBRepo(props, tripRepo);

            AgencyService agencyService = new(agencyRepo);
            TripService tripService = new(tripRepo);
            ReservationService reservationService = new(reservationRepo);

            Service service = new(agencyService, tripService, reservationService);

            //string host = "127.0.0.1";
            //int port = 55553;

            string host = ConfigurationManager.AppSettings.Get("host");
            int port = int.Parse(ConfigurationManager.AppSettings.Get("port"));
            Console.WriteLine("Port " + port);

            AbstractServer server = new RpcConcurrentServer(host, port, service);

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
