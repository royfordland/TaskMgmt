using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Interfaces;
using TaskStatus = Service.Models.TaskStatus;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/admin")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class AdminController(IAdminService adminService, ITaskStatusService taskStatusService, IUserService userService) : ExtendedControllerBase(userService)
	{
		[HttpGet("taskstatuses")]
		public ActionResult<IEnumerable<TaskStatus>> GetStatuses()
		{
			var statuses = taskStatusService.GetStatuses();

			return Ok(statuses);
		}

		[HttpGet("taskstatus/{id}")]
		public ActionResult<TaskStatus> GetStatus(int id)
		{
			var status = taskStatusService.GetStatus(id);

			return Ok(status);
		}

		[HttpPost("taskstatus")]
		public ActionResult<TaskStatus> InsertStatus(string status)
		{
			int userId = 1; // Placeholder for authenticated user ID

			var id = taskStatusService.InsertStatus(status, userId);

			return Ok(id);
		}

		[HttpPatch("taskstatus")]
		public ActionResult<TaskStatus> UpdateStatus(TaskStatus status)
		{
			int userId = 1; // Placeholder for authenticated user ID

			var id = taskStatusService.UpdateStatus(status, userId);

			return Ok(id);
		}

		[HttpDelete("taskstatus")]
		public ActionResult<TaskStatus> DeleteStatus(int id)
		{
			int userId = 1; // Placeholder for authenticated user ID

			taskStatusService.DeleteStatus(id, userId);

			return Ok(StatusCodes.Status204NoContent);
		}

		[HttpPatch("user")]
		public ActionResult<TaskStatus> UpdateUsername(string name, int id)
		{
			int userId = 1; // Placeholder for authenticated user ID

			adminService.UpdateUsername(name, id, userId);

			return Ok(StatusCodes.Status204NoContent);
		}
	}
}