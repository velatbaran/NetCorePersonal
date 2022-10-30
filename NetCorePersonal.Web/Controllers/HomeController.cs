using Microsoft.AspNetCore.Mvc;
using NetCorePersonal.Repository.DataContext;
using System.Diagnostics;

namespace NetCorePersonal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public HomeController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}