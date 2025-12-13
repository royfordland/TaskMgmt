using System.Data;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Service.Interfaces
{
	[ScopedService]
	public interface IDbService
	{
		/// <summary>
		/// Create and return an open DB connection for use with Insight.Database.
		/// </summary>
		IDbConnection CreateConnection();
	}
}