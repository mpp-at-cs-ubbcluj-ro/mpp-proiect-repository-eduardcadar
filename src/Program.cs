using System;
using System.Collections.Generic;
using System.Configuration;
using AgentiiDeTurism.src.domain;
using AgentiiDeTurism.src.repository;
using log4net.Config;

namespace AgentiiDeTurism.src
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("app.config"));
            Console.WriteLine("Configuration Settings for agenciesDB {0}", GetConnectionStringByName("agenciesDB"));
            IDictionary<string, string> props = new SortedList<string, string>();
            props.Add("ConnectionString", GetConnectionStringByName("agenciesDB"));

            IAgencyRepo repo = new AgencyDBRepo(props);
            foreach (Agency a in repo.getAll())
            {
                Console.WriteLine(a);
            }
            repo.save(new Agency("agentie2", "parola2"));
            foreach (Agency a in repo.getAll())
            {
                Console.WriteLine(a);
            }
            repo.deleteById(4);
            Console.WriteLine();
            foreach (Agency a in repo.getAll())
            {
                Console.WriteLine(a);
            }
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
