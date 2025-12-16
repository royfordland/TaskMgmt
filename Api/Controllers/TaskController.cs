using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models;
using Task = Service.Models.Task;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/task")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class TaskController(ITaskService taskService, ITaskStatusService taskStatusService, IUserService userService) : ExtendedControllerBase(userService)
	{
		private readonly ITaskService taskService = taskService;

		[HttpGet("statuslist")]
		public ActionResult<IEnumerable<Task>> GetStatuses()
		{
			var statuses = taskStatusService.GetStatuses(true);

			return Ok(statuses);
		}

		[HttpGet()]
		public ActionResult<IEnumerable<Task>> GetTasks()
		{
			var tasks = taskService.GetTasks();

			return Ok(tasks);
		}

		[HttpGet("{id}")]
		public ActionResult<UpsertTask> GetTask(long id)
		{
			var task = taskService.GetTask(id);

			return Ok(task);
		}

		[HttpPost()]
		public ActionResult<long> InsertTask(UpsertTask task)
		{
			var id = taskService.InsertTask(task, UserId);

			return Ok(id);
		}

		[HttpPatch()]
		public ActionResult<long> UpdateTask(UpsertTask task)
		{
			var id = taskService.UpdateTask(task, UserId);

			return Ok(id);
		}
	}
}