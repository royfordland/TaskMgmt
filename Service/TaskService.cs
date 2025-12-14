using Service.Interfaces;
using Service.Models;
using Task = Service.Models.Task;

namespace Service
{
	public class TaskService(IDbQueryHelper dbQueryHelper) : ITaskService
	{
		public IEnumerable<Task> GetTasks()
		{
			var sql = @"
				SELECT
					t.id,
					title,
					description,
					status,
					username as assigneduser
				FROM taskmgmt.public.task t
				INNER JOIN taskmgmt.public.""user"" u
					ON t.assigned_user_id = u.id
				INNER JOIN taskmgmt.public.task_status ts
					ON t.status_id = ts.id ";

			return dbQueryHelper.QueryList<Task>(sql);
		}

		public UpsertTask? GetTask(long id)
		{
			var sql = @"
				SELECT id, title, description, status_id, assigned_user_id
				FROM taskmgmt.public.task
				WHERE t.id = @id ";

			return dbQueryHelper.QuerySingle<UpsertTask>(sql, new { id });
		}

		public long InsertTask(UpsertTask task, long userId)
		{
			var sql = @"
				INSERT INTO taskmgmt.public.task (title, description, status_id, assigned_user_id, created_by)
				VALUES (@Title, @Description, @StatusId, @AssignedUserId, @userId) RETURNING ID ";

			var parameters = new
			{
				task.Title,
				task.Description,
				task.StatusId,
				task.AssignedUserId,
				userId
			};

			return dbQueryHelper.QueryScalar<long>(sql, parameters);
		}

		public long UpdateTask(UpsertTask task, long userId)
		{
			var sql = @"
				UPDATE taskmgmt.public.task
				SET title = @Title,
					description = @Description,
					status_id = @StatusId,
					assigned_user_id = @AssignedUserId,
					updated_dt = NOW(),
					updated_by = @userId
				WHERE id = @Id ";

			var parameters = new
			{
				task.Title,
				task.Description,
				task.StatusId,
				task.AssignedUserId,
				userId,
				task.Id
			};

			dbQueryHelper.Execute(sql, parameters);

			return task.Id!.Value;
		}
	}
}