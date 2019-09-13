namespace Testinator.Core
{
    /// <summary>
    /// The API model to return back from login's call
    /// </summary>
    public class LoginResultApiModel
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
        /// The JWT token generated for the user to log in using it
        /// </summary>
        public string Token { get; set; }
    }
}
