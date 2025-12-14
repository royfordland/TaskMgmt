using Service.Models;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Service.Interfaces
{
	[ScopedService]
	public interface IAuthService
	{
		long? RegisterUser(string username, string email, string password);

		User? GetUser(string username);
	}
}