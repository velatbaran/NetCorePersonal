using Microsoft.AspNetCore.Mvc;

namespace NetCorePersonal.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
