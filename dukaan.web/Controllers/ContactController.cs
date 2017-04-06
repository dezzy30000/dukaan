using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index(ContactContent content)
        {
            return View();
        }
    }
}
