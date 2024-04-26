using Microsoft.AspNetCore.Mvc;

namespace Recon_proto.Controllers
{
	public class PipelineController : Controller
	{

        private readonly IConfiguration _configuration;

        public PipelineController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Pipeline()
		{
            var pipe = _configuration.GetValue<string>("AppSettings:pipeline");
            ViewBag.pipe = pipe;
            return View();
		}
	}
}
