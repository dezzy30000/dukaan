using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class WishListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
