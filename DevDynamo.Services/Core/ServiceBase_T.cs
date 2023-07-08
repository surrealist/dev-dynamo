using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevDynamo.Services.Core
{
    public abstract class ServiceBase<T> : IService<T> where T : class
    {
        protected readonly App app;
        public ServiceBase(App app) => this.app = app;

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> predicate) => app.db.Set<T>().Where(predicate);
        public IQueryable<T> All() => Where(_ => true);
        public virtual T? Find(params object[] keys) => app.db.Set<T>().Find(keys);
        public virtual ValueTask<T?> FindAsync(params object[] keys) => app.db.Set<T>().FindAsync(keys);

        public virtual T Add(T item) => app.db.Set<T>().Add(item).Entity;
        public virtual T Update(T item) => app.db.Set<T>().Update(item).Entity;
        public virtual T Remove(T item) => app.db.Set<T>().Remove(item).Entity;

        public virtual ValueTask<EntityEntry<T>> AddAsync(T item) => app.db.Set<T>().AddAsync(item);
    }
}
