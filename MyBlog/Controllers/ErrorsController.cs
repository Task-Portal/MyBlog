using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult UploadError()
        {
            return View();
        }

        public IActionResult ExtentionError()
        {
            return View();
        }
    }
}
