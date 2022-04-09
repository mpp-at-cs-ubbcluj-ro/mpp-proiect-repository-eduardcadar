using System.Collections.Generic;
using System.Data;
using log4net;
using Model;

namespace Persistence
{
    public class AgencyDBRepo : IAgencyRepo
    {
        private static readonly ILog _log = LogManager.GetLogger("AgencyDBRepo");
        private readonly string _tableName = "agencies";
        private readonly IDictionary<string, string> _props;
        public AgencyDBRepo(IDictionary<string, string> props)
        {
            _log.Info("Creating AgencyDBRepo");
            _props = props;
        }
        public void Clear()
        {
            _log.Info("Entering clear");
            IDbConnection con = DBUtils.GetConnection(_props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from " + _tableName;
                int result = comm.ExecuteNonQuery();
                _log.InfoFormat("Deleted {0} agencies", result);
            }
            _log.Info("Exiting clear");
        }

        public void Delete(Agency elem) => DeleteById(elem.Name);

        public void DeleteById(string name)
        {
            _log.InfoFormat("Entering deleteById with value {0}", name);
            IDbConnection con = DBUtils.GetConnection(_props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from " + _tableName + " where name = @name";
                IDbDataParameter paramName = comm.CreateParameter();
                paramName.ParameterName = "@name";
                paramName.Value = name;
                comm.Parameters.Add(paramName);

                int result = comm.ExecuteNonQuery();
                _log.InfoFormat("Deleted {0} agencies", result);
            }
            _log.Info("Exiting deleteById");
        }

        public IEnumerable<Agency> GetAll()
        {
            _log.Info("Entering getAll");
            IDbConnection con = DBUtils.GetConnection(_props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from " + _tableName;

                using (var dataR = comm.ExecuteReader())
                {
                    List<Agency> agencies = new List<Agency>();
                    while (dataR.Read())
                    {
                        string name = dataR.GetString(0);
                        string password = dataR.GetString(1);
                        Agency agency = new Agency(name, password);
                        agencies.Add(agency);
                    }
                    _log.InfoFormat("Exiting getAll with {0} results", agencies.Count);
                    return agencies;
                }
            }
        }

        public Agency GetById(string id)
        {
            _log.InfoFormat("Entering getById with value {0}", id);
            IDbConnection con = DBUtils.GetConnection(_props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from " + _tableName + " where name = @id";
                IDbDataParameter paramID = comm.CreateParameter();
                paramID.ParameterName = "@id";
                paramID.Value = id;
                comm.Parameters.Add(paramID);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        string name = dataR.GetString(0);
                        string password = dataR.GetString(1);
                        Agency agency = new Agency(name, password);
                        _log.InfoFormat("Exiting getById with value {0}", agency);
                        return agency;
                    }
                }
            }
            _log.InfoFormat("Exiting getById with value {0}", null);
            return null;
        }

        public void Save(Agency elem)
        {
            _log.InfoFormat("Entering save with value {0}", elem);
            IDbConnection con = DBUtils.GetConnection(_props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into " + _tableName + " (name, password) values (@name, @password)";
                
                IDbDataParameter paramName = comm.CreateParameter();
                paramName.ParameterName = "@name";
                paramName.Value = elem.Name;
                comm.Parameters.Add(paramName);
                
                IDbDataParameter paramPassword = comm.CreateParameter();
                paramPassword.ParameterName = "@password";
                paramPassword.Value = elem.Password;
                comm.Parameters.Add(paramPassword);

                int result = comm.ExecuteNonQuery();
                _log.InfoFormat("Saved {0} agencies", result);
            }
            _log.Info("Exiting save");
        }
        public void Update(Agency elem, int id)
        {
            _log.InfoFormat("Entering update with values {0}, id = {1}", elem, id);
            IDbConnection con = DBUtils.GetConnection(_props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "update " + _tableName + " set name = @name, password = @password where id = @id";

                IDbDataParameter paramName = comm.CreateParameter();
                paramName.ParameterName = "@name";
                paramName.Value = elem.Name;
                comm.Parameters.Add(paramName);

                IDbDataParameter paramPassword = comm.CreateParameter();
                paramPassword.ParameterName = "@password";
                paramPassword.Value = elem.Password;
                comm.Parameters.Add(paramPassword);
                
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                int result = comm.ExecuteNonQuery();
                _log.InfoFormat("Updated {0} agencies", result);
            }
            _log.Info("Exiting update");
        }

        public Agency Get(string name, string password)
        {
            _log.InfoFormat("Entering getByName with value {0}", name);
            IDbConnection con = DBUtils.GetConnection(_props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from " + _tableName + " where name = @name and password = @password";
                IDbDataParameter paramName = comm.CreateParameter();
                paramName.ParameterName = "@name";
                paramName.Value = name;
                comm.Parameters.Add(paramName);
                
                IDbDataParameter paramPassword = comm.CreateParameter();
                paramPassword.ParameterName = "@password";
                paramPassword.Value = password;
                comm.Parameters.Add(paramPassword);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        Agency agency = new(name, password);
                        _log.InfoFormat("Exiting getById with value {0}", agency);
                        return agency;
                    }
                }
            }
            _log.InfoFormat("Exiting getByName with value {0}", null);
            return null;
        }
    }
}
