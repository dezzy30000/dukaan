using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
