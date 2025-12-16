using Insight.Database;

namespace Service.Models
{
	public abstract class TaskBase
	{
		public long? Id { get; set; }

		public string Title { get; set; } = "";

		public string Description { get; set; } = "";
	}

	public class Task : TaskBase
	{
		public string Status { get; set; } = "";

		public string AssignedUser { get; set; } = "";
	}

	public class UpsertTask : TaskBase
	{
		[Column("status_id")]
		public long StatusId { get; set; }

		[Column("assigned_user_id")]
		public long AssignedUserId { get; set; }
	}
}