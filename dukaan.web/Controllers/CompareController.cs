using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class CompareController : Controller
    {
        public IActionResult Index(CompareContent content)
        {
            return View();
        }
    }
}
