using System.Collections.Generic;
using System.Data;
using AgentiiDeTurism.src.domain;
using log4net;

namespace AgentiiDeTurism.src.repository
{
    public class AgencyDBRepo : IAgencyRepo
    {
        private static readonly ILog log = LogManager.GetLogger("AgencyDBRepo");
        private readonly string tableName = "agencies";
        IDictionary<string, string> props;
        public AgencyDBRepo(IDictionary<string, string> props)
        {
            log.Info("Creating AgencyDBRepo");
            this.props = props;
        }
        public void clear()
        {
            log.Info("Entering clear");
            IDbConnection con = DBUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from " + tableName;
                int result = comm.ExecuteNonQuery();
                log.InfoFormat("Deleted {0} agencies", result);
            }
            log.Info("Exiting clear");
        }

        public void delete(Agency elem)
        {
            deleteById(elem.id);
        }

        public void deleteById(int id)
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
                log.InfoFormat("Deleted {0} agencies", result);
            }
            log.Info("Exiting deleteById");
        }

        public ICollection<Agency> getAll()
        {
            log.Info("Entering getAll");
            IDbConnection con = DBUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from " + tableName;

                using (var dataR = comm.ExecuteReader())
                {
                    List<Agency> agencies = new List<Agency>();
                    while (dataR.Read())
                    {
                        int idAgency = dataR.GetInt32(0);
                        string name = dataR.GetString(1);
                        string password = dataR.GetString(2);
                        Agency agency = new Agency(idAgency, name, password);
                        agencies.Add(agency);
                    }
                    log.InfoFormat("Exiting getAll with {0} results", agencies.Count);
                    return agencies;
                }
            }
        }

        public Agency getById(int id)
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
                        int idAgency = dataR.GetInt32(0);
                        string name = dataR.GetString(1);
                        string password = dataR.GetString(2);
                        Agency agency = new Agency(idAgency, name, password);
                        log.InfoFormat("Exiting getById with value {0}", agency);
                        return agency;
                    }
                }
            }
            log.InfoFormat("Exiting getById with value {0}", null);
            return null;
        }

        public void save(Agency elem)
        {
            log.InfoFormat("Entering save with value {0}", elem);
            IDbConnection con = DBUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into " + tableName + " (name, password) values (@name, @password)";
                
                IDbDataParameter paramName = comm.CreateParameter();
                paramName.ParameterName = "@name";
                paramName.Value = elem.name;
                comm.Parameters.Add(paramName);
                
                IDbDataParameter paramPassword = comm.CreateParameter();
                paramPassword.ParameterName = "@password";
                paramPassword.Value = elem.password;
                comm.Parameters.Add(paramPassword);

                int result = comm.ExecuteNonQuery();
                log.InfoFormat("Saved {0} agencies", result);
            }
            log.Info("Exiting save");
        }

        public void update(Agency elem, int id)
        {
            log.InfoFormat("Entering update with values {0}, id = {1}", elem, id);
            IDbConnection con = DBUtils.GetConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "update " + tableName + " set name = @name, password = @password where id = @id";

                IDbDataParameter paramName = comm.CreateParameter();
                paramName.ParameterName = "@name";
                paramName.Value = elem.name;
                comm.Parameters.Add(paramName);

                IDbDataParameter paramPassword = comm.CreateParameter();
                paramPassword.ParameterName = "@password";
                paramPassword.Value = elem.password;
                comm.Parameters.Add(paramPassword);
                
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                int result = comm.ExecuteNonQuery();
                log.InfoFormat("Updated {0} agencies", result);
            }
            log.Info("Exiting update");
        }
    }
}
