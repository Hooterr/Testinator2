using System.Threading.Tasks;
using Testinator.Web.Database;

namespace Testinator.Web.Core
{
    public interface IAccountService
    {
        Task<bool> LoginUserAsync(string email, string password);
        Task<bool> RegisterUserAsync(ApplicationUser user, string password);
    }
}
