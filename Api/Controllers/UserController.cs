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
	public class UserController(IUserService userService) : ExtendedControllerBase(userService)
	{
		private readonly IUserService _userService = userService;

		[HttpGet()]
		public ActionResult<IEnumerable<User>> GetTasks()
		{
			var users = _userService.GetUsers();

			return Ok(users);
		}

		[HttpGet("{id}")]
		public ActionResult<User> GetUser(int id)
		{
			var task = _userService.GetUser(id);

			return Ok(task);
		}

		[HttpPost()]
		public ActionResult<long> InsertUser(string username, string email)
		{
			var id = _userService.InsertUser(username, email, "");

			return Ok(id);
		}

		[HttpPatch()]
		public ActionResult<long> UpdateUser(User user)
		{
			var id = _userService.UpdateUser(user, UserId);

			return Ok(id);
		}

		[HttpDelete()]
		public ActionResult<int> DeleteUser(int id)
		{
			_userService.DeleteUser(id, UserId);

			return Ok(StatusCodes.Status204NoContent);
		}
	}
}