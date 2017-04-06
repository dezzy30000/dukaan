using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class TypographyController : Controller
    {
        public IActionResult Index(TypographyContent content)
        {
            return View();
        }
    }
}
