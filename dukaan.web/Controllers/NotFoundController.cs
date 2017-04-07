using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
