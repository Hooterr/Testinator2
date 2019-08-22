using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Testinator.Web.Database;

namespace Testinator.Web.Core
{
    public class AccountService : IAccountService
    {
        #region Private Members

        private readonly SignInManager<ApplicationUser> mSignInManager;

        private readonly UserManager<ApplicationUser> mUserManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public AccountService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            mSignInManager = signInManager;
            mUserManager = userManager;
        }

        #endregion

        #region Interface Implementation

        public async Task<bool> LoginUserAsync(string email, string password)
        {
            var result = await mSignInManager.PasswordSignInAsync(email, password, true, false);

            return result.Succeeded;
        }

        public async Task<bool> RegisterUserAsync(ApplicationUser user, string password)
        {
            var result = await mUserManager.CreateAsync(user, password);

            return result.Succeeded;
        }

        #endregion
    }
}
