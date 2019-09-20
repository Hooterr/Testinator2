using Microsoft.EntityFrameworkCore;
using System.IO;
using Testinator.Server.Domain;

namespace Testinator.Server.Database
{
    /// <summary>
    /// The database context for this application
    /// Contains every database table as db sets
    /// </summary>
    public class TestinatorServerDbContext : DbContext
    {
        #region Db Sets

        /// <summary>
        /// The table for saved application's settings
        /// </summary>
        public DbSet<Setting> Settings { get; set; }

        /// <summary>
        /// The table for current logged in user's data
        /// </summary>
        public DbSet<UserData> Users { get; set; }

        #endregion

        #region Database Configuration

        /// <summary>
        /// Configures SQLite database specifically for this application
        /// </summary>
        /// <param name="optionsBuilder">Default options builder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Do default things that base class requires
            base.OnConfiguring(optionsBuilder);

            // Configure the builder to save database locally on the device
            optionsBuilder.UseSqlite($"Filename={Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "testinatorDB.sqlite")}");
        }

        #endregion
    }
}
