using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using log4net.Config;
using Microsoft.EntityFrameworkCore;
using Networking.server;
using Networking.server.ams;
using Persistence;
using Persistence.orm;
using Persistence.orm.repos;
using Server.server;
using Services.ams;

namespace Server
{
    public static class StartServer
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("log4net.xml"));

            string connectionString = ConfigurationManager.AppSettings["connectionString"]; // ORM
            //string connectionString = GetConnectionStringByName("agenciesDB");
            Console.WriteLine("Configuration Settings for agenciesDB {0}", connectionString);
            IDictionary<string, string> props = new SortedList<string, string>
            {
                { "ConnectionString", connectionString }
            };

            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<AgenciesContext>()
                .UseSqlServer(sqlConnectionStringBuilder.ConnectionString,
                options => options.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null));

            AgenciesContext context = new(dbContextOptionsBuilder.Options);
            context.Database.EnsureCreated();
            
            //IAgencyRepo agencyRepo = new AgencyDBRepo(props);
            //ITripRepo tripRepo = new TripDBRepo(props);
            //IReservationRepo reservationRepo = new ReservationDBRepo(props, tripRepo);

            IAgencyRepo agencyRepo = new AgencyDbOrmRepo(context);
            ITripRepo tripRepo = new TripDbOrmRepo(context);
            IReservationRepo reservationRepo = new ReservationDbOrmRepo(context);

            AgencyService agencyService = new(agencyRepo);
            TripService tripService = new(tripRepo);
            ReservationService reservationService = new(reservationRepo);
            INotificationService notifSrv = new NotificationServiceImpl();

            Service service = new(agencyService, tripService, reservationService);
            //ServerAMS service = new(agencyService, tripService, reservationService, notifSrv);

            //string host = "127.0.0.1";
            //int port = 55553;

            string host = ConfigurationManager.AppSettings.Get("host");
            int port = int.Parse(ConfigurationManager.AppSettings.Get("port"));
            Console.WriteLine("Port " + port);

            AbstractServer server;
            server = new RpcConcurrentServer(host, port, service);
            //server = new ProtoServer(host, port, service);
            //server = new RpcAMSConcurrentServer(host, port, service);

            server.Start();
        }
    }
}
