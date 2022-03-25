using System;
using System.Collections.Generic;
using System.Configuration;
using AgentiiDeTurism.src.repository;
using AgentiiDeTurism.src.service;
using log4net.Config;
using System.Windows.Forms;
using AgentiiDeTurism.src.controller;

namespace AgentiiDeTurism.src
{
    public static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyForm());
        }

        public static Service GetService()
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("log4net.xml"));
            Console.WriteLine("Configuration Settings for agenciesDB {0}", GetConnectionStringByName("agenciesDB"));
            IDictionary<string, string> props = new SortedList<string, string>();
            props.Add("ConnectionString", GetConnectionStringByName("agenciesDB"));
            IAgencyRepo agencyRepo = new AgencyDBRepo(props);
            ITripRepo tripRepo = new TripDBRepo(props);
            IReservationRepo resRepo = new ReservationDBRepo(props, tripRepo);

            AgencyService agencyService = new AgencyService(agencyRepo);
            TripService tripService = new TripService(tripRepo);
            ReservationService reservationService = new ReservationService(resRepo);

            return new Service(agencyService, tripService, reservationService);
        }

        static string GetConnectionStringByName(string name)
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
