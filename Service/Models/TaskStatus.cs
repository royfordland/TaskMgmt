namespace Service.Models
{
	public class TaskStatus
	{
		public int Id { get; set; }

		public string Status { get; set; } = string.Empty;

		public bool IsActive { get; set; }
	}
}