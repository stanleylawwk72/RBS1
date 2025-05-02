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

public partial class RoleMaintenance : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
       // if (!IsPostBack)
        //{
            //mvRoles.ActiveViewIndex = 0;

        //GridView1.DataSource = new UserController().getRoles();
        //GridView1.DataBind();
          
        //}
        if (new GeneralFunctionController().checkIsUnderMaintenance())
            Response.Redirect("~/MaintenancePage.aspx");
    }

    //Role
    protected void btnAddRole_Click(object sender, EventArgs e)
    {
        mvRoles.ActiveViewIndex = 2;
    }

    protected void CancelUpdateRoleBtn_Click(object sender, EventArgs e)
    {
        mvRoles.ActiveViewIndex = 0;
    }

    protected void CancelAddRoleBtn_Click(object sender, EventArgs e)
    {
        mvRoles.ActiveViewIndex = 0;
    }


    protected void AddRoleBtn_Click(object sender, EventArgs e)
    {

        FailureRoleAddText.Text = "";
       
        string UID = "";
        bool bStatus;
        //bool bIsAdmin;

        if (txtCode_Role_Add.Text == "")
        {
            FailureRoleAddText.Text = "Please enter 'Code'! ";
            txtCode_Role_Add.Focus();
            return;
        }

        if (txtDescription_Role_Add.Text == "")
        {
            FailureRoleAddText.Text = "Please enter 'Description'! ";
            txtDescription_Role_Add.Focus();
            return;
        }

        if (ddlStatus_Role_Add.SelectedValue == "True")
	        bStatus = true;
        else
	        bStatus = false;

        //if (ddlDeptAdmin_Role_Add.SelectedValue == "True")
       //     bIsAdmin = true;
       // else
       //     bIsAdmin = false;


        if (Session["UID"] != null)
            UID = Session["UID"].ToString();

        if (addRole(txtCode_Role_Add.Text, txtDescription_Role_Add.Text, UID, bStatus))
        {
           Response.Redirect("~/Account/RoleMaintenance.aspx");
            //mvRoles.ActiveViewIndex = 0;
        }
        else
            FailureRoleAddText.Text =FailureRoleAddText.Text +"Add Role Unsuccess! Please try again.";
    }

    protected void UpdateRoleBtn_Click(object sender, EventArgs e)
    {

        FailureRoleUpdateText.Text = "";
        string dObsoleteDate;
        DateTime dObsDate;

        string UID = "";
        bool bStatus;
        //bool bDeptAdmin;

        if (
            txtCode_Role_Edit.Text == txtCode_Role_Edit_Old.Text &&
            txtDescription_Role_Edit.Text == txtDescription_Role_Edit_Old.Text &&
            ddlStatus_Role_Edit.SelectedValue == ddlStatus_Role_Edit_Old.SelectedValue &&
           // ddlDeptAdmin_Role_Edit.SelectedValue == ddlDeptAdmin_Role_Edit_Old.SelectedValue &&
            txtObsoleteDate_Role_Edit.Text == txtObsoleteDate_Role_Edit_Old.Text
            )
        {
            FailureRoleUpdateText.Text = "The information is the same, no need to update! ";
            return;
        }

        if (txtCode_Role_Edit.Text == "")
        {
            FailureRoleUpdateText.Text = "Please enter 'Code'! ";
            txtCode_Role_Edit.Focus();
            return;
        }

        if (txtDescription_Role_Edit.Text == "")
        {
            FailureRoleUpdateText.Text = "Please enter 'Description'! ";
            txtDescription_Role_Edit.Focus();
            return;
        }

        if (txtObsoleteDate_Role_Edit.Text == "")
        {
            dObsoleteDate = string.Empty;
            //FailureRoleUpdateText.Text = "dObsoleteDate"+dObsoleteDate;
            // return;
        }
        else
        {
            if (DateTime.TryParse(txtObsoleteDate_Role_Edit.Text, out dObsDate))
                dObsoleteDate = txtObsoleteDate_Role_Edit.Text;
            else
            {
                FailureRoleUpdateText.Text = "Please enter the valid Obsolete date! ";
                return;
            }
        }

        if (ddlStatus_Role_Edit.SelectedValue == "True")
            bStatus = true;
        else
            bStatus = false;

       // if (ddlDeptAdmin_Role_Edit.SelectedValue == "True")
       //     bDeptAdmin = true;
      //  else
      //      bDeptAdmin = false;


        if (Session["UID"] != null)
            UID = Session["UID"].ToString();


        if (updateRole(txtCode_Role_Edit.Text, txtDescription_Role_Edit.Text, UID, bStatus, dObsoleteDate))
        {
            Response.Redirect("~/Account/RoleMaintenance.aspx");
            //mvRoles.ActiveViewIndex = 0;
        }
        else
            FailureRoleUpdateText.Text = FailureRoleUpdateText.Text + "Update Role Unsuccess! Please try again.";

    }


    private bool updateRole(string sCode, string sDesc, string sEditBy, bool bStatus, string dObsoleteDate)
    {
        /*
        FailureRoleUpdateText.Text = FailureRoleUpdateText.Text + " spRoleUpdate @sCode='" + sCode + "'";
        FailureRoleUpdateText.Text = FailureRoleUpdateText.Text + ",@sDesc='" + sDesc + "'";
        FailureRoleUpdateText.Text = FailureRoleUpdateText.Text + ",@sEditBy='" + sEditBy + "'";
        FailureRoleUpdateText.Text = FailureRoleUpdateText.Text + ",@bStatus=" + bStatus.ToString();
        */
        bool updateSuccess;

        DataSet ds = new UserController().updateRole(sCode, sDesc, sEditBy, bStatus, dObsoleteDate);

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
    
    private bool addRole(string sCode, string sDesc, string sAddBy, bool bStatus)
    {
        /*
        FailureRoleAddText.Text = FailureRoleAddText.Text + " spRoleAdd @sCode='" + sCode + "'";
        FailureRoleAddText.Text = FailureRoleAddText.Text + ",@sDesc='" + sDesc + "'";
        FailureRoleAddText.Text = FailureRoleAddText.Text + ",@sAddBy='" + sAddBy + "'";
        FailureRoleAddText.Text = FailureRoleAddText.Text + ",@bStatus=" + bStatus.ToString();
        FailureRoleAddText.Text = FailureRoleAddText.Text + ",@bIsAdmin=" + bStatus.ToString();
        */
        bool addSuccess;
        DataSet ds = new UserController().addRole(sCode, sDesc, sAddBy, bStatus);

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

    protected void gvRole_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        UserController uc = new UserController();
        DataSet dsRole;
        DataSet dsRoleDetail;
        DataRow drRole;
        DataRow drRoleDetail;

        switch (e.CommandName.ToString().ToUpper())
        {
            case "E": //edit
                dsRole = uc.getRoles(e.CommandArgument.ToString());
                //Response.Write (e.CommandArgument.ToString());
                try
                {
                    FailureRoleUpdateText.Text = "";
                    //Response.Write("4");
                    drRole = dsRole.Tables[0].Rows[0];
                    //txtID_Role_Edit.Text = e.CommandArgument.ToString();
                    txtCode_Role_Edit_Old.Text = Convert.ToString(drRole["Code"]);
                    txtCode_Role_Edit.Text = Convert.ToString(drRole["Code"]);
                    txtDescription_Role_Edit_Old.Text = Convert.ToString(drRole["Description"]);
                    txtDescription_Role_Edit.Text = Convert.ToString(drRole["Description"]);
                    txtAddDate_Role_Edit.Text = Convert.ToString(drRole["AddDate"]);
                    txtAddBy_Role_Edit.Text = Convert.ToString(drRole["AddBy"]);
                    txtEditDate_Role_Edit.Text = Convert.ToString(drRole["EditDate"]);
                    txtEditBy_Role_Edit.Text = Convert.ToString(drRole["EditBy"]);
                    txtObsoleteDate_Role_Edit.Text = Convert.ToString(drRole["ObsoleteDate"]);
                    txtObsoleteDate_Role_Edit_Old.Text = Convert.ToString(drRole["ObsoleteDate"]);
                    ddlStatus_Role_Edit.SelectedIndex = ddlStatus_Role_Edit.Items.IndexOf(ddlStatus_Role_Edit.Items.FindByValue(Convert.ToString(drRole["Status"])));
                    ddlStatus_Role_Edit_Old.SelectedIndex = ddlStatus_Role_Edit_Old.Items.IndexOf(ddlStatus_Role_Edit_Old.Items.FindByValue(Convert.ToString(drRole["Status"])));
                    //ddlDeptAdmin_Role_Edit.SelectedIndex = ddlDeptAdmin_Role_Edit.Items.IndexOf(ddlDeptAdmin_Role_Edit.Items.FindByValue(Convert.ToString(drRole["DeptAdmin"])));
                   // ddlDeptAdmin_Role_Edit_Old.SelectedIndex = ddlDeptAdmin_Role_Edit_Old.Items.IndexOf(ddlDeptAdmin_Role_Edit_Old.Items.FindByValue(Convert.ToString(drRole["DeptAdmin"])));


                    mvRoles.ActiveViewIndex = 1;
                }
                catch
                {
                    //ErrorMessageM.Text = "Failed to load survey. Please contact IT for further assistance.";
                }
                break;
            case "D": //edit
                //dsRoleDetail = uc.getRoleDetails(string.Empty, e.CommandArgument.ToString(), true, true);

                

                //GridViewRoleDetails.DataSource = null;
               // GridViewRoleDetails.DataBind(); 
               // Response.Write (e.CommandArgument.ToString());
                //mvRoles.ActiveViewIndex = 3;
                txtRoleCodeForAddNewRoleDetail.Text = "";
                try
                {
                    loadRoleDetailList(string.Empty, e.CommandArgument.ToString());
                    txtRoleCodeForAddNewRoleDetail.Text = e.CommandArgument.ToString();
                    //odsRoleDetails.SelectMethod=""
                    //Response.Write(e.CommandArgument.ToString());
                    //Response.Write("4");
                    //GridViewRoleDetails.DataSource = null;
                   // GridViewRoleDetails.DataSource = dsRoleDetail;
                   // GridViewRoleDetails.DataBind();     
                    //GridViewRoleDetails.DataSource = null;
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



    protected void PageRoleIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridViewRoles.PageIndex = e.NewPageIndex;
            GridViewRoles.DataBind();
        }
        catch { }
    }

    //Role Detail
    protected void btnBackToRole_Click(object sender, EventArgs e)
    {
        mvRoles.ActiveViewIndex = 0;
        txtRoleCodeForAddNewRoleDetail.Text = "";
    }


    protected void btnAddRoleDetail_Click(object sender, EventArgs e)
    {
        mvRoles.ActiveViewIndex = 5;

        UserController uc = new UserController();
        DataSet dsDept;
        DataSet rsDept;

        rsDept = uc.getRoles(false, false);

        ddlRoleCode_RoleDetail_Add.DataSource = rsDept;
        ddlRoleCode_RoleDetail_Add.DataTextField = "Code";
        ddlRoleCode_RoleDetail_Add.DataValueField = "Code";
        ddlRoleCode_RoleDetail_Add.DataBind();

        //ddlRoleCode_RoleDetail_Add.Items.Insert(0, new ListItem(String.Empty, String.Empty));
        //ddlRoleCode_RoleDetail_Add.SelectedIndex = 0;

        ddlRoleCode_RoleDetail_Add.SelectedIndex = ddlRoleCode_RoleDetail_Add.Items.IndexOf(ddlRoleCode_RoleDetail_Add.Items.FindByValue(txtRoleCodeForAddNewRoleDetail.Text));
    }

    protected void CancelUpdateRoleDetailBtn_Click(object sender, EventArgs e)
    {
        mvRoles.ActiveViewIndex = 3;
    }

    protected void CancelAddRoleDetailBtn_Click(object sender, EventArgs e)
    {
        if (txtRoleCodeForAddNewRoleDetail.Text!="") 
            mvRoles.ActiveViewIndex = 3;
        else
            mvRoles.ActiveViewIndex = 0;
    }



    protected void gvRoleDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        UserController uc = new UserController();
        DataSet dsRoleDetail;
        DataRow drRoleDetail;

        switch (e.CommandName.ToString().ToUpper())
        {
            case "E": //edit
                dsRoleDetail = uc.getRoleDetails(e.CommandArgument.ToString(),string.Empty, true, true);
               // Response.Write (e.CommandArgument.ToString());
                try
                {
                    FailureRoleDetailUpdateText.Text = "";
                    //Response.Write("4");
                    drRoleDetail = dsRoleDetail.Tables[0].Rows[0];
                    //txtID_Role_Edit.Text = e.CommandArgument.ToString();
                    txtID_RoleDetail_Edit.Text = Convert.ToString(drRoleDetail["ID"]);
                    txtRoleCode_RoleDetail_Edit.Text = Convert.ToString(drRoleDetail["RoleCode"]);
                    txtRoleDetailCode_RoleDetail_Edit.Text = Convert.ToString(drRoleDetail["RoleDetailCode"]);
                    txtRoleDetailCode_RoleDetail_Edit_Old.Text = Convert.ToString(drRoleDetail["RoleDetailCode"]);
                    txtDescription_RoleDetail_Edit_Old.Text = Convert.ToString(drRoleDetail["Description"]);
                    txtDescription_RoleDetail_Edit.Text = Convert.ToString(drRoleDetail["Description"]);
                    txtAddDate_RoleDetail_Edit.Text = Convert.ToString(drRoleDetail["AddDate"]);
                    txtAddBy_RoleDetail_Edit.Text = Convert.ToString(drRoleDetail["AddBy"]);
                    txtEditDate_RoleDetail_Edit.Text = Convert.ToString(drRoleDetail["EditDate"]);
                    txtEditBy_RoleDetail_Edit.Text = Convert.ToString(drRoleDetail["EditBy"]);
                    txtObsoleteDate_RoleDetail_Edit.Text = Convert.ToString(drRoleDetail["ObsoleteDate"]);
                    txtObsoleteDate_RoleDetail_Edit_Old.Text = Convert.ToString(drRoleDetail["ObsoleteDate"]);
                    ddlStatus_RoleDetail_Edit.SelectedIndex = ddlStatus_RoleDetail_Edit.Items.IndexOf(ddlStatus_RoleDetail_Edit.Items.FindByValue(Convert.ToString(drRoleDetail["Status"])));
                    ddlStatus_RoleDetail_Edit_Old.SelectedIndex = ddlStatus_RoleDetail_Edit_Old.Items.IndexOf(ddlStatus_RoleDetail_Edit_Old.Items.FindByValue(Convert.ToString(drRoleDetail["Status"])));
                    ddlPermission_RoleDetail_Edit.SelectedIndex = ddlPermission_RoleDetail_Edit.Items.IndexOf(ddlPermission_RoleDetail_Edit.Items.FindByValue(Convert.ToString(drRoleDetail["Permission"])));
                    ddlPermission_RoleDetail_Edit_Old.SelectedIndex = ddlPermission_RoleDetail_Edit_Old.Items.IndexOf(ddlPermission_RoleDetail_Edit_Old.Items.FindByValue(Convert.ToString(drRoleDetail["Permission"])));


                    mvRoles.ActiveViewIndex = 4;
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

    protected void UpdateRoleDetailBtn_Click(object sender, EventArgs e)
    {

        FailureRoleDetailUpdateText.Text = "";
        string dObsoleteDate;
        DateTime dObsDate;

        string UID = "";
        bool bStatus;
        bool bPermission;

        if (
            txtRoleDetailCode_RoleDetail_Edit.Text == txtRoleDetailCode_RoleDetail_Edit_Old.Text &&
            txtDescription_RoleDetail_Edit.Text == txtDescription_RoleDetail_Edit_Old.Text &&
            ddlStatus_RoleDetail_Edit.SelectedValue == ddlStatus_RoleDetail_Edit_Old.SelectedValue &&
            ddlPermission_RoleDetail_Edit.SelectedValue == ddlPermission_RoleDetail_Edit_Old.SelectedValue &&
            txtObsoleteDate_RoleDetail_Edit.Text == txtObsoleteDate_RoleDetail_Edit_Old.Text
            )
        {
            FailureRoleDetailUpdateText.Text = "The information is the same, no need to update! ";
            return;
        }


        if (txtRoleDetailCode_RoleDetail_Edit.Text == "")
        {
            FailureRoleDetailUpdateText.Text = "Please enter 'Role Detail Code'! ";
            txtRoleDetailCode_RoleDetail_Edit.Focus();
            return;
        }

        if (txtDescription_RoleDetail_Edit.Text == "")
        {
            FailureRoleDetailUpdateText.Text = "Please enter 'Description'! ";
            txtDescription_RoleDetail_Edit.Focus();
            return;
        }

        if (txtObsoleteDate_RoleDetail_Edit.Text == "")
        {
            dObsoleteDate = string.Empty;
            //FailureRoleDetailUpdateText.Text = "dObsoleteDate"+dObsoleteDate;
            // return;
        }
        else
        {
            if (DateTime.TryParse(txtObsoleteDate_RoleDetail_Edit.Text, out dObsDate))
                dObsoleteDate = txtObsoleteDate_RoleDetail_Edit.Text;
            else
            {
                FailureRoleDetailUpdateText.Text = "Please enter the valid Obsolete date! ";
                return;
            }
        }

        if (ddlStatus_RoleDetail_Edit.SelectedValue == "True")
            bStatus = true;
        else
            bStatus = false;

        if (ddlPermission_RoleDetail_Edit.SelectedValue == "True")
            bPermission = true;
        else
            bPermission = false;

        if (Session["UID"] != null)
            UID = Session["UID"].ToString();


        if (updateRoleDetail(txtID_RoleDetail_Edit.Text, txtRoleCode_RoleDetail_Edit.Text, txtRoleDetailCode_RoleDetail_Edit.Text, txtDescription_RoleDetail_Edit.Text, bStatus, bPermission, UID, dObsoleteDate))
        {
            //Response.Redirect("~/Account/RoleMaintenance.aspx");
            //mvRoles.ActiveViewIndex = 0;
           
            try
            {
                
                //GridViewRoleDetails.DataSource = null; 
                loadRoleDetailList(string.Empty,txtRoleCode_RoleDetail_Edit.Text);
            }
            catch
            {
                //ErrorMessageM.Text = "Failed to load survey. Please contact IT for further assistance.";
            }

        }
        else
            FailureRoleUpdateText.Text = FailureRoleUpdateText.Text + "Update Role Unsuccess! Please try again.";

    }

    private void loadRoleDetailList(string iID,string sRoleCode)
    {
         UserController uc = new UserController();
         DataSet dsRoleDetail;

         dsRoleDetail = uc.getRoleDetails(iID, sRoleCode, true, true);
         GridViewRoleDetails.DataSource = null;
         GridViewRoleDetails.DataBind();
         mvRoles.ActiveViewIndex = 3;
         GridViewRoleDetails.DataSource = dsRoleDetail;
         GridViewRoleDetails.DataBind();
    
    }

    protected void PageRoleDetailIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            loadRoleDetailList(string.Empty, txtRoleCode_RoleDetail_Edit.Text);
            GridViewRoleDetails.PageIndex = e.NewPageIndex;
            GridViewRoleDetails.DataBind();
        }
        catch { }
        
    }
    /*
    private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case SortDirection.Ascending:
                newSortDirection = "ASC";
                break;

            case SortDirection.Descending:
                newSortDirection = "DESC";
                break;
        }

        return newSortDirection;
    }



    protected void GridViewRoleDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dataTable = GridViewRoleDetails.DataSource as DataTable;

        Response.Write(e.SortExpression.ToString() + "     ");
        Response.Write(e.SortDirection.ToString());
        
        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);
            Response.Write(e.SortExpression.ToString());
            Response.Write(e.SortDirection.ToString());
            GridViewRoleDetails.DataSource = dataView;
            GridViewRoleDetails.DataBind();
        }


    }
    */
    private bool updateRoleDetail(string iID, string sRoleCode, string sRoleDetailCode, string sDesc, bool bStatus, bool bPermission, string sEditBy, string dObsoleteDate)
    {
        /*
        FailureRoleDetailUpdateText.Text = FailureRoleDetailUpdateText.Text + " spRoleDetailUpdate @iID='" + iID + "'";
        FailureRoleDetailUpdateText.Text = FailureRoleDetailUpdateText.Text + ",@sRoleCode='" + sRoleCode + "'";
        FailureRoleDetailUpdateText.Text = FailureRoleDetailUpdateText.Text + ",@sRoleDetailCode='" + sRoleDetailCode + "'";
        FailureRoleDetailUpdateText.Text = FailureRoleDetailUpdateText.Text + ",@sDesc='" + sDesc + "'";
        FailureRoleDetailUpdateText.Text = FailureRoleDetailUpdateText.Text + ",@bStatus='" + bStatus.ToString() + "'";
        FailureRoleDetailUpdateText.Text = FailureRoleDetailUpdateText.Text + ",@bPermission='" + bPermission.ToString() + "'";
        FailureRoleDetailUpdateText.Text = FailureRoleDetailUpdateText.Text + ",@sEditBy='" + sEditBy + "'";
        FailureRoleDetailUpdateText.Text = FailureRoleDetailUpdateText.Text + ",@dObsoleteDate='" + dObsoleteDate + "'";
        */
        bool updateSuccess;
        updateSuccess = false;

       // return updateSuccess;

        DataSet ds = new UserController().updateRoleDetail(iID, sRoleCode, sRoleDetailCode, sDesc, bStatus, bPermission, sEditBy, dObsoleteDate);

        
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


    protected void AddRoleDetailBtn_Click(object sender, EventArgs e)
    {

        FailureRoleDetailAddText.Text = "";

        string UID = "";
        bool bStatus;
        bool bPermission;

        if (ddlRoleCode_RoleDetail_Add.SelectedItem.Value.ToString() == "")
        {
            FailureRoleDetailAddText.Text = "Please select 'Role Code'! ";
            ddlRoleCode_RoleDetail_Add.Focus();
            return;
        }

        if (txtRoleDetailCode_RoleDetail_Add.Text == "")
        {
            FailureRoleDetailAddText.Text = "Please enter 'Role Detail Code'! ";
            txtRoleDetailCode_RoleDetail_Add.Focus();
            return;
        }

        if (txtDescription_RoleDetail_Add.Text == "")
        {
            FailureRoleDetailAddText.Text = "Please enter 'Description'! ";
            txtDescription_RoleDetail_Add.Focus();
            return;
        }

        if (ddlStatus_RoleDetail_Add.SelectedValue == "True")
            bStatus = true;
        else
            bStatus = false;
      
        if (ddlPermission_RoleDetail_Add.SelectedValue == "True")
            bPermission = true;
       else
            bPermission = false;

        if (Session["UID"] != null)
            UID = Session["UID"].ToString();

        if (addRoleDetail(ddlRoleCode_RoleDetail_Add.SelectedItem.Value.ToString(), txtRoleDetailCode_RoleDetail_Add.Text, txtDescription_RoleDetail_Add.Text,bStatus,bPermission, UID))
        {
            ddlStatus_RoleDetail_Add.SelectedIndex = 0;
            txtRoleDetailCode_RoleDetail_Add.Text = "";
            txtDescription_RoleDetail_Add.Text = "";
            txtRoleCodeForAddNewRoleDetail.Text = ddlRoleCode_RoleDetail_Add.SelectedItem.Value.ToString();
            loadRoleDetailList(string.Empty, ddlRoleCode_RoleDetail_Add.SelectedItem.Value.ToString());
            //mvRoleDetails.ActiveViewIndex = 0;
        }
        else
            FailureRoleDetailAddText.Text = FailureRoleDetailAddText.Text + "Add RoleDetail Unsuccess! Please try again.";

    }

    private bool addRoleDetail(string sRoleCode, string sRoleDetailCode, string sDesc, bool bStatus, bool bPermission, string sAddBy)
    {

        /*
         FailureRoleDetailAddText.Text = FailureRoleDetailAddText.Text + " spRoleDetailAdd @sRoleCode='" + sRoleCode + "'";
                FailureRoleDetailAddText.Text = FailureRoleDetailAddText.Text + ",@sRoleDetailCode='" + sRoleDetailCode + "'";
                FailureRoleDetailAddText.Text = FailureRoleDetailAddText.Text + ",@sDesc='" + sDesc + "'";
                FailureRoleDetailAddText.Text = FailureRoleDetailAddText.Text + ",@sAddBy='" + sAddBy + "'";
                FailureRoleDetailAddText.Text = FailureRoleDetailAddText.Text + ",@bStatus=" + bStatus.ToString();
                FailureRoleDetailAddText.Text = FailureRoleDetailAddText.Text + ",@bPermission=" + bPermission.ToString();
        */
        bool addSuccess;
        DataSet ds = new UserController().addRoleDetail(sRoleCode, sRoleDetailCode, sDesc, bStatus, bPermission, sAddBy);

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
}


