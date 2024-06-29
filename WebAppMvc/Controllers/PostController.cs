using Microsoft.AspNetCore.Mvc;

namespace WebAppMvc.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
