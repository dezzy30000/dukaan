using dukaan.web.Models.Content;
using Microsoft.AspNetCore.Mvc;

namespace dukaan.web.Controllers
{
    public class BlogDetailsController : Controller
    {
        public IActionResult Index(BlogDetailsContent content)
        {
            return View();
        }
    }
}
