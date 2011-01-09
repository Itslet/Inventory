using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Core
{
    public class BladeServer
    {
        public string Hostname { get; set;}
        public string ServerRole { get; set; }
        public string IPAddress { get; set; }
        public string OS { get; set; }
        public BladeChassis BladeChassis { get; set; }

    }

    //the bladeserver goes into the bladechassis
    public class BladeChassis
    {
        public string ChassisID { get; set; }
        public string Description { get; set; }
        public int UnitsHigh { get; set; }
        public int Bays { get; set; }
        public string Power { get; set; }
        public List<BladeServer> Servers { get; set; }
       

    }
 }
