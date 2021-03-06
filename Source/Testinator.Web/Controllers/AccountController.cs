﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Testinator.Web.Database;

namespace Testinator.Web
{
    /// <summary>
    /// The controller for every account-related action
    /// Such as logging/registering/veryfing etc.
    /// </summary>
    [Authorize]
    [Route(WebRoutes.AccountPrefixRoute)]
    public class AccountController : Controller
    {
        #region Private Members

        private readonly SignInManager<ApplicationUser> mSignInManager;
        private readonly UserManager<ApplicationUser> mUserManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            // Inject services from DI
            mSignInManager = signInManager;
            mUserManager = userManager;
        }

        #endregion

        #region User Login

        [AllowAnonymous]
        [Route(WebRoutes.LoginRoute)]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route(WebRoutes.LoginRoute)]
        public async Task<IActionResult> LoginAsync(LoginViewModel loginViewModel)
        {
            // If view model isn't valid one to attempt logging in
            if (!ModelState.IsValid)
            {
                // Return the model back to show the errors
                return RedirectToAction(nameof(Login), loginViewModel);
            }

            // Find the user by email
            var user = await mUserManager.FindByEmailAsync(loginViewModel.Email);

            // If not found...
            if (user == null)
            {
                // Return error
                ModelState.AddModelError("", "Taki użytkownik nie istnieje");
                return RedirectToAction(nameof(Login), loginViewModel);
            }

            // Log the user in
            var result = await mSignInManager.PasswordSignInAsync(user, loginViewModel.Password, true, false);

            // If successfully logged in...
            if (result.Succeeded)
            {
                // Redirect to dashboard
                return RedirectToAction("Index", "Dashboard");
            }
            // If user was locked out from logging in...
            if (result.IsLockedOut)
            {
                // Return the error
                return RedirectToAction(nameof(Login), "Lockout");
            }
            // If user can't be logged in with just password...
            if (result.RequiresTwoFactor)
            {
                // Return the error
                return RedirectToAction(nameof(Login), "Two factor required");
            }
            // If something went wrong in general
            ModelState.AddModelError("", "Nie udało się zalogować");
            return RedirectToAction(nameof(Login), loginViewModel);
        }

        #endregion

        #region User Register

        [AllowAnonymous]
        [Route(WebRoutes.RegisterRoute)]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(WebRoutes.RegisterRoute)]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            // If view model isn't valid one to register new user...
            if (!ModelState.IsValid)
            {
                // Return the model back to show the errors
                return RedirectToAction(nameof(Register), registerViewModel);
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
            var result = await mUserManager.CreateAsync(newUser, registerViewModel.Password);

            // If registration succeeded...
            if (result.Succeeded)
            {
                // TODO: For now, redirect to login, but maybe automatically log in here or sth
                return RedirectToAction(nameof(Login), new LoginViewModel { Email = registerViewModel.Email });
            }

            // Registration failed, return the errors
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return RedirectToAction(nameof(Register), registerViewModel);
        }

        #endregion

        #region User Management

        #endregion
    }
}
