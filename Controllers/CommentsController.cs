using Microsoft.AspNetCore.Mvc;

namespace MyBlogApp.Controllers
{
    public class CommentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
