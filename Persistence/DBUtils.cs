using System.Collections.Generic;
using System.Data;

namespace Persistence
{
    class DBUtils
    {
        private static IDbConnection instance = null;

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
