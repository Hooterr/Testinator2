using Microsoft.AspNetCore.Mvc;

namespace Testinator.Web
{
    public class DashboardController : Controller
    {
        public DashboardController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
