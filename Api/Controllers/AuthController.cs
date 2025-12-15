using Api.Contracts.Requests;
using Api.Contracts.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/auth")]
	[AllowAnonymous]
	public class AuthController(IAuthService authService, ITokenService tokenService) : ControllerBase
	{
		[HttpPost("register")]
		public ActionResult<long> Register([FromBody] RegisterRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
			{
				return BadRequest();
			}

			var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
			var userId = authService.RegisterUser(request.Username, request.Email, passwordHash);

			if (userId.HasValue)
			{
				return Ok(userId);
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost("login")]
		[AllowAnonymous]
		public ActionResult<AuthResponse> Login([FromBody] LoginRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
			{
				return BadRequest();
			}

			var user = authService.GetUser(request.Username);

			if (user is not null)
			{
				if (user.IsActive)
				{
					if (BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
					{
						var token = tokenService.CreateToken(user);
						return Ok(new AuthResponse { Token = token });
					}
					else
					{
						return Unauthorized();
					}
				}
				else
				{
					return Forbid();
				}
			}
			else
			{
				return Unauthorized();
			}
		}
	}
}