using dukaan.web.Models;
using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class AccountHistoryController : Controller
    {
        public IActionResult Index(Node node, AccountHistoryContent content)
        {
            return View();
        }
    }
}
