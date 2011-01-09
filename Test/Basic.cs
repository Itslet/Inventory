using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Infrastructure;
using Core;
       
namespace Test
{
       
    [TestFixture]
    public class PersistenceTest

    {
    
        [Test]
        public void DB_Should_Save_Multiple_Nested_Objects()
        {

            using (var s = SessionFactory.Current)
            {

                s.DeleteAll<BladeServer>();
                s.DeleteAll<BladeChassis>();

                var bc = new BladeChassis { ChassisID = "1", UnitsHigh = 8 };

                var bs = new BladeServer { Hostname = "SRV01", IPAddress = "10.10.10.12", OS = "Server 2008 R2", ServerRole = "Domain Controller", BladeChassis = bc };

                var bs1 = new BladeServer { Hostname = "SRV02", IPAddress = "10.10.10.11", OS = "Server 2008 R2", ServerRole = "Exchange 2010", BladeChassis = bc };

                List<BladeServer> servers = new List<BladeServer>();
                servers.Add(bs);
                servers.Add(bs1);

                bc.Servers = servers;

                s.Save(bc);
                s.CommitChanges();

                var server = s.Single<BladeServer>(x => x.Hostname == "SRV02");
                Assert.NotNull(server);

                Assert.AreEqual(2, s.All<BladeServer>().Count());
                Assert.AreEqual(1, s.All<BladeChassis>().Count());
            }
        }
        }
    }

