using dukaan.web.Models;
using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class WishListController : Controller
    {
        public IActionResult Index(Node node, WishListContent content)
        {
            return View();
        }
    }
}
