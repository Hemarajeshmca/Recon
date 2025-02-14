using Microsoft.AspNetCore.Mvc;

namespace Recon_proto.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var conversionTypes = _configuration.GetSection("ConversionSettings:ConversionTypes").Get<List<ConversionType>>();
            var defaultType = _configuration["ConversionSettings:DefaultConversionType"];

            ViewBag.ConversionTypes = conversionTypes;
            ViewBag.DefaultConversionType = defaultType;

            return View();
        }

        public class ConversionType
        {
            public string Key { get; set; }
            public string Label { get; set; }
        }
    }
}
