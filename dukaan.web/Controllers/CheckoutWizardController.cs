using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class CheckoutWizardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
