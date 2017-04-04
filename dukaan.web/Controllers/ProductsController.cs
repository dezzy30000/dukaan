using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
