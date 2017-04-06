using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index(RegisterContent content)
        {
            return View();
        }
    }
}
