using Microsoft.AspNetCore.Mvc;

namespace Recon_proto.Controllers
{
    public class PluginController : Controller
    {
        private readonly IConfiguration _configuration;

        public PluginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Plugin()
        {
            var conn = _configuration.GetValue<string>("AppSettings:connector");
            var user_code = _configuration.GetValue<string>("AppSettings:user_code");

            // Validate URL
            if (!Uri.TryCreate(conn, UriKind.Absolute, out Uri validUri))
            {
                return BadRequest("Invalid connector URL");
            }

            //// Allow only trusted domain (IMPORTANT)
            //if (validUri.Host != "yourdomain.com")   // change this
            //{
            //    return BadRequest("Unauthorized source");
            //}

            // Build URL safely
            var safeUrl = $"{validUri}?user_code={Uri.EscapeDataString(user_code)}";
            ViewBag.SafeUrl = safeUrl;
            return View();
        }
        public IActionResult Importedfile()
        {
            var conn = _configuration.GetValue<string>("AppSettings:Importedfile");
            var user_code = _configuration.GetSection("AppSettings")["user_code"].ToString();
            ViewBag.conn = conn + "?user_code=" + user_code;
            return View();
        }
        public IActionResult pipelinedataset()
        {
            var conn = _configuration.GetValue<string>("AppSettings:pipelinedataset");
            var user_code = _configuration.GetSection("AppSettings")["user_code"].ToString();
            ViewBag.conn = conn + "?user_code=" + user_code;
            return View();
        }
    }
}
