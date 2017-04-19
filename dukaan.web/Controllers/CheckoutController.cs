using dukaan.web.Models;
using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index(Node node, CheckoutContent content)
        {
            return View();
        }
    }
}
