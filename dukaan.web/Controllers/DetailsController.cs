using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class DetailsController : Controller
    {
        public IActionResult Index(DetailsContent content)
        {
            return View();
        }
    }
}
