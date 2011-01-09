using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Infrastructure
{
    public class SessionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
                Db4oSession session = SessionFactory.Current;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (SessionFactory.Current == null)
            {
                SessionFactory.Current.Dispose();
            }
        }
    }
}