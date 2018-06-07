using System.Data.Entity;
using System.Linq;
using projectory.DbContext;
using projectory.Interfaces.Repositories;

namespace projectory.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly System.Data.Entity.DbContext _context;

        public GenericRepository(IRepoDbContext context)
        {
            _context = (System.Data.Entity.DbContext)context;

        }
        public void Add(T entity)
        {
            ChangeState(entity, EntityState.Added);
            SaveChanges();
        }

        public IDbSet<T> GetDbSet()
        {
            return _context.Set<T>();
        }

        public IQueryable<T> All()
        {
            return _context.Set<T>();
        }

        public T Delete(T entity)
        {
            ChangeState(entity, EntityState.Deleted);
            SaveChanges();
            return entity;
        }

        public T Delete(object id)
        {
            T entity = Find(id);
            Delete(entity);
            return entity;
        }

        public T Find(object id)
        {
            return _context.Set<T>().Find(id);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Update(T entity)
        {
            ChangeState(entity, EntityState.Modified);
            SaveChanges();
        }

        private void ChangeState(T entity, EntityState state)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _context.Set<T>().Attach(entity);
            }

            entry.State = state;
        }
    }
}
