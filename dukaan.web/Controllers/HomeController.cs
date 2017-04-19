using dukaan.web.Models;
using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(Node node, HomeContent content)
        {
            return View();
        }
    }
}
