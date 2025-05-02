using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using System.IO;
using System.Configuration;

using wv.Users;
using wv.GeneralFunction;

public partial class Logout : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (new GeneralFunctionController().checkIsUnderMaintenance())
            Response.Redirect("~/MaintenancePage.aspx");

        //FormsAuthentication.SignOut();
        new UserController().resetSession();
        //Response.Redirect("~/Default.aspx");
        Response.Redirect(ConfigurationManager.AppSettings["PageAfterLogout"]);
    }

  

  
}


