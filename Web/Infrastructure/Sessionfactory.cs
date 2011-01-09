using Db4objects.Db4o;
using System.Linq;
using Db4objects.Db4o.Linq;
using System.Web;
using System.IO;
using System;
using System.Collections.Generic;

namespace Web.Infrastructure {

    public class SessionFactory
    {
        static ISession _current;
        //this needs to stay static - can't have more than 
        //one server on the file
        static IObjectServer _server;

        public static ISession CreateSession()
        {

            if (_server == null)
            {
                _server = Db4oFactory.OpenServer( @"c:\temp\inventoryDb", 0);
            }

            return new Db4oSession(_server);
        }

        public static Db4oSession Current
        {
            get
            {
                if (_current == null)
                    _current = CreateSession();
                return (Db4oSession) _current;
            }
        }
    }
}