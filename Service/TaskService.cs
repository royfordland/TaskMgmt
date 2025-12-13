using Service.Interfaces;

namespace Service
{
	public class TaskService(IDbQueryHelper dbQueryHelper) : ITaskService
	{
		public IEnumerable<Models.TaskStatus> GetTaskStatuses()
		{
			const string sql = "SELECT id, status, is_active FROM taskmgmt.public.task_status";

			return dbQueryHelper.QueryList<Models.TaskStatus>(sql);
		}
	}
}