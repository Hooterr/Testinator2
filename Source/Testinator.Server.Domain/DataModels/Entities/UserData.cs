using Testinator.Core;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The entity for user account data stored in the database after successful login
    /// </summary>
    public class UserData : BaseEntity<int>
    {
        /// <summary>
        /// The user's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The user's second name (surname)
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// The user's email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The user's login JWT token
        /// Use this to authorize all the API calls
        /// </summary>
        public string Token { get; set; }
    }
}
