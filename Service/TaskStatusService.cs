using Service.Interfaces;
using TaskStatus = Service.Models.TaskStatus;

namespace Service
{
	public class TaskStatusService(IDbQueryHelper dbQueryHelper) : ITaskStatusService
	{
		public IEnumerable<TaskStatus> GetStatuses(bool isActiveOnly = false)
		{
			var sql = $"SELECT id, status, is_active FROM taskmgmt.public.task_status {(isActiveOnly ? " WHERE is_active = true " : "")}";

			return dbQueryHelper.QueryList<TaskStatus>(sql);
		}

		public TaskStatus? GetStatus(long id)
		{
			var sql = @"
				SELECT id, status, is_active
				FROM taskmgmt.public.task_status
				WHERE id = @id ";
			return dbQueryHelper.QuerySingle<TaskStatus>(sql, new { id });
		}

		public long InsertStatus(string status, long userId)
		{
			var sql = @"
				INSERT INTO taskmgmt.public.task_status (status, created_by)
				VALUES (@status, @userId) RETURNING ID ";

			return dbQueryHelper.QueryScalar<long>(sql, new { status, userId });
		}

		public long UpdateStatus(TaskStatus status, long userId)
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

		public void DeleteStatus(long id, long userId)
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