using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Testinator.Web
{
    /// <summary>
    /// The controller for main user dashboard page that displays his data
    /// </summary>
    [Authorize]
    public class DashboardController : Controller
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DashboardController()
        {
        }

        #endregion

        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                FirstName = User.Identity.Name
            };
            return View(model);
        }
    }
}
