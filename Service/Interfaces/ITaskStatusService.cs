using TanvirArjel.Extensions.Microsoft.DependencyInjection;
using TaskStatus = Service.Models.TaskStatus;

namespace Service.Interfaces
{
	[ScopedService]
	public interface ITaskStatusService
	{
		IEnumerable<TaskStatus> GetStatuses();

		TaskStatus? GetStatusById(int id);

		int InsertStatus(string status, int userId);

		int UpdateStatus(TaskStatus status, int userId);

		void DeleteStatus(int id, int userId);
	}
}