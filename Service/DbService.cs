using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Service.Interfaces;

namespace Service
{
	public class DbService(IConfiguration configuration) : IDbService
	{
		private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
				?? Environment.GetEnvironmentVariable("CONNECTION_STRING")
				?? throw new InvalidOperationException("Connection string 'DefaultConnection' not configured.");

		/// <summary>
		/// Instantiates and opens a new SqlConnection suitable for use with Insight.Database.
		/// </summary>
		public IDbConnection CreateConnection()
		{
			var connection = new NpgsqlConnection(_connectionString);
			connection.Open();
			return connection;
		}
	}
}