using Microsoft.AspNetCore.Mvc;

namespace Recon_proto.Controllers
{
	public class ConnnectorController : Controller
	{

        private readonly IConfiguration _configuration;

        public ConnnectorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult connectors()
		{
            var conn = _configuration.GetValue<string>("AppSettings:connector");
            ViewBag.conn = conn;
            return View();
		}
	}
}
