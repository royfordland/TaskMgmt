using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
