using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index(LoginContent content)
        {
            return View();
        }
    }
}
