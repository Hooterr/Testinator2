namespace Testinator.Core
{
    /// <summary>
    /// The API model for login call containing all the needed credentials
    /// </summary>
    public class LoginCredentialsApiModel
    {
        /// <summary>
        /// The user's email used as his username to log in
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The user's password
        /// </summary>
        public string Password { get; set; }
    }
}
