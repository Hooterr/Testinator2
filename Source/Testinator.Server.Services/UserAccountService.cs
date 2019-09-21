using Dna;
using System.Threading.Tasks;
using Testinator.Core;
using Testinator.Server.Domain;

namespace Testinator.Server.Services
{
    /// <summary>
    /// The service that handles all the user account data related things
    /// </summary>
    public class UserAccountService : IUserAccountService
    {
        #region Private Members

        private readonly UserMapper mUserMapper;
        private readonly IUserRepository mUserRepository;
        private readonly ApplicationViewModel mApplicationVM;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserAccountService(UserMapper userMapper, IUserRepository userRepository, ApplicationViewModel applicationVM)
        {
            // Inject DI services
            mUserMapper = userMapper;
            mUserRepository = userRepository;
            mApplicationVM = applicationVM;
        }

        #endregion

        #region Interface Implementation

        /// <summary>
        /// Initializes first page in the application by looking if we have user logged in
        /// If we have, goes to home page
        /// If we don't, goes to login page
        /// </summary>
        public void InitializeApplicationPageBasedOnUser()
        {
            // Setup the application view model based on if we are logged in
            mApplicationVM.GoToPage(
                // If we are logged in...
                mUserRepository.ContainsUserData() ?
                // Go to home page
                ApplicationPage.Home :
                // Otherwise, go to login page
                ApplicationPage.Login);

        }

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
            var url = "https://localhost:44306/" + ApiRoutes.LoginRoute; // TODO: Put host in configuration
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
            if (result.ServerResponse.Successful)
            {
                // User is logged in, store the data in database
                var user = mUserMapper.Map(result.ServerResponse.Response);
                mUserRepository.SaveNewUserData(user);

                // Return no error
                return null;
            }

            // We got the response, but logging in didnt succeed, return the error
            return result.ServerResponse.ErrorMessage;
        }

        /// <summary>
        /// Gets currently logged in user
        /// If no user was found, logs out of the account and goes back to login page
        /// </summary>
        /// <returns>Logged in user's data</returns>
        public UserContext GetCurrentUserData()
        {
            // Get currently saved user from the database
            var user = mUserRepository.GetCurrentUserData();

            // If it was not found...
            if (user == null)
            {
                // TODO: Instead of throwing exception, simply just logout the user and go back to login page
                throw new System.Exception("User was not found");
            }

            // Return it mapped as context
            return mUserMapper.Map(user);
        }

        #endregion
    }
}
