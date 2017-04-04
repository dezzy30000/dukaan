using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class DetailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
