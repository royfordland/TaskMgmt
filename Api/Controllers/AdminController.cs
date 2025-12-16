using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using TaskStatus = Service.Models.TaskStatus;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/admin")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class AdminController(IAdminService adminService, ITaskStatusService taskStatusService, IUserService userService) : ExtendedControllerBase(userService)
	{
		[HttpGet("taskstatus")]
		public ActionResult<IEnumerable<TaskStatus>> GetStatuses()
		{
			if (LoggedInUser is not null && LoggedInUser.IsAdmin)
			{
				var statuses = taskStatusService.GetStatuses();

				return Ok(statuses);
			}
			else
			{
				return Unauthorized();
			}
		}

		[HttpGet("taskstatus/{id}")]
		public ActionResult<TaskStatus> GetStatus(int id)
		{
			if (LoggedInUser is not null && LoggedInUser.IsAdmin)
			{
				var status = taskStatusService.GetStatus(id);

				return Ok(status);
			}
			else
			{
				return Unauthorized();
			}
		}

		[HttpPost("taskstatus")]
		public ActionResult<TaskStatus> InsertStatus(string status)
		{
			if (LoggedInUser is not null && LoggedInUser.IsAdmin)
			{
				var id = taskStatusService.InsertStatus(status, UserId);

				return Ok(id);
			}
			else
			{
				return Unauthorized();
			}
		}

		[HttpPatch("taskstatus")]
		public ActionResult<TaskStatus> UpdateStatus(TaskStatus status)
		{
			if (LoggedInUser is not null && LoggedInUser.IsAdmin)
			{
				var id = taskStatusService.UpdateStatus(status, UserId);

				return Ok(id);
			}
			else
			{
				return Unauthorized();
			}
		}

		[HttpDelete("taskstatus")]
		public ActionResult<TaskStatus> DeleteStatus(int id)
		{
			if (LoggedInUser is not null && LoggedInUser.IsAdmin)
			{
				taskStatusService.DeleteStatus(id, UserId);

				return Ok(StatusCodes.Status204NoContent);
			}
			else
			{
				return Unauthorized();
			}
		}

		[HttpPatch("user")]
		public ActionResult<TaskStatus> UpdateUsername(string name, int id)
		{
			if (LoggedInUser is not null && LoggedInUser.IsAdmin)
			{
				adminService.UpdateUsername(name, id, UserId);

				return Ok(StatusCodes.Status204NoContent);
			}
			else
			{
				return Unauthorized();
			}
		}
	}
}