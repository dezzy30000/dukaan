using dukaan.web.Models;
using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index(Node node, ContactContent content)
        {
            return View();
        }
    }
}
