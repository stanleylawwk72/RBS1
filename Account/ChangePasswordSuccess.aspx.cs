using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wv.GeneralFunction;

public partial class ChangePasswordSuccess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (new GeneralFunctionController().checkIsUnderMaintenance())
            Response.Redirect("~/MaintenancePage.aspx");
    }
}
