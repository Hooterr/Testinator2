using System.Linq;

namespace Testinator.Core
{
    /// <summary>
    /// The base repository interface to implement by every repository in the application
    /// Contains some basic functionalities to work with the database
    /// </summary>
    public interface IRepository<T, K> where T : IBaseEntity<K>, new()
    {
        void Add(T entity);
        void Delete(T entity);
        void Delete(K id);
        T GetById(K id);
        IQueryable<T> GetAll();
        void SaveChanges();
    }
}
