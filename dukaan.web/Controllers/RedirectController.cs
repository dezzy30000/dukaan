using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class RedirectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
