using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EJ.Entities;
using EJ.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using SHARED.Common.Extensions;

namespace EJ.Domain.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : EntityBase
    {
        private readonly DbContext _context;

        public EfRepository(DbContext context)
        {
            _context = context;
        }

        public T Find(int id)
        {
            return _context.Find<T>(id);
        }

        public Task<T> FindAsync(int id)
        {
            return _context.FindAsync<T>(id);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public T FindFirst(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public Task<T> FindFirstAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> GetAllReadOnly()
        {
            return GetAll().AsNoTracking();
        }

        public void AddRange(IEnumerable<T> data, bool save = true)
        {
            _context.AddRange(data);
            if (save)
                Save();
        }

        public T Add(T model, bool save = true)
        {
            //_context.Entry(model).State = EntityState.Added; не работает в EF Core для Child Collections

            _context.Add(model);

            if (save)
                Save();
            return model;
        }

        public async Task<T> AddAsync(T model, bool save = true)
        {
            //_context.Entry(model).State = EntityState.Added; не работает в EF Core для Child Collections

            await _context.AddAsync(model);

            if (save)
                await SaveAsync().ConfigureAwait(false);

            return model;
        }

        public T Update(T model, bool save = true, params Expression<Func<T, object>>[] properties)
        {
            var updatedEntityEntry = _context.Entry(model);

            if (updatedEntityEntry.State == EntityState.Detached)
            {
                _context.Set<T>().Attach(model);
            }

            if (properties.Any())
            {
                foreach (var property in properties)
                    updatedEntityEntry.Property(SHARED.Common.Extensions.PropertyExtensions.GetMemberInfo(property).Member.Name).IsModified = true;
            }
            else
            {
                //_context.Entry(model).State = EntityState.Modified;
                _context.Update(model);
            }

            if (save)
                Save();

            return model;
        }

        public async Task<T> UpdateAsync(T model, bool save = true, params Expression<Func<T, object>>[] properties)
        {
            var updatedEntityEntry = _context.Entry(model);

            if (updatedEntityEntry.State == EntityState.Detached)
            {
                _context.Set<T>().Attach(model);
            }

            if (properties.Any())
            {
                foreach (var property in properties)
                    updatedEntityEntry.Property(SHARED.Common.Extensions.PropertyExtensions.GetMemberInfo(property).Member.Name).IsModified = true;
            }
            else
            {
                //_context.Entry(model).State = EntityState.Modified;
                _context.Update(model);
            }


            if (save)
                await SaveAsync().ConfigureAwait(false);

            return model;
        }

        public T Save(T model, bool save = true)
        {
            return model.Id == 0 ? Add(model, save) : Update(model, save);
        }

        public async Task<T> SaveAsync(T model, bool save = true)
        {
            return model.Id == 0 ? await AddAsync(model, save).ConfigureAwait(false) : await UpdateAsync(model, save).ConfigureAwait(false);
        }

        public void Remove(T model, bool save = true)
        {
            var deletedEntityEntry = _context.Entry(model);

            if (deletedEntityEntry.State == EntityState.Detached)
            {
                _context.Set<T>().Attach(model);
            }

            _context.Set<T>().Remove(model);

            if (save)
                Save();
        }

        public void RemoveRange(IEnumerable<T> models, bool save = true)
        {
            _context.Set<T>().RemoveRange(models);

            if (save)
                Save();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public void Detach(T model)
        {
            var updatedEntityEntry = _context.Entry(model);

            if (updatedEntityEntry.State != EntityState.Detached)
            {
                updatedEntityEntry.State = EntityState.Detached;
            }
        }

        public void RunInTransaction(Action action)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                action();
                transaction.Commit();
            }
        }

        public void RunSql(string sql)
        {
            _context.Database.ExecuteSqlCommand(new RawSqlString(sql));
        }

        IEnumerable<T> IRepository<T>.RunFromSql(string sql, params object[] parameters)
        {
            return _context.Set<T>().FromSql(sql, parameters);
        }


        public bool UpdateCollection<TInput, TKey>(ICollection<TInput> existingItems, IEnumerable<TInput> newItems, Func<TInput, TKey> comparer, Action<TInput> updateMapper = null)
        where TInput : class
        {
            var eItems = existingItems.ToList();
            var nItems = newItems.ToList();

            var addedItems = nItems.Except(eItems, comparer).ToList();

            var deletedItems = eItems.Except(nItems, comparer).ToList();

            var updatedItems = eItems.Except(deletedItems, comparer).ToList();

            addedItems.ForEach(existingItems.Add);

            deletedItems.ForEach(t =>
            {
                _context.Entry(t).State = EntityState.Deleted;
            });
            updatedItems.ForEach(t =>
            {
                _context.Entry(t).State = EntityState.Modified;
                updateMapper?.Invoke(t);
            });

            return addedItems.Any() || deletedItems.Any();
        }

        public void Attach(T model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.Entry(model).References.ForEach(t => t.Load());
        }

        public IQueryable<IGrouping<TKey, T>> GroupBy<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            return _context.Set<T>().GroupBy(keySelector);
        }

        public IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath)
        {
            return _context.Set<T>().Include(navigationPropertyPath);
        }
    }
}