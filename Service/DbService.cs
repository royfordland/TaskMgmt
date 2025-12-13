using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Service.Interfaces;

namespace Service
{
	public class DbService : IDbService
	{
		private readonly string _connectionString;

		public DbService(IConfiguration configuration)
		{
			// Read from "ConnectionStrings:DefaultConnection" first, fall back to environment variable.
			_connectionString = configuration.GetConnectionString("DefaultConnection")
				?? Environment.GetEnvironmentVariable("CONNECTION_STRING")
				?? throw new InvalidOperationException("Connection string 'DefaultConnection' not configured.");
		}

		/// <summary>
		/// Instantiates and opens a new SqlConnection suitable for use with Insight.Database.
		/// </summary>
		public IDbConnection CreateConnection()
		{
			var connection = new SqlConnection(_connectionString);
			connection.Open();
			return connection;
		}
	}
}