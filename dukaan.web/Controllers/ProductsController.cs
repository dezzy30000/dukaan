using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index(ProductsContent content)
        {
            return View();
        }
    }
}
