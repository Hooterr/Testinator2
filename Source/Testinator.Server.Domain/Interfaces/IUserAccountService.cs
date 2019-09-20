using System.Threading.Tasks;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The interface for a service that handles all the user account data interactions
    /// </summary>
    public interface IUserAccountService
    {
        Task<string> LogInAsync(string email, string password);
        UserContext GetCurrentUserData();
        void InitializeApplicationPageBasedOnUser();
    }
}
