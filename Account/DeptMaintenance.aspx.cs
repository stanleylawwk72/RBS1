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

using wv.Users;
using wv.GeneralFunction;

public partial class DeptMaintenance : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (new GeneralFunctionController().checkIsUnderMaintenance())
            Response.Redirect("~/MaintenancePage.aspx");

       // if (!IsPostBack)
        //{
            //mvDepartments.ActiveViewIndex = 0;

        //GridView1.DataSource = new UserController().getDepartments();
        //GridView1.DataBind();
          
        //}
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        mvDepartments.ActiveViewIndex = 2;
    }

    protected void PageDeptIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewDepartments.PageIndex = e.NewPageIndex;
        GridViewDepartments.DataBind();

    }

    protected void AddDepartmentBtn_Click(object sender, EventArgs e)
    {

        string UID = "";
        bool bStatus;

        FailureAddText.Text = "";

        if (ddlStatus_Add.SelectedValue == "True")
	        bStatus = true;
        else
	        bStatus = false;


        if (Session["UID"] != null)
            UID = Session["UID"].ToString();

        if (addDepartment(txtCode_Add.Text, txtDescription_Add.Text, UID,bStatus))
        {
           Response.Redirect("~/Account/DeptMaintenance.aspx");
            //mvDepartments.ActiveViewIndex = 0;
        }
        else
            FailureAddText.Text =FailureAddText.Text +"Add Department Unsuccess! Please try again.";
    }


    protected void UpdateDepartmentBtn_Click(object sender, EventArgs e)
    {
        string dObsoleteDate;
        DateTime dObsDate;
        string UID = "";
        bool bStatus;

        FailureUpdateText.Text = "";

        if (
            txtCode_Edit.Text == txtCode_Edit_Old.Text &&
            txtDescription_Edit.Text == txtDescription_Edit_Old.Text &&
            ddlStatus_Edit.SelectedValue == ddlStatus_Edit_Old.SelectedValue &&
            txtObsoleteDate_Edit.Text == txtObsoleteDate_Edit_Old.Text 
            )
        {
            FailureUpdateText.Text = "The information is the same, no need to update! ";
            return;
        }

        if (txtObsoleteDate_Edit.Text == "")
        {
            dObsoleteDate = string.Empty;
            //FailureUpdateText.Text = "dObsoleteDate"+dObsoleteDate;
            // return;
        }
        else
        {
            if (DateTime.TryParse(txtObsoleteDate_Edit.Text, out dObsDate))
                dObsoleteDate = txtObsoleteDate_Edit.Text;
            else
            {
                FailureUpdateText.Text = "Please input the valid Obsolete date! ";
                return;
            }
        }


        if (ddlStatus_Edit.SelectedValue == "True")
            bStatus = true;
        else
            bStatus = false;

 
        if (Session["UID"] != null)
            UID=Session["UID"].ToString();


        if (updateDepartment(txtCode_Edit.Text, txtDescription_Edit.Text, UID, bStatus, dObsoleteDate))
        {
            Response.Redirect("~/Account/DeptMaintenance.aspx");
            //mvDepartments.ActiveViewIndex = 0;
        }
        else
            FailureUpdateText.Text = FailureUpdateText.Text +"Update Department Unsuccess! Please try again.";

        
    }

    private bool updateDepartment(string sCode, string sDesc, string sEditBy, bool bStatus, string dObsoleteDate)
    {
/*
        FailureUpdateText.Text = FailureUpdateText.Text + " spDepartmentUpdate @sCode='" + sCode + "'";
        FailureUpdateText.Text = FailureUpdateText.Text + ",@sDesc='" + sDesc + "'";
        FailureUpdateText.Text = FailureUpdateText.Text + ",@sEditBy='" + sEditBy + "'";
        FailureUpdateText.Text = FailureUpdateText.Text + ",@bStatus=" + bStatus.ToString();
        */
        bool updateSuccess;

        DataSet ds = new UserController().updateDepartment(sCode, sDesc, sEditBy, bStatus, dObsoleteDate);

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


    private bool addDepartment(string sCode, string sDesc, string sAddBy,bool bStatus)
    {
        /*
        FailureAddText.Text = FailureAddText.Text + " spAppDepartmentAdd @bStatus=" + bStatus.ToString();
        FailureAddText.Text = FailureAddText.Text + ",@bIsAdmin=" + bIsAdmin.ToString();
        FailureAddText.Text = FailureAddText.Text + ",@sEmailAddress='" + sEmailAddress + "'";
        FailureAddText.Text = FailureAddText.Text + ",@sPwd='" + sPwd + "'";
        FailureAddText.Text = FailureAddText.Text + ",@sAddBy='" + sAddBy + "'";
        FailureAddText.Text = FailureAddText.Text + ",@sWindowsAuthDepartmentID='" + sWindowsAuthDepartmentID + "'";
        FailureAddText.Text = FailureAddText.Text + ",@sDepartmentID='" + sDepartmentID + "'";
        FailureAddText.Text = FailureAddText.Text + ",@sDepartmentName='" + sDepartmentName + "'";
         * */
        bool addSuccess;
        DataSet ds = new UserController().addDepartment(sCode, sDesc, sAddBy,bStatus);

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

    protected void CancelUpdateDepartmentBtn_Click(object sender, EventArgs e)
    {
        mvDepartments.ActiveViewIndex = 0;
    }

    protected void CancelAddDepartmentBtn_Click(object sender, EventArgs e)
    {
        mvDepartments.ActiveViewIndex = 0;
    }

    

    /// <summary>
    /// Based on gridview's row action, switch to corresponding logics
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        UserController uc = new UserController();
        DataSet dsDepartment;
        DataRow drDepartment;

        switch (e.CommandName.ToString().ToUpper())
        {
            case "E": //edit
                dsDepartment = uc.getDepartments(e.CommandArgument.ToString());
                try
                {
                    FailureUpdateText.Text = "";
                    drDepartment = dsDepartment.Tables[0].Rows[0];
                    //txtID_Edit.Text = e.CommandArgument.ToString();
                    txtCode_Edit_Old.Text = Convert.ToString(drDepartment["Code"]);
                    txtCode_Edit.Text = Convert.ToString(drDepartment["Code"]);
                    txtDescription_Edit_Old.Text = Convert.ToString(drDepartment["Description"]);
                    txtDescription_Edit.Text = Convert.ToString(drDepartment["Description"]);
                    txtAddDate_Edit.Text = Convert.ToString(drDepartment["AddDate"]);
                    txtAddBy_Edit.Text = Convert.ToString(drDepartment["AddBy"]);
                    txtEditDate_Edit.Text = Convert.ToString(drDepartment["EditDate"]);
                    txtEditBy_Edit.Text = Convert.ToString(drDepartment["EditBy"]);
                    txtObsoleteDate_Edit.Text = Convert.ToString(drDepartment["ObsoleteDate"]);
                    txtObsoleteDate_Edit_Old.Text = Convert.ToString(drDepartment["ObsoleteDate"]);
                    ddlStatus_Edit.SelectedIndex = ddlStatus_Edit.Items.IndexOf(ddlStatus_Edit.Items.FindByValue(Convert.ToString(drDepartment["Status"])));
                    ddlStatus_Edit_Old.SelectedIndex = ddlStatus_Edit_Old.Items.IndexOf(ddlStatus_Edit_Old.Items.FindByValue(Convert.ToString(drDepartment["Status"])));

                    mvDepartments.ActiveViewIndex = 1;
                }
                catch
                {
                    //ErrorMessageM.Text = "Failed to load survey. Please contact IT for further assistance.";
                }

                break;
            default:
                break;
        }
    }
}


