using Microsoft.AspNetCore.Mvc;

namespace Recon_proto.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Users()
        {
            return View();
        }
    }
}
