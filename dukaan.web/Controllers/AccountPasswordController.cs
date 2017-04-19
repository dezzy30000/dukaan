using dukaan.web.Models;
using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class AccountPasswordController : Controller
    {
        public IActionResult Index(Node node, AccountPasswordContent content)
        {
            return View();
        }
    }
}
