using System.Threading.Tasks;

namespace Testinator.Web.Core
{
    public interface IAccountService
    {
        Task<bool> LoginUserAsync(string username, string password);
    }
}
