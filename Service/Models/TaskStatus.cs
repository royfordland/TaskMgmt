using Insight.Database;

namespace Service.Models
{
	public class TaskStatus
	{
		public int Id { get; set; }

		public string Status { get; set; } = string.Empty;

		[Column("is_active")]
		public bool IsActive { get; set; }
	}
}