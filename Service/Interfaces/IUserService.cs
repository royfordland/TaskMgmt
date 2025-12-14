using Service.Models;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Service.Interfaces
{
	[ScopedService]
	public interface IUserService
	{
		IEnumerable<User> GetUsers();

		User? GetUser(long id);

		long InsertUser(string username, string email, string password);

		long UpdateUser(User user, long userId);

		void DeleteUser(long id, long userId);
	}
}