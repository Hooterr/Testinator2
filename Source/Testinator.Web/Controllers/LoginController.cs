using Microsoft.AspNetCore.Mvc;
using Testinator.Web.Core;

namespace Testinator.Web
{
    public class LoginController : Controller
    {
        private readonly IAccountService mAccountService;

        public LoginController(IAccountService accountService)
        {
            mAccountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return Ok();
        }
    }
}
