using Microsoft.AspNetCore.Mvc;

namespace Recon_proto.Controllers
{
	public class KnockOffController : Controller
	{
		public IActionResult AutoRuleBase()
		{
			return View();
		}
		public IActionResult PreviewRuleBase()
		{
			return View();
		}
		public IActionResult ManualKO()
		{
			return View();
		}

		public IActionResult UndoKO()
		{
			return View();
		}

		public IActionResult UndoKObyJob()
		{
			return View();
		}

		public IActionResult KoFilebased()
		{
			return View();
		}
	}
}
