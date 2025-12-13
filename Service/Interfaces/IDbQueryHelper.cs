using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Service.Interfaces
{
	[ScopedService]
	public interface IDbQueryHelper
	{
		IEnumerable<T> QueryList<T>(string sql);

		IEnumerable<T> QueryList<T>(string sql, object parameters);
	}
}