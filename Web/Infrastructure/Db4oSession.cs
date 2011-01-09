using Db4objects.Db4o;
using System.Linq;
using Db4objects.Db4o.Linq;
using System.Web;
using System.IO;
using System;
using System.Collections.Generic;

namespace Web.Infrastructure
{

    public class Db4oSession : IDisposable, ISession
    {
        private IObjectContainer db;
        //private IObjectServer _server;
        public IObjectContainer Container
        {
            get
            {
                return db;
            }
        }


        public Db4oSession(IObjectServer server)
        {

            db = server.OpenClient();
        }

        

        /// <summary>
        /// Returns all T records in the repository
        /// </summary>
        public IQueryable<T> All<T>()
        {
            return (from T items in db
                    select items).AsQueryable();
        }

        /// <summary>
        /// Finds an item using a passed-in expression lambda
        /// </summary>
        public T Single<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return All<T>().SingleOrDefault(expression);
        }

        /// <summary>
        /// Saves an item to the database
        /// </summary>
        /// <param name="item"></param>
        public void Save<T>(T item)
        {
            db.Store(item);
        }

        /// <summary>
        /// Deletes an item from the database
        /// </summary>
        /// <param name="item"></param>
        public void Delete<T>(T item)
        {
            db.Delete(item);
        }

        /// <summary>
        /// Deletes subset of objects
        /// </summary>
        public void Delete<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            var items = All<T>().Where(expression).ToList();
            items.ForEach(x => db.Delete(x));
        }

        /// <summary>
        /// Deletes all T objects
        /// </summary>
        public void DeleteAll<T>()
        {
            var items = All<T>().ToList();
            items.ForEach(x => db.Delete(x));
        }


        /// <summary>
        /// Commits changes to disk
        /// </summary>
        public void CommitChanges()
        {
            //commit the changes
            db.Commit();

        }
        public void Dispose()
        {
            //explicitly close
            db.Close();
            //dispose 'em
            db.Dispose();
        }
    }
}