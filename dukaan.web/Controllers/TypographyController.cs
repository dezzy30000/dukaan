using dukaan.web.Models;
using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class TypographyController : Controller
    {
        public IActionResult Index(Node node, TypographyContent content)
        {
            return View();
        }
    }
}
