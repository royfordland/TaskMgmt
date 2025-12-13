using Service.Interfaces;
using TaskStatus = Service.Models.TaskStatus;

namespace Service
{
	public class TaskStatusService(IDbQueryHelper dbQueryHelper) : ITaskStatusService
	{
		public IEnumerable<TaskStatus> GetStatuses()
		{
			const string sql = "SELECT id, status, is_active FROM taskmgmt.public.task_status";

			return dbQueryHelper.QueryList<TaskStatus>(sql);
		}

		public TaskStatus? GetStatusById(int id)
		{
			var sql = @"
				SELECT id, status, is_active
				FROM taskmgmt.public.task_status
				WHERE id = @id ";
			return dbQueryHelper.QuerySingle<TaskStatus>(sql, new { id });
		}

		public int InsertStatus(string status, int userId)
		{
			var sql = @"
				INSERT INTO taskmgmt.public.task_status (status, created_by)
				VALUES (@status, @userId) RETURNING ID ";

			return dbQueryHelper.QueryScalar<int>(sql, new { status, userId });
		}

		public int UpdateStatus(TaskStatus status, int userId)
		{
			var sql = @"
				UPDATE taskmgmt.public.task_status
				SET status = @Status,
					is_active = @IsActive,
					updated_dt = NOW(),
					updated_by = @userId
				WHERE id = @Id ";

			var parameters = new
			{
				status.Status,
				status.IsActive,
				userId,
				status.Id
			};

			dbQueryHelper.Execute(sql, parameters);

			return status.Id;
		}

		public void DeleteStatus(int id, int userId)
		{
			var sql = @"
				UPDATE taskmgmt.public.task_status
				SET is_active = false,
					updated_dt = NOW(),
					updated_by = @userId
				WHERE id = @Id ";

			dbQueryHelper.Execute(sql, new { id, userId });
		}
	}
}