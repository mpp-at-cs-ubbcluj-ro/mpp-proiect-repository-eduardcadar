using Microsoft.EntityFrameworkCore;
using Persistence.orm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Persistence
{
    class DBUtils
    {
        private static IDbConnection instance = null;
        private static AgenciesContext _context = null;

        public static AgenciesContext GetDbContext(string connectionString)
        {
            if (_context == null)
            {
                var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
                var dbContextOptionsBuilder = new DbContextOptionsBuilder<AgenciesContext>()
                    .UseSqlServer(sqlConnectionStringBuilder.ConnectionString,
                    options => options.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null));
                _context = new(dbContextOptionsBuilder.Options);
            }
            return _context;
        }

        private static IDbConnection getNewConnection(IDictionary<string, string> props)
        {
            return ConnectionFactory.getInstance().createConnection(props);
        }
        public static IDbConnection GetConnection(IDictionary<string, string> props)
        {
            if (instance == null || instance.State == System.Data.ConnectionState.Closed)
            {
                instance = getNewConnection(props);
                instance.Open();
            }
            return instance;
        }
    }
}
