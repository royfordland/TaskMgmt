using TanvirArjel.Extensions.Microsoft.DependencyInjection;
using TaskStatus = Service.Models.TaskStatus;

namespace Service.Interfaces
{
	[ScopedService]
	public interface ITaskStatusService
	{
		IEnumerable<TaskStatus> GetStatuses(bool isActiveOnly = false);

		TaskStatus? GetStatus(long id);

		long InsertStatus(string status, long userId);

		long UpdateStatus(TaskStatus status, long userId);

		void DeleteStatus(long id, long userId);
	}
}