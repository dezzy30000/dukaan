using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
