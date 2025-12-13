using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/task")]
	public class TaskController(ITaskService taskService) : Controller
	{
		[HttpGet("/statuses")]
		public ActionResult<IEnumerable<Service.Models.TaskStatus>> GetTaskStatuses()
		{
			var statuses = taskService.GetTaskStatuses();

			return Ok(statuses);
		}
	}
}