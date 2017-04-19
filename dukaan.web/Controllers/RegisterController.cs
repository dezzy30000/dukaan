using dukaan.web.Models;
using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index(Node node, RegisterContent content)
        {
            return View();
        }
    }
}
