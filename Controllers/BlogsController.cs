using Microsoft.AspNetCore.Mvc;

namespace MyBlogApp.Controllers
{
    public class BlogsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
