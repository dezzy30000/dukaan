using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class AccountHistoryController : Controller
    {
        public IActionResult Index(AccountHistoryContent content)
        {
            return View();
        }
    }
}
