using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EJ.Entities;

namespace EJ.Models.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        T Find(int id);

        Task<T> FindAsync(int id);

        T FindFirst(Expression<Func<T, bool>> predicate);

        Task<T> FindFirstAsync(Expression<Func<T, bool>> predicate);

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll();

        IQueryable<T> GetAllReadOnly();

        void AddRange(IEnumerable<T> data, bool save = true);

        T Add(T model, bool save = true);

        Task<T> AddAsync(T model, bool save = true);

        T Update(T model, bool save = true, params Expression<Func<T, object>>[] properties);

        Task<T> UpdateAsync(T model, bool save = true, params Expression<Func<T, object>>[] properties);

        T Save(T model, bool save = true);

        Task<T> SaveAsync(T model, bool save = true);

        void Remove(T model, bool save = true);

        void RemoveRange(IEnumerable<T> models, bool save = true);

        bool Save();

        Task<bool> SaveAsync();
        void Attach(T model);
        void Detach(T model);

        void RunInTransaction(Action action);

        void RunSql(string sql);

        IEnumerable<T> RunFromSql(string sql, params object[] parameters);

        bool UpdateCollection<TInput, TKey>(ICollection<TInput> existingItems, IEnumerable<TInput> newItems, Func<TInput, TKey> comparer, Action<TInput> updateMapper = null) where TInput : class;

        IQueryable<IGrouping<TKey, T>> GroupBy<TKey>(Expression<Func<T, TKey>> keySelector);

        IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath);
    }
}