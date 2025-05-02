using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace ResourceBorrowing
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        /*
        protected void Session_Start(object src, EventArgs e)
        {
            if (Context.Session != null && Context.Session.IsNewSession)
            {
                string sCookieHeader = Request.Headers["Cookie"];
                if (null != sCookieHeader && sCookieHeader.IndexOf("ASP.NET_SessionId") >= 0)
                {

                    Response.Redirect("Account/Login");
                }

            }
        }
        */
    }
}