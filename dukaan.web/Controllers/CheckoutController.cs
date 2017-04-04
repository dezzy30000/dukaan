using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
