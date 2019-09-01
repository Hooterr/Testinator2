using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Testinator.Core;
using Testinator.Web.Database;

namespace Testinator.Web
{
    /// <summary>
    /// The controller for account-related actions
    /// Accessed by API calls, typically from Server App
    /// </summary>
    [AuthorizeToken]
    public class AccountApiController : Controller
    {
        #region Private Members

        private readonly SignInManager<ApplicationUser> mSignInManager;
        private readonly UserManager<ApplicationUser> mUserManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public AccountApiController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            // Inject services from DI
            mSignInManager = signInManager;
            mUserManager = userManager;
        }

        #endregion

        #region User Login

        /// <summary>
        /// Tries to log in a user using token-based authentication
        /// </summary>
        /// <returns>The result of the login request</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route(ApiRoutes.LoginRoute)]
        public async Task<ApiResponse<LoginResultApiModel>> LoginAsync([FromBody]LoginCredentialsApiModel loginCredentials)
        {
            // Prepare the error response in case login fails
            var errorResponse = new ApiResponse<LoginResultApiModel>
            {
                ErrorMessage = "Invalid username of password"
            };

            // Check if provided data is eligible
            if (loginCredentials?.Email == null || string.IsNullOrWhiteSpace(loginCredentials.Email))
                // Return error message to user
                return errorResponse;

            // Find the user in database
            var user = await mUserManager.FindByEmailAsync(loginCredentials.Email);

            // If no user was found...
            if (user == null)
                // Return error message to user
                return errorResponse;

            // Check if the password is valid
            if (!await mUserManager.CheckPasswordAsync(user, loginCredentials.Password))
                // Return error message to user
                return errorResponse;

            // If we get here, we are valid and the user passed the correct login details

            // Return JWT token to the user so he can log in using it
            return new ApiResponse<LoginResultApiModel>
            {
                // Pass back the user details and the token
                Response = new LoginResultApiModel
                {
                    FirstName = user.FirstName,
                    SecondName = user.SecondName,
                    Email = user.Email,
                    Token = user.GenerateJwtToken()
                }
            };
        }

        #endregion

        #region User Management

        #endregion
    }
}
