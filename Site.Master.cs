using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace ResourceBorrowing
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //txtFilter_WindowsAuthUserID.Text = Session["WindowAuthID"].ToString();
            SystemName.Text = ConfigurationManager.AppSettings["SystemName"].ToString();

            if (ConfigurationManager.AppSettings["DebugMode"].ToString() == "1")
                SystemName.Text = SystemName.Text + " (DebugMode)";

            if (ConfigurationManager.AppSettings["UAT"].ToString() == "1")
                SystemName.Text = SystemName.Text + " (UAT)";

            MainContentAdmin.Visible = false;

            if (ConfigurationManager.AppSettings["LoginMode"] == "W")
                hypChgPwd.Visible = false;
            else
                hypChgPwd.Visible = true;

            if (Session["IsAuthenticated"] != null) //Correct login
            {
                LoginName.Text = Session["Username"].ToString();
                LoginDate.Text = Session["LoginDateTime"].ToString();
                try
                {
                    lblDept.Text = "Department: " + Session["Dept"].ToString() + " ";
                }
                catch { }
                try
                {
                    lblRole.Text = "Role: " + Session["Role"].ToString() + " ";
                }
                catch { }
                try
                {
                    lblEmailAddress.Text = "Email Address: " + Session["EmailAddress"].ToString() + " ";
                }
                catch { }


                LoginView.ActiveViewIndex = 1;

                if (Session["IsAuthenticated"].ToString() == "Yes")
                //if (Context.User.Identity.IsAuthenticated)
                {
                    if (Session["NeedChgPwdImmediately"] != null)
                    {
                        if (Session["NeedChgPwdImmediately"].ToString() == "Y")
                        {
                            MainChangePassword.Visible = true;
                            isAuthenticated(false);
                        }
                        else
                           isAuthenticated(true);
                    }
                    else
                        isAuthenticated(true);

                    MainLogin.Visible = false;

                    if (Session["isAdmin"] != null) // is Admin
                    {
                        //SystemMessage.Text = Session["isAdmin"].ToString();

                        if (Session["isAdmin"].ToString() == "")
                            isAdmin(false);
                        else
                            isAdmin(true);
                    }
                    else
                        isAdmin(false);

                }
                else
                {
                    MainChangePassword.Visible = false;
                    MainLogin.Visible = true;

                    isAuthenticated(false);
                    isAdmin(false);
                }
            }
            else
            {
                LoginView.ActiveViewIndex = 0;
                MainLogin.Visible = true;
                MainChangePassword.Visible = false;

                isAuthenticated(false);
                isAdmin(false);

                //Response.Redirect("~/Account/Login.aspx");

            }

            //Home.Visible = false;
            //About.Visible = false;

        }

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }


        protected void isAdmin(Boolean b)
        {
            // b = true;
            MainContentAdmin.Visible = b;
            // NavigationMenuSystemAdmin.Enabled = b;
            AccountMaintenance.Visible = b;
            UserMaintenance.Visible = b;
            DeptMaintenance.Visible = b;
            RoleMaintenance.Visible = b;

        }

        protected void isAuthenticated(Boolean b)
        {
            // NavigationMenu.Enabled = b;
            /*
             * Maintenance.Visible = b;
             Client.Visible = b;
             Item.Visible = b;
             Booking.Visible = b;
             EPOForm.Visible = b;
             */
            Home.Visible = b;
            Client.Visible = b;
            ItemMaintenance.Visible = b;
            Item.Visible = b;
            ItemType.Visible = b;
            ItemLanguage.Visible = b;
            ItemObsoleteReason.Visible = b;
            ClientMaintenance.Visible = b;
            ClientType.Visible = b;
            Client.Visible = b;
            Transaction.Visible = b;
            Report.Visible = b;

            MainContent.Visible = b;
        }
    }
}