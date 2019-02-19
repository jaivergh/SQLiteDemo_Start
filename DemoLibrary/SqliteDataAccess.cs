using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary
{
	public class SqliteDataAccess
	{
		public static List<PersonModel> LoadPeople()
		{
			using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
			{
				var output = cnn.Query<PersonModel>("SELECT * FROM Person", new DynamicParameters());
				return output.ToList();
			}
		}

		public static void SavePerson(PersonModel person)
		{
			using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
			{
				cnn.Execute("INSERT INTO Person (FirstName, LastName) VALUES (@FirstName, @LastName)", person);
			}
		}

		private static string LoadConnectionString(string id = "Default")
		{
			return ConfigurationManager.ConnectionStrings[id].ConnectionString;
		}
	}
}
