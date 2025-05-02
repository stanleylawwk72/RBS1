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
using System.Web.Security;
using System.Configuration;

using wv.Users;
using wv.Encrypt;
using wv.GeneralFunction;

//using wv.User;
public partial class Login : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (new GeneralFunctionController().checkIsUnderMaintenance())
            Response.Redirect("~/MaintenancePage.aspx");

        if (Session["ReferenceNoFromOutSide"] != null)
        {

            txtReferenceNoFromOutSide.Text = Session["ReferenceNoFromOutSide"].ToString();
            // FilterBtn_Click(null, null);
        }

        lblSvrName.Text = ConfigurationManager.AppSettings["SvrName"];
        lblDbName.Text = ConfigurationManager.AppSettings["DbName"]; 

        //if (ConfigurationManager.AppSettings["WinAuthOnly"]=="Yes")
        if (ConfigurationManager.AppSettings["LoginMode"]=="W") //Window Auth
        {
            lblWindowAuthID.Visible=true;
            txtWindowAuthID.Enabled = false;
            txtWindowAuthID.Visible=true;

            chkWinAuth.Enabled = false;
            chkWinAuth.Checked = true;
            chkWinAuth.Visible = false;

            lblLogin.Visible=false;
            txtLogin.Enabled=false;
            txtLogin.Visible=false;

            lblPwd.Visible=false;
            txtPwd.Enabled=false;
            txtPwd.Visible=false;
            hypRequestResetPassword.Visible = false;

            txtWindowAuthID.Text = HttpContext.Current.User.Identity.Name.ToString();
            btnLogin_Click(null,null);

        }
        else if (ConfigurationManager.AppSettings["LoginMode"] == "P") //Window Auth
        {
            lblWindowAuthID.Visible=false;
            txtWindowAuthID.Enabled = false;
            txtWindowAuthID.Visible=false;

            chkWinAuth.Enabled = false;
            chkWinAuth.Checked = false;
            chkWinAuth.Visible = false;

            lblLogin.Visible=true;
            txtLogin.Enabled=true;
            txtLogin.Visible=true;

            lblPwd.Visible=true;
            txtPwd.Enabled=true;
            txtPwd.Visible=true;
            hypRequestResetPassword.Visible = true;
        }
        else //Mix
        {
            lblWindowAuthID.Visible=true;
            txtWindowAuthID.Enabled = false;
            txtWindowAuthID.Visible=true;

            chkWinAuth.Enabled = true;
            //chkWinAuth.Checked = true;
            chkWinAuth.Visible = true;

            lblLogin.Visible=true;
            //txtLogin.Enabled = false;
            txtLogin.Visible=true;

            lblPwd.Visible=true;
            //txtPwd.Enabled = false;
            txtPwd.Visible=true;
            hypRequestResetPassword.Visible = true;
        }
  
        txtWindowAuthID.Text = HttpContext.Current.User.Identity.Name.ToString(); //User.Identity.Name ;

    }

    protected void chkWinAuth_Click(object sender, EventArgs e)
    {
        if (chkWinAuth.Checked)
        {
            txtLogin.Enabled = false;
            txtPwd.Enabled = false;
           // Response.Write("Checked");
        }
        else
        {
            txtLogin.Enabled = true;
            txtPwd.Enabled = true;
            //Response.Write("UnChecked");
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //Check login
        string sClientMachineName,sClientIPAddress;

        sClientMachineName="";
        sClientIPAddress="";

        if (chkWinAuth.Checked)
        {
            if (!User.Identity.IsAuthenticated)
            {
                ErrorMessage.Text = "Windows Authentication is incorrect";
                return;
            }
        }
        else
        {
            if (txtLogin.Text == "")
            {
                ErrorMessage.Text = "Please enter the login name";
                return;

            }
            else if (txtPwd.Text == "")
            {
                ErrorMessage.Text = "Please enter the password";
                return;
            }
        }

        try 
        {
            sClientMachineName = System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName;
            sClientIPAddress = Request.UserHostAddress;
        }
        catch { }

        if (checkLogin(chkWinAuth.Checked, txtWindowAuthID.Text, txtLogin.Text, txtPwd.Text, sClientMachineName, sClientIPAddress, ConfigurationManager.AppSettings["LockAccCounter"], ConfigurationManager.AppSettings["eKey"]))
        {
        
            if (Session["NeedChgPwdImmediately"] != null)
            {
                if (Session["NeedChgPwdImmediately"] == "Y")
                    Response.Redirect("~/Account/ChangePassword.aspx");
            }

            Response.Redirect(ConfigurationManager.AppSettings["PageAfterLogin"]);
            

            //ErrorMessage.Text = Session["IsAuthenticated"].ToString();
        }
        else
        {
        }
    }

    private bool checkLogin(bool bWindowsAuth, string sWindowAuthID, string sUserID, string sPwd, string sClientMachineName, string sClientIPAddress, string iLockAccCounter,string sKey)
    {

        bool loginsuccess;
        bool bHasrecord;
        DataSet ds = new UserController().checkLogin(bWindowsAuth, sWindowAuthID, sUserID, sPwd, sClientMachineName, sClientIPAddress, iLockAccCounter, sKey);
        DataRow dr;
        int iNeedToChgPwdDay;
        string sNeedToChgPwdMessage;

        loginsuccess = false;
        bHasrecord = false;
        iNeedToChgPwdDay=0;

        ErrorMessage.Text = "";

        //eFormController().ch

        new UserController().resetSession();

        if (ds != null)
            if (ds.Tables[0].Rows.Count > 0)
                bHasrecord = true;
        else
            ErrorMessage.Text = ErrorMessage.Text+"Login Fail!";
        /*
        try
        {
            dr = ds.Tables[0].Rows[0];
        }
        catch (Exception ex)
        {
            ErrorMessage.Text = "Login Fail!";
        }
        */
        /*
        try
        {
           
        }
        catch (Exception ex)
        {
            ErrorMessage.Text = "Login Fail!";
        }
        */
        //ErrorMessage.Text = Convert.ToString(ds.Tables[0].Rows.Count);

        if (bHasrecord)
        {
            dr = ds.Tables[0].Rows[0];
            if (Convert.ToString(dr["LoginSuccess"]) == "True")
                loginsuccess = true;
            else
                loginsuccess = false;
        }

        if (loginsuccess)
        {

            dr = ds.Tables[0].Rows[0];
            Session["UID"] = Convert.ToString(dr["sys_ID"]);
            Session["UserName"] = Convert.ToString(dr["UserName"]);
            Session["IsAuthenticated"] = "Yes";
            Session["LoginDateTime"] = Convert.ToString(dr["LoginDate"]);
            Session["EmailAddress"] = Convert.ToString(dr["EmailAddress"]);

            Session["Dept"] = Convert.ToString(dr["Dept"]);
            Session["DeptCode"] = Convert.ToString(dr["DeptCode"]);
            Session["Role"] = Convert.ToString(dr["Role"]);
            Session["RoleCode"] = Convert.ToString(dr["RoleCode"]);

            Session["NeedChgPwdImmediately"] = "N";
            Session["ChangePwdWhenLogin"] = Convert.ToString(dr["ChangePwdWhenLogin"]);
            Session["LatestChangePwdDate"] = Convert.ToString(dr["LatestChangePwdDate"]);

            //SessionParameter sWindowAuthID = new SessionParameter();
            //sWindowAuthID.Name = "sWindowAuthID ";
            //sWindowAuthID.Type = TypeCode.String;
            //sWindowAuthID.SessionField= "sWindowAuthID ";
            Session["WindowAuthID"] = Convert.ToString(sWindowAuthID);   //J20180605

            if (ConfigurationManager.AppSettings["LoginMode"] != "W" && !chkWinAuth.Checked) //not window auth and login use password
            {
                if (Session["ChangePwdWhenLogin"].ToString() == "True")
                {
                    Session["NeedChgPwdImmediately"] = "Y";
                    //ErrorMessage.Text = Session["NeedChgPwdImmediately"].ToString();
                }
                
               // ErrorMessage.Text = Session["NeedChgPwdImmediately"].ToString();
               // loginsuccess = false;

                iNeedToChgPwdDay = Convert.ToInt32(ConfigurationManager.AppSettings["ChangePwdDay"]);

                DateTime LatestChangePwdDate = Convert.ToDateTime(Session["LatestChangePwdDate"]);
                DateTime dToday = (DateTime.Now);

                TimeSpan difference = dToday - LatestChangePwdDate;
                if (difference.TotalDays > iNeedToChgPwdDay)
                {
                    Session["NeedChgPwdImmediately"] = "Y";
                }

            }
            //get permissions
            DataSet dsp = new UserController().getPermissions(Convert.ToString(dr["sys_ID"]));
            if (dsp != null)
            {
                if (dsp.Tables[0].Rows.Count > 0)
                {
                    //set permission
                    foreach (DataTable table in dsp.Tables)
                    {
                        foreach (DataRow drp in table.Rows)
                        {
                            if (dsp.Tables[0].Rows[0]["RoleDetailCode"].ToString() == "SysAdmin" && dsp.Tables[0].Rows[0]["Permission"].ToString() == "True")
                                Session["IsAdmin"] = "Yes";

                            if (dsp.Tables[0].Rows[0]["RoleDetailCode"].ToString() == "DeptAdmin" && dsp.Tables[0].Rows[0]["Permission"].ToString() == "True")
                                Session["DeptAdmin"] = "Yes";

                        }
                    }
                }
            }

            //Session["IsAdmin"] = Convert.ToString(dr["IsAdmin"]);
            ///User u = new User();
           // u.setname("dvv");
            
            if (chkWinAuth.Checked)
            {
                Session["LoginByWinAuthID"] = "True";
                Session["NeedChgPwd"]=null;
            }
            else
            {
                Session["LoginByWinAuthID"] = "False";
            }
        }
        else
        {
            new UserController().resetSession();

            if (bHasrecord)
            {
                dr = ds.Tables[0].Rows[0];
                ErrorMessage.Text = Convert.ToString(dr["ErrorMsg"]);
            }
            else
            {
                ErrorMessage.Text = ErrorMessage.Text + "Login Fail!";
            }
        }

        return loginsuccess;


    }

  
}


