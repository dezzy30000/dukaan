using dukaan.web.Models;
using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index(Node node, AboutContent content)
        {
            return View();
        }
    }
}
