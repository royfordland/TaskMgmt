using Service.Models;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;
using Task = Service.Models.Task;

namespace Service.Interfaces
{
	[ScopedService]
	public interface ITaskService
	{
		IEnumerable<Task> GetTasks();

		UpsertTask? GetTask(long id);

		long InsertTask(UpsertTask task, long userId);

		long UpdateTask(UpsertTask task, long userId);
	}
}