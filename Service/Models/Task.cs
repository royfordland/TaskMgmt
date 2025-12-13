namespace Service.Models
{
	public abstract class TaskBase
	{
		public int? Id { get; set; }

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
		public int StatusId { get; set; }

		public int AssignedUserId { get; set; }
	}
}