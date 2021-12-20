﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace EFCore.DataAccess.Abstract
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
        void SaveChanges();
    }
}
