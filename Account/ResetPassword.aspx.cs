using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Configuration;

using wv.Users;
using wv.GeneralFunction;

public partial class ResetPassword : System.Web.UI.Page
{
    private string sEKey;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (new GeneralFunctionController().checkIsUnderMaintenance())
            Response.Redirect("~/MaintenancePage.aspx");

        this.sEKey = ConfigurationManager.AppSettings["eKey"];

        //txtACode.Text = Request.QueryString["acd"];
        if (Request.QueryString["acd"] == "" || Request.QueryString["acd"] ==null)
            Response.Redirect("~/Default.aspx");
    }
    protected void CancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Account/Login.aspx");
    }
    private bool resetPwd(string sAccessCode, string sNewPwd, string sClientMachineName, string sClientIPAddress,string sKey)
    {


    
        FailureText.Text = "";
        /*
        FailureText.Text = FailureText.Text + " spAppUserResetPwd ";
        FailureText.Text = FailureText.Text + " @sAccessCode='" + sAccessCode + "'";
        FailureText.Text = FailureText.Text + ",@sNewPwd='" + sNewPwd + "'";
        FailureText.Text = FailureText.Text + ",@sClientMachineName='" + sClientMachineName + "'";
        FailureText.Text = FailureText.Text + ",@sClientIPAddress='" + sClientIPAddress + "'";
        */
        bool bResetSuccess;
        string script = "";
        string sLink = "";

        DataSet ds = new UserController().resetPwd(sAccessCode, sNewPwd, sClientMachineName, sClientIPAddress, sKey);
        DataRow dr;

        bResetSuccess = false;

        try
        {
            dr = ds.Tables[0].Rows[0];

            if (Convert.ToString(dr["ErrorMsg"]) == "")
                bResetSuccess = true;
            else
            {
                bResetSuccess = false;
                FailureText.Text = FailureText.Text + Convert.ToString(dr["ErrorMsg"]);
            }

        }
        catch { bResetSuccess = false; }

        return bResetSuccess;

    }

    protected void btnChangePwd_Click(object sender, EventArgs e)
    {

        if (resetPwd(Request.QueryString["acd"], NewPassword.Text, System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName, Request.UserHostAddress, sEKey))
        {
            FailureText.Text = FailureText.Text + "Change Password Success!";
            //Response.Redirect("~/Account/ChangePasswordSuccess.aspx");
        }
        else
            FailureText.Text = FailureText.Text + "Change Password Unsuccess! Please try again.";
    }


}
