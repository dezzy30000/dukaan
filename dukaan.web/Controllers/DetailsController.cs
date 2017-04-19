using dukaan.web.Models;
using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class DetailsController : Controller
    {
        public IActionResult Index(Node node, DetailsContent content)
        {
            return View();
        }
    }
}
