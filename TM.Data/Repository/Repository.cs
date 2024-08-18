using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TM.Arquitecture.Models;

namespace TM.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApDatabaseContext _context;

        public Repository(ApDatabaseContext context)
        {
            _context = context;
        }

        public bool Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null) return false;

            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }
            return entity;
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
