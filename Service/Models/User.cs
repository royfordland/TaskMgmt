using Insight.Database;

namespace Service.Models
{
	public class User
	{
		public long Id { get; set; }

		public string Username { get; set; } = "";

		public string Email { get; set; } = "";

		public string Password { get; set; } = "";

		[Column("is_admin")]
		public bool IsAdmin { get; set; }

		[Column("is_active")]
		public bool IsActive { get; set; }
	}
}