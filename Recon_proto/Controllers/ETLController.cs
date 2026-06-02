using Microsoft.AspNetCore.Mvc;

namespace Recon_proto.Controllers
{
    public class ETLController : Controller
    {
        private readonly IConfiguration _configuration;

        public ETLController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult ETL()
        {
            var pipe = _configuration.GetValue<string>("AppSettings:pipeline");
            var user_code = _configuration.GetSection("AppSettings")["user_code"].ToString();
            ViewBag.pipe = pipe + "?user_code=" + user_code;
            return View();
        }
    }
}
