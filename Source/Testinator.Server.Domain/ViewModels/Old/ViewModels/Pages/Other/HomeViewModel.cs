using Testinator.Core;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for home page
    /// </summary>
    public class HomeViewModel : BaseViewModel
    {
        #region Private Members

        private readonly IUserAccountService mUserService;

        #endregion

        #region Public Properties

        public string UserName { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public HomeViewModel(IUserAccountService userAccountService)
        {
            // Inject DI services
            mUserService = userAccountService;

            var user = mUserService.GetCurrentUserData();
            UserName = $"Witaj {user.FirstName} {user.SecondName} {user.Email}";
        }

        #endregion
    }
}
