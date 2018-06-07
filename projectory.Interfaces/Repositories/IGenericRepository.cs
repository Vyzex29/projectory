using System.Data.Entity;
using System.Linq;

namespace projectory.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IDbSet<T> GetDbSet();
        IQueryable<T> All();
        void Add(T entity);
        T Find(object id);
        void Update(T entity);
        T Delete(T entity);
        T Delete(object id);
        int SaveChanges();
    }
}
