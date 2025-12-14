using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces;
using Service.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
	public class TokenService(IConfiguration configuration) : ITokenService
	{
		private readonly JwtSettings _settings = configuration.GetSection("Jwt").Get<JwtSettings>() ?? throw new InvalidOperationException("Jwt configuration missing");

		public string CreateToken(User user)
		{
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim("is_admin", user.IsAdmin ? "1" : "0")
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _settings.Issuer,
				audience: _settings.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes),
				signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}