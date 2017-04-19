using dukaan.web.Models;
using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index(Node node, LoginContent content)
        {
            return View();
        }
    }
}
