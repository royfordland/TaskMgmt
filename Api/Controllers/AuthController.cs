using Api.Contracts.Requests;
using Api.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/auth")]
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

		//[HttpPost("register")]
		//public ActionResult<AuthResponse> Register([FromBody] RegisterRequest req)
		//{
		//	if (string.IsNullOrWhiteSpace(req.Username) || string.IsNullOrWhiteSpace(req.Password))
		//		return BadRequest();

		//	// Hash password
		//	var passwordHash = BCrypt.Net.BCrypt.HashPassword(req.Password);

		//	var sql = @"
		//		INSERT INTO taskmgmt.public.""user"" (username, email, password)
		//		VALUES (@Username, @Email, @PasswordHash)
		//		RETURNING id, username, email, is_admin
		//	";

		//	var created = dbQueryHelper.QuerySingle<dynamic>(sql, new { req.Username, req.Email, PasswordHash = passwordHash });
		//	if (created == null) return StatusCode(500);

		//	long userId = (long)created.id;
		//	bool isAdmin = (bool)created.is_admin;
		//	var token = tokenService.CreateToken(userId, created.username, created.email, isAdmin);

		//	return Ok(new AuthResponse { Token = token });
		//}

		//[HttpPost("login")]
		//public ActionResult<AuthResponse> Login([FromBody] LoginRequest req)
		//{
		//	if (string.IsNullOrWhiteSpace(req.Username) || string.IsNullOrWhiteSpace(req.Password))
		//		return BadRequest();

		//	var sql = @"
		//		SELECT id, username, email, password, is_admin, is_active
		//		FROM taskmgmt.public.""user""
		//		WHERE username = @Username
		//		LIMIT 1
		//	";

		//	var user = dbQueryHelper.QuerySingle<dynamic>(sql, new { req.Username });
		//	if (user == null) return Unauthorized();

		//	if (!(bool)user.is_active) return Forbid();

		//	string storedHash = (string)user.password;
		//	if (!BCrypt.Net.BCrypt.Verify(req.Password, storedHash)) return Unauthorized();

		//	long userId = (long)user.id;
		//	bool isAdmin = (bool)user.is_admin;
		//	var token = tokenService.CreateToken(userId, user.username, user.email, isAdmin);

		//	return Ok(new AuthResponse { Token = token });
		//}
	}
}