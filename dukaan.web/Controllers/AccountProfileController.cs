using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class AccountProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
