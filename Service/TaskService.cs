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
				SELECT id, title, description, status_id, assigned_user_id
				FROM taskmgmt.public.task ";

			return dbQueryHelper.QueryList<Task>(sql);
		}

		public int InsertTask(UpsertTask task, int userId)
		{
			var sql = @"
				INSERT INTO taskmgmt.public.task (title, description, status_id, assigned_user_id)
				VALUES (@Title, @Description, @StatusId, @AssignedUserId) ";

			var parameters = new
			{
				task.Title,
				task.Description,
				task.StatusId,
				task.AssignedUserId,
				userId
			};

			return dbQueryHelper.QueryScalar<int>(sql, parameters);
		}

		public int UpdateTask(UpsertTask task, int userId)
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