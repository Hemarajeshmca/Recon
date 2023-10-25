using Microsoft.AspNetCore.Mvc;

namespace Recon_proto.Controllers
{
	public class SupportFileController : Controller
	{
		public IActionResult Rulebased()
		{
			return View();
		}
		public IActionResult Linebased()
		{
			return View();
		}
	}
}
