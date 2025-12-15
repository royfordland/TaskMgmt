using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models;
using System.Security.Claims;
using Task = Service.Models.Task;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/task")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class TaskController(ITaskService taskService) : ExtendedControllerBase
	{
		[HttpGet()]
		public ActionResult<IEnumerable<Task>> GetTasks()
		{
			var userid = UserId;

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
			// Extract user id from JWT claims (support common claim types)
			var nameId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("sub")?.Value;
			if (!long.TryParse(nameId, out var userId))
			{
				return Unauthorized();
			}

			var id = taskService.InsertTask(task, userId);

			return Ok(id);
		}

		[HttpPatch()]
		public ActionResult<long> UpdateTask(UpsertTask task)
		{
			// Extract user id from JWT claims (support common claim types)
			var nameId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("sub")?.Value;
			if (!long.TryParse(nameId, out var userId))
			{
				return Unauthorized();
			}

			var id = taskService.UpdateTask(task, userId);

			return Ok(id);
		}
	}
}