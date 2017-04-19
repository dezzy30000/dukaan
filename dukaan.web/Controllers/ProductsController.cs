using dukaan.web.Models;
using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index(Node node, ProductsContent content)
        {
            return View();
        }
    }
}
