using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class RedirectController : Controller
    {
        public IActionResult Index(string redirectPath)
        {
            return View();
        }
    }
}
