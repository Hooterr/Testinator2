namespace Testinator.Web
{
    /// <summary>
    /// The view model for user registration
    /// </summary>
    public class RegisterViewModel
    {
        #region Public Properties

        /// <summary>
        /// The user's email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The user's password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Repeated password from user, should match the original password
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// The user's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The user's second name (surname)
        /// </summary>
        public string SecondName { get; set; }

        #endregion
    }
}