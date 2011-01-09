using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;


namespace Web.Model
{
    public class BladeServerEditViewData
    {
        public string Hostname { get; set; }
        public string IPAddress { get; set; }
        public string OS { get; set; }
        public string ServerRole { get; set; }
        public IEnumerable<SelectListItem> BladeEnclosures { get; set; }
        public string SelectedBladeEnclosure { get; set; }

    }
}