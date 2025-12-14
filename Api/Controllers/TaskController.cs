using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models;
using Task = Service.Models.Task;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/task")]
	public class TaskController(ITaskService taskService) : Controller
	{
		[HttpGet()]
		public ActionResult<IEnumerable<Task>> GetTasks()
		{
			var tasks = taskService.GetTasks();

			return Ok(tasks);
		}

		[HttpGet("{id}")]
		public ActionResult<UpsertTask> GetTask(int id)
		{
			var task = taskService.GetTask(id);

			return Ok(task);
		}

		[HttpPost()]
		public ActionResult<int> InsertTask(UpsertTask task)
		{
			int userId = 1; // Placeholder for authenticated user ID

			var id = taskService.InsertTask(task, userId);

			return Ok(id);
		}

		[HttpPatch()]
		public ActionResult<int> UpdateTask(UpsertTask task)
		{
			int userId = 1; // Placeholder for authenticated user ID

			var id = taskService.UpdateTask(task, userId);

			return Ok(id);
		}
	}
}