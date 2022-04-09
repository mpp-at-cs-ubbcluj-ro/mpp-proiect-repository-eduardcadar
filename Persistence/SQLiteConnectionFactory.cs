using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;

namespace Persistence
{
    class SQLiteConnectionFactory : ConnectionFactory
    {
		public override IDbConnection createConnection(IDictionary<string, string> props)
		{
			//Mono Sqlite Connection

			//String connectionString = "URI=file:/Users/grigo/didactic/MPP/ExempleCurs/2017/database/tasks.db,Version=3";
			string connectionString = props["ConnectionString"];
            Console.WriteLine("SQLite ---Se deschide o conexiune la ... {0}", connectionString);
            return new SqliteConnection(connectionString);

			// Windows SQLite Connection, fisierul .db ar trebuie sa fie in directorul debug/bin
			//String connectionString = "Data Source=tasks.db;Version=3";
			//return new SQLiteConnection(connectionString);
		}
	}
}
