using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult Index(NotFoundContent content)
        {
            return View();
        }
    }
}
