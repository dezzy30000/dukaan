using dukaan.web.Models;
using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class BlogDetailsController : Controller
    {
        public IActionResult Index(Node node, BlogDetailsContent content)
        {
            return View();
        }
    }
}
