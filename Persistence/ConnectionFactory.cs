using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Persistence
{
    public abstract class ConnectionFactory
	{
		private static ConnectionFactory instance;
		protected ConnectionFactory()
		{
		}
		public static ConnectionFactory getInstance()
		{
			if (instance == null)
			{
				Assembly assem = Assembly.GetExecutingAssembly();
				Type[] types = assem.GetTypes();
				foreach (var type in types)
				{
					if (type.IsSubclassOf(typeof(ConnectionFactory)))
						instance = (ConnectionFactory)Activator.CreateInstance(type);
				}
			}
			return instance;
		}

		public abstract IDbConnection createConnection(IDictionary<string, string> props);
	}
}


