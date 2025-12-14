using Service.Models;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Service.Interfaces
{
	[ScopedService]
	public interface ITokenService
	{
		string CreateToken(User user);
	}
}