using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index(BlogContent content)
        {
            return View();
        }
    }
}
