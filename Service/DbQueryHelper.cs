using Insight.Database;
using Service.Interfaces;
using System.Data;

namespace Service
{
	public class DbQueryHelper(IDbService dbService) : IDbQueryHelper
	{
		private readonly IDbConnection _connection = dbService.CreateConnection();

		public virtual IEnumerable<T> QueryList<T>(string sql)
		{
			return _connection.Query<T>(sql);
		}

		public virtual IEnumerable<T> QueryList<T>(string sql, object parameters)
		{
			return _connection.Query<T>(sql, parameters);
		}
	}
}