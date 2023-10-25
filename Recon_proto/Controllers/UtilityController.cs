using Microsoft.AspNetCore.Mvc;

namespace Recon_proto.Controllers
{
	public class UtilityController : Controller
	{
		public IActionResult InProgress()
		{
			return View();
		}
		public IActionResult ProcessCompleted()
		{
			return View();
		}
	}
}
