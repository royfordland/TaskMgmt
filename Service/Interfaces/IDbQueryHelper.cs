using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Service.Interfaces
{
	[ScopedService]
	public interface IDbQueryHelper
	{
		IEnumerable<T> QueryList<T>(string sql);

		IEnumerable<T> QueryList<T>(string sql, object parameters);

		T? QuerySingle<T>(string sql, object parameters);

		T QueryScalar<T>(string sql, object parameters);

		void Execute(string sql, object parameters);
	}
}