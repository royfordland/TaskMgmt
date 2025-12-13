using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/task")]
	public class TaskController(ITaskService taskService) : Controller
	{
		[HttpGet()]
		public ActionResult<IEnumerable<Service.Models.TaskStatus>> GetTasks()
		{
			var statuses = taskService.GetTasks();

			return Ok(statuses);
		}
	}
}