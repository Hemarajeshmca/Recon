using Microsoft.AspNetCore.Mvc;

namespace Recon_proto.Controllers
{
	public class ReconversionController : Controller
	{
		private IConfiguration _configuration;
		public ReconversionController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		string urlstring = "";
		public IActionResult ReconversionList()
		{
			return View();
		}
	}
}
