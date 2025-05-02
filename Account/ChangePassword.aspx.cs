using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

using wv.Users;
using wv.GeneralFunction;


    public partial class ChangePassword : Page
    {
        private string sEKey;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (new GeneralFunctionController().checkIsUnderMaintenance())
                Response.Redirect("~/MaintenancePage.aspx");

            this.sEKey = ConfigurationManager.AppSettings["eKey"];

            UserController uc = new UserController();
            DataSet dsUser;
            DataRow drUser;

            if (Session["UID"] != null)
                dsUser = uc.getUsers(Session["UID"].ToString());
            else
                dsUser = null;
            try
            {

                drUser = dsUser.Tables[0].Rows[0];
                if (Session["loginByWinAuthID"] != null)
                {
                    //if (Session["loginByWinAuthID"].ToString() == "1")
                    // {
                    txtWindowAuthID.Text = Convert.ToString(drUser["WindowsAuthUserID"]);
                    // }
                    // else
                    // {
                    txtLogin.Text = Convert.ToString(drUser["UserID"]);
                    //}
                }

            }
            catch
            {
                //ErrorMessageM.Text = "Failed to load survey. Please contact IT for further assistance.";
            }


        }
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        protected void btnChangePwd_Click(object sender, EventArgs e)
        {
            bool bWindowsAuth = false;

            if (txtWindowAuthID.Text == "1")
                bWindowsAuth = true;

            if (changepwd(Session["UID"].ToString(), CurrentPassword.Text, NewPassword.Text, sEKey))
            {
                Session["NeedChgPwdImmediately"] = "N";
                Response.Redirect("~/Account/ChangePasswordSuccess.aspx");
            }
            else
                FailureText.Text = "Change Password Unsuccess! Please try again.";
        }

        private bool changepwd(string sUserID, string sCurrentPwd, string sNewPwd, string sKey)
        {
            bool changeSuccess;
            DataSet ds = new UserController().changePwd(sUserID, sCurrentPwd, sNewPwd, sKey);

            changeSuccess = false;

            //eFormController().ch
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                    changeSuccess = true;
            }
            catch (Exception ex)
            {

            }

            return changeSuccess;

        }

    }
