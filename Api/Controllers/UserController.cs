using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/user")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class UserController(IUserService userService) : Controller
	{
		[HttpGet()]
		public ActionResult<IEnumerable<User>> GetTasks()
		{
			var users = userService.GetUsers();

			return Ok(users);
		}

		[HttpGet("{id}")]
		public ActionResult<User> GetUser(int id)
		{
			var task = userService.GetUser(id);

			return Ok(task);
		}

		[HttpPost()]
		public ActionResult<long> InsertUser(string username, string email)
		{
			var id = userService.InsertUser(username, email, "");

			return Ok(id);
		}

		[HttpPatch()]
		public ActionResult<long> UpdateUser(User user)
		{
			var userId = 1; // Placeholder for authenticated user ID

			var id = userService.UpdateUser(user, userId);

			return Ok(id);
		}

		[HttpDelete()]
		public ActionResult<int> DeleteUser(int id)
		{
			int userId = 1; // Placeholder for authenticated user ID

			userService.DeleteUser(id, userId);

			return Ok(StatusCodes.Status204NoContent);
		}
	}
}