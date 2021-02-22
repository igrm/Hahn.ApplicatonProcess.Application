using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Domain.Interfaces
{
    public interface IDataRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T? GetById(int id);
        T? Get(Expression<Func<T, bool>> where);
        T? GetNoTracking(Expression<Func<T, bool>> where);
        Task<T?> GetAsync(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        IEnumerable<T> GetManyNoTracking(Expression<Func<T, bool>> where);
    }
}
