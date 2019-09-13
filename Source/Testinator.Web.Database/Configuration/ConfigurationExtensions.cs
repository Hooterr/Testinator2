using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;

namespace Testinator.Web.Database
{
    /// <summary>
    /// The database configuration methods as extensions
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Seeds initial database data
        /// </summary>
        /// <param name="context">The database itself</param>
        /// <param name="userManager">The user manager from DI</param>
        public static void SeedDatabaseData(this TestinatorWebDbContext context, UserManager<ApplicationUser> userManager)
        {
            // If currently there are no user roles in the database...
            if (context.Roles.Count() == 0)
            {
                // That means database is not initialized yet
                // Create Admin role
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore, null, null, null, null);
                roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Admin));

                // Create admin user
                var admin = new ApplicationUser
                {
                    UserName = "admin@testinator.com",
                    Email = "admin@testinator.com",
                    FirstName = "Minorsonek",
                    SecondName = "Hooterr",
                    EmailConfirmed = true
                };
                userManager.CreateAsync(admin, "polynitymb183").Wait();

                // Give admin permissions
                userManager.AddToRoleAsync(admin, ApplicationRoles.Admin).Wait();

                // Save everything we've made to public database
                context.SaveChanges();
            }
        }
    }
}
