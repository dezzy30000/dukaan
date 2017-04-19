using dukaan.web.Models;
using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index(Node node, BlogContent content)
        {
            return View();
        }
    }
}
