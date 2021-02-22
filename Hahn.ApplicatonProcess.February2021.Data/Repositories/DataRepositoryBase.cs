using Hahn.ApplicatonProcess.February2021.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Data.Repositories
{
    public abstract class DataRepositoryBase<T> where T : class
    {
        #region Properties

        private DbSet<T> DbSet => DbContext.Set<T>();
        protected AssetContext DbContext { get; set; }

        #endregion

        protected DataRepositoryBase(AssetContext context)
        {
            DbContext = context;
        }

        #region Implementation
        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = DbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                DbSet.Remove(obj);
        }

        public T? GetById(int id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return DbSet.Where(where).ToList();
        }

        public IEnumerable<T> GetManyNoTracking(Expression<Func<T, bool>> where)
        {
            return DbSet.AsNoTracking().Where(where).ToList();
        }

        public T? Get(Expression<Func<T, bool>> where)
        {
            return DbSet.Where(where).FirstOrDefault<T>();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> where)
        {
            return await DbSet.Where(where).FirstOrDefaultAsync<T>();
        }

        public T? GetNoTracking(Expression<Func<T, bool>> where)
        {
            return DbSet.AsNoTracking().Where(where).FirstOrDefault<T>();
        }

        #endregion

    }
}
