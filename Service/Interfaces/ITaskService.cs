using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Service.Interfaces
{
	[ScopedService]
	public interface ITaskService
	{
		IEnumerable<Models.TaskStatus> GetTaskStatuses();
	}
}