using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Testinator.Core
{
    /// <summary>
    /// Base repository class to derive from by every repository in the application
    /// </summary>
    public abstract class BaseRepository<T, K, DBC> : IRepository<T, K> where T : class, IBaseEntity<K>, new() where DBC : DbContext
    {
        #region Protected Properties

        /// <summary>
        /// This application's database
        /// </summary>
        protected DBC Db { get; set; }
         
        /// <summary>
        /// Provides access to any table that's set by generic parameter
        /// </summary>
        protected abstract DbSet<T> DbSet { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="db">This app's database that can be injected here</param>
        public BaseRepository(DBC db)
        {
            Db = db;
        }

        #endregion

        #region Interface Implementation

        /// <summary>
        /// Adds specified entity to the database table
        /// </summary>
        /// <param name="entity">The entity to add</param>
        public virtual void Add(T entity) => DbSet.Add(entity);

        /// <summary>
        /// Deletes specified entity from the database table
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        public virtual void Delete(T entity) => DbSet.Remove(entity);

        /// <summary>
        /// Deletes an entity by id
        /// </summary>
        /// <param name="id">The id of an entity to delete</param>
        public void Delete(K id)
        {
            var entity = new T()
            {
                Id = id,
            };
            
            Db.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// Gets entity by specified id
        /// </summary>
        /// <param name="id">The id of entity</param>
        /// <returns>Entity if found</returns>
        public T GetById(K id) => DbSet.Find(id);

        /// <summary>
        /// Gets every entity stored in the database table
        /// </summary>
        /// <returns>Entities</returns>
        public IQueryable<T> GetAll() => DbSet;

        /// <summary>
        /// Saves every single change made to the real database
        /// </summary>
        /// <returns>Success or failure</returns>
        public void SaveChanges()
        {
            try
            {
                var changes = Db.SaveChanges();
            }
            
            catch(DbUpdateException ex)
            {
                if (ex.ForeginKeyViolation())
                    throw new Exception("Foreign key violation!");

                throw ex;
            }
        }

        #endregion
    }
}
