using Microsoft.AspNetCore.Identity;

namespace Testinator.Web.Database
{
    /// <summary>
    /// An entity for every user in the application
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// The user's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The user's second name (surname)
        /// </summary>
        public string SecondName { get; set; }
    }
}
