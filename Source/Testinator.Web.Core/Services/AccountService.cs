using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Testinator.Web.Database;

namespace Testinator.Web.Core
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<ApplicationUser> mSignInManager;

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public AccountService(SignInManager<ApplicationUser> signInManager)
        {
            mSignInManager = signInManager;
        }

        #endregion

        #region Interface Implementation

        public async Task<bool> LoginUserAsync(string username, string password)
        {
            var result = await mSignInManager.PasswordSignInAsync(username, password, true, false);

            return result.Succeeded;
        }

        #endregion
    }
}
