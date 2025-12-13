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
			return _connection.QuerySql<T>(sql);
		}

		public virtual IEnumerable<T> QueryList<T>(string sql, object parameters)
		{
			return _connection.QuerySql<T>(sql, parameters);
		}

		public T? QuerySingle<T>(string sql, object parameters)
		{
			return _connection.QuerySql<T>(sql, parameters).FirstOrDefault();
		}

		public T QueryScalar<T>(string sql, object parameters)
		{
			return _connection.QuerySql<T>(sql, parameters).First();
		}

		public void Execute(string sql, object parameters)
		{
			_connection.ExecuteSql(sql, parameters);
		}
	}
}