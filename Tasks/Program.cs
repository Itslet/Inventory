﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using Web.Infrastructure;

namespace Tasks
{
    class Program
    {
        public static void initialFill()
        {
            using (var s = SessionFactory.Current)
            {

                s.DeleteAll<BladeServer>();
                s.DeleteAll<BladeChassis>();

                var bc = new BladeChassis { ChassisID = "1", Description="HP BladeSystem 7000c", UnitsHigh = 8, Bays = 8, Power="2400W"};
                var bc1 = new BladeChassis { ChassisID = "2", Description = "HP BladeSystem 300c", UnitsHigh = 4, Bays = 6, Power = "2400W" };

                var bs = new BladeServer { Hostname = "SRV01", IPAddress = "10.10.10.12", OS = "Server 2008 R2", ServerRole = "Domain Controller", BladeChassis = bc };

                var bs1 = new BladeServer { Hostname = "SRV02", IPAddress = "10.10.10.11", OS = "Server 2008 R2", ServerRole = "Exchange 2010", BladeChassis = bc };

                var bs2 = new BladeServer { Hostname = "SRV03", IPAddress = "10.10.10.13", OS = "Server 2008 R2", ServerRole = "SQL 2008", BladeChassis = bc1 };

                List<BladeServer> servers = new List<BladeServer>();
                servers.Add(bs);
                servers.Add(bs1);
                servers.Add(bs2);

                bc.Servers = servers;
                
                s.Save(bc);
                s.Save(bc1);
                
                s.CommitChanges();

                var server = s.Single<BladeServer>(x => x.Hostname == "SRV02");
                Console.WriteLine(server.IPAddress);

                var aantal = s.All<BladeServer>().Count();
                Console.WriteLine(aantal);
            }
        }
        
        public static void Fout() {
            using (var s = SessionFactory.Current)
            {
                var server = s.All<BladeServer>();
                var todelete = from srv in server where srv.BladeChassis == null select srv;
                foreach (BladeServer bs in todelete)
                {
                    s.Delete(bs);
                }

            }
        }


        static void Main(string[] args)
        {
            initialFill();
            //Fout();
            Console.WriteLine("Ready");
            Console.ReadKey();
        }

        }
    }

