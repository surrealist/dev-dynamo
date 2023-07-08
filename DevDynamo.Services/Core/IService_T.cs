using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevDynamo.Services.Core
{
    public interface IService<T> where T : class
    {
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        IQueryable<T> All();
        T? Find(params object[] keys);
        ValueTask<T?> FindAsync(params object[] keys);

        T Add(T item);
        ValueTask<EntityEntry<T>> AddAsync(T item);
        T Update(T item);
        T Remove(T item);
    }
}
