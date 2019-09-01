using Microsoft.EntityFrameworkCore;
using System.Linq;
using Testinator.Core;

namespace Testinator.Server.Database
{
    /// <summary>
    /// The repository to access currently logged in user's data in the database
    /// </summary>
    public class UserRepository : BaseRepository<UserData, int, TestinatorServerDbContext>, IUserRepository
    {
        #region Protected Properties

        /// <summary>
        /// The table in database that holds currently logged in user's data
        /// </summary>
        protected override DbSet<UserData> DbSet => Db.Users;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbContext">The database context for this application</param>
        public UserRepository(TestinatorServerDbContext dbContext) : base(dbContext) { }

        #endregion

        #region Interface Implementation

        /// <summary>
        /// Checks if there is any user currently saved in database
        /// </summary>
        /// <returns>True if found, false if database is empty</returns>
        public bool ContainsUserData()
        {
            // Try to get currently saved user and return if it was found or not
            return GetCurrentUserData() != null;
        }

        /// <summary>
        /// Gets current user data that is saved in database
        /// </summary>
        /// <returns>The user data as a model, or null if user was not found</returns>
        public UserData GetCurrentUserData()
        {
            // Get first user available
            var user = DbSet.FirstOrDefault();

            // Return it
            return user;
        }

        /// <summary>
        /// Saves given user to the database
        /// </summary>
        /// <param name="userData">The user's data</param>
        public void SaveNewUserData(UserData userData)
        {
            // Remove any previous users
            DbSet.RemoveRange(DbSet);

            // Add new user
            DbSet.Add(userData);

            // Save changes in the database
            SaveChanges();
        }

        /// <summary>
        /// Clears the database from all previous users
        /// </summary>
        public void ClearAllUserData()
        {
            // Remove all the users
            DbSet.RemoveRange(DbSet);

            // Save changes in the database
            SaveChanges();
        }

        #endregion
    }
}
