using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	public class ExtendedControllerBase : ControllerBase
	{
		private long? _userId;

		public long UserId
		{
			get
			{
				if (!_userId.HasValue)
				{
					var nameId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
						?? User.FindFirst("sub")?.Value;

					if (!long.TryParse(nameId, out var userId))
					{
						throw new UnauthorizedAccessException("User ID claim is missing or invalid.");
					}

					_userId = userId;
				}

				return _userId.Value;
			}
		}
	}
}