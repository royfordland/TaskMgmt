using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	public class TaskController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
