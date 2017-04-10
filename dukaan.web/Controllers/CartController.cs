using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index(CartContent content)
        {
            return View();
        }
    }
}
