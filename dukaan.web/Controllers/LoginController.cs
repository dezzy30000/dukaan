using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
