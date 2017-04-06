using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(HomeContent content)
        {
            return View();
        }
    }
}
