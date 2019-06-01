using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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

        public async Task<IActionResult> LoginAsync(LoginViewModel loginViewModel)
        {
            // If view model isn't valid one to attempt logging in
            if (!ModelState.IsValid)
            {
                // Return the model back to show the errors
                return View(loginViewModel);
            }

            await mAccountService.LoginUserAsync(loginViewModel.Username, loginViewModel.Password);

            return Ok();
        }
    }
}
