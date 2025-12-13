using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Service.Interfaces
{
	[ScopedService]
	public interface IAdminService
	{
		void UpdateUsername(string name, int id, int userId);
	}
}