using dukaan.web.Models;
using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class CompareController : Controller
    {
        public IActionResult Index(Node node, CompareContent content)
        {
            return View();
        }
    }
}
