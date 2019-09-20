using Testinator.Core;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The interface for repository that handles accessing currently logged in user's data in the database
    /// </summary>
    public interface IUserRepository : IRepository<UserData, int>
    {
        bool ContainsUserData();
        UserData GetCurrentUserData();
        void SaveNewUserData(UserData userData);
        void ClearAllUserData();
    }
}
