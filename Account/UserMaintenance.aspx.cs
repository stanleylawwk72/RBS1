using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using System.IO;
using System.Configuration;

using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

using System.Xml;

using System.Net.Mail;
using System.DirectoryServices;
using wv.Encrypt;

using wv.Users;
using wv.GeneralFunction;


    public partial class UserMaintenance : System.Web.UI.Page
    {
        private string sEKey;
        EncColltroller _dn = new EncColltroller();
        DataSet dsDept, dsRole;
        UserController uc = new UserController();




        protected void Page_Load(object sender, EventArgs e)
        {
            if (new GeneralFunctionController().checkIsUnderMaintenance())
                Response.Redirect("~/MaintenancePage.aspx");
            // if (!IsPostBack)
            //{
            //mvUsers.ActiveViewIndex = 0;

            //GridView1.DataSource = new UserController().getUsers();
            //GridView1.DataBind();
            //}

            // int abc;
            //abc = +1;
            //Response.Write(abc.ToString());

            this.sEKey = ConfigurationManager.AppSettings["eKey"];

            if (ConfigurationManager.AppSettings["LoginMode"] == "W") //Window Auth
            {
                //Add
                lblWinWuthUserID_Add.Visible = true;
                txtWinAuthUserID_Add.Visible = true;
                //txtWinAuthUserID_Add.Text = "";

                lblUserID_Add.Visible = false;
                txtUserID_Add.Visible = false;

                lblPwd_Add.Visible = false;
                lblPwd_Confirm_Add.Visible = false;
                txtPwd_Add.Visible = false;
                txtPwd_Confirm_Add.Visible = false;

                //Edit
                lblWinWuthUserID_Edit.Visible = true;
                txtWinAuthUserID_Edit.Visible = true;

                lblUserID_Edit.Visible = false;
                txtUserID_Edit.Visible = false;

                chkboxChgPwd.Visible = false;
                lblPwd_Edit.Visible = false;
                txtPwd_Edit.Visible = false;

                //RegularExpression
                ConfirmPasswordRequired.Enabled = false;
                PasswordCompare.Enabled = false;
                RegularExpressionValidatorPwd.Enabled = false;
                PasswordRequired.Enabled = false;
                GridViewUsers.Columns[1].Visible = false;


            }
            else if (ConfigurationManager.AppSettings["LoginMode"] == "P") //Window Auth
            {
                //Add
                lblWinWuthUserID_Add.Visible = false;
                txtWinAuthUserID_Add.Visible = false;
                txtWinAuthUserID_Add.Text = "";

                lblUserID_Add.Visible = true;
                txtUserID_Add.Visible = true;

                lblPwd_Add.Visible = true;
                lblPwd_Confirm_Add.Visible = true;
                txtPwd_Add.Visible = true;
                txtPwd_Confirm_Add.Visible = true;

                //Edit
                lblWinWuthUserID_Edit.Visible = false;
                txtWinAuthUserID_Edit.Visible = false;

                lblUserID_Edit.Visible = true;
                txtUserID_Edit.Visible = true;

                chkboxChgPwd.Visible = true;
                lblPwd_Edit.Visible = true;
                txtPwd_Edit.Visible = true;

                //RegularExpression
                ConfirmPasswordRequired.Enabled = true;
                PasswordCompare.Enabled = true;
                RegularExpressionValidatorPwd.Enabled = true;
                PasswordRequired.Enabled = true;
                GridViewUsers.Columns[1].Visible = false;

            }
            else //Mix
            {
                // do nothing
            }

            /*
            if (ConfigurationManager.AppSettings["WinAuthOnly"] == "Yes")
            {
                lblUserID_Add.Visible = false;
                lblPwd_Add.Visible = false;
                lblPwd_Confirm_Add.Visible = false;
                txtUserID_Add.Visible = false;
                txtPwd_Add.Visible = false;
                txtPwd_Confirm_Add.Visible = false;

                lblUserID_Edit.Visible = false;
                txtUserID_Edit.Visible = false;
                lblPwd_Edit.Visible = false;
                chkboxChgPwd.Visible = false;
                txtPwd_Edit.Visible = false;
                ConfirmPasswordRequired.Enabled = false;
                PasswordCompare.Enabled = false;
                RegularExpressionValidatorPwd.Enabled = false;
                PasswordRequired.Enabled = false;
            }
            */

            try
            {


                if (ddlFilter_Dept.SelectedValue == "")
                {
                    dsDept = uc.getDepartments(false, false);

                    ddlFilter_Dept.DataSource = dsDept;
                    ddlFilter_Dept.DataTextField = "Description";
                    ddlFilter_Dept.DataValueField = "Code";
                    ddlFilter_Dept.DataBind();

                    ddlFilter_Dept.Items.Insert(0, new ListItem("All", String.Empty));

                }
            }
            catch { }

            try
            {

                if (ddlFilter_Role.SelectedValue == "")
                {
                    dsRole = uc.getRoles(false, false);

                    ddlFilter_Role.DataSource = dsRole;
                    ddlFilter_Role.DataTextField = "Description";
                    ddlFilter_Role.DataValueField = "Code";
                    ddlFilter_Role.DataBind();

                    ddlFilter_Role.Items.Insert(0, new ListItem("All", String.Empty));

                }
            }
            catch { }


            if (Session["IsAuthenticated"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void GridViewUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 1)
            {
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[16].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
            }
        }

        protected void FilterBtn_Click(object sender, EventArgs e)
        {
            try
            {
                odsUsers.Update();
            }
            catch { }

        }

        protected void FilterClearBtn_Click(object sender, EventArgs e)
        {
            try
            {
                txtFilter_ID.Text = "";
                txtFilter_UserName.Text = "";
                ddlFilter_Dept.SelectedIndex = 0;
                ddlFilter_Status.SelectedIndex = 0;
                odsUsers.Update();
            }
            catch { }

        }

        protected void PageUserIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewUsers.PageIndex = e.NewPageIndex;
            GridViewUsers.DataBind();

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            mvUsers.ActiveViewIndex = 2;

            UserController uc = new UserController();
            DataSet dsDept;
            DataSet rsDept;

            dsDept = uc.getDepartments(false, false);
            rsDept = uc.getRoles(false, false);

            ddlDept_Add.DataSource = dsDept;
            ddlDept_Add.DataTextField = "Description";
            ddlDept_Add.DataValueField = "Code";
            ddlDept_Add.DataBind();

            ddlDept_Add.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlDept_Add.SelectedIndex = 0;


            ddlRole_Add.DataSource = rsDept;
            ddlRole_Add.DataTextField = "Description";
            ddlRole_Add.DataValueField = "Code";
            ddlRole_Add.DataBind();

            ddlRole_Add.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlRole_Add.SelectedIndex = 0;
        }


        protected void AddUserBtn_Click(object sender, EventArgs e)
        {

            FailureAddText.Text = "";

            if (ConfigurationManager.AppSettings["LoginMode"] == "W") //Window Auth
            {

                if (txtWinAuthUserID_Add.Text == "")
                {
                    FailureAddText.Text = FailureUpdateText.Text + "Please enter 'Windows Auth User ID'! ";
                    txtWinAuthUserID_Add.Focus();
                    return;
                }
            }
            else if (ConfigurationManager.AppSettings["LoginMode"] == "P") //Window Auth
            {
                if (txtUserID_Add.Text == "")
                {
                    FailureAddText.Text = FailureAddText.Text + "Please enter 'User ID'! ";
                    txtUserID_Add.Focus();
                    return;
                }
            }
            else //Mix
            {
                if (txtWinAuthUserID_Add.Text == "" && txtUserID_Add.Text == "")
                {
                    FailureAddText.Text = FailureAddText.Text + "Please enter 'Windows Auth User ID' or 'User ID'! ";
                    txtWinAuthUserID_Add.Focus();
                    return;
                }
            }

            if (txtUserName_Add.Text == "")
            {
                FailureAddText.Text = "Please enter 'User Name'! ";
                txtUserName_Add.Focus();
                return;
            }

            if (txtUserName_Add.Text == "")
            {
                FailureAddText.Text = "Please enter 'Email Address'! ";
                txtUserName_Add.Focus();
                return;
            }


            if (txtPwd_Add.Text == "" && txtUserID_Add.Text != "")
            {
                FailureAddText.Text = "Please enter the password! ";
                FailureAddText.Focus();
                return;
            }

            string UID = "";
            bool bStatus;
            //bool bIsAdmin;

            // ddlStatus_Add.TabIndex;
            if (ddlStatus_Add.SelectedValue == "True")
                bStatus = true;
            else
                bStatus = false;
            /*
            if (ddlIsAdmin_Add.SelectedValue == "True")
                bIsAdmin = true;
            else
                bIsAdmin = false;
            */
            if (Session["UID"] != null)
                UID = Session["UID"].ToString();


            if (addUser(bStatus, txtEmailAddress_Add.Text, txtPwd_Add.Text, UID, txtWinAuthUserID_Add.Text, txtUserID_Add.Text, txtUserName_Add.Text, ddlDept_Add.SelectedItem.Value.ToString(), ddlRole_Add.SelectedItem.Value.ToString(), sEKey))
            {
                Response.Redirect("~/Account/UserMaintenance.aspx");
                //mvUsers.ActiveViewIndex = 0;
            }
            else
                FailureAddText.Text = FailureAddText.Text + "Add User Unsuccess! Please try again.";
        }


        protected void UpdateUserBtn_Click(object sender, EventArgs e)
        {
            string sObsDate;

            DateTime dObsoleteDate;

            FailureUpdateText.Text = "";

            //Response.Write ("asd") ;


            if (txtUserName_Edit.Text == "")
            {
                FailureUpdateText.Text = FailureUpdateText.Text + "Please enter 'User Name'! ";
                txtUserName_Edit.Focus();
                return;
            }

            if (ConfigurationManager.AppSettings["LoginMode"] == "W") //Window Auth
            {

                if (txtWinAuthUserID_Edit.Text == "")
                {
                    FailureUpdateText.Text = FailureUpdateText.Text + "Please enter 'Windows Auth User ID'! ";
                    txtWinAuthUserID_Edit.Focus();
                    return;
                }
            }
            else if (ConfigurationManager.AppSettings["LoginMode"] == "P") //Window Auth
            {
                if (txtUserID_Edit.Text == "")
                {
                    FailureUpdateText.Text = FailureUpdateText.Text + "Please enter 'User ID'! ";
                    txtUserID_Edit.Focus();
                    return;
                }
            }
            else //Mix
            {
                if (txtWinAuthUserID_Edit.Text == "" && txtUserID_Edit.Text == "")
                {
                    FailureUpdateText.Text = FailureUpdateText.Text + "Please enter 'Windows Auth User ID' or 'User ID'! ";
                    txtWinAuthUserID_Edit.Focus();
                    return;
                }
            }

            if (txtObsoleteDate_Edit.Text == "")
            {
                sObsDate = string.Empty;
                //FailureUpdateText.Text = "sObsDate"+sObsDate;
                // return;
            }
            else
            {
                if (DateTime.TryParse(txtObsoleteDate_Edit.Text, out dObsoleteDate))
                    sObsDate = txtObsoleteDate_Edit.Text;
                else
                {
                    FailureUpdateText.Text = FailureUpdateText.Text + "Please enter the valid Obsolete date! ";
                    txtObsoleteDate_Edit.Focus();
                    return;
                }
            }

            if (
                txtWinAuthUserID_Edit.Text == txtWinAuthUserID_Edit_Old.Text &&
                txtUserID_Edit.Text == txtUserID_Edit_Old.Text &&
                txtUserName_Edit.Text == txtUserName_Edit_Old.Text &&
                txtEmailAddress_Edit.Text == txtEmailAddress_Edit_Old.Text &&
                ddlStatus_Edit.SelectedValue == ddlStatus_Edit_Old.SelectedValue &&
                //ddlIsAdmin_Edit.SelectedValue == ddlIsAdmin_Edit_Old.SelectedValue &&
                ddlLocked_Edit.SelectedValue == ddlLocked_Edit_Old.SelectedValue &&
                chkboxChgPwd.Checked == chkboxChgPwd_Old.Checked &&
                ddlDept_Edit.SelectedIndex == ddlDept_Edit_Old.SelectedIndex &&
                ddlRole_Edit.SelectedIndex == ddlRole_Edit_Old.SelectedIndex &&
                txtObsoleteDate_Edit.Text == txtObsoleteDate_Edit_Old.Text
                )
            {
                FailureUpdateText.Text = FailureUpdateText.Text + "The information is the same, no need to update! ";
                return;
            }


            if (chkboxChgPwd.Checked && txtPwd_Edit.Text == "")
            {
                FailureUpdateText.Text = FailureUpdateText.Text + "Please enter the password! ";
                txtPwd_Edit.Focus();
                return;
            }

            string UID = "";
            bool bStatus;
            //bool bIsAdmin;
            bool bChangePwd;
            bool bLocked;

            // ddlStatus_Edit.TabIndex;
            if (ddlStatus_Edit.SelectedValue == "True")
                bStatus = true;
            else
                bStatus = false;
            /*
            if (ddlIsAdmin_Edit.SelectedValue == "True")
                bIsAdmin = true;
            else
                bIsAdmin = false;
            */
            if (ddlLocked_Edit.SelectedValue == "True")
                bLocked = true;
            else
                bLocked = false;

            if (chkboxChgPwd.Checked)
                bChangePwd = true;
            else
                bChangePwd = false;

            if (Session["UID"] != null)
                UID = Session["UID"].ToString();


            if (updateUser(txtSysID_Edit.Text, bStatus, txtEmailAddress_Edit.Text, bChangePwd, txtPwd_Edit.Text, UID, bLocked, txtWinAuthUserID_Edit.Text, txtUserID_Edit.Text, txtUserName_Edit.Text, ddlDept_Edit.SelectedItem.Value.ToString(), ddlRole_Edit.SelectedItem.Value.ToString(), sObsDate, sEKey))
            {
                //Response.Redirect("~/Account/UserMaintenance.aspx");
                mvUsers.ActiveViewIndex = 0;
                txtFilter_ID.Text = txtFilter_ID.Text + " ";
                FilterBtn_Click(null, null);
                //mvUsers.ActiveViewIndex = 0;
            }
            else
                FailureUpdateText.Text = "Update User Unsuccess! Please try again.";


        }

        private bool updateUser(string sSys_ID, bool bStatus, string sEmailAddress, bool bChangePwd, string sPwd, string sEditBy, bool bLocked, string sWindowsAuthUserID, string sUserID, string sUserName, string sDeptCode, string sRoleCode, string sObsDate, string sKey)
        {   /*
        FailureUpdateText.Text = FailureUpdateText.Text + " spAppUserAdd @sSys_ID=" + sSys_ID;
        FailureUpdateText.Text = FailureUpdateText.Text + ",@bStatus=" + bStatus.ToString();
        FailureUpdateText.Text = FailureUpdateText.Text + ",@bIsAdmin=" + bIsAdmin.ToString();
        FailureUpdateText.Text = FailureUpdateText.Text + ",@bLocked=" + bLocked.ToString();
        FailureUpdateText.Text = FailureUpdateText.Text + ",@sEmailAddress='" + sEmailAddress + "'";
        FailureUpdateText.Text = FailureUpdateText.Text + ",@sUserName='" + sUserName + "'";
        FailureUpdateText.Text = FailureUpdateText.Text + ",@sUserID='" + sUserID + "'";
        FailureUpdateText.Text = FailureUpdateText.Text + ",@sWindowsAuthUserID='" + sWindowsAuthUserID + "'";
        FailureUpdateText.Text = FailureUpdateText.Text + ",@bChangePwd=" + bChangePwd.ToString();
        FailureUpdateText.Text = FailureUpdateText.Text + ",@sPwd='" + sPwd + "'";
        FailureUpdateText.Text = FailureUpdateText.Text + ",@sDeptCode='" + sDeptCode + "'";
        FailureUpdateText.Text = FailureUpdateText.Text + ",@sRoleCode='" + sRoleCode + "'";
        FailureUpdateText.Text = FailureUpdateText.Text + ",@sEditBy='" + sEditBy + "'";
        FailureUpdateText.Text = FailureUpdateText.Text + ",@dObsoleteDate='" + sObsDate + "'";
        */
            bool updateSuccess;
            DataSet ds = new UserController().updateUser(sSys_ID, bStatus, txtEmailAddress_Edit.Text, bChangePwd, txtPwd_Edit.Text, sEditBy, bLocked, sWindowsAuthUserID, sUserID, sUserName, sDeptCode, sRoleCode, sObsDate, sKey);

            updateSuccess = false;

            //eFormController().ch
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                    updateSuccess = true;
            }
            catch (Exception ex)
            {

            }

            return updateSuccess;

        }


        private bool addUser(bool bStatus, string sEmailAddress, string sPwd, string sAddBy, string sWindowsAuthUserID, string sUserID, string sUserName, string sDeptCode, string sRoleCode, string sKey)
        {
            /*
            FailureAddText.Text = FailureAddText.Text + " spAppUserAdd @bStatus=" + bStatus.ToString();
            FailureAddText.Text = FailureAddText.Text + ",@bIsAdmin=" + bIsAdmin.ToString();
            FailureAddText.Text = FailureAddText.Text + ",@sEmailAddress='" + sEmailAddress + "'";
            FailureAddText.Text = FailureAddText.Text + ",@sPwd='" + sPwd + "'";
            FailureAddText.Text = FailureAddText.Text + ",@sAddBy='" + sAddBy + "'";
            FailureAddText.Text = FailureAddText.Text + ",@sWindowsAuthUserID='" + sWindowsAuthUserID + "'";
            FailureAddText.Text = FailureAddText.Text + ",@sUserID='" + sUserID + "'";
            FailureAddText.Text = FailureAddText.Text + ",@sUserName='" + sUserName + "'";
            FailureAddText.Text = FailureAddText.Text + ",@sDeptCode='" + sDeptCode + "'";
            FailureAddText.Text = FailureAddText.Text + ",@sRoleCode='" + sRoleCode + "'";
            */
            bool addSuccess;
            DataSet ds = new UserController().addUser(bStatus, sEmailAddress, sPwd, sAddBy, sWindowsAuthUserID, sUserID, sUserName, sDeptCode, sRoleCode, sKey);

            addSuccess = false;

            //eFormController().ch
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    addSuccess = true;
                }
            }
            catch (Exception ex)
            {

            }
            return addSuccess;

        }

        protected void CancelUpdateUserBtn_Click(object sender, EventArgs e)
        {
            FailureUpdateText.Text = "";
            mvUsers.ActiveViewIndex = 0;
        }

        protected void CancelAddUserCancelBtn_Click(object sender, EventArgs e)
        {
            ErrorMessageSync.Text = "";
            mvUsers.ActiveViewIndex = 0;
        }


        protected void CancelAddUserBtn_Click(object sender, EventArgs e)
        {
            FailureAddText.Text = "";
            mvUsers.ActiveViewIndex = 0;
        }

        protected void chkboxChgPwd_Click(object sender, EventArgs e)
        {
            if (chkboxChgPwd.Checked)
                txtPwd_Edit.Enabled = true;
            else
                txtPwd_Edit.Enabled = false;

        }

        private bool requestResetPwd(string sAppUserSys_ID, string iExpiryMintue, string sRequestBy)
        {
            /*
            FailureAddText.Text = FailureAddText.Text + " spAppUserAdd @bStatus=" + bStatus.ToString();
            FailureAddText.Text = FailureAddText.Text + ",@bIsAdmin=" + bIsAdmin.ToString();
            FailureAddText.Text = FailureAddText.Text + ",@sEmailAddress='" + sEmailAddress + "'";
            FailureAddText.Text = FailureAddText.Text + ",@sPwd='" + sPwd + "'";
            FailureAddText.Text = FailureAddText.Text + ",@sAddBy='" + sAddBy + "'";
            FailureAddText.Text = FailureAddText.Text + ",@sWindowsAuthUserID='" + sWindowsAuthUserID + "'";
            FailureAddText.Text = FailureAddText.Text + ",@sUserID='" + sUserID + "'";
            FailureAddText.Text = FailureAddText.Text + ",@sUserName='" + sUserName + "'";
            FailureAddText.Text = FailureAddText.Text + ",@sDeptCode='" + sDeptCode + "'";
            FailureAddText.Text = FailureAddText.Text + ",@sRoleCode='" + sRoleCode + "'";
            */
            bool bResetSuccess;
            string script = "";
            string sLink = "";

            DataSet ds = new UserController().requestResetPwd(sAppUserSys_ID, iExpiryMintue, sRequestBy, "BySysAdmin", "");
            DataRow dr;

            bResetSuccess = false;

            dr = ds.Tables[0].Rows[0];

            if (Convert.ToString(dr["ErrorMsg"]) == "")
                bResetSuccess = true;
            else
                bResetSuccess = false;

            if (bResetSuccess)
            {
                script = "alert(\"Sent to " + Convert.ToString(dr["SendToEmailAddress"]) + "!\");";

                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]);

                    mail.From = new MailAddress(Convert.ToString(dr["SendFromEmailAddress"]));
                    mail.To.Add(Convert.ToString(dr["SendToEmailAddress"]));
                    mail.Subject = "Please use the link to reset the password!";

                    sLink = HttpContext.Current.Request.Url.PathAndQuery;
                    sLink = HttpContext.Current.Request.Url.AbsoluteUri.Replace(sLink, "/") + "eForm_Web/Account/ResetPassword.aspx" + "?acd=" + Convert.ToString(dr["AccessCode"]);

                    mail.Body = sLink;

                    SmtpServer.Port = 25;
                    //SmtpServer.Credentials = new System.Net.NetworkCredential("username", "password");
                    //SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);

                }
                catch (Exception ex)
                {
                    script = "alert(\"" + ex.ToString() + "\");";
                    //System.Windows.Forms.MessageBox.Show(ex.ToString());
                    //RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.ToString() + "');", true);
                }
            }
            else
            {
                script = "alert(\"Fail!\");";

            }

            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);

            return bResetSuccess;

        }

        /// <summary>
        /// Based on gridview's row action, switch to corresponding logics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            UserController uc = new UserController();

            DataSet dsUser;
            DataRow drUser;
            DataSet dsDept;
            DataSet rsDept;

            switch (e.CommandName.ToString().ToUpper())
            {
                case "E": //edit
                    dsUser = uc.getUsers(e.CommandArgument.ToString());
                    try
                    {
                        FailureUpdateText.Text = "";

                        dsDept = uc.getDepartments(false, false);
                        rsDept = uc.getRoles(false, false);

                        ddlDept_Edit.DataSource = dsDept;
                        ddlDept_Edit.DataTextField = "Description";
                        ddlDept_Edit.DataValueField = "Code";
                        ddlDept_Edit.DataBind();

                        ddlDept_Edit.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        ddlDept_Edit.SelectedIndex = 0;

                        ddlDept_Edit_Old.DataSource = dsDept;
                        ddlDept_Edit_Old.DataTextField = "Description";
                        ddlDept_Edit_Old.DataValueField = "Code";
                        ddlDept_Edit_Old.DataBind();

                        ddlDept_Edit_Old.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        ddlDept_Edit_Old.SelectedIndex = 0;

                        ddlRole_Edit.DataSource = rsDept;
                        ddlRole_Edit.DataTextField = "Description";
                        ddlRole_Edit.DataValueField = "Code";
                        ddlRole_Edit.DataBind();

                        ddlRole_Edit.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        ddlRole_Edit.SelectedIndex = 0;

                        ddlRole_Edit_Old.DataSource = rsDept;
                        ddlRole_Edit_Old.DataTextField = "Description";
                        ddlRole_Edit_Old.DataValueField = "Code";
                        ddlRole_Edit_Old.DataBind();

                        ddlRole_Edit_Old.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        ddlRole_Edit_Old.SelectedIndex = 0;

                        chkboxChgPwd.Checked = false;
                        txtPwd_Edit.Text = "";
                        txtPwd_Edit.Enabled = false;

                        drUser = dsUser.Tables[0].Rows[0];
                        //txtID_Edit.Text = e.CommandArgument.ToString();
                        txtSysID_Edit.Text = Convert.ToString(drUser["sys_ID"]);
                        txtID_Edit.Text = Convert.ToString(drUser["ID"]);
                        txtTotalLoginFail_Edit.Text = Convert.ToString(drUser["TotalLoginFail"]);
                        txtUserName_Edit.Text = Convert.ToString(drUser["UserName"]);
                        txtUserName_Edit_Old.Text = Convert.ToString(drUser["UserName"]);
                        txtUserID_Edit.Text = Convert.ToString(drUser["UserID"]);
                        txtUserID_Edit_Old.Text = Convert.ToString(drUser["UserID"]);
                        txtWinAuthUserID_Edit.Text = Convert.ToString(drUser["WindowsAuthUserID"]);
                        txtWinAuthUserID_Edit_Old.Text = Convert.ToString(drUser["WindowsAuthUserID"]);
                        txtEmailAddress_Edit.Text = Convert.ToString(drUser["EmailAddress"]);
                        txtEmailAddress_Edit_Old.Text = Convert.ToString(drUser["EmailAddress"]);
                        txtAddDate_Edit.Text = Convert.ToString(drUser["AddDate"]);
                        txtAddBy_Edit.Text = Convert.ToString(drUser["AddBy"]);
                        txtEditDate_Edit.Text = Convert.ToString(drUser["EditDate"]);
                        txtEditBy_Edit.Text = Convert.ToString(drUser["EditBy"]);
                        txtLatestSuccessLoginDate_Edit.Text = Convert.ToString(drUser["LatestSuccessLoginDate"]);
                        txtObsoleteDate_Edit.Text = Convert.ToString(drUser["ObsoleteDate"]);
                        txtObsoleteDate_Edit_Old.Text = Convert.ToString(drUser["ObsoleteDate"]);
                        txtLatestChangePwdDate_Edit.Text = Convert.ToString(drUser["LatestChangePwdDate"]);

                        ddlStatus_Edit.SelectedIndex = ddlStatus_Edit.Items.IndexOf(ddlStatus_Edit.Items.FindByValue(Convert.ToString(drUser["Status"])));
                        ddlStatus_Edit_Old.SelectedIndex = ddlStatus_Edit_Old.Items.IndexOf(ddlStatus_Edit_Old.Items.FindByValue(Convert.ToString(drUser["Status"])));
                        //ddlIsAdmin_Edit.SelectedIndex = ddlIsAdmin_Edit.Items.IndexOf(ddlIsAdmin_Edit.Items.FindByValue(Convert.ToString(drUser["IsAdmin"])));
                        // ddlIsAdmin_Edit_Old.SelectedIndex = ddlIsAdmin_Edit_Old.Items.IndexOf(ddlIsAdmin_Edit_Old.Items.FindByValue(Convert.ToString(drUser["IsAdmin"])));
                        ddlLocked_Edit.SelectedIndex = ddlLocked_Edit.Items.IndexOf(ddlLocked_Edit.Items.FindByValue(Convert.ToString(drUser["Locked"])));
                        ddlLocked_Edit_Old.SelectedIndex = ddlLocked_Edit_Old.Items.IndexOf(ddlLocked_Edit_Old.Items.FindByValue(Convert.ToString(drUser["Locked"])));

                        ddlDept_Edit.SelectedIndex = ddlDept_Edit.Items.IndexOf(ddlDept_Edit.Items.FindByValue(Convert.ToString(drUser["DeptCode"])));
                        ddlDept_Edit_Old.SelectedIndex = ddlDept_Edit_Old.Items.IndexOf(ddlDept_Edit_Old.Items.FindByValue(Convert.ToString(drUser["DeptCode"])));

                        ddlRole_Edit.SelectedIndex = ddlRole_Edit.Items.IndexOf(ddlRole_Edit.Items.FindByValue(Convert.ToString(drUser["RoleCode"])));
                        ddlRole_Edit_Old.SelectedIndex = ddlRole_Edit_Old.Items.IndexOf(ddlRole_Edit_Old.Items.FindByValue(Convert.ToString(drUser["RoleCode"])));

                        ddlChangePwdWhenLogin_Edit.SelectedIndex = ddlChangePwdWhenLogin_Edit.Items.IndexOf(ddlChangePwdWhenLogin_Edit.Items.FindByValue(Convert.ToString(drUser["ChangePwdWhenLogin"])));
                        // Response.Write(drUser["DeptCode"]);
                        /*
                        txtSurveyNameE.Text = Convert.ToString(drSurvey["name"]);
                        txtDescE.Text = Convert.ToString(drSurvey["description"]);
                        txtContentE.Text = Convert.ToString(drSurvey["value"]);
                        ddlStatusE.SelectedIndex = ddlStatusE.Items.IndexOf(ddlStatusE.Items.FindByValue(Convert.ToString(drSurvey["status"])));
                        */
                        mvUsers.ActiveViewIndex = 1;
                    }
                    catch
                    {
                        //ErrorMessageM.Text = "Failed to load survey. Please contact IT for further assistance.";
                    }
                    break;

                case "R": //edit
                          //e.CommandArgument.ToString();
                          //dsUser = uc.getUsers(e.CommandArgument.ToString());

                    string UID = "";

                    if (Session["UID"] != null)
                        UID = Session["UID"].ToString();

                    requestResetPwd(e.CommandArgument.ToString(), ConfigurationManager.AppSettings["ResetPwdExpiryMintue"], UID);


                    break;
                default:
                    break;
            }
        }

        //Sync AD
        private DirectoryEntry GetDirectoryObject(string name, string password)
        {
            DirectoryEntry oDE;
            oDE = new DirectoryEntry("LDAP://" + _dn.Dc(ConfigurationManager.AppSettings["ActiveDirectorySvrName"], ConfigurationManager.AppSettings["EnKey"]) + "", name, password);

            return oDE;
        }


        protected void btnSyncADUser_Click(object sender, EventArgs e)
        {

            mvUsers.ActiveViewIndex = 3;
            txtAD_DN.Text = _dn.Dc(ConfigurationManager.AppSettings["ActiveDirectoryDN"], ConfigurationManager.AppSettings["EnKey"]);
            txtRole.Text = ConfigurationManager.AppSettings["DefaultNewUserRole"].ToString();

            if (ConfigurationManager.AppSettings["DefaultNewUserStatus"].ToString() == "1")
            { txtStatus.Text = "Active"; }
            else { txtStatus.Text = "Inactive"; }

            txtWindowAuthID.Text = HttpContext.Current.User.Identity.Name.ToString();

            ErrorMessageSync.Text = "";
        }

        private SearchResultCollection GetAllUsers(string name, string password)
        {
            DirectoryEntry de = GetDirectoryObject(name, password);
            DirectorySearcher deSearch = new DirectorySearcher();
            deSearch.SearchRoot = de;

            deSearch.Filter = "(&(objectCategory=person)(objectClass=user)(!(userAccountControl:1.2.840.113556.1.4.803:=2)))";// (memberOf=CN=PE Vol,OU=PE,OU=HKOffice,OU=HKUser,DC=wvhk,DC=local))"; //"(&(sAMAccountType=805306368)(userAccountControl:1.2.840.113556.1.4.803:=2))"; //(&(sAMAccountType=805306368)(!(userAccountControl:1.2.840.113556.1.4.803:=2)))";// "(&(objectCategory=Person)(objectClass=user)(!(userAccountControl:1.2.840.113556.1.4.803:=2)))";
            deSearch.SearchScope = SearchScope.Subtree;
            SearchResultCollection results = deSearch.FindAll();

            return results;
        }

        protected void btnSyncADUserProcess_Click(object sender, EventArgs e)
        {

            string sUserID = "";
            string sUserName = "";
            string sDistinguishedName;
            string sAction = "";
            string sAddBy = "";
            string sRole = "";
            bool bStatus = false;
            string sUserList = "";
            string sADDN = "";
            string UID = "";
            bool sSyncSuccess = false;

            EncColltroller _dn = new EncColltroller();

            sADDN = txtAD_DN.Text;
            sRole = txtRole.Text;

            if (txtStatus.Text == "1" || txtStatus.Text == "true")
                bStatus = true;

            if (Session["UID"] != null)
                UID = Session["UID"].ToString();

            sAddBy = UID;
            sAction = "A";

            if (txtPwd.Text == "")
            {
                ErrorMessageSync.Text = "Enter the password";
                return;
            }

            SearchResultCollection MyUsers = GetAllUsers(txtWindowAuthID.Text, txtPwd.Text);

            try
            {
                if (MyUsers != null && MyUsers.Count > 0)
                {
                    foreach (SearchResult m_User in MyUsers)
                    {
                        DirectoryEntry d = m_User.GetDirectoryEntry();

                        sUserID = sADDN + "\\" + m_User.Properties["samaccountname"][0].ToString();
                        sUserName = m_User.Properties["CN"][0].ToString();
                        sDistinguishedName = m_User.Properties["distinguishedName"][0].ToString();

                        if (sUserName == "")
                            sUserName = m_User.Properties["samaccountname"][0].ToString();

                        DataSet ds = new UserController().synADUsers(sUserID, sUserName, sDistinguishedName, sAction, sAddBy, sRole, bStatus, "", sADDN);
                        sUserList += sUserID + ",";
                        //txtUserList.Text += sUserID;
                        txtUserList2.Text += sUserID + " | " + sUserName + " | " + sDistinguishedName + " | " + sAction + " | " + sAddBy + " | " + sRole + " | " + bStatus.ToString() + " | ";
                        //lblUserID.Text = lblUserID.Text + sUserID + " <br>";
                    }

                    sAction = "U";
                    DataSet dsDisable = new UserController().synADUsers("", "", "", sAction, sAddBy, sRole, bStatus, sUserList, sADDN);

                    sSyncSuccess = true;
                    ErrorMessageSync.Text = "";
                    Response.Redirect("~/Account/UserMaintenance.aspx");
                }

            }
            catch (Exception ex)
            {
                ErrorMessageSync.Text = ex.Message;
                sSyncSuccess = false;
            }

            txtUserList.Text = sUserList;

        }
    }


