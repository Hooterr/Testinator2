using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Testinator.Web.Database;

namespace Testinator.Web
{
    /// <summary>
    /// The controller for main admin panel
    /// </summary>
    [Authorize(Roles = ApplicationRoles.Admin)]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
