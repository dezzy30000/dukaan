using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class CheckoutWizardController : Controller
    {
        public IActionResult Index(CheckoutWizardContent content)
        {
            return View();
        }
    }
}
