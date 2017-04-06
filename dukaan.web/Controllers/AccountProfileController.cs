using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class AccountProfileController : Controller
    {
        public IActionResult Index(AccountProfileContent content)
        {
            return View();
        }
    }
}
