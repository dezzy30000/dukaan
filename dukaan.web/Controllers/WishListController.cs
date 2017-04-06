using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class WishListController : Controller
    {
        public IActionResult Index(WishListContent content)
        {
            return View();
        }
    }
}
