using System;
using System.Web;
using System.Web.UI;

using wv.GeneralFunction;


namespace ResourceBorrowing
{

    public partial class MaintenancePage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            /*
            if (new GeneralFunctionController().checkIsUnderMaintenance())
                Response.Redirect("~/MaintenancePage.aspx");
            */

            lblMessage1.Text = new GeneralFunctionController().getUnderMaintenanceMsg1();
            lblMessage2.Text = new GeneralFunctionController().getUnderMaintenanceMsg2();
        }
    }

}