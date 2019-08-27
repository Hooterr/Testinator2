using Dna;
using System.Threading.Tasks;
using Testinator.Core;
using Testinator.Server.Database;

namespace Testinator.Server.Core
{
    /// <summary>
    /// The service that handles all the user account data related things
    /// </summary>
    public class UserAccountService : IUserAccountService
    {
        #region Private Members

        private readonly UserMapper mUserMapper;
        private readonly IUserRepository mUserRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserAccountService(UserMapper userMapper, IUserRepository userRepository)
        {
            // Inject DI services
            mUserMapper = userMapper;
            mUserRepository = userRepository;

            // Setup the application view model based on if we are logged in
            DI.Application.GoToPage(
                // If we are logged in...
                mUserRepository.ContainsUserData() ?
                // Go to home page
                ApplicationPage.Home :
                // Otherwise, go to login page
                ApplicationPage.Login);
        }

        #endregion

        #region Interface Implementation

        /// <summary>
        /// Tries to log the user in by making an API call to our website
        /// </summary>
        /// <param name="email">The user's email</param>
        /// <param name="password">The user's password</param>
        /// <returns>
        ///     In case login fails, the returned string is an error message to display
        ///     Otherwise, null string is returned
        /// </returns>
        public async Task<string> LogInAsync(string email, string password)
        {
            // Prepare user data to send
            var url = "https://localhost:5000/" + ApiRoutes.LoginRoute; // TODO: Fix host
            var credentials = new LoginCredentialsApiModel
            {
                Email = email,
                Password = password
            };

            // Make a POST request to the API and catch the response
            var result = await WebRequests.PostAsync<ApiResponse<LoginResultApiModel>>(url, credentials);

            // If response was null
            if (result?.ServerResponse == null)
            {
                return LocalizationResource.UnableToConnectWithWeb;
            }

            // We got the response, check if we successfully logged in
            if (result.Successful)
            {
                // User is logged in, store the data in database
                var user = mUserMapper.Map(result.ServerResponse.Response);
                mUserRepository.SaveNewUserData(user);

                // Return no error
                return null;
            }

            // We got the response, but logging in didnt succeed, return the error
            return result.ErrorMessage;
        }
    }
}
