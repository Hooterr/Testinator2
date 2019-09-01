namespace Testinator.Server.Core
{
    /// <summary>
    /// The context for user data object used in the code
    /// </summary>
    public class UserContext
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
