using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Testinator.Web.Core;
using Testinator.Web.Database;

namespace Testinator.Web
{
    public class RegisterController : Controller
    {
        private readonly IAccountService mAccountService;

        public RegisterController(IAccountService accountService)
        {
            mAccountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            // If view model isn't valid one to register new user...
            if (!ModelState.IsValid)
            {
                // Return the model back to show the errors
                return View(registerViewModel);
            }

            // Create brand-new user with provided data
            var newUser = new ApplicationUser
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email,
                FirstName = registerViewModel.FirstName,
                SecondName = registerViewModel.SecondName
            };

            // Try to register the user
            await mAccountService.RegisterUserAsync(newUser, registerViewModel.Password);

            // TODO: If succeded etc...
            return Ok();
        }
    }
}
