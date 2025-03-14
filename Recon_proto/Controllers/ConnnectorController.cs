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
            var user_code = _configuration.GetSection("AppSettings")["user_code"].ToString();
            ViewBag.conn = conn+"?user_code=" + user_code;
            return View();
		}
	}
}
