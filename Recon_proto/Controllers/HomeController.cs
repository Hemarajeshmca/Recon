using Microsoft.AspNetCore.Mvc;

namespace Recon_proto.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
