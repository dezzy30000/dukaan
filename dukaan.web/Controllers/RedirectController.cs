using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class RedirectController : Controller
    {
        public IActionResult Index(RedirectContent content)
        {
            return View();
        }
    }
}
