using Service.Models;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;
using Task = Service.Models.Task;

namespace Service.Interfaces
{
	[ScopedService]
	public interface ITaskService
	{
		IEnumerable<Task> GetTasks();

		int InsertTask(UpsertTask task, int userId);

		int UpdateTask(UpsertTask task, int userId);
	}
}