using Microsoft.AspNetCore.Mvc;

namespace Recon_proto.Controllers
{
	public class DatasetfileController : Controller
	{
		public IActionResult Inputfile()
		{
			return View();
		}
		public IActionResult Datasetconversion()
		{
			return View();
		}
	}
}
