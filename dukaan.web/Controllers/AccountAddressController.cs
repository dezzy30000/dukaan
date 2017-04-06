using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class AccountAddressController : Controller
    {
        public IActionResult Index(AccountAddressContent content)
        {
            return View();
        }
    }
}
