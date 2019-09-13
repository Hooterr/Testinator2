using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Testinator.Web.Database
{
    /// <summary>
    /// The main database context for web application
    /// </summary>
    public class TestinatorWebDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Constructor

        /// <summary>
        /// Default constructor, expecting database options passed in
        /// </summary>
        /// <param name="options">The database context options</param>
        public TestinatorWebDbContext(DbContextOptions<TestinatorWebDbContext> options) : base(options) { }

        #endregion
    }
}
