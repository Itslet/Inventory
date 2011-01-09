using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Infrastructure
{
        public interface ISession:IDisposable {
        void CommitChanges();
        Db4objects.Db4o.IObjectContainer Container { get; }
        void Delete<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression);
        void Delete<T>(T item);
        void DeleteAll<T>();
        void Dispose();
        T Single<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression);
        System.Linq.IQueryable<T> All<T>();
        void Save<T>(T item);
    }
}
  