using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EFCore.DataAccess.Abstract;
using EFCore.DataAccess.Context;

namespace EFCore.DataAccess.Concrete
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected MyDbContext MyDb { get; set; }

        public Repository()
        {
            MyDb = new MyDbContext();
        }

        public IQueryable<T> GetAll()
        {
            return MyDb.Set<T>();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return MyDb.Set<T>().Where(expression);
        }

        public void Insert(T entity)
        {
            MyDb.Set<T>().Add(entity);
            SaveChanges();
        }

        public void Delete(T entity)
        {
            MyDb.Set<T>().Remove(entity);
            SaveChanges();
        }

        public void Update(T entity)
        {
            MyDb.Set<T>().Update(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            MyDb.SaveChanges();
        }
    }
}
