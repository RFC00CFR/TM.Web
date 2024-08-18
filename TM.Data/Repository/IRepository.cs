using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TM.Data.Repository;
using TM.Arquitecture.Models;

namespace TM.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        bool Add(T entity);
        T Update(T entity);
        bool Delete(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    }
}