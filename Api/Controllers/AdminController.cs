using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using TaskStatus = Service.Models.TaskStatus;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/admin")]
	public class AdminController(IAdminService adminService, ITaskStatusService taskStatusService) : Controller
	{
		[HttpGet("/statuses")]
		public ActionResult<IEnumerable<TaskStatus>> GetStatuses()
		{
			var statuses = taskStatusService.GetStatuses();

			return Ok(statuses);
		}

		[HttpGet("/status/{id}")]
		public ActionResult<TaskStatus> GetStatus(int id)
		{
			var status = taskStatusService.GetStatusById(id);

			return Ok(status);
		}

		[HttpPost("/status")]
		public ActionResult<TaskStatus> InsertStatus(string status)
		{
			int userId = 1; // Placeholder for authenticated user ID

			var id = taskStatusService.InsertStatus(status, userId);

			return Ok(id);
		}

		[HttpPut("/status")]
		public ActionResult<TaskStatus> UpdateStatus(TaskStatus status)
		{
			int userId = 1; // Placeholder for authenticated user ID

			var id = taskStatusService.UpdateStatus(status, userId);

			return Ok(id);
		}

		[HttpDelete("/status")]
		public ActionResult<TaskStatus> DeleteStatus(int id)
		{
			int userId = 1; // Placeholder for authenticated user ID

			taskStatusService.DeleteStatus(id, userId);

			return Ok();
		}

		[HttpPut("/user")]
		public ActionResult<TaskStatus> UpdateUsername(string name, int id)
		{
			int userId = 1; // Placeholder for authenticated user ID

			adminService.UpdateUsername(name, id, userId);

			return Ok();
		}
	}
}