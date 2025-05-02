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
using System.Net.Mail;
using wv.GeneralFunction;

public partial class RequestResetPassword : System.Web.UI.Page
{
    private string sEKey;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (new GeneralFunctionController().checkIsUnderMaintenance())
            Response.Redirect("~/MaintenancePage.aspx");
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Account/Login.aspx");
    }

    protected void btnRequestResetPassword_Click(object sender, EventArgs e)
    {
        FailureText.Text = "";

        if (requestResetPwd("", ConfigurationManager.AppSettings["ResetPwdExpiryMintue"], "", txtEmailAddress.Text))
        {
            txtEmailAddress.Text = "";
            if (ConfigurationManager.AppSettings["DebugMode"] != "1")
            {
                Response.Redirect("~/Account/Logout.aspx");
            }
        }

    }

    private bool requestResetPwd(string sAppUserSys_ID, string iExpiryMintue, string sRequestBy, string sEmailAdr)
    {
            
        bool bResetSuccess;
        string script = "";
        string sLink = "";
        string sMethod="Email";


        if (ConfigurationManager.AppSettings["DebugMode"] == "1")
        {
            FailureText.Text = FailureText.Text + " spAppUserRequestResetPwd @sAppUserSys_ID='" + sAppUserSys_ID + "'";
            FailureText.Text = FailureText.Text + ",@iExpiryMintue=" + iExpiryMintue;
            FailureText.Text = FailureText.Text + ",@sRequestBy='" + sRequestBy + "'";
            FailureText.Text = FailureText.Text + ",@sEmailAddress='" + sEmailAdr + "'";
            FailureText.Text = FailureText.Text + ",@sMethod='" + sMethod + "'";

        }
        DataSet ds = new UserController().requestResetPwd(sAppUserSys_ID, iExpiryMintue, sRequestBy,sMethod,sEmailAdr);
        DataRow dr;

        bResetSuccess = false;

        try
        {
            dr = ds.Tables[0].Rows[0];

            if (Convert.ToString(dr["ErrorMsg"]) == "")
                bResetSuccess = true;
            else
                bResetSuccess = false;

            if (bResetSuccess)
            {
                script = "alert(\"Sent to " + Convert.ToString(dr["SendToEmailAddress"]) + "!\");";


                if (ConfigurationManager.AppSettings["DebugMode"] == "1")
                {
                    FailureText.Text = FailureText.Text + "SMTP: " + ConfigurationManager.AppSettings["SmtpServer"] + "    ";
                    FailureText.Text = FailureText.Text + "SysAdminEmailAdr: " + ConfigurationManager.AppSettings["SysAdminEmailAdr"] + "    ";
                    FailureText.Text = FailureText.Text + "SendToEmailAddress: " + Convert.ToString(dr["SendToEmailAddress"]) + "    ";
                }

                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]);

                    mail.From = new MailAddress(ConfigurationManager.AppSettings["SysAdminEmailAdr"]);
                    mail.To.Add(Convert.ToString(dr["SendToEmailAddress"]));
                    mail.Subject = "Please use the link to reset the password!";

                    sLink = HttpContext.Current.Request.Url.PathAndQuery;
                    sLink = HttpContext.Current.Request.Url.AbsoluteUri.Replace(sLink, "/") + "ePO/Account/ResetPassword.aspx" + "?acd=" + Convert.ToString(dr["AccessCode"]);

                    mail.Body = sLink;

                    SmtpServer.Port = 25;
                    //SmtpServer.Credentials = new System.Net.NetworkCredential("username", "password");
                    //SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);

           
            }
            else
            {
                script = "alert(\"Fail!\");";
                bResetSuccess = false;
            }
        }
        catch (Exception ex)
        {
           script = "alert(\"" + ex.ToString() + "\");";
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                //RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.ToString() + "');", true);
        }
        ScriptManager.RegisterStartupScript(this, GetType(),
                              "ServerControlScript", script, true);

        return bResetSuccess;

    }


}
