using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using wv.Users;
using System.Data;
using wv.GeneralFunction;
using wv.Reports;
//using Microsoft.Reporting.WebForms;
using ResourceBorrowing.Items;

using System.Configuration;
using ResourceBorrowing.Clients;

namespace ResourceBorrowing.Transaction
{
    public partial class Transaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //txtFilter_WindowsAuthUserID.Text = Session["WindowAuthID"].ToString();

                //odsTransaction.SelectParameters["sWindowsAuthUserID"].DefaultValue = Session["WindowAuthID"].ToString();
                txtFilter_WindowsAuthUserID.Text = Session["WindowAuthID"].ToString();    //J20180605
                txtTranClientFilter_WindowsAuthUserID.Text = Session["WindowAuthID"].ToString();    //J20180605
                // odsTransaction.

                SetupDdlFormStatus();

                //SetAccessRight();

                if (!this.IsPostBack)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[5] { new DataColumn("ItemID"), new DataColumn("ItemNo"), new DataColumn("Description"), new DataColumn("AvailableQty"), new DataColumn("BorrowingQty") });
                    ViewState["TempTransaction"] = dt;
                    this.BindGrid();
                    ViewState["TempTransactionEdit"] = dt;
                    this.BindGridEdit();
                    //ViewState["TempTransactionItemAdd"] = dt;
                    //this.BindGridItemAdd();


                }
            }
            catch { }
        }

        protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)GridViewItems.HeaderRow.FindControl("chkboxSelectAll");
            bool isCheck = ChkBoxHeader.Checked;
            foreach (GridViewRow row in GridViewItems.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkAddItem");
                ChkBoxRows.Checked = isCheck;
                /*
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                */
            }
        }

        protected void chkboxEditSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)GridViewEdit_Items.HeaderRow.FindControl("chkboxEditSelectAll");
            bool isCheck = ChkBoxHeader.Checked;
            foreach (GridViewRow row in GridViewEdit_Items.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkEditItem");
                ChkBoxRows.Checked = isCheck;
            }
        }

        protected void chkboxShowEditSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)gvShow_Selected_Edit.HeaderRow.FindControl("chkboxShowEditSelectAll");
            bool isCheck = ChkBoxHeader.Checked;
            foreach (GridViewRow row in gvShow_Selected_Edit.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkShowEditItem");
                ChkBoxRows.Checked = isCheck;
            }
        }

        /*
                 protected void TranItemFilterTypeSelection(object sender, CommandEventArgs e)
                {
                    switch (e.CommandName.ToString().ToUpper())
                    {
                        case "A": //select all
                            for (int i = 0; i < chkTranItemFilter_Type.Items.Count; i++)
                            {
                                chkTranItemFilter_Type.Items[i].Selected = true;
                            }
                            break;

                        case "U": //unselect all
                            for (int i = 0; i < chkTranItemFilter_Type.Items.Count; i++)
                            {
                                chkTranItemFilter_Type.Items[i].Selected = false;
                            }
                            break;

                        default:
                            break;
                    }
                }
         */

        /*
    protected void AddItemDataBind()
    {
        foreach (GridViewRow row in GridViewItems.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                int iAvailableQty = Int32.Parse(row.Cells[6].Text);
                TextBox tAvailableQty = (TextBox)GridViewItems.Rows[row.RowIndex].FindControl("AvailableQty");

                tAvailableQty.Text = iAvailableQty.ToString();

                if (iAvailableQty == 0)
                {
                    TextBox tBorrowingQty = (TextBox)GridViewItems.Rows[row.RowIndex].FindControl("BorrowingQty");
                    tBorrowingQty.Text = "0";

                    CheckBox cb = (CheckBox)GridViewItems.Rows[row.RowIndex].FindControl("chkAddItem");
                    cb.Enabled = false;

                }
            }
        }
    }
        */

        protected void BindGrid()
        {
            gvSelected.DataSource = (DataTable)ViewState["TempTransaction"];
            gvSelected.DataBind();
        }

        protected void BindGridEdit()
        {
            gvEdit_Selected.DataSource = (DataTable)ViewState["TempTransactionEdit"];
            gvEdit_Selected.DataBind();
        }

        /*HEIDI 20171006
        protected void BindGridItemAdd()
        {
            gvShow_Selected.DataSource = (DataTable)ViewState["TempTransactionItemAdd"];
            gvShow_Selected.DataBind();
        }
        */
        /*
        protected void gvSelected_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "D")
            {
                int index = Int32.Parse(e.CommandArgument.ToString());
                //GridViewRow row = gvSelected.Rows[index];

                
            }
        }
        */

        protected void gvSelected_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ViewState["TempTransaction"] != null)
            {
                DataTable dt = (DataTable)ViewState["TempTransaction"];
                int rowIndex = Convert.ToInt32(e.RowIndex);

                //dt.Rows.RemoveAt(e.RowIndex);
                //gvSelected.DeleteRow(index);
                //dt.Rows.Remove(dt.Rows[rowIndex]);
                dt.Rows[rowIndex].Delete();

                ViewState["TempTransaction"] = dt;
                BindGrid();
                //DeleteItem(Convert.ToInt16(e.CommandArgument));
            }

        }

        protected void gvEdit_Selected_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ViewState["TempTransactionEdit"] != null)
            {
                DataTable dt = (DataTable)ViewState["TempTransactionEdit"];
                int rowIndex = Convert.ToInt32(e.RowIndex);

                //dt.Rows.RemoveAt(e.RowIndex);
                //gvSelected.DeleteRow(index);
                //dt.Rows.Remove(dt.Rows[rowIndex]);
                dt.Rows[rowIndex].Delete();

                ViewState["TempTransactionEdit"] = dt;
                BindGridEdit();
                //DeleteItem(Convert.ToInt16(e.CommandArgument));
            }

        }
        /*
        protected void AddTran_RemoveItem_OnCommand(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["TempTransaction"];
            dt.Rows.RemoveAt(e.RowIndex);
            //int index = Int32.Parse(e.CommandArgument.ToString());
            //gvSelected.DeleteRow(index);

            ViewState["TempTransaction"] = gvSelected.DataSource;
            this.BindGrid();
        }
        */
        /*
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemID", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("AvailableQty", typeof(string)));
            dt.Columns.Add(new DataColumn("BorrowingQty", typeof(string)));
            dr = dt.NewRow();
            
            dr["RowNumber"] = 1;
            dr["ItemID"] = string.Empty;
            dr["ItemNo"] = string.Empty;
            dr["Description"] = string.Empty;
            dr["AvailableQty"] = string.Empty;
            dr["BorrowingQty"] = string.Empty;
            dt.Rows.Add(dr);
            //dr = dt.NewRow();
            

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;

            gvSelected.DataSource = dt;
            gvSelected.DataBind();
        }
    */

        protected void FilterTranStatusSelection(object sender, CommandEventArgs e)
        {
            switch (e.CommandName.ToString().ToUpper())
            {
                case "A": //select all
                    for (int i = 0; i < checkBoxList_TranStatus.Items.Count; i++)
                    {
                        checkBoxList_TranStatus.Items[i].Selected = true;
                    }
                    break;

                case "U": //unselect all
                    for (int i = 0; i < checkBoxList_TranStatus.Items.Count; i++)
                    {
                        checkBoxList_TranStatus.Items[i].Selected = false;
                    }
                    break;

                default:
                    break;
            }
        }

        protected void Filterbtn_ToBorrowDateFrom_Click(object sender, EventArgs e)
        {
            if (Filterbtn_ToBorrowDateFrom.Text == "[Pick Date]")
            {
                CalendarFilter_ToBorrowDateFrom.Visible = true;
                Filterbtn_ToBorrowDateFrom.Text = "[Pick Date (Cancel)]";
            }
            else
            {
                CalendarFilter_ToBorrowDateFrom.Visible = false;
                Filterbtn_ToBorrowDateFrom.Text = "[Pick Date]";
            }
        }

        protected void btnAdd_BorrowDateFrom_Click(object sender, EventArgs e)
        {
            if (btnAdd_BorrowDateFrom.Text == "[Pick Date]")
            {
                CalendarAdd_BorrowDateFrom.Visible = true;
                btnAdd_BorrowDateFrom.Text = "[Pick Date (Cancel)]";
            }
            else
            {
                CalendarAdd_BorrowDateFrom.Visible = false;
                btnAdd_BorrowDateFrom.Text = "[Pick Date]";
            }
        }

        protected void btnEdit_BorrowDateFrom_Click(object sender, EventArgs e)
        {
            if (btnEdit_BorrowDateFrom.Text == "[Pick Date]")
            {
                CalendarEdit_BorrowDateFrom.Visible = true;
                btnEdit_BorrowDateFrom.Text = "[Pick Date (Cancel)]";
            }
            else
            {
                CalendarEdit_BorrowDateFrom.Visible = false;
                btnEdit_BorrowDateFrom.Text = "[Pick Date]";
            }
        }

        protected void btnAdd_BorrowDateTo_Click(object sender, EventArgs e)
        {
            if (btnAdd_BorrowDateTo.Text == "[Pick Date]")
            {
                CalendarAdd_BorrowDateTo.Visible = true;
                btnAdd_BorrowDateTo.Text = "[Pick Date (Cancel)]";
            }
            else
            {
                CalendarAdd_BorrowDateTo.Visible = false;
                btnAdd_BorrowDateTo.Text = "[Pick Date]";
            }
        }

        protected void btnEdit_BorrowDateTo_Click(object sender, EventArgs e)
        {
            if (btnEdit_BorrowDateTo.Text == "[Pick Date]")
            {
                CalendarEdit_BorrowDateTo.Visible = true;
                btnEdit_BorrowDateTo.Text = "[Pick Date (Cancel)]";
            }
            else
            {
                CalendarEdit_BorrowDateTo.Visible = false;
                btnEdit_BorrowDateTo.Text = "[Pick Date]";
            }
        }

        protected void btnEdit_ActionE_BorrowDateFrom_Click(object sender, EventArgs e)
        {
            if (btnEdit_ActionE_BorrowDateFrom.Text == "[Pick Date]")
            {
                CalendarEdit_ActionE_BorrowDateFrom.Visible = true;
                btnEdit_ActionE_BorrowDateFrom.Text = "[Pick Date (Cancel)]";
            }
            else
            {
                CalendarEdit_ActionE_BorrowDateFrom.Visible = false;
                btnEdit_ActionE_BorrowDateFrom.Text = "[Pick Date]";
            }
        }

        protected void btnEdit_ActionE_BorrowDateTo_Click(object sender, EventArgs e)
        {
            if (btnEdit_ActionE_BorrowDateTo.Text == "[Pick Date]")
            {
                CalendarEdit_ActionE_BorrowDateTo.Visible = true;
                btnEdit_ActionE_BorrowDateTo.Text = "[Pick Date (Cancel)]";
            }
            else
            {
                CalendarEdit_ActionE_BorrowDateTo.Visible = false;
                btnEdit_ActionE_BorrowDateTo.Text = "[Pick Date]";
            }
        }

        protected void btnEdit_ActionN_BorrowDateTo_Click(object sender, EventArgs e)
        {
            if (btnEdit_ActionN_BorrowDateTo.Text == "[Pick Date]")
            {
                CalendarEdit_ActionN_BorrowDateTo.Visible = true;
                btnEdit_ActionN_BorrowDateTo.Text = "[Pick Date (Cancel)]";
            }
            else
            {
                CalendarEdit_ActionN_BorrowDateTo.Visible = false;
                btnEdit_ActionN_BorrowDateTo.Text = "[Pick Date]";
            }
        }

        protected void btnEdit_ActionR_ReturnDate_Click(object sender, EventArgs e)
        {
            if (btnEdit_ActionR_ReturnDate.Text == "[Pick Date]")
            {
                CalendarEdit_ActionR_ReturnDate.Visible = true;
                btnEdit_ActionR_ReturnDate.Text = "[Pick Date (Cancel)]";
            }
            else
            {
                CalendarEdit_ActionR_ReturnDate.Visible = false;
                btnEdit_ActionR_ReturnDate.Text = "[Pick Date]";
            }
        }

        protected void btnEdit_ActionB_BorrowDateFrom_Click(object sender, EventArgs e)
        {
            if (btnEdit_ActionB_BorrowDateFrom.Text == "[Pick Date]")
            {
                CalendarEdit_ActionB_BorrowDateFrom.Visible = true;
                btnEdit_ActionB_BorrowDateFrom.Text = "[Pick Date (Cancel)]";
            }
            else
            {
                CalendarEdit_ActionB_BorrowDateFrom.Visible = false;
                btnEdit_ActionB_BorrowDateFrom.Text = "[Pick Date]";
            }
        }
        /*
        protected void btnEdit_ActionB_BorrowDateTo_Click(object sender, EventArgs e)
        {
            if (btnEdit_ActionB_BorrowDateTo.Text == "[Pick Date]")
            {
                CalendarEdit_ActionB_BorrowDateTo.Visible = true;
                btnEdit_ActionB_BorrowDateTo.Text = "[Pick Date (Cancel)]";
            }
            else
            {
                CalendarEdit_ActionB_BorrowDateTo.Visible = false;
                btnEdit_ActionB_BorrowDateTo.Text = "[Pick Date]";
            }
        }
        */

        protected void CalendarFilter_ToBorrowDateFrom_SelectionChanged(object sender, EventArgs e)
        {
            txtFilter_ToBorrowDateFrom.Text = CalendarFilter_ToBorrowDateFrom.SelectedDate.ToString("yyyy/MM/dd");
            CalendarFilter_ToBorrowDateFrom.Visible = false;
            Filterbtn_ToBorrowDateFrom.Text = "[Pick Date]";
            CalendarFilter_ToBorrowDateFrom.SelectedDate = Convert.ToDateTime("1900/01/01");
        }

        protected void CalendarAdd_BorrowDateFrom_SelectionChanged(object sender, EventArgs e)
        {
            txtAdd_BorrowDateFrom.Text = CalendarAdd_BorrowDateFrom.SelectedDate.ToString("yyyy/MM/dd");
            CalendarAdd_BorrowDateFrom.Visible = false;
            btnAdd_BorrowDateFrom.Text = "[Pick Date]";
            CalendarAdd_BorrowDateFrom.SelectedDate = Convert.ToDateTime("1900/01/01");
        }

        protected void CalendarEdit_BorrowDateFrom_SelectionChanged(object sender, EventArgs e)
        {
            txtEdit_TranItemFilter_RequestDateFrom.Text = CalendarEdit_BorrowDateFrom.SelectedDate.ToString("yyyy/MM/dd");
            CalendarEdit_BorrowDateFrom.Visible = false;
            btnEdit_BorrowDateFrom.Text = "[Pick Date]";
            CalendarEdit_BorrowDateFrom.SelectedDate = Convert.ToDateTime("1900/01/01");
        }

        protected void CalendarAdd_BorrowDateTo_SelectionChanged(object sender, EventArgs e)
        {
            txtAdd_BorrowDateTo.Text = CalendarAdd_BorrowDateTo.SelectedDate.ToString("yyyy/MM/dd");
            CalendarAdd_BorrowDateTo.Visible = false;
            btnAdd_BorrowDateTo.Text = "[Pick Date]";
            CalendarAdd_BorrowDateTo.SelectedDate = Convert.ToDateTime("1900/01/01");
        }

        protected void CalendarEdit_BorrowDateTo_SelectionChanged(object sender, EventArgs e)
        {
            txtEdit_TranItemFilter_RequestDateTo.Text = CalendarEdit_BorrowDateTo.SelectedDate.ToString("yyyy/MM/dd");
            CalendarEdit_BorrowDateTo.Visible = false;
            btnEdit_BorrowDateTo.Text = "[Pick Date]";
            CalendarEdit_BorrowDateTo.SelectedDate = Convert.ToDateTime("1900/01/01");
        }

        protected void CalendarEdit_ActionE_BorrowDateFrom_SelectionChanged(object sender, EventArgs e)
        {
            txtEdit_ActionE_BorrowDateFrom.Text = CalendarEdit_ActionE_BorrowDateFrom.SelectedDate.ToString("yyyy/MM/dd");
            CalendarEdit_ActionE_BorrowDateFrom.Visible = false;
            btnEdit_ActionE_BorrowDateFrom.Text = "[Pick Date]";
            CalendarEdit_ActionE_BorrowDateFrom.SelectedDate = Convert.ToDateTime("1900/01/01");
        }

        protected void CalendarEdit_ActionE_BorrowDateTo_SelectionChanged(object sender, EventArgs e)
        {
            txtEdit_ActionE_BorrowDateTo.Text = CalendarEdit_ActionE_BorrowDateTo.SelectedDate.ToString("yyyy/MM/dd");
            CalendarEdit_ActionE_BorrowDateTo.Visible = false;
            btnEdit_ActionE_BorrowDateTo.Text = "[Pick Date]";
            CalendarEdit_ActionE_BorrowDateTo.SelectedDate = Convert.ToDateTime("1900/01/01");
        }

        protected void CalendarEdit_ActionN_BorrowDateTo_SelectionChanged(object sender, EventArgs e)
        {
            txtEdit_ActionN_BorrowDateTo.Text = CalendarEdit_ActionN_BorrowDateTo.SelectedDate.ToString("yyyy/MM/dd");
            CalendarEdit_ActionN_BorrowDateTo.Visible = false;
            btnEdit_ActionN_BorrowDateTo.Text = "[Pick Date]";
            CalendarEdit_ActionN_BorrowDateTo.SelectedDate = Convert.ToDateTime("1900/01/01");
        }


        protected void CalendarEdit_ActionB_BorrowDateFrom_SelectionChanged(object sender, EventArgs e)
        {
            txtEdit_ActionB_BorrowDateFrom.Text = CalendarEdit_ActionB_BorrowDateFrom.SelectedDate.ToString("yyyy/MM/dd");
            CalendarEdit_ActionB_BorrowDateFrom.Visible = false;
            btnEdit_ActionB_BorrowDateFrom.Text = "[Pick Date]";
            CalendarEdit_ActionB_BorrowDateFrom.SelectedDate = Convert.ToDateTime("1900/01/01");
        }
        protected void CalendarEdit_ActionR_ReturnDate_SelectionChanged(object sender, EventArgs e)
        {
            txtEdit_ActionR_ReturnDate.Text = CalendarEdit_ActionR_ReturnDate.SelectedDate.ToString("yyyy/MM/dd");
            CalendarEdit_ActionR_ReturnDate.Visible = false;
            btnEdit_ActionR_ReturnDate.Text = "[Pick Date]";
            CalendarEdit_ActionR_ReturnDate.SelectedDate = Convert.ToDateTime("1900/01/01");
        }


        /*
        protected void Filterbtn_ToBorrowDateTo_Click(object sender, EventArgs e)
        {
            if (Filterbtn_ToBorrowDateTo.Text == "[Pick Date]")
            {
                CalendarFilter_ToBorrowDateTo.Visible = true;
                Filterbtn_ToBorrowDateTo.Text = "[Pick Date (Cancel)]";
            }
            else
            {
                CalendarFilter_ToBorrowDateTo.Visible = false;
                Filterbtn_ToBorrowDateTo.Text = "[Pick Date]";
            }
        }

        protected void CalendarFilter_ToBorrowDateTo_SelectionChanged(object sender, EventArgs e)
        {
            txtFilter_ToBorrowDateTo.Text = CalendarFilter_ToBorrowDateTo.SelectedDate.ToString("yyyy/MM/dd");
            CalendarFilter_ToBorrowDateTo.Visible = false;
            Filterbtn_ToBorrowDateTo.Text = "[Pick Date]";
            CalendarFilter_ToBorrowDateTo.SelectedDate = Convert.ToDateTime("1900/01/01");
        }
        */

        protected void Filterbtn_ToReturnDateFrom_Click(object sender, EventArgs e)
        {
            if (Filterbtn_ToReturnDateFrom.Text == "[Pick Date]")
            {
                CalendarFilter_ToReturnDateFrom.Visible = true;
                Filterbtn_ToReturnDateFrom.Text = "[Pick Date (Cancel)]";
            }
            else
            {
                CalendarFilter_ToReturnDateFrom.Visible = false;
                Filterbtn_ToReturnDateFrom.Text = "[Pick Date]";
            }
        }

        protected void CalendarFilter_ToReturnDateFrom_SelectionChanged(object sender, EventArgs e)
        {
            txtFilter_ToReturnDateFrom.Text = CalendarFilter_ToReturnDateFrom.SelectedDate.ToString("yyyy/MM/dd");
            CalendarFilter_ToReturnDateFrom.Visible = false;
            Filterbtn_ToReturnDateFrom.Text = "[Pick Date]";
            CalendarFilter_ToReturnDateFrom.SelectedDate = Convert.ToDateTime("1900/01/01");
        }

        /*
        protected void Filterbtn_ToReturnDateTo_Click(object sender, EventArgs e)
        {
            if (Filterbtn_ToReturnDateTo.Text == "[Pick Date]")
            {
                CalendarFilter_ToReturnDateTo.Visible = true;
                Filterbtn_ToReturnDateTo.Text = "[Pick Date (Cancel)]";
            }
            else
            {
                CalendarFilter_ToReturnDateTo.Visible = false;
                Filterbtn_ToBorrowDateFrom.Text = "[Pick Date]";
            }
        }

        protected void CalendarFilter_ToReturnDateTo_SelectionChanged(object sender, EventArgs e)
        {
            txtFilter_ToReturnDateTo.Text = CalendarFilter_ToReturnDateTo.SelectedDate.ToString("yyyy/MM/dd");
            CalendarFilter_ToReturnDateTo.Visible = false;
            Filterbtn_ToReturnDateTo.Text = "[Pick Date]";
            CalendarFilter_ToReturnDateTo.SelectedDate = Convert.ToDateTime("1900/01/01");
        }
        */

        protected void FilterBtn_Click(object sender, EventArgs e)
        {

            try
            {
                txtFilter_TranStatus.Text = "";
                foreach (ListItem item in checkBoxList_TranStatus.Items)
                {
                    if (item.Selected)
                    {
                        txtFilter_TranStatus.Text += item.Value + ',';
                    }
                }

                txtFilter_Overdue.Text = "";
                foreach (ListItem item in checkBoxList_Overdue.Items)
                {
                    if (item.Selected)
                    {
                        txtFilter_Overdue.Text += item.Value + ',';
                    }
                }


                odsTransaction.Update();


            }
            catch { }
        }

        /*
        //Transaction - Item List
        protected void TranFilter_ItemBtn_Click(object sender, EventArgs e)
        {
            try
            {
                txtTranFilter_Item_ItemTypeID.Text = "";
                foreach (ListItem item in checkBoxList_Type.Items)
                {
                    if (item.Selected)
                    {
                        txtTranFilter_Item_ItemTypeID.Text += item.Value + ',';
                    }
                }

                txtTranFilter_Item_ItemLangID.Text = "";
                foreach (ListItem item in checkBoxList_Lang.Items)
                {
                    if (item.Selected)
                    {
                        txtTranFilter_Item_ItemLangID.Text += item.Value + ',';
                    }
                }

                txtTranFilter_Item_NeedApproval.Text = "";
                foreach (ListItem item in checkBoxList_NeedApproval.Items)
                {
                    if (item.Selected)
                    {
                        txtTranFilter_Item_NeedApproval.Text += item.Value + ',';
                    }
                }

                txtTranFilter_Item_GlobalUse.Text = "";
                foreach (ListItem item in checkBoxList_GlobalUse.Items)
                {
                    if (item.Selected)
                    {
                        txtTranFilter_Item_GlobalUse.Text += item.Value + ',';
                    }
                }

                txtTranFilter_Item_Status.Text = "";
                foreach (ListItem item in checkBoxList_Status.Items)
                {
                    if (item.Selected)
                    {
                        txtTranFilter_Item_Status.Text += item.Value + ',';
                    }
                }
                //odsEPOForms.ID(
                //if (txtFilter_Status.Text == "")
                //    txtFilter_Status.Text = " ";

                odsItems.Update();


            }
            catch { }

        }
*/

        protected void FilterClearBtn_Click(object sender, EventArgs e)
        {

            try
            {
                txtFilter_RefNo.Text = "";

                for (int i = 0; i < checkBoxList_TranStatus.Items.Count; i++)
                {
                    //checkBoxList_TranStatus.Items[i].Selected = true;
                    checkBoxList_TranStatus.Items[i].Selected = false;
                }
                txtFilter_TranStatus.Text = "";

                for (int i = 0; i < checkBoxList_Overdue.Items.Count; i++)
                {
                    //checkBoxList_Overdue.Items[i].Selected = true;
                    checkBoxList_Overdue.Items[i].Selected = false;
                }
                txtFilter_Overdue.Text = "";

                txtFilter_ToBorrowDateFrom.Text = "";
                //txtFilter_ToBorrowDateTo.Text = "";
                txtFilter_ToReturnDateFrom.Text = "";
                //txtFilter_ToReturnDateTo.Text = "";

                txtFilter_ItemCode.Text = "";
                txtFilter_Description.Text = "";

                txtFilter_ClientName.Text = "";
                txtFilter_ContactPersonName.Text = "";
                txtFilter_PartnerID.Text = "";

                ddlFilter_InternalUserDept.SelectedIndex = 0;
                ddlFilter_InternalUserName.SelectedIndex = 0;
                ddlFilter_InternalUserName.Items.Clear();
                //txtFilter_InternalUser.Text = "";



                /*
                                SetAccessRight();

                                if (Session["IsAdmin"] != null)
                                {
                                    if (Session["IsAdmin"].ToString() == "Yes")
                                    {
                                        ddlFilter_InternalUserDept.SelectedIndex = 0;
                                    }


                                if (Session["RoleCode"].ToString() != null)
                                {
                                    if (Session["RoleCode"].ToString() == "IT_Appn")
                                    {
                                        ddlFilter_InternalUserDept.SelectedIndex = 0;
                                    }
                                }
                                */

                txtFilter_TranStatus.Text = "0";
                //odsTransaction.Update();
                //}
            }
            catch { }

        }



        protected void PageTransactionIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewTransaction.PageIndex = e.NewPageIndex;
            GridViewTransaction.DataBind();

        }


        protected void SetupDdlFormStatus()
        {
            TransactionController tc = new TransactionController();
            UserController uc = new UserController();
            ItemController ic = new ItemController();
            DataSet ds;




            //Filter
            if (ddlFilter_InternalUserDept.SelectedValue == "")
            {
                ds = uc.getDepartments();

                ddlFilter_InternalUserDept.DataSource = ds;
                ddlFilter_InternalUserDept.DataTextField = "Description";
                ddlFilter_InternalUserDept.DataValueField = "Code";
                ddlFilter_InternalUserDept.DataBind();

                ddlFilter_InternalUserDept.Items.Insert(0, new ListItem("All", String.Empty));
                ddlFilter_InternalUserDept.SelectedIndex = 0;
            }

            if (checkBoxList_TranStatus.Items.Count <= 0)
            {
                ds = tc.getTransactionStatuses();

                checkBoxList_TranStatus.DataSource = ds;
                checkBoxList_TranStatus.DataTextField = "Description";
                checkBoxList_TranStatus.DataValueField = "Value";
                checkBoxList_TranStatus.DataBind();

                for (int i = 0; i < checkBoxList_TranStatus.Items.Count; i++)
                {
                    //checkBoxList_TranStatus.Items[i].Selected = true;
                    checkBoxList_TranStatus.Items[i].Selected = false;
                }
                txtFilter_TranStatus.Text = "";
            }
            /*
            for (int i = 0; i < checkBoxList_Overdue.Items.Count; i++)
            {
                //checkBoxList_Overdue.Items[i].Selected = true;
                checkBoxList_Overdue.Items[i].Selected = false;
            }
            txtFilter_Overdue.Text = "";
            */
            /*//Create Transaction
            if (ddlAdd_InternalUserDept.SelectedValue == "")
            {
                ds = uc.getDepartments();

                ddlAdd_InternalUserDept.DataSource = ds;
                ddlAdd_InternalUserDept.DataTextField = "Description";
                ddlAdd_InternalUserDept.DataValueField = "Code";
                ddlAdd_InternalUserDept.DataBind();

                ddlAdd_InternalUserDept.Items.Insert(0, new ListItem("All", String.Empty));

            }
            */

        }

        protected void SetupDdlFormStatus_CreateTran()
        {
            UserController uc = new UserController();

            //Create Transaction
            if (ddlAdd_InternalUserDept.SelectedValue == "")
            {
                DataSet ds; ds = uc.getDepartments();

                ddlAdd_InternalUserDept.DataSource = ds;
                ddlAdd_InternalUserDept.DataTextField = "Description";
                ddlAdd_InternalUserDept.DataValueField = "Code";
                ddlAdd_InternalUserDept.DataBind();



                ddlAdd_InternalUserDept.Items.Insert(0, new ListItem("--Select--", String.Empty));
                ddlAdd_InternalUserName.Items.Insert(0, new ListItem("--Select--", String.Empty));


                //ddlAdd_InternalUserDept.Items.Insert(0, new ListItem("All", String.Empty));
            }

            ////Default: Client
            //ddlAdd_InternalUserDept.SelectedIndex = 0;
            //ddlAdd_InternalUserName.SelectedIndex = 0;

            //ddlAdd_ContactPersonName.Items.Insert(0, new ListItem("--Select--", String.Empty));
            //ddlAdd_ContactPersonName.SelectedIndex = 0;

            //CompareValidatorAdd_InternalUserDept.Enabled = false;
            //CompareValidatorAdd_InternalUserName.Enabled = false;
            //CompareValidatorAdd_ContactPersonName.Enabled = true;
        }

        //For Client Type Filter

        protected void SetupDdlFormStatus_SelectClient()
        {

            Clients.ClientController cc = new Clients.ClientController();
            DataSet dsClientType;

            if (ddlTranClientFilter_ClientType.SelectedValue == "")
            {
                dsClientType = cc.GetClientType(false, false);

                ddlTranClientFilter_ClientType.DataSource = dsClientType;
                ddlTranClientFilter_ClientType.DataTextField = "Description";
                ddlTranClientFilter_ClientType.DataValueField = "ID";
                ddlTranClientFilter_ClientType.DataBind();

                ddlTranClientFilter_ClientType.Items.Insert(0, new ListItem("All", String.Empty));

            }
        }

        //J20180605
        protected void SetupDdlFormStatus_SelectContactPerson(int iClientID)
        {
            DataSet cp;
            TransactionController tc = new TransactionController();

            cp = tc.getContactPerson(iClientID);

            ddlAdd_ContactPersonName.DataSource = cp;
            ddlAdd_ContactPersonName.DataTextField = "Contact Person";
            ddlAdd_ContactPersonName.DataValueField = "ID";
            ddlAdd_ContactPersonName.DataBind();

            //ddlAdd_ContactPersonName.Items.Insert(0, new ListItem("test", "0"));
            lblAdd_ContactPersonNameValue.Text = ddlAdd_ContactPersonName.SelectedValue;

            txtAddNew_ContactPersonNameValue.Text = "";
        }

        /*
        protected void SetupDdlFormStatus_EditClient()
        {

            Clients.ClientController cc = new Clients.ClientController();
            DataSet dsClientType;

            if (ddlEdit_TranClientFilter_ClientType.SelectedValue == "")
            {
                dsClientType = cc.GetClientType(false, false);

                ddlEdit_TranClientFilter_ClientType.DataSource = dsClientType;
                ddlEdit_TranClientFilter_ClientType.DataTextField = "Description";
                ddlEdit_TranClientFilter_ClientType.DataValueField = "ID";
                ddlEdit_TranClientFilter_ClientType.DataBind();

                ddlEdit_TranClientFilter_ClientType.Items.Insert(0, new ListItem("All", String.Empty));

            }
        }
        */

        protected void SetupDdlFormStatus_AddItem()
        {

            UserController uc = new UserController();
            ItemController ic = new ItemController();
            DataSet ds;

            if (chkTranItemFilter_Type.Items.Count <= 0)
            {
                ds = ic.getItemTypes(0, false, false);

                chkTranItemFilter_Type.DataSource = ds;
                chkTranItemFilter_Type.DataTextField = "Description";
                chkTranItemFilter_Type.DataValueField = "ID";
                chkTranItemFilter_Type.DataBind();

                for (int i = 0; i < chkTranItemFilter_Type.Items.Count; i++)
                {
                    //chkTranItemFilter_Type.Items[i].Selected = true;
                    chkTranItemFilter_Type.Items[i].Selected = false;
                }
            }

            if (chkTranItemFilter_Lang.Items.Count <= 0)
            {
                ds = ic.getItemLanguages(0, false);

                chkTranItemFilter_Lang.DataSource = ds;
                chkTranItemFilter_Lang.DataTextField = "Description";
                chkTranItemFilter_Lang.DataValueField = "ID";
                chkTranItemFilter_Lang.DataBind();

                for (int i = 0; i < chkTranItemFilter_Lang.Items.Count; i++)
                {
                    //chkTranItemFilter_Lang.Items[i].Selected = true;
                    chkTranItemFilter_Lang.Items[i].Selected = false;
                }
            }

            if (ddlTranItemFilter_Dept.SelectedValue == "")
            {
                ds = uc.getDepartments();

                ddlTranItemFilter_Dept.DataSource = ds;
                ddlTranItemFilter_Dept.DataTextField = "Description";
                ddlTranItemFilter_Dept.DataValueField = "Code";
                ddlTranItemFilter_Dept.DataBind();

                ddlTranItemFilter_Dept.Items.Insert(0, new ListItem("All", String.Empty));
                ddlTranItemFilter_Dept.SelectedIndex = 0;
            }
        }

        protected void SetupDdlFormStatus_EditItem()
        {

            UserController uc = new UserController();
            ItemController ic = new ItemController();
            DataSet ds, dsDept;

            if (ddlEdit_TranItemFilter_Dept.SelectedValue == "")
            {
                dsDept = uc.getDepartments();

                ddlEdit_TranItemFilter_Dept.DataSource = dsDept;
                ddlEdit_TranItemFilter_Dept.DataTextField = "Description";
                ddlEdit_TranItemFilter_Dept.DataValueField = "Code";
                ddlEdit_TranItemFilter_Dept.DataBind();

                ddlEdit_TranItemFilter_Dept.Items.Insert(0, new ListItem("All", String.Empty));

            }

            if (chkEdit_TranItemFilter_Type.Items.Count <= 0)
            {
                ds = ic.getItemTypes(0, false, false);

                chkEdit_TranItemFilter_Type.DataSource = ds;
                chkEdit_TranItemFilter_Type.DataTextField = "Description";
                chkEdit_TranItemFilter_Type.DataValueField = "ID";
                chkEdit_TranItemFilter_Type.DataBind();

                for (int i = 0; i < chkEdit_TranItemFilter_Type.Items.Count; i++)
                {
                    //chkEdit_TranItemFilter_Type.Items[i].Selected = true;
                    chkEdit_TranItemFilter_Type.Items[i].Selected = false;
                }
            }

            if (chkEdit_TranItemFilter_Lang.Items.Count <= 0)
            {
                ds = ic.getItemLanguages(0, false);

                chkEdit_TranItemFilter_Lang.DataSource = ds;
                chkEdit_TranItemFilter_Lang.DataTextField = "Description";
                chkEdit_TranItemFilter_Lang.DataValueField = "ID";
                chkEdit_TranItemFilter_Lang.DataBind();

                for (int i = 0; i < chkEdit_TranItemFilter_Lang.Items.Count; i++)
                {
                    //chkEdit_TranItemFilter_Lang.Items[i].Selected = true;
                    chkEdit_TranItemFilter_Lang.Items[i].Selected = false;
                }
            }

            //Setting default value
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now.AddDays(7);

            txtEdit_TranItemFilter_RequestDateFrom.Text = dtStart.ToString("yyyy/MM/dd");
            txtEdit_TranItemFilter_RequestDateTo.Text = dtEnd.ToString("yyyy/MM/dd");

            /*
            if (ddlEdit_TranItemFilter_Dept.SelectedValue == "")
            {
                ds = uc.getDepartments();

                ddlEdit_TranItemFilter_Dept.DataSource = ds;
                ddlEdit_TranItemFilter_Dept.DataTextField = "Description";
                ddlEdit_TranItemFilter_Dept.DataValueField = "Code";
                ddlEdit_TranItemFilter_Dept.DataBind();

                ddlEdit_TranItemFilter_Dept.Items.Insert(0, new ListItem("All", String.Empty));

            }*/
        }

        protected void SetAccessRight()
        {
            Boolean bEnable = true;
            //Boolean bEnable = false;
            //Boolean bEnableFin = false;
            /*
            if (Session["IsAdmin"] != null)
            {
                if (Session["IsAdmin"].ToString() == "Yes")
                {
                    bEnable = true;
                }
                else
                {
                    bEnable = false;
                }
            }
            else if (Session["RoleCode"].ToString() != null)
            {
                if (Session["RoleCode"].ToString() == "Fin_Admin" || Session["RoleCode"].ToString() == "IT_Appn" || Session["RoleCode"].ToString() == "Int_Aduitor" || Session["Role"].ToString() == "COO" || Session["RoleCode"].ToString() == "CEO")
                {
                    bEnable = true;
                }
                else
                {
                    bEnable = false;
                }
                
                if (Session["RoleCode"].ToString() == "Fin_Admin")
                {
                    bEnableFin = true;
                }
                
            }
            else
            {
                bEnable = false;
            }
    */
            lblFilter_InternalUserDept.Visible = bEnable;
            ddlFilter_InternalUserDept.Visible = bEnable;


            if (!bEnable)
                ddlFilter_InternalUserDept.SelectedIndex = ddlFilter_InternalUserDept.Items.IndexOf(ddlFilter_InternalUserDept.Items.FindByValue(Convert.ToString(Session["DeptCode"])));
        }

        protected void ddlFilter_InternalUserDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsUser;
            UserController uc = new UserController();

            string sSelectedDept = "";

            sSelectedDept = ddlFilter_InternalUserDept.SelectedValue;

            if (sSelectedDept != "")
            {
                dsUser = uc.getUsers("", "", sSelectedDept, 1);
                //dsUser = uc.getUsers("", "", sSelectedDept, 2);

                ddlFilter_InternalUserName.DataSource = dsUser;
                ddlFilter_InternalUserName.DataTextField = "UserName";
                ddlFilter_InternalUserName.DataValueField = "ID";
                ddlFilter_InternalUserName.DataBind();

                ddlFilter_InternalUserName.Items.Insert(0, new ListItem("", String.Empty));
            }
        }

        protected void ddlAdd_TranType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iTranType;

            iTranType = Int32.Parse(ddlTranType_Add.SelectedValue);
            lblTranType_AddValue.Text = iTranType.ToString();

            if (iTranType == 2)//Internal User
            {
                mvBorrowingUser.ActiveViewIndex = 1;

                //CompareValidatorAdd_ContactPersonName.Enabled = false;
                //CompareValidatorAdd_InternalUserDept.Enabled = true;
                //CompareValidatorAdd_InternalUserName.Enabled = true;

                txtAdd_ClientNameDescValue.Enabled = false;
                ddlAdd_ContactPersonName.Enabled = false;
                txtAdd_ClientNameDescValue.Visible = false;
                ddlAdd_ContactPersonName.Visible = false;
                //txtAdd_Client.Visible = false;
                TranSelectClientBtn.Visible = false;
                lblAdd_ClientName.Visible = false;
                lblAdd_ContactPersonName.Visible = false;

                txtAdd_ClientNameDescValue.Text = "";

                //20180605
                txtAddNew_ContactPersonNameValue.Text = "";
                lblAddNew_ContactPersonNameValue.Text = "";


                //ddlAdd_InternalUserDept.Items.Insert(0, new ListItem("--Select--", String.Empty));
                //ddlAdd_InternalUserName.Items.Insert(0, new ListItem("--Select--", String.Empty));
                //ddlAdd_InternalUserDept.SelectedIndex = 0;
                //ddlAdd_InternalUserName.SelectedIndex = 0;

                //ddlAdd_ContactPersonName.SelectedIndex = 0;

                ddlAdd_InternalUserDept.Enabled = true;
                ddlAdd_InternalUserName.Enabled = true;
                ddlAdd_InternalUserDept.Visible = true;
                ddlAdd_InternalUserName.Visible = true;
                //txtAdd_InternalUser.Visible = true;
                lblAdd_InternalUserDept.Visible = true;
                lblAdd_InternalUserName.Visible = true;

                ddlAdd_InternalUserDept.SelectedValue = "PE";
                lblAdd_InternalUserDeptValue.Text = ddlAdd_InternalUserDept.SelectedValue;

                //Default value: PE
                UserController uc = new UserController();
                DataSet dsUser = uc.getUsers("", "", "PE", 1);

                ddlAdd_InternalUserName.DataSource = dsUser;
                ddlAdd_InternalUserName.DataTextField = "UserName";
                ddlAdd_InternalUserName.DataValueField = "ID";
                ddlAdd_InternalUserName.DataBind();

                ddlAdd_InternalUserName.Items.Insert(0, new ListItem("--Select--", String.Empty));




            }
            else //Client
            {
                mvBorrowingUser.ActiveViewIndex = 0;

                //CompareValidatorAdd_InternalUserDept.Enabled = false;
                //CompareValidatorAdd_InternalUserName.Enabled = false;
                //CompareValidatorAdd_ContactPersonName.Enabled = true;

                ddlAdd_InternalUserDept.Enabled = false;
                ddlAdd_InternalUserName.Enabled = false;
                ddlAdd_InternalUserDept.Visible = false;
                ddlAdd_InternalUserName.Visible = false;

                ddlAdd_ContactPersonName.Items.Insert(0, new ListItem("--Select--", String.Empty));
                ddlAdd_ContactPersonName.SelectedIndex = 0;

                //ddlAdd_InternalUserDept.SelectedIndex = 0;
                //ddlAdd_InternalUserName.SelectedIndex = 0;


                txtAdd_ClientNameDescValue.Enabled = true;
                ddlAdd_ContactPersonName.Enabled = true;
                txtAdd_ClientNameDescValue.Visible = true;
                ddlAdd_ContactPersonName.Visible = true;
                lblAdd_ClientName.Visible = true;
                TranSelectClientBtn.Visible = true;
                lblAdd_ContactPersonName.Visible = true;

                //20180605
                //txtAddNew_ContactPersonNameValue.Text = "";
                //lblAddNew_ContactPersonNameValue.Text = "";
            }
        }


        /*
        protected void ddlEdit_TranType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iTranType;

            iTranType = Int32.Parse(ddlEdit_TranType.SelectedValue);
            lblEdit_TranTypeValue.Text = iTranType.ToString();

            if (iTranType == 1)//Internal User
            {
                txtEdit_ClientNameDesc.Enabled = false;
                ddlEdit_ContactPersonName.Enabled = false;

                txtEdit_ClientNameDesc.Text = "";
                ddlEdit_ContactPersonName.Items.Insert(0, new ListItem("", String.Empty));
                ddlEdit_ContactPersonName.SelectedIndex = 0;

                ddlEdit_InternalUserDept.Enabled = true;
                ddlEdit_InternalUserName.Enabled = true;
            }
            else //Client
            {
                ddlEdit_InternalUserDept.Enabled = false;
                ddlEdit_InternalUserName.Enabled = false;

                ddlEdit_InternalUserDept.Items.Insert(0, new ListItem("", String.Empty));
                ddlEdit_InternalUserName.Items.Insert(0, new ListItem("", String.Empty));
                ddlEdit_InternalUserDept.SelectedIndex = 0;
                ddlEdit_InternalUserName.SelectedIndex = 0;

                txtEdit_ClientNameDesc.Enabled = true;
                ddlEdit_ContactPersonName.Enabled = true;
            }
        }
        */

        protected void CreateTranBtn_Click(object sender, EventArgs e)
        {
            try
            {

                txtTranItemFilter_WindowsAuthUserID.Text = Session["WindowAuthID"].ToString();    //J20180605
                ClearAddFields();
                SetupDdlFormStatus_CreateTran();

                DateTime dtStart = DateTime.Now;
                DateTime dtEnd = DateTime.Now.AddDays(7);
                //                string sDateStart = dtStart.ToShortDateString();
                //                string sDateEnd = dtEnd.ToShortDateString();

                txtAdd_BorrowDateFrom.Text = dtStart.ToString("yyyy/MM/dd"); ;
                txtAdd_BorrowDateTo.Text = dtEnd.ToString("yyyy/MM/dd");

                lblAdd_ContactPersonNameValue.Text = "";
                lblAdd_InternalUserDeptValue.Text = "";
                lblAdd_InternalUserNameValue.Text = "";


                mvTransaction.ActiveViewIndex = 1;
                mvClientList.ActiveViewIndex = 1;
                mvItemList.ActiveViewIndex = 2;

            }
            catch { }
        }

        protected void TranSelectClientBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //mvTransaction.ActiveViewIndex = 3;
                FailureAddText.Text = "";
                SetupDdlFormStatus_SelectClient();

                if (TranSelectClientBtn.Text == "Select Client")
                {
                    mvClientList.ActiveViewIndex = 0;
                    TranSelectClientBtn.Text = "Select Client (Close)"; //J20180605_20190603
                }
                else
                {
                    mvClientList.ActiveViewIndex = 1;
                    TranSelectClientBtn.Text = "Select Client";
                }


            }
            catch { }
        }

        /*
        protected void btnEdit_TranSelectClient_Click(object sender, EventArgs e)
        {
            try
            {
                //mvTransaction.ActiveViewIndex = 3;

                SetupDdlFormStatus_EditClient();

                if (TranSelectClientBtn.Text == "Select Client")
                {
                    mvClientList.ActiveViewIndex = 0;
                    TranSelectClientBtn.Text = "Close";
                }
                else
                {
                    mvClientList.ActiveViewIndex = 1;
                    TranSelectClientBtn.Text = "Select Client";
                }


            }
            catch { }
        }
        */

        protected void btnAdd_TranAddItemBtn_Click(object sender, EventArgs e)
        {
            try
            {

                FailureAddText.Text = "";
                SetupDdlFormStatus_AddItem();

                txtTranItemFilter_ID.Text = "999999";   //Initiate Search result
                //btnGetSelected.Visible = false;

                string sRequestDateFrom = txtAdd_BorrowDateFrom.Text;
                string sRequestDateTo = txtAdd_BorrowDateTo.Text;
                int iNoOfLagDay = Int32.Parse(txtAdd_NoOfLagDay.Text);

                txtTranItemFilter_RequestDateFrom.Text = sRequestDateFrom;
                txtTranItemFilter_RequestDateTo.Text = sRequestDateTo;
                txtTranItemFilter_NoOfLagDay.Text = iNoOfLagDay.ToString();

                //TESTING 20170828
                //AddItemDataBind();

                //DataTable dt = new DataTable();
                //dt.Columns.AddRange(new DataColumn[5] { new DataColumn("ItemID"), new DataColumn("ItemNo"), new DataColumn("Description"), new DataColumn("AvailableQty"), new DataColumn("BorrowingQty") });

                mvAdd_AddItem_ViewTxn.ActiveViewIndex = 1;

                if (btnAdd_TranAddItemBtn.Text == "Add Item")
                {
                    mvItemList.ActiveViewIndex = 0;
                    btnAdd_TranAddItemBtn.Text = "Add Item (Close)"; //J20180605_20190603
                }
                else
                {
                    mvItemList.ActiveViewIndex = 2;

                    btnAdd_TranAddItemBtn.Text = "Add Item";
                }

            }
            catch { }
        }

        protected void btnEdit_TranAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                txtEdit_TranItemFilter_WindowsAuthUserID.Text = Session["WindowAuthID"].ToString();    //J20180605
                SetupDdlFormStatus_EditItem();

                lblEdit_ViewItemTran.Visible = false;
                btnEdit_ViewTxn_Close.Visible = false;
                //btnEdit_GetSelected.Visible = false;

                txtEdit_TranItemFilter_ID.Text = "999999";

                string sRequestDateFrom = txtEdit_TranItemFilter_RequestDateFrom.Text;
                string sRequestDateTo = txtEdit_TranItemFilter_RequestDateTo.Text;
                int iNoOfLagDay = Int32.Parse(txtEdit_TranItemFilter_NoOfLagDay.Text);

                txtTranItemFilter_RequestDateFrom.Text = sRequestDateFrom;
                txtTranItemFilter_RequestDateTo.Text = sRequestDateTo;
                txtTranItemFilter_NoOfLagDay.Text = iNoOfLagDay.ToString();

                if (btnEdit_TranAddItem.Text == "Add Item")
                {
                    mvEdit_ItemList.ActiveViewIndex = 0;
                    //btnEdit_TranAddItem.Text = "Close";
                    btnEdit_TranAddItem.Text = "Add Item (Close)";   //J20180605_20190603
                }
                else
                {
                    mvEdit_ItemList.ActiveViewIndex = 1;
                    btnEdit_TranAddItem.Text = "Add Item";
                }



            }
            catch { }
        }

        protected void btnEdit_TranRefresh_Click(object sender, EventArgs e)
        {
            int iTranID = 0;
            iTranID = Int32.Parse(txtEdit_TranID.Text);

            loadTranDetailsList_Edit(iTranID);
        }


        //ClientList

        /*
    protected void SelectClientBtn_select_Click(object sender, EventArgs e)
    {
        try
        {

            int ClientID = Int32.Parse(txtClientID_select.Text);

            TransactionController tc = new TransactionController();
            DataSet cp, c;
            DataRow drClient;

            c = tc.getClientName(ClientID);
            if (c.Tables[0].Rows.Count > 0)
            {
                drClient = c.Tables[0].Rows[0];
                txtAdd_ClientNameDesc.Text = Convert.ToString(drClient["Name"]);
            }

            cp = tc.getContactPerson(ClientID);

            ddlAdd_ContactPersonName.DataSource = cp;
            ddlAdd_ContactPersonName.DataTextField = "Name";
            ddlAdd_ContactPersonName.DataValueField = "ID";
            ddlAdd_ContactPersonName.DataBind();
            //ddlAdd_ContactPersonName.Items.Insert(0, new ListItem("test", "0"));

            txtAdd_ClientNameValue.Text = txtClientID_select.Text;
            lblAdd_ContactPersonNameValue.Text = ddlAdd_ContactPersonName.SelectedValue;

            mvTransaction.ActiveViewIndex = 1;

        }
        catch { }
    }
    */

        protected void Tran_SelectClient(object sender, CommandEventArgs e)
        {

            try
            {
                //string name = GridViewClientForms.SelectedRow.Cells[1].Text;
                int ClientID = Int32.Parse(e.CommandArgument.ToString());
                lblAdd_ClientNameValue.Text = e.CommandArgument.ToString();

                TransactionController tc = new TransactionController();
                DataSet cp, c;
                DataRow drClient;

                c = tc.getClientName(ClientID);
                if (c.Tables[0].Rows.Count > 0)
                {
                    drClient = c.Tables[0].Rows[0];
                    txtAdd_ClientNameDescValue.Text = Convert.ToString(drClient["Client Name"]);
                }

                cp = tc.getContactPerson(ClientID);

                ddlAdd_ContactPersonName.DataSource = cp;
                ddlAdd_ContactPersonName.DataTextField = "Contact Person";
                ddlAdd_ContactPersonName.DataValueField = "ID";
                ddlAdd_ContactPersonName.DataBind();
                //ddlAdd_ContactPersonName.Items.Insert(0, new ListItem("test", "0"));
                lblAdd_ContactPersonNameValue.Text = ddlAdd_ContactPersonName.SelectedValue;

                //lblAdd_ClientNameValue.Text = txtClientID_select.Text;

                mvClientList.ActiveViewIndex = 1;
                TranSelectClientBtn.Text = "Select Client";

            }
            catch { }
        }

        protected void GetSelectedRecords(object sender, EventArgs e)
        {
            /*
            string sCheckError = CheckFieldsAddItem_Add();

            if (sCheckError != "")
            {
                FailureCreateTranAddItemText.Text = sCheckError;
                return;
            }
            FailureCreateTranAddItemText.Text = "";
            */

            DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[5] { new DataColumn("ItemID"), new DataColumn("ItemNo"), new DataColumn("Description"), new DataColumn("AvailableQty"), new DataColumn("BorrowingQty") });

            //BtnAdd_AddItem.Visible = true;

            if (ViewState["TempTransaction"] != null)
            {
                //DataTable dt = (DataTable)ViewState["TempTransaction"];
                dt = (DataTable)ViewState["TempTransaction"];
            }
            else
            {
                dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[5] { new DataColumn("ItemID"), new DataColumn("ItemNo"), new DataColumn("Description"), new DataColumn("AvailableQty"), new DataColumn("BorrowingQty") });
                ViewState["TempTransaction"] = dt;
                this.BindGrid();
            }

            foreach (GridViewRow row in GridViewItems.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkAddItem") as CheckBox);
                    //CheckBox chkRow = (row.Cells[0].Controls[0] as CheckBox);
                    if (chkRow.Checked)
                    {
                        string ItemID = row.Cells[8].Text;
                        string ItemNo = row.Cells[9].Text;
                        string Description = row.Cells[10].Text;
                        string AvailableQty = row.Cells[7].Text;
                        int iAvailableQty = Int32.Parse(AvailableQty);

                        TextBox tBorrowingQty = (TextBox)GridViewItems.Rows[row.RowIndex].FindControl("BorrowingQty");
                        string BorrowingQty = tBorrowingQty.Text;
                        int iBorrowingQty = Int32.Parse(BorrowingQty);

                        string sCheckError = "";

                        if (iBorrowingQty <= 0)
                        {
                            sCheckError = ItemNo + " " + Description + " - Borrowing Qty must be positive integar." + "<br>";
                        }

                        if (iBorrowingQty > iAvailableQty)

                        {
                            sCheckError += ItemNo + " " + Description + " - Borrowing Qty cannot be greater than Available Qty. Item cannot be added." + "<br>";
                        }

                        //HEIDI 20171009
                        sCheckError = sCheckError + CheckFieldsAddItem_Add(ItemID, iBorrowingQty);

                        if (sCheckError != "")
                        {
                            FailureCreateTranAddItemText.Text = sCheckError;
                            return;
                        }
                        FailureCreateTranAddItemText.Text = "";

                        //string BorrowingQty = row.Cells[1].Text;
                        //string Description = (row.Cells[2].FindControl("lblCountry") as Label).Text;


                        //lblTranItem_ItemID.Text = lblTranItem_ItemID.Text + row.Cells[6].Text.ToString() + ",";
                        //lblTranItem_BorrowQty.Text = lblTranItem_BorrowQty.Text + row.Cells[4].Text.ToString() + ",";
                        //lblTranItem_ToBorrowDate.Text = txtAdd_BorrowDateFrom.Text;
                        //lblTranItem_ToReturnDate.Text = txtAdd_BorrowDateTo.Text;
                        //lblTranItem_iAction.Text = ddlAction_Add.SelectedValue.ToString();


                        dt.Rows.Add(ItemID, ItemNo, Description, AvailableQty, BorrowingQty);
                        chkRow.Checked = false;
                    }
                    CheckBox ChkBoxHeader = (CheckBox)GridViewItems.HeaderRow.FindControl("chkboxSelectAll");
                    ChkBoxHeader.Checked = false;

                    //mvItemList.ActiveViewIndex = 1;
                    //btnAdd_TranAddItemBtn.Text = "Add Item";
                }
            }

            gvSelected.DataSource = dt;
            gvSelected.DataBind();
            //gvShow_Selected.DataSource = dt;
            //gvShow_Selected.DataBind();

            ViewState["TempTransaction"] = dt;
            this.BindGrid();

        }

        /*
                private void SetPreviousData()
                {
                    int RowIndex = 0;
                    if (ViewState["CurrentTable"] != null)
                    {
                        DataTable dt = (DataTable)ViewState["CurrentTable"];
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string sItemID = gvSelected.Rows[RowIndex].Cells[1].FindControl("ItemID").ToString();
                                string sItemNo = gvSelected.Rows[RowIndex].Cells[2].FindControl("ItemNo").ToString();
                                string sDescription = gvSelected.Rows[RowIndex].Cells[3].FindControl("Description").ToString();
                                string sAvailableQty = gvSelected.Rows[RowIndex].Cells[4].FindControl("AvailableQty").ToString();
                                TextBox tBorrowingQty = (TextBox)gvSelected.Rows[RowIndex].Cells[5].FindControl("BorrowingQty");

                                sItemID = dt.Rows[i]["ItemID"].ToString();
                                sItemNo = dt.Rows[i]["ItemNo"].ToString();
                                sDescription = dt.Rows[i]["Description"].ToString();
                                sAvailableQty= dt.Rows[i]["AvailableQty"].ToString();
                                tBorrowingQty.Text= dt.Rows[i]["BorrowingQty"].ToString();

                                RowIndex++;
                            }
                        }
                    }
                }


                protected void AddNewRowToGrid()
                {
                    int RowIndex = 0;

                    if (ViewState["CurrentTable"] != null)
                    {
                        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                        DataRow drCurrentRow = null;
                        if (dtCurrentTable.Rows.Count > 0)
                        {
                            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                            {
                                string sItemID = gvSelected.Rows[RowIndex].Cells[1].FindControl("ItemID").ToString();
                                string sItemNo = gvSelected.Rows[RowIndex].Cells[2].FindControl("ItemNo").ToString();
                                string sDescription = gvSelected.Rows[RowIndex].Cells[3].FindControl("Description").ToString();
                                string sAvailableQty = gvSelected.Rows[RowIndex].Cells[4].FindControl("AvailableQty").ToString();
                                TextBox tBorrowingQty = (TextBox)gvSelected.Rows[RowIndex].Cells[5].FindControl("BorrowingQty");

                                drCurrentRow = dtCurrentTable.NewRow();
                                drCurrentRow["RowNumber"] = i + 1;

                                dtCurrentTable.Rows[i - 1]["ItemID"] = sItemID;
                                dtCurrentTable.Rows[i - 1]["ItemNo"] = sItemNo;
                                dtCurrentTable.Rows[i - 1]["Description"] = sDescription;
                                dtCurrentTable.Rows[i - 1]["AvailableQty"] = sAvailableQty;
                                dtCurrentTable.Rows[i - 1]["BorrowingQty"] = tBorrowingQty.Text;

                                RowIndex++;
                            }
                            dtCurrentTable.Rows.Add(drCurrentRow);
                            ViewState["CurrentTable"] = dtCurrentTable;

                            gvSelected.DataSource = dtCurrentTable;
                            gvSelected.DataBind();
                        }
                    }
                    else
                    {
                        Response.Write("ViewState is null");
                    }

                    //Set Previous Data on Postbacks
                    SetPreviousData();
                }

                protected void GetSelectedRecords(object sender, EventArgs e)
                {
                    AddNewRowToGrid();
                }
        */
        /*
        private void AddNewRowToGrid()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox box1 = (TextBox)gvSelected.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                        TextBox box2 = (TextBox)gvSelected.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                        TextBox box3 = (TextBox)gvSelected.Rows[rowIndex].Cells[3].FindControl("TextBox3");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["Column1"] = box1.Text;
                        dtCurrentTable.Rows[i - 1]["Column2"] = box2.Text;
                        dtCurrentTable.Rows[i - 1]["Column3"] = box3.Text;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    Gridview1.DataSource = dtCurrentTable;
                    Gridview1.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousData();
        }
        */


        //protected void GetSelectedRecords_Edit(object sender, EventArgs e)
        //{

        //    //lblTranItem_ItemID.Text = "ItemID: ";
        //    //lblTranItem_BorrowQty.Text = "BorrowQty: ";

        //    BtnEdit_AddItem.Visible = true;

        //    DataTable dt = new DataTable();
        //    dt.Columns.AddRange(new DataColumn[4] { new DataColumn("ItemID"), new DataColumn("ItemNo"), new DataColumn("Description"), new DataColumn("AvailableQty") });
        //    foreach (GridViewRow row in GridViewEdit_Items.Rows)
        //    {
        //        if (row.RowType == DataControlRowType.DataRow)
        //        {
        //            CheckBox chkRow = (row.Cells[0].FindControl("chkEditItem") as CheckBox);
        //            //CheckBox chkRow = (row.Cells[0].Controls[0] as CheckBox);
        //            if (chkRow.Checked)
        //            {
        //                string ItemID = row.Cells[6].Text;
        //                string ItemNo = row.Cells[7].Text;
        //                string Description = row.Cells[8].Text;
        //                string AvailableQty = row.Cells[5].Text;
        //                //string Description = (row.Cells[2].FindControl("lblCountry") as Label).Text;

        //                /*
        //                lblTranItem_ItemID.Text = lblTranItem_ItemID.Text + row.Cells[6].Text.ToString() + ",";
        //                //lblTranItem_BorrowQty.Text = lblTranItem_BorrowQty.Text + row.Cells[4].Text.ToString() + ",";
        //                lblTranItem_ToBorrowDate.Text = txtAdd_BorrowDateFrom.Text;
        //                lblTranItem_ToReturnDate.Text = txtAdd_BorrowDateTo.Text;
        //                lblTranItem_iAction.Text = ddlAction_Add.SelectedValue.ToString();
        //                */

        //                dt.Rows.Add(ItemID, ItemNo, Description, AvailableQty);
        //            }
        //        }
        //    }
        //    gvEdit_Selected.DataSource = dt;
        //    gvEdit_Selected.DataBind();


        //}
        protected void GetSelectedRecords_Edit(object sender, EventArgs e)
        {

            //lblTranItem_ItemID.Text = "ItemID: ";
            //lblTranItem_BorrowQty.Text = "BorrowQty: ";

            BtnEdit_AddItem.Visible = true;

            DataTable dt = (DataTable)ViewState["TempTransactionEdit"];

            //DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[4] { new DataColumn("ItemID"), new DataColumn("ItemNo"), new DataColumn("Description"), new DataColumn("AvailableQty") });
            foreach (GridViewRow row in GridViewEdit_Items.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkEditItem") as CheckBox);
                    //CheckBox chkRow = (row.Cells[0].Controls[0] as CheckBox);
                    if (chkRow.Checked)
                    {
                        string ItemID = row.Cells[8].Text;
                        string ItemNo = row.Cells[9].Text;
                        string Description = row.Cells[10].Text;
                        //string Description = row.Cells[0].FindControl("Description").ToString();
                        string AvailableQty = row.Cells[7].Text;
                        int iAvailableQty = Int32.Parse(AvailableQty);

                        TextBox tBorrowingQty = (TextBox)GridViewEdit_Items.Rows[row.RowIndex].FindControl("BorrowingQtyEdit");
                        string BorrowingQty = tBorrowingQty.Text;
                        int iBorrowingQty = Int32.Parse(BorrowingQty);

                        string sCheckError = "";

                        if (iBorrowingQty <= 0)
                        {
                            sCheckError = sCheckError + ItemNo + " " + Description + " - Borrowing Qty must be positive integar." + "<br>";
                        }

                        if (iBorrowingQty > iAvailableQty)

                        {
                            sCheckError += ItemNo + " " + Description + " - Borrowing Qty cannot be greater than Available Qty. Item cannot be added." + "<br>";
                        }

                        //HEIDI 20171009
                        sCheckError = CheckFieldsAddItem_Edit(ItemID, iBorrowingQty);

                        if (sCheckError != "")
                        {
                            FailureEditTranAddItemText.Text = sCheckError;
                            return;
                        }
                        FailureEditTranAddItemText.Text = "";

                        dt.Rows.Add(ItemID, ItemNo, Description, AvailableQty, BorrowingQty);
                        chkRow.Checked = false;
                    }
                    CheckBox ChkBoxHeader = (CheckBox)GridViewEdit_Items.HeaderRow.FindControl("chkboxEditSelectAll");
                    ChkBoxHeader.Checked = false;
                }
            }
            gvEdit_Selected.DataSource = dt;
            gvEdit_Selected.DataBind();

            ViewState["TempTransactionEdit"] = dt;
            this.BindGridEdit();

        }

        protected void btnAdd_TranClientFilter_Click(object sender, EventArgs e)
        {
            try
            {
                odsClient.Update();
            }
            catch { }
        }
        /*
        protected void btnEdit_TranClientFilter_Click(object sender, EventArgs e)
        {
            try
            {
                odsEdit_Client.Update();
            }
            catch { }
        }
        */

        protected void btnAdd_TranClientFilterClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtTranClientFilter_PartnerID.Text = "";
                txtTranClientFilter_ClientName.Text = "";
                ddlTranClientFilter_ClientType.SelectedIndex = 0;
                txtTranClientFilter_ClientPhoneNo.Text = "";
                txtTranClientFilter_ClientFax.Text = "";

                //txtTranClientFilter_ContactPersonName.Text = "";
                //txtTranClientFilter_ContactPersonPhoneNo.Text = "";

                //for (int i = 0; i < checkBoxList_Status.Items.Count; i++)
                //{
                //    checkBoxList_Status.Items[i].Selected = true;
                //}

                //SetAccessRight();

                //if (Session["IsAdmin"] != null)
                //{
                //    if (Session["IsAdmin"].ToString() == "Yes")
                //    {
                //        ddlFilter_Dept.SelectedIndex = 0;
                //    }
                //}
                //if (Session["RoleCode"].ToString() != null)
                //{
                //    if (Session["RoleCode"].ToString() == "Fin_Admin" || Session["RoleCode"].ToString() == "IT_Appn")
                //    {
                //        ddlFilter_Dept.SelectedIndex = 0;
                //    }
                //}
                //txtFilter_Status.Text = "0";
                odsClient.Update();
            }
            catch { }

        }
        /*
        protected void btnEdit_TranClientFilterClear_Click(object sender, EventArgs e)
        {
            try
            {
                ddlEdit_TranClientFilter_ClientType.SelectedIndex = 0;
                txtEdit_TranClientFilter_ClientName.Text = "";
                txtEdit_TranClientFilter_ContactPersonName.Text = "";
                txtEdit_TranClientFilter_ClientPhoneNo.Text = "";
                txtEdit_TranClientFilter_ContactPersonPhoneNo.Text = "";

                //for (int i = 0; i < checkBoxList_Status.Items.Count; i++)
                //{
                //    checkBoxList_Status.Items[i].Selected = true;
                //}

                //SetAccessRight();

                //if (Session["IsAdmin"] != null)
                //{
                //    if (Session["IsAdmin"].ToString() == "Yes")
                //    {
                //        ddlFilter_Dept.SelectedIndex = 0;
                //    }
                //}
                //if (Session["RoleCode"].ToString() != null)
                //{
                //    if (Session["RoleCode"].ToString() == "Fin_Admin" || Session["RoleCode"].ToString() == "IT_Appn")
                //    {
                //        ddlFilter_Dept.SelectedIndex = 0;
                //    }
                //}
                //txtFilter_Status.Text = "0";
                odsEdit_Client.Update();
            }
            catch { }

        }
        */

        protected void PageClientFormIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewClientForms.PageIndex = e.NewPageIndex;
            GridViewClientForms.DataBind();

        }

        /*
        protected void PageClientFormIndexChanging_edit(object sender, GridViewPageEventArgs e)
        {
            GridViewClientForms_edit.PageIndex = e.NewPageIndex;
            GridViewClientForms_edit.DataBind();

        }
        */


        protected void GridViewTransaction_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            /*
            if (e.Row.Cells.Count > 1)
            {
                
                if (e.Row.Cells[6].Text!="BOOK")
                //if (e.Row.TemplateControl.FindControl("Action").Equals("BORROW"))
                {
                    e.Row.TemplateControl.FindControl("btnEditBooking").Visible = false;
                }
                

            }

            */
            if (e.Row.Cells.Count > 1)
            {
                e.Row.Cells[1].Visible = false; //ID
                e.Row.Cells[2].Visible = false; //TransactionID
                                                // e.Row.Cells[6].Width =%;  //Action

            }
        }

        protected void gvShow_Selected_Edit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 1)
            {
                e.Row.Cells[2].Visible = false; //ID
                e.Row.Cells[16].Visible = false; //itemID

            }
        }



        protected void GridViewClientForms_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 1)
            {
                //e.Row.Cells[1].Visible = false;
                /*
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[15].Visible = false;
                */
                e.Row.Cells[1].Visible = false;     //ClientID
                e.Row.Cells[4].Visible = false;     //ClientTypeID
                //e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            }
        }


        protected void GridViewItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            foreach (GridViewRow row in GridViewItems.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    int iAvailableQty = Int32.Parse(row.Cells[7].Text); //Available Qty
                    TextBox tAvailableQty = (TextBox)GridViewItems.Rows[row.RowIndex].FindControl("AvailableQty");

                    tAvailableQty.Text = iAvailableQty.ToString();

                    if (iAvailableQty <= 0)
                    {
                        TextBox tBorrowingQty = (TextBox)GridViewItems.Rows[row.RowIndex].FindControl("BorrowingQty");
                        tBorrowingQty.Text = "0";

                        CheckBox cb = (CheckBox)GridViewItems.Rows[row.RowIndex].FindControl("chkAddItem");
                        cb.Enabled = false;
                    }
                }
            }

            if (e.Row.Cells.Count > 1)
            {
                e.Row.Cells[7].Visible = false; //Available Qty
                e.Row.Cells[8].Visible = false; //ID

                e.Row.Cells[3].Visible = false; //Available
                e.Row.Cells[4].Visible = false; //BookedQty
                e.Row.Cells[5].Visible = false; //BorrowedQty
                e.Row.Cells[13].Visible = false; //DeptCode
            }

        }

        protected void GridViewEdit_Items_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            foreach (GridViewRow row in GridViewEdit_Items.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    int iAvailableQty = Int32.Parse(row.Cells[7].Text); //Available Qty
                    TextBox tAvailableQty = (TextBox)GridViewEdit_Items.Rows[row.RowIndex].FindControl("AvailableQtyEdit");

                    tAvailableQty.Text = iAvailableQty.ToString();

                    if (iAvailableQty <= 0)
                    {
                        TextBox tBorrowingQty = (TextBox)GridViewEdit_Items.Rows[row.RowIndex].FindControl("BorrowingQtyEdit");
                        tBorrowingQty.Text = "0";

                        CheckBox cb = (CheckBox)GridViewEdit_Items.Rows[row.RowIndex].FindControl("chkEditItem");
                        cb.Enabled = false;

                    }
                }
            }

            if (e.Row.Cells.Count > 1)
            {
                e.Row.Cells[7].Visible = false; //Available Qty
                e.Row.Cells[8].Visible = false; //ID

                e.Row.Cells[3].Visible = false; //Available
                e.Row.Cells[4].Visible = false; //BookedQty
                e.Row.Cells[5].Visible = false; //BorrowedQty
                e.Row.Cells[13].Visible = false; //DeptCode

            }

        }

        protected void GridViewTxns_Add_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 1)
            {
                e.Row.Cells[0].Visible = false; //ID
                e.Row.Cells[1].Visible = false; //TransactionID

            }
        }

        protected void GridViewTxns_Edit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 1)
            {
                e.Row.Cells[0].Visible = false; //ID
                e.Row.Cells[1].Visible = false; //TransactionID

            }
        }

        protected void GridViewTxns_SelectedItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 1)
            {
                e.Row.Cells[0].Visible = false; //ID
                e.Row.Cells[1].Visible = false; //TransactionID

            }
        }


        protected void SelectItemBtn_select_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch { }
        }


        //Item List
        protected void TranItemFilterTypeSelection(object sender, CommandEventArgs e)
        {
            switch (e.CommandName.ToString().ToUpper())
            {
                case "A": //select all
                    for (int i = 0; i < chkTranItemFilter_Type.Items.Count; i++)
                    {
                        chkTranItemFilter_Type.Items[i].Selected = true;
                    }
                    break;

                case "U": //unselect all
                    for (int i = 0; i < chkTranItemFilter_Type.Items.Count; i++)
                    {
                        chkTranItemFilter_Type.Items[i].Selected = false;
                    }
                    break;

                default:
                    break;
            }
        }


        protected void TranItemFilterTypeSelection_Edit(object sender, CommandEventArgs e)
        {
            switch (e.CommandName.ToString().ToUpper())
            {
                case "A": //select all
                    for (int i = 0; i < chkEdit_TranItemFilter_Type.Items.Count; i++)
                    {
                        chkEdit_TranItemFilter_Type.Items[i].Selected = true;
                    }
                    break;

                case "U": //unselect all
                    for (int i = 0; i < chkEdit_TranItemFilter_Type.Items.Count; i++)
                    {
                        chkEdit_TranItemFilter_Type.Items[i].Selected = false;
                    }
                    break;

                default:
                    break;
            }
        }

        protected void TranItemFilterLangSelection(object sender, CommandEventArgs e)
        {
            switch (e.CommandName.ToString().ToUpper())
            {
                case "A": //select all
                    for (int i = 0; i < chkTranItemFilter_Lang.Items.Count; i++)
                    {
                        chkTranItemFilter_Lang.Items[i].Selected = true;
                    }
                    break;

                case "U": //unselect all
                    for (int i = 0; i < chkTranItemFilter_Lang.Items.Count; i++)
                    {
                        chkTranItemFilter_Lang.Items[i].Selected = false;
                    }
                    break;

                default:
                    break;
            }
        }

        protected void TranItemFilterLangSelection_Edit(object sender, CommandEventArgs e)
        {
            switch (e.CommandName.ToString().ToUpper())
            {
                case "A": //select all
                    for (int i = 0; i < chkEdit_TranItemFilter_Lang.Items.Count; i++)
                    {
                        chkEdit_TranItemFilter_Lang.Items[i].Selected = true;
                    }
                    break;

                case "U": //unselect all
                    for (int i = 0; i < chkEdit_TranItemFilter_Lang.Items.Count; i++)
                    {
                        chkEdit_TranItemFilter_Lang.Items[i].Selected = false;
                    }
                    break;

                default:
                    break;
            }
        }



        protected void btnAdd_TranItemFilter_Click(object sender, EventArgs e)
        {

            try
            {
                GridViewItems.EmptyDataText = "No Record Found!";
                btnGetSelected.Visible = true;

                txtTranItemFilter_ID.Text = "0";
                txtTranItemFilter_ItemTypeID.Text = "";

                foreach (ListItem item in chkTranItemFilter_Type.Items)
                {
                    if (item.Selected)
                    {
                        txtTranItemFilter_ItemTypeID.Text += item.Value + ',';
                    }
                }

                txtTranItemFilter_ItemLangID.Text = "";
                foreach (ListItem item in chkTranItemFilter_Lang.Items)
                {
                    if (item.Selected)
                    {
                        txtTranItemFilter_ItemLangID.Text += item.Value + ',';
                    }
                }

                txtTranItemFilter_NeedApproval.Text = "";
                foreach (ListItem item in checkBoxList_NeedApproval.Items)
                {
                    if (item.Selected)
                    {
                        txtTranItemFilter_NeedApproval.Text += item.Value + ',';
                    }
                }

                txtTranItemFilter_GlobalUse.Text = "";
                foreach (ListItem item in checkBoxList_GlobalUse.Items)
                {
                    if (item.Selected)
                    {
                        txtTranItemFilter_GlobalUse.Text += item.Value + ',';
                    }
                }
                /*
                txtTranItemFilter_Status.Text = "";
                foreach (ListItem item in checkBoxList_Status.Items)
                {
                    if (item.Selected)
                    {
                        txtTranItemFilter_Status.Text += item.Value + ',';
                    }
                }
                */
                //odsEPOForms.ID(
                //if (txtFilter_Status.Text == "")
                //    txtFilter_Status.Text = " ";

                odsItems.Update();
                //AddItemDataBind();

            }
            catch { }
        }

        protected void btnEdit_TranItemFilter_Click(object sender, EventArgs e)
        {

            try
            {

                GridViewEdit_Items.EmptyDataText = "No Record Found!";
                btnEdit_GetSelected.Visible = true;

                txtEdit_TranItemFilter_ID.Text = "0";
                txtEdit_TranItemFilter_ItemTypeID.Text = "";

                foreach (ListItem item in chkEdit_TranItemFilter_Type.Items)
                {
                    if (item.Selected)
                    {
                        txtEdit_TranItemFilter_ItemTypeID.Text += item.Value + ',';
                    }
                }

                txtEdit_TranItemFilter_ItemLangID.Text = "";
                foreach (ListItem item in chkEdit_TranItemFilter_Lang.Items)
                {
                    if (item.Selected)
                    {
                        txtEdit_TranItemFilter_ItemLangID.Text += item.Value + ',';
                    }
                }

                txtEdit_TranItemFilter_NeedApproval.Text = "";
                foreach (ListItem item in checkBoxListEdit_NeedApproval.Items)
                {
                    if (item.Selected)
                    {
                        txtEdit_TranItemFilter_NeedApproval.Text += item.Value + ',';
                    }
                }

                txtEdit_TranItemFilter_GlobalUse.Text = "";
                foreach (ListItem item in checkBoxListEdit_GlobalUse.Items)
                {
                    if (item.Selected)
                    {
                        txtEdit_TranItemFilter_GlobalUse.Text += item.Value + ',';
                    }
                }
                /*
                txtEdit_TranItemFilter_Status.Text = "";
                foreach (ListItem item in checkBoxListEdit_Status.Items)
                {
                    if (item.Selected)
                    {
                        txtEdit_TranItemFilter_Status.Text += item.Value + ',';
                    }
                }
                */
                //odsEPOForms.ID(
                //if (txtFilter_Status.Text == "")
                //    txtFilter_Status.Text = " ";

                odsEdit_Items.Update();



            }
            catch { }
        }


        protected void TranItemFilterClearBtn_Click(object sender, EventArgs e)
        {

            try
            {
                for (int i = 0; i < chkTranItemFilter_Type.Items.Count; i++)
                {
                    //chkTranItemFilter_Type.Items[i].Selected = true;
                    chkTranItemFilter_Type.Items[i].Selected = false;
                }
                txtTranItemFilter_ItemTypeID.Text = "";

                for (int i = 0; i < chkTranItemFilter_Lang.Items.Count; i++)
                {
                    //chkTranItemFilter_Lang.Items[i].Selected = true;
                    chkTranItemFilter_Lang.Items[i].Selected = false;
                }
                txtTranItemFilter_ItemLangID.Text = "";


                txtTranItemFilter_ItemCode.Text = "";
                txtTranItemFilter_Description.Text = "";

                /*
                for (int i = 0; i < checkBoxList_Status.Items.Count; i++)
                {
                    checkBoxList_Status.Items[i].Selected = true;
                }
                txtTranItemFilter_Status.Text = "";
                */

                ddlTranItemFilter_Dept.SelectedIndex = 0;

                for (int i = 0; i < checkBoxList_NeedApproval.Items.Count; i++)
                {
                    checkBoxList_NeedApproval.Items[i].Selected = true;
                }
                txtTranItemFilter_NeedApproval.Text = "";

                for (int i = 0; i < checkBoxList_GlobalUse.Items.Count; i++)
                {
                    checkBoxList_GlobalUse.Items[i].Selected = true;
                }
                txtTranItemFilter_GlobalUse.Text = "";

                //txtTranItemFilter_ItemCopiesID.Text = "";

                /*
                SetAccessRight();

                if (Session["IsAdmin"] != null)
                {
                    if (Session["IsAdmin"].ToString() == "Yes")
                    {
                        ddlFilter_Dept.SelectedIndex = 0;
                    }
                }
                if (Session["RoleCode"].ToString() != null)
                {
                    if (Session["RoleCode"].ToString() == "IT_Appn")
                    {
                        ddlFilter_Dept.SelectedIndex = 0;
                    }
                }
                */

                //txtTranItemFilter_Status.Text = "0";
                //odsItems.Update();
            }
            catch { }

        }

        protected void btnEdit_TranItemFilterClear_Click(object sender, EventArgs e)
        {

            try
            {

                for (int i = 0; i < chkEdit_TranItemFilter_Type.Items.Count; i++)
                {
                    //chkEdit_TranItemFilter_Type.Items[i].Selected = true;
                    chkEdit_TranItemFilter_Type.Items[i].Selected = false;
                }
                txtEdit_TranItemFilter_ItemTypeID.Text = "";

                for (int i = 0; i < chkEdit_TranItemFilter_Lang.Items.Count; i++)
                {
                    //chkEdit_TranItemFilter_Lang.Items[i].Selected = true;
                    chkEdit_TranItemFilter_Lang.Items[i].Selected = false;
                }
                txtEdit_TranItemFilter_ItemLangID.Text = "";

                txtEdit_TranItemFilter_ID.Text = "0";
                txtEdit_TranItemFilter_ItemCode.Text = "";
                txtEdit_TranItemFilter_Description.Text = "";

                /*
                for (int i = 0; i < checkBoxListEdit_Status.Items.Count; i++)
                {
                    checkBoxListEdit_Status.Items[i].Selected = true;
                }
                txtEdit_TranItemFilter_Status.Text = "";
                */

                ddlEdit_TranItemFilter_Dept.SelectedIndex = 0;

                for (int i = 0; i < checkBoxListEdit_NeedApproval.Items.Count; i++)
                {
                    checkBoxListEdit_NeedApproval.Items[i].Selected = true;
                }
                txtEdit_TranItemFilter_NeedApproval.Text = "";

                for (int i = 0; i < checkBoxListEdit_GlobalUse.Items.Count; i++)
                {
                    checkBoxListEdit_GlobalUse.Items[i].Selected = true;
                }
                txtEdit_TranItemFilter_GlobalUse.Text = "";


                /*
                SetAccessRight();

                if (Session["IsAdmin"] != null)
                {
                    if (Session["IsAdmin"].ToString() == "Yes")
                    {
                        ddlFilter_Dept.SelectedIndex = 0;
                    }
                }
                if (Session["RoleCode"].ToString() != null)
                {
                    if (Session["RoleCode"].ToString() == "IT_Appn")
                    {
                        ddlFilter_Dept.SelectedIndex = 0;
                    }
                }
                */
                //txtEdit_TranItemFilter_Status.Text = "0";
                //odsEdit_Items.Update();
            }
            catch { }

        }


        protected void PageItemIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridViewItems.PageIndex = e.NewPageIndex;
            GridViewItems.DataBind();
            //AddItemDataBind();

        }

        protected void ddlAdd_ContactPersonName_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblAdd_ContactPersonNameValue.Text = ddlAdd_ContactPersonName.SelectedValue;
        }

        protected void ddlEdit_ContactPersonName_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblEdit_ContactPersonNameValue.Text = ddlAdd_ContactPersonName.SelectedValue;
        }

        protected void ddlAdd_InternalUserDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsUser;
            UserController uc = new UserController();

            string sSelectedDept = "";
            FailureAddText.Text = "";

            sSelectedDept = ddlAdd_InternalUserDept.SelectedValue;
            lblAdd_InternalUserDeptValue.Text = ddlAdd_InternalUserDept.SelectedValue;

            if (sSelectedDept != "")
            {
                //dsUser = uc.getUsers("", "", sSelectedDept, 2);
                dsUser = uc.getUsers("", "", sSelectedDept, 1);

                ddlAdd_InternalUserName.DataSource = dsUser;
                ddlAdd_InternalUserName.DataTextField = "UserName";
                ddlAdd_InternalUserName.DataValueField = "ID";
                ddlAdd_InternalUserName.DataBind();

                ddlAdd_InternalUserName.Items.Insert(0, new ListItem("", String.Empty));
            }
        }


        protected void ddlAdd_InternalUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblAdd_InternalUserNameValue.Text = ddlAdd_InternalUserName.SelectedValue;
        }

        protected void AddTranBtn_Click(object sender, CommandEventArgs e)
        {
            string sTranID = "";
            string sRefNo = "";
            int iContactPersonID = 0;// Int32.Parse(ddlAdd_ContactPersonName.SelectedValue); 
            int iInternalUserID = 0;// Int32.Parse(ddlAdd_InternalUserName.SelectedValue);
            string UID = "";
            string sRemarks = "";

            string sCheckError = CheckFields_Add();

            if (sCheckError != "")
            {
                FailureAddText.Text = sCheckError;
                return;
            }
            FailureAddText.Text = "";

            if (ddlAdd_ContactPersonName.SelectedValue != "")
            {
                iContactPersonID = Int32.Parse(ddlAdd_ContactPersonName.SelectedValue);
            }
            if (ddlAdd_InternalUserName.SelectedValue != "")
            {
                iInternalUserID = Int32.Parse(ddlAdd_InternalUserName.SelectedValue);
            }


            FailureAddText.Text = "";

            if (Session["UID"] != null)
                UID = Session["UID"].ToString();

            sRemarks = txtAdd_Remarks.Text;

            //var Tuple= getTranID("", iContactPersonID, iInternalUserID, UID);
            var Tuple = getTranID("", iContactPersonID, iInternalUserID, UID, sRemarks);
            sTranID = Tuple.Item1;
            sRefNo = Tuple.Item2;

            //For printing confirmation note
            txtPrintTranID.Text = sTranID;
            txtPrintRefNo.Text = sRefNo;


            //FailureAddText.Text = sTranID + "successfully added";

            int iTranID = Int32.Parse(sTranID);
            int iItemID = 0;
            int iBorrowingQty = 1;
            string sToBorrowDate = "";
            string sToReturnDate = "";
            int iAction = 0;
            int iNoOfLagDays = 0;   //J20180605_20190603

            sToBorrowDate = txtAdd_BorrowDateFrom.Text;
            sToReturnDate = txtAdd_BorrowDateTo.Text;
            iAction = Int32.Parse(ddlAction_Add.SelectedValue);
            iNoOfLagDays = Int32.Parse(txtAdd_NoOfLagDay.Text); //J20180605_20190603

            //foreach (GridViewRow row in gvShow_Selected.Rows)
            foreach (GridViewRow row in gvSelected.Rows)

            {

                //iItemID= Int32.Parse(gvShow_Selected.Rows[gr.RowIndex].Cells[1].Text);
                //iItemID = Int32.Parse(row.Cells[0].Text);
                iItemID = Int32.Parse(row.Cells[1].Text);

                //iBorrowingQty = Int32.Parse(gvShow_Selected.Rows[gr.RowIndex].Cells[5].Text);
                //iBorrowingQty = Int32.Parse(row.Cells[4].Text);
                iBorrowingQty = Int32.Parse(row.Cells[5].Text);

                DataSet ds;
                //ds = new TransactionController().addTransactionDetail(iTranID, iItemID, iAction, iBorrowingQty, false, false, sToBorrowDate, sToReturnDate, UID, "A");
                ds = new TransactionController().addTransactionDetail(iTranID, iItemID, iAction, iBorrowingQty, false, false, sToBorrowDate, sToReturnDate, UID, "A", iNoOfLagDays);    //J20180605_20190603

            }
            //loadTranList();
            //odsTransaction.Update();

            switch (e.CommandName.ToString().ToUpper())
            {
                case "P": //print
                    try
                    {
                        mvTransaction.ActiveViewIndex = 2;
                        showPrintOutForm(txtPrintTranID.Text, txtPrintRefNo.Text);

                        //showPrintOutForm("0", txtEPONumber_Edit.Text);
                        //txtActionBeforePrint.Text = "";
                        return;
                    }
                    catch
                    {
                        //ErrorMessageM.Text = "Failed to load survey. Please contact IT for further assistance.";
                    }
                    break;
                case "A": //After added transaction
                    Response.Redirect("~/Transaction/Transaction.aspx");
                    break;
                default:
                    break;
            }


        }

        protected void BackToListBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transaction/Transaction.aspx");
        }

        protected void btnAdd_AddTran_Back_Click(object sender, EventArgs e)
        {
            //loadTranList();
            //odsTransaction.Update();
            Response.Redirect("~/Transaction/Transaction.aspx");
        }

        protected void btnEdit_EditTran_Back_Click(object sender, EventArgs e)
        {
            //loadTranList();
            //odsTransaction.Update();
            Response.Redirect("~/Transaction/Transaction.aspx");
        }


        protected void BtnEdit_AddItem_Click(object sender, CommandEventArgs e)
        {
            string sTranID = "";//txtEdit_TranID.Text;

            string UID = "";

            //FailureAddText.Text = "";

            if (Session["UID"] != null)
                UID = Session["UID"].ToString();

            //FailureAddText.Text = sTranID + "successfully added";

            int iTranID = 0;//Int32.Parse(sTranID);
            int iItemID = 0;
            int iBorrowingQty = 1;
            string sToBorrowDate = "";
            string sToReturnDate = "";
            int iAction = 0;
            int iNoOfLagDay = Int32.Parse(txtEdit_TranItemFilter_NoOfLagDay.Text);  //J20180605_20190603

            string sRemarks = txtEdit_Remarks.Text; //PE19003

            foreach (GridViewRow gr in gvEdit_Selected.Rows)
            {

                sTranID = txtEdit_TranID.Text;  //PE19003
                iTranID = Int32.Parse(sTranID);	//PE19003

                iItemID = Int32.Parse(gvEdit_Selected.Rows[gr.RowIndex].Cells[1].Text);
                //iBorrowingQty= Int32.Parse(gvSelected.Rows[gr.RowIndex].Cells[4].Text);

                //TextBox tBorrowingQty = (TextBox)gvEdit_Selected.Rows[gr.RowIndex].FindControl("BorrowingQty");
                //iBorrowingQty = Int32.Parse(tBorrowingQty.Text);

                iBorrowingQty = Int32.Parse(gvEdit_Selected.Rows[gr.RowIndex].Cells[5].Text);

                //iBorrowingQty = Int32.Parse(gvSelected.Rows[gr.RowIndex].FindControl("BorrowingQty"));

                sToBorrowDate = txtEdit_TranItemFilter_RequestDateFrom.Text;
                sToReturnDate = txtEdit_TranItemFilter_RequestDateTo.Text;
                iAction = 1;
                /*
                lblTranItem_ItemID.Text = iItemID.ToString();
                lblTranItem_BorrowQty.Text = iBorrowingQty.ToString();
                lblTranItem_ToBorrowDate.Text = sToBorrowDate;
                lblTranItem_ToReturnDate.Text = sToReturnDate;
                lblTranItem_iAction.Text = iAction.ToString();
                */

                DataSet ds;
                //ds = new TransactionController().addTransactionDetail(iTranID, iItemID, iAction, iBorrowingQty, false, false, sToBorrowDate, sToReturnDate, UID, "A");
                ds = new TransactionController().addTransactionDetail(iTranID, iItemID, iAction, iBorrowingQty, false, false, sToBorrowDate, sToReturnDate, UID, "A", iNoOfLagDay); //J20180605_20190603
                                                                                                                                                                                    //PE19003
                                                                                                                                                                                    //PE19003
                ds = new TransactionController().addTransaction(iTranID, 0, 0, UID, sRemarks, "E");

                loadTranDetailsList_Edit(iTranID);
                //loadTranDetailsList_Edit(Int32.Parse(txtEdit_TranID.Text));

                BtnEdit_AddItem.Visible = false;

            }

            /*
            gvEdit_Selected.Datasource = ds;
            gvEdit_Selected.Databind();
            ds.Clear();
            */

            if (ViewState["TempTransactionEdit"] != null)
            {
                DataTable dt = (DataTable)ViewState["TempTransactionEdit"];

                dt.Rows.Clear();

                ViewState["TempTransactionEdit"] = null;
                BindGridEdit();

                GridViewEdit_Items.PageIndex = 1;
            }

            mvEdit_ItemList.ActiveViewIndex = 1;
            btnEdit_TranAddItem.Text = "Add Item";
        }

        /* TEST 20170829
        protected void BtnAdd_AddItem_Click(object sender, CommandEventArgs e)
        {
            //string sTranID = txtEdit_TranID.Text;

            string UID = "";

            //FailureAddText.Text = "";

            if (Session["UID"] != null)
                UID = Session["UID"].ToString();

            //FailureAddText.Text = sTranID + "successfully added";

            //int iTranID = Int32.Parse(sTranID);
            int iItemID = 0;
            int iBorrowingQty = 1;
            string sToBorrowDate = "";
            string sToReturnDate = "";
            //int iAction = 0;

            foreach (GridViewRow gr in gvSelected.Rows)
            {

                iItemID = Int32.Parse(gvSelected.Rows[gr.RowIndex].Cells[1].Text);
                iBorrowingQty = Int32.Parse(gvSelected.Rows[gr.RowIndex].Cells[5].Text);


                sToBorrowDate = txtTranItemFilter_RequestDateFrom.Text;
                sToReturnDate = txtTranItemFilter_RequestDateTo.Text;
                //iAction = 1;


                //DataSet ds;
                //ds = new TransactionController().addTransactionDetail(iTranID, iItemID, iAction, iBorrowingQty, false, false, sToBorrowDate, sToReturnDate, UID, "A");

                loadTranDetailsList(iTranID);
            }

            mvEdit_ItemList.ActiveViewIndex = 1;
        }
        */

        /* HEIDI 20171006
        protected void BtnAdd_AddItem_Click(object sender, EventArgs e)
        {
            if (ViewState["TempTransactionItemAdd"] != null)
            {
                DataTable dt = (DataTable)ViewState["TempTransactionItemAdd"];

                foreach (GridViewRow row in gvSelected.Rows)
                {
                    string ItemID = row.Cells[1].Text;
                    string ItemNo = row.Cells[2].Text;
                    string Description = row.Cells[3].Text;
                    string AvailableQty = row.Cells[4].Text;
                    string BorrowingQty = row.Cells[5].Text;

                    dt.Rows.Add(ItemID, ItemNo, Description, AvailableQty, BorrowingQty);
                }

                gvShow_Selected.DataSource = dt;
                gvShow_Selected.DataBind();

                BindGridItemAdd();
                ViewState["TempTransactionItemAdd"] = dt;

            }

            if (ViewState["TempTransaction"] != null)
            {
                DataTable dt = (DataTable)ViewState["TempTransaction"];

                dt.Rows.Clear();

                ViewState["TempTransaction"] = null;
                BindGrid();

                GridViewItems.PageIndex = 1;
            }
            mvItemList.ActiveViewIndex = 1;
            btnAdd_TranAddItemBtn.Text = "Add Item";

        }
        */

        protected void btnEdit_EditTranBtn_Click(object sender, CommandEventArgs e)
        {
            try
            {
                int iTranID = Int32.Parse(txtEdit_TranID.Text);
                int iTranType = Int32.Parse(ddlEdit_TranType.SelectedValue);
                //string sTranID = txtEdit_TranID.Text;
                //string sRefNo = txtEdit_RefNo.Text;

                string UID = "";
                if (Session["UID"] != null)
                    UID = Session["UID"].ToString();

                /*
                if (iTranType == 2)//Internal User
                {
                    if (ddlEdit_InternalUserName.SelectedValue != lblEdit_InternalUserName_Old.Text)
                    {
                        int iInternalUserID = Int32.Parse(ddlEdit_InternalUserName.SelectedValue);
                        DataSet ds = new TransactionController().addTransaction(iTranID, 0, iInternalUserID, UID, "E");
                        DataRow drTranEdit;

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            drTranEdit = ds.Tables[0].Rows[0];
                            sTranID = Convert.ToString(drTranEdit["ID"]);
                            sRefNo = Convert.ToString(drTranEdit["RefNo"]);
                        }
                    }
                }
                else //Client
                {
                    if (ddlEdit_ContactPersonName.SelectedValue != lblEdit_ContactPersonNameValue_Old.Text)
                    {
                        int iContactPersonID = Int32.Parse(lblEdit_ContactPersonNameValue.Text);
                        DataSet ds = new TransactionController().addTransaction(iTranID, iContactPersonID, 0, UID, "E");
                        DataRow drTranEdit;

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            drTranEdit = ds.Tables[0].Rows[0];
                            sTranID = Convert.ToString(drTranEdit["ID"]);
                            sRefNo = Convert.ToString(drTranEdit["RefNo"]);
                        }
                    }

                }
                */
            }
            catch (Exception ex)
            {

            }
            //loadTranList();
            //odsTransaction.Update();

            switch (e.CommandName.ToString().ToUpper())
            {
                case "P": //print
                    try
                    {
                        string sRemarks = txtEdit_Remarks.Text;
                        string sOldRemark = txtEdit_Remarks_old.Text;
                        int iTranID = Int32.Parse(txtEdit_TranID.Text);

                        string UID = "";
                        if (Session["UID"] != null)
                            UID = Session["UID"].ToString();

                        if (sRemarks != sOldRemark)
                        {
                            DataSet ds;
                            ds = new TransactionController().addTransaction(iTranID, 0, 0, UID, sRemarks, "E");
                        }

                        mvTransaction.ActiveViewIndex = 2;
                        showPrintOutForm(txtEdit_TranID.Text, txtEdit_RefNo.Text);

                        //showPrintOutForm("0", txtEPONumber_Edit.Text);
                        //txtActionBeforePrint.Text = "";
                        return;
                    }
                    catch
                    {
                        //ErrorMessageM.Text = "Failed to load survey. Please contact IT for further assistance.";
                    }
                    break;
                case "S": //Save Remarks
                    try
                    {
                        string sRemarks = txtEdit_Remarks.Text;
                        string sOldRemark = txtEdit_Remarks_old.Text;
                        int iTranID = Int32.Parse(txtEdit_TranID.Text);

                        string UID = "";
                        if (Session["UID"] != null)
                            UID = Session["UID"].ToString();

                        if (sRemarks != sOldRemark)
                        {
                            DataSet ds;
                            ds = new TransactionController().addTransaction(iTranID, 0, 0, UID, sRemarks, "E");
                        }
                        Response.Redirect("~/Transaction/Transaction.aspx");
                    }
                    catch
                    {
                        //ErrorMessageM.Text = "Failed to load survey. Please contact IT for further assistance.";
                    }
                    break;
                default:
                    break;
            }
            Response.Redirect("~/Transaction/Transaction.aspx");
        }
        /*
        protected void btnEdit_EditTran_Click(object sender, CommandEventArgs e)
        {


            string sTranID = "";
            int iContactPersonID = 0;// Int32.Parse(ddlEdit_ContactPersonName.SelectedValue); 
            int iInternalUserID = 0;// Int32.Parse(ddlEdit_InternalUserName.SelectedValue);
            string UID = "";

            if (ddlEdit_ContactPersonName.SelectedValue != "")
            {
                iContactPersonID = Int32.Parse(ddlEdit_ContactPersonName.SelectedValue);
            }
            if (ddlEdit_InternalUserName.SelectedValue != "")
            {
                iInternalUserID = Int32.Parse(ddlEdit_InternalUserName.SelectedValue);
            }


            FailureAddText.Text = "";

            if (Session["UID"] != null)
                UID = Session["UID"].ToString();

            sTranID = getTranID("", iContactPersonID, iInternalUserID, UID);

            FailureAddText.Text = sTranID + "successfully added";

            int iTranID = Int32.Parse(sTranID);
            int iItemID = 0;
            int iBorrowingQty = 1;
            string sToBorrowDate = "";
            string sToReturnDate = "";
            int iAction = 0;

            foreach (GridViewRow gr in gvEdit_Selected.Rows)
            {

                iItemID = Int32.Parse(gvEdit_Selected.Rows[gr.RowIndex].Cells[0].Text);
                //iBorrowingQty= Int32.Parse(gvEdit_Selected.Rows[gr.RowIndex].Cells[4].Text);

                TextBox tBorrowingQty = (TextBox)gvEdit_Selected.Rows[gr.RowIndex].FindControl("BorrowingQty");
                iBorrowingQty = Int32.Parse(tBorrowingQty.Text);
                //iBorrowingQty = Int32.Parse(gvEdit_Selected.Rows[gr.RowIndex].FindControl("BorrowingQty"));

                sToBorrowDate = txtEdit_BorrowDateFrom.Text;
                sToReturnDate = txtEdit_BorrowDateTo.Text;
                iAction = Int32.Parse(ddlAction_Add.SelectedValue);


                DataSet ds;
                ds = new TransactionController().addTransactionDetail(iTranID, iItemID, iAction, iBorrowingQty, false, false, sToBorrowDate, sToReturnDate, UID, "A");
            }
            Response.Redirect("~/Transaction/Transaction.aspx");
        }
*/

        private Tuple<string, string> getTranID(string sRefNo, int iContactPersonID, int iInternalUserID, string sAddUser, string sRemarks)
        {
            string sNewTranID = "";
            string sNewRefNo = "";

            //DataSet ds = new TransactionController().addTransaction(0, iContactPersonID, iInternalUserID, sAddUser, "A");
            DataSet ds = new TransactionController().addTransaction(0, iContactPersonID, iInternalUserID, sAddUser, sRemarks, "A");
            DataRow drTranAdd;

            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drTranAdd = ds.Tables[0].Rows[0];
                    sNewTranID = Convert.ToString(drTranAdd["ID"]);
                    sNewRefNo = Convert.ToString(drTranAdd["RefNo"]);
                }
            }

            catch (Exception ex)
            {

            }
            return new Tuple<string, string>(sNewTranID, sNewRefNo);

        }




        protected void EditTransaction(object sender, CommandEventArgs e)
        {

            //string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { '#' });
            //string sTranID = commandArgs[0];
            //string sRefNo = commandArgs[1];
            //string sAction = commandArgs[2];

            string sTranID = e.CommandArgument.ToString();
            int iTranID = Int32.Parse(sTranID);
            txtEdit_TranID.Text = sTranID;
            string sRefNo = "";
            //txtEdit_RefNo.Text = sRefNo;


            loadTranDetailsList_Edit(iTranID);

            TransactionController tc = new TransactionController();
            DataSet dsTran, dsSelectedUser, dsUser, dsContactPerson, dsSelectedContactPerson; //, dsTranDetail
            DataRow drTran, drUser, drContactPerson;    //, drTranDetail

            switch (e.CommandName.ToString().ToUpper())
            {
                case "E": //edit

                    try
                    {
                        mvEdit_SelectedItem_ViewTxn.ActiveViewIndex = 1;

                        dsTran = tc.getTransaction(iTranID);
                        //dsTranDetail = tc.getTransactionDetails(0, iTranID);
                        drTran = dsTran.Tables[0].Rows[0];

                        sRefNo = Convert.ToString(drTran["RefNo"]);
                        txtEdit_RefNo.Text = sRefNo;
                        string sAction = Convert.ToString(drTran["TranStatus"]);

                        string sInternalUserID = Convert.ToString(drTran["InternalUserID"]);
                        int iInternalUserID = Int32.Parse(sInternalUserID);
                        string sDeptCode = "";
                        string sContactPersonID = Convert.ToString(drTran["ContactPersonID"]);
                        int iContactPersonID = Int32.Parse(sContactPersonID);
                        int iClientID = 0;

                        string sRemarks = Convert.ToString(drTran["Remarks"]);
                        txtEdit_Remarks.Text = sRemarks;
                        txtEdit_Remarks_old.Text = sRemarks;

                        txtEdit_AddedBy.Text = Convert.ToString(drTran["AddBy"]);
                        txtEdit_AddedOn.Text = Convert.ToString(drTran["AddOn"]);
                        txtEdit_EditedBy.Text = Convert.ToString(drTran["EditBy"]);
                        txtEdit_EditedOn.Text = Convert.ToString(drTran["EditOn"]);

                        if (iInternalUserID != 0)  //Internal User
                        {
                            ddlEdit_TranType.SelectedIndex = 0;
                            ddlEdit_TranType.Enabled = false;

                            txtEdit_InternalUser.Visible = false;
                            txtEdit_Client.Visible = false;
                            lblEdit_ClientName.Visible = false;
                            txtEdit_ClientNameDesc.Visible = false;
                            ddlEdit_ContactPersonName.Visible = false;
                            lblEdit_ContactPersonName.Visible = false;
                            lblEdit_ContactPersonNameValue.Visible = false;
                            lblEdit_ContactPersonNameValue_Old.Visible = false;

                            dsSelectedUser = tc.getUsers(iInternalUserID, "");
                            drUser = dsSelectedUser.Tables[0].Rows[0];
                            //ddlEdit_InternalUserDept.SelectedIndex = ddlEdit_InternalUserName.Items.IndexOf(ddlEdit_InternalUserName.Items.FindByText(Convert.ToString(drTran["DeptCode"])));
                            sDeptCode = Convert.ToString(drUser["DeptCode"]);
                            ddlEdit_InternalUserDept.Items.Insert(0, new ListItem(sDeptCode));
                            ddlEdit_InternalUserDept.SelectedIndex = 0;
                            ddlEdit_InternalUserDept.Enabled = false;

                            dsUser = tc.getUsers(0, sDeptCode);
                            ddlEdit_InternalUserName.DataSource = dsUser;
                            ddlEdit_InternalUserName.DataTextField = "UserName";
                            ddlEdit_InternalUserName.DataValueField = "ID";
                            ddlEdit_InternalUserName.DataBind();

                            ddlEdit_InternalUserName.SelectedIndex = ddlEdit_InternalUserName.Items.IndexOf(ddlEdit_InternalUserName.Items.FindByValue(sInternalUserID));
                            lblEdit_InternalUserName_Old.Text = sInternalUserID;

                            ddlEdit_InternalUserName.Enabled = false;


                        }
                        else //Client
                        {
                            ddlEdit_TranType.SelectedIndex = 1;
                            ddlEdit_TranType.Enabled = false;

                            txtEdit_Client.Visible = false;
                            txtEdit_InternalUser.Visible = false;
                            lblEdit_InternalUserDept.Visible = false;
                            ddlEdit_InternalUserDept.Visible = false;
                            lblEdit_InternalUserName.Visible = false;
                            ddlEdit_InternalUserName.Visible = false;
                            lblEdit_InternalUserName_Old.Visible = false;

                            dsSelectedContactPerson = tc.getContactPerson(0, iContactPersonID);
                            drContactPerson = dsSelectedContactPerson.Tables[0].Rows[0];
                            iClientID = Int32.Parse(drContactPerson["ClientID"].ToString());

                            //txtEdit_ClientNameValue.Text = iClientID.ToString();

                            DataSet dsClient = tc.getClientName(iClientID);
                            if (dsClient.Tables[0].Rows.Count > 0)
                            {
                                DataRow drClient = dsClient.Tables[0].Rows[0];
                                txtEdit_ClientNameDesc.Text = Convert.ToString(drClient["Client Name"].ToString());
                            }
                            txtEdit_ClientNameDesc.Enabled = false;

                            dsContactPerson = tc.getContactPerson(iClientID);

                            ddlEdit_ContactPersonName.DataSource = dsContactPerson;
                            ddlEdit_ContactPersonName.DataTextField = "Contact Person";
                            ddlEdit_ContactPersonName.DataValueField = "ID";
                            ddlEdit_ContactPersonName.DataBind();

                            ddlEdit_ContactPersonName.SelectedIndex = ddlEdit_ContactPersonName.Items.IndexOf(ddlEdit_ContactPersonName.Items.FindByValue(sContactPersonID));
                            lblEdit_ContactPersonNameValue_Old.Text = sContactPersonID;

                            ddlEdit_ContactPersonName.Enabled = false;

                        }
                    }
                    catch { }
                    break;

                case "P": //print
                    try
                    {
                        //mvTransaction.ActiveViewIndex = 3;
                        //showPrintOutForm(e.CommandArgument.ToString(), "");

                        dsTran = tc.getTransaction(iTranID);
                        if (dsTran.Tables[0].Rows.Count > 0)
                        {
                            drTran = dsTran.Tables[0].Rows[0];
                            sRefNo = Convert.ToString(drTran["RefNo"]);
                        }

                        mvTransaction.ActiveViewIndex = 2;
                        showPrintOutForm(sTranID, sRefNo);
                        //txtActionBeforePrint.Text = "";
                    }
                    catch
                    {
                        //ErrorMessageM.Text = "Failed to load survey. Please contact IT for further assistance.";
                    }
                    break;
            }

            /*
            string sTranID = e.CommandArgument.ToString();
            int iTranID = Int32.Parse(sTranID);
            txtEdit_TranID.Text = sTranID;
            odsTransactionDetails.Update();

            TransactionController tc = new TransactionController();
            DataSet dsTran, dsTranDetail, dsUser, dsContactPerson, dsSelectedContactPerson;
            DataRow drTran, drTranDetail, drUser, drContactPerson;

            switch (e.CommandName.ToString().ToUpper())
            {
                case "E": //edit

                    try
                    {
                        dsTran = tc.getTransaction(iTranID);
                        dsTranDetail = tc.getTransactionDetails(0, iTranID);
                        drTran = dsTran.Tables[0].Rows[0];


                        string sInternalUserID= Convert.ToString(drTran["InternalUserID"]);
                        int iInternalUserID = Int32.Parse(sInternalUserID);
                        string sContactPersonID = Convert.ToString(drTran["ContactPersonID"]);
                        int iContactPersonID = Int32.Parse(sContactPersonID);
                        int iClientID = 0;

                        if(iInternalUserID!=0)  //Internal User
                        {
                            ddlEdit_TranType.SelectedIndex = 0;
                            ddlEdit_TranType.Enabled = false;

                            ddlEdit_InternalUserName.SelectedIndex = ddlEdit_InternalUserName.Items.IndexOf(ddlEdit_InternalUserName.Items.FindByValue(sInternalUserID));
                            lblEdit_InternalUserName_Old.Text = sInternalUserID;

                            dsUser = tc.getUsers(iInternalUserID);
                            drUser= dsUser.Tables[0].Rows[0];
                            ddlEdit_InternalUserDept.SelectedIndex = ddlEdit_InternalUserName.Items.IndexOf(ddlEdit_InternalUserName.Items.FindByText(Convert.ToString(drTran["DeptCode"])));
                            ddlEdit_InternalUserDept.Enabled = false;
                        }
                        else //Client
                        {
                            ddlEdit_TranType.SelectedIndex = 1;
                            ddlEdit_TranType.Enabled = false;

                            dsSelectedContactPerson = tc.getContactPerson(0, iContactPersonID);
                            drContactPerson = dsSelectedContactPerson.Tables[0].Rows[0];
                            iClientID = Int32.Parse(drContactPerson["ClientID"].ToString());

                            txtEdit_ClientNameValue.Text = iClientID.ToString();

                            DataSet dsClient = tc.getClientName(iClientID);
                            if (dsClient.Tables[0].Rows.Count > 0)
                            {
                                DataRow drClient = dsClient.Tables[0].Rows[0];
                                txtEdit_ClientNameDesc.Text = Convert.ToString(drClient["Client Name"].ToString());
                            }
                            txtEdit_ClientNameDesc.Enabled = false;

                            dsContactPerson = tc.getContactPerson(iClientID);

                            ddlEdit_ContactPersonName.DataSource = dsContactPerson;
                            ddlEdit_ContactPersonName.DataTextField = "Name";
                            ddlEdit_ContactPersonName.DataValueField = "ID";
                            ddlEdit_ContactPersonName.DataBind();

                            ddlEdit_ContactPersonName.SelectedIndex = ddlEdit_ContactPersonName.Items.IndexOf(ddlEdit_ContactPersonName.Items.FindByValue(sContactPersonID));
                            lblEdit_ContactPersonNameValue_Old.Text = sInternalUserID;


                        }


                    }
                    catch { }
                    break;
            }


            mvTransaction.ActiveViewIndex = 3;
            mvEdit_ItemList.ActiveViewIndex = 1;
            */
        }

        protected void ClearAddFields()
        {
        }

        /*
        private void loadTranDetailsList(int iTranID)
        {
            TransactionController tc = new TransactionController();
            DataSet dsTranDetail;

            dsTranDetail = tc.getTransactionDetails(0, iTranID);
            gvShow_Selected.DataSource = null;
            gvShow_Selected.DataBind();
            //multiviewClient.ActiveViewIndex = 3;
            gvShow_Selected.DataSource = dsTranDetail;
            gvShow_Selected.DataBind();

        }
        */

        private void loadTranDetailsList_Edit(int iTranID)
        {
            TransactionController tc = new TransactionController();
            DataSet dsTranDetail, dsTran;
            DataRow drTran;

            dsTranDetail = tc.getTransactionDetails(0, iTranID);
            gvShow_Selected_Edit.DataSource = null;
            gvShow_Selected_Edit.DataBind();
            //multiviewClient.ActiveViewIndex = 3;
            gvShow_Selected_Edit.DataSource = dsTranDetail;
            gvShow_Selected_Edit.DataBind();

            dsTran = tc.getTransaction(iTranID);
            drTran = dsTran.Tables[0].Rows[0];
            txtEdit_EditedBy.Text = Convert.ToString(drTran["EditBy"]);
            txtEdit_EditedOn.Text = Convert.ToString(drTran["EditOn"]);


        }
        /*
        private void loadTranList()
        {
            TransactionController tc = new TransactionController();
            DataSet dsTran;

            dsTran = tc.getTransaction();
            GridViewTransaction.DataSource = null;
            GridViewTransaction.DataBind();
            //multiviewClient.ActiveViewIndex = 3;
            GridViewTransaction.DataSource = dsTran;
            GridViewTransaction.DataBind();

        }
        */

        /*
    protected void GridViewTransaction_Sorting(object sender, GridViewSortEventArgs e)
    {
        SetSortDirection(SortDirection);
        if (dataTable != null)
        {
            dataTable.DefaultView.Sort = e.SortExpression + " " + _sortDirection;
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
            SortDireaction = _sortDirection;
        }
    }
    protected void SetSortDirection(string sortDirection)
    {
        if (sortDirection == "ASC")
        {
            _sortDirection = "DESC";
        }
        else
        {
            _sortDirection = "ASC";
        }
    }
    */

        protected void gvTransaction_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //string sTranID = e.CommandArgument.ToString();

            /*
            //string sTranID = e.CommandName.ToString().ToUpper();
            //string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { '#' });

            //string sTranID = commandArgs[0];
            //string sRefNo = commandArgs[1];
            //string sAction = commandArgs[2];
            */

            //int iTranID = Int32.Parse(sTranID);

            //txtEdit_TranID.Text = sTranID;
            //txtEdit_RefNo.Text = sRefNo;

            //loadTranDetailsList_Edit(iTranID);

            TransactionController tc = new TransactionController();
            DataSet dsTran, dsSelectedUser, dsUser, dsContactPerson, dsSelectedContactPerson; //, dsTranDetail
            DataRow drTran, drUser, drContactPerson;    //, drTranDetail

            switch (e.CommandName.ToString().ToUpper())
            {
                case "E": //edit
                    dsTran = tc.getTransaction(Int32.Parse(e.CommandArgument.ToString()));
                    try
                    {

                        //dsTran = tc.getTransaction(iTranID);

                        //dsTranDetail = tc.getTransactionDetails(0, iTranID);
                        drTran = dsTran.Tables[0].Rows[0];

                        string sTranID = e.CommandArgument.ToString();
                        txtEdit_TranID.Text = sTranID;
                        txtEdit_RefNo.Text = Convert.ToString(drTran["RefNo"]);

                        string sInternalUserID = Convert.ToString(drTran["InternalUserID"]);
                        int iInternalUserID = Int32.Parse(sInternalUserID);
                        string sDeptCode = "";
                        string sContactPersonID = Convert.ToString(drTran["ContactPersonID"]);
                        int iContactPersonID = Int32.Parse(sContactPersonID);
                        int iClientID = 0;

                        if (iInternalUserID != 0)  //Internal User
                        {
                            ddlEdit_TranType.SelectedIndex = 1;
                            ddlEdit_TranType.Enabled = false;

                            txtEdit_InternalUser.Visible = false;
                            txtEdit_Client.Visible = false;
                            lblEdit_ClientName.Visible = false;
                            txtEdit_ClientNameDesc.Visible = false;
                            ddlEdit_ContactPersonName.Visible = false;
                            lblEdit_ContactPersonName.Visible = false;
                            lblEdit_ContactPersonNameValue.Visible = false;
                            lblEdit_ContactPersonNameValue_Old.Visible = false;

                            dsSelectedUser = tc.getUsers(iInternalUserID, "");
                            drUser = dsSelectedUser.Tables[0].Rows[0];
                            //ddlEdit_InternalUserDept.SelectedIndex = ddlEdit_InternalUserName.Items.IndexOf(ddlEdit_InternalUserName.Items.FindByText(Convert.ToString(drTran["DeptCode"])));
                            sDeptCode = Convert.ToString(drUser["DeptCode"]);
                            ddlEdit_InternalUserDept.Items.Insert(0, new ListItem(sDeptCode));
                            ddlEdit_InternalUserDept.SelectedIndex = 0;
                            ddlEdit_InternalUserDept.Enabled = false;

                            dsUser = tc.getUsers(0, sDeptCode);
                            ddlEdit_InternalUserName.DataSource = dsUser;
                            ddlEdit_InternalUserName.DataTextField = "UserName";
                            ddlEdit_InternalUserName.DataValueField = "ID";
                            ddlEdit_InternalUserName.DataBind();

                            ddlEdit_InternalUserName.SelectedIndex = ddlEdit_InternalUserName.Items.IndexOf(ddlEdit_InternalUserName.Items.FindByValue(sInternalUserID));
                            lblEdit_InternalUserName_Old.Text = sInternalUserID;

                            ddlEdit_InternalUserName.Enabled = false;


                        }
                        else //Client
                        {
                            ddlEdit_TranType.SelectedIndex = 0;
                            ddlEdit_TranType.Enabled = false;

                            txtEdit_Client.Visible = false;
                            txtEdit_InternalUser.Visible = false;
                            lblEdit_InternalUserDept.Visible = false;
                            ddlEdit_InternalUserDept.Visible = false;
                            lblEdit_InternalUserName.Visible = false;
                            ddlEdit_InternalUserName.Visible = false;
                            lblEdit_InternalUserName_Old.Visible = false;

                            dsSelectedContactPerson = tc.getContactPerson(0, iContactPersonID);
                            drContactPerson = dsSelectedContactPerson.Tables[0].Rows[0];
                            iClientID = Int32.Parse(drContactPerson["ClientID"].ToString());

                            //txtEdit_ClientNameValue.Text = iClientID.ToString();

                            DataSet dsClient = tc.getClientName(iClientID);
                            if (dsClient.Tables[0].Rows.Count > 0)
                            {
                                DataRow drClient = dsClient.Tables[0].Rows[0];
                                txtEdit_ClientNameDesc.Text = Convert.ToString(drClient["Client Name"].ToString());
                            }
                            txtEdit_ClientNameDesc.Enabled = false;

                            dsContactPerson = tc.getContactPerson(iClientID);

                            ddlEdit_ContactPersonName.DataSource = dsContactPerson;
                            ddlEdit_ContactPersonName.DataTextField = "Contact Person";
                            ddlEdit_ContactPersonName.DataValueField = "ID";
                            ddlEdit_ContactPersonName.DataBind();

                            ddlEdit_ContactPersonName.SelectedIndex = ddlEdit_ContactPersonName.Items.IndexOf(ddlEdit_ContactPersonName.Items.FindByValue(sContactPersonID));
                            lblEdit_ContactPersonNameValue_Old.Text = sContactPersonID;

                            ddlEdit_ContactPersonName.Enabled = false;


                        }

                        mvTransaction.ActiveViewIndex = 3;
                        mvEdit_ItemList.ActiveViewIndex = 1;
                        mvEdit_Action.ActiveViewIndex = 5;

                    }
                    catch { }
                    break;
            }
        }


        protected void Tran_AddItem_ViewTxn(object sender, CommandEventArgs e)
        {
            int ItemID = Int32.Parse(e.CommandArgument.ToString());
            txtFilter_txnItemID_Add.Text = ItemID.ToString();

            mvAdd_AddItem_ViewTxn.ActiveViewIndex = 0;
        }

        protected void btnAdd_ViewTxn_Close_OnClick(object sender, EventArgs e)
        {
            mvAdd_AddItem_ViewTxn.ActiveViewIndex = 1;
        }

        protected void Tran_AddItem_ViewTxn_Edit(object sender, CommandEventArgs e)
        {

            lblEdit_ViewItemTran.Visible = true;
            btnEdit_ViewTxn_Close.Visible = true;

            int ItemID = Int32.Parse(e.CommandArgument.ToString());
            txtFilter_txnItemID_Edit.Text = ItemID.ToString();

            mvEdit_AddItem_ViewTxn.ActiveViewIndex = 0;
        }

        protected void btnEdit_ViewTxn_Close_OnClick(object sender, EventArgs e)
        {
            mvEdit_AddItem_ViewTxn.ActiveViewIndex = 1;
        }

        protected void Tran_SelectedItem_ViewTxn_Edit(object sender, CommandEventArgs e)
        {
            int ItemID = Int32.Parse(e.CommandArgument.ToString());
            txtFilter_txnItemID.Text = ItemID.ToString();

            mvEdit_SelectedItem_ViewTxn.ActiveViewIndex = 0;
        }

        protected void btnEdit_SelectedItem_ViewTxn_Close_OnClick(object sender, EventArgs e)
        {
            mvEdit_SelectedItem_ViewTxn.ActiveViewIndex = 1;
        }


        /*
        protected void btnEdit_ViewItemTxn_Click(object sender, EventArgs e)
        {
            int i = 0;
            for (i = 0; i <= gvShow_Selected_Edit.Rows.Count - 1; i++)
            {
                CheckBox chkRow = (gvShow_Selected_Edit.Rows[i].FindControl("chkShowEditItem") as CheckBox);
                if (chkRow.Checked)
                {
                    txtFilter_txnItemID.Text = gvShow_Selected_Edit.Rows[i].Cells[15].Text;
                    break;
                }
            }

            if (btnEdit_ViewItemTxn.Text == "View Item Transaction")
            {
                mvEdit_Action.ActiveViewIndex = 6;  //View Item Transaction
                btnEdit_ViewItemTxn.Text = "Close";
            }
            else
            {
                mvEdit_Action.ActiveViewIndex = 5;
                btnEdit_ViewItemTxn.Text = "View Item Transaction";

            }

        }
*/

        protected string CheckCheckedBox_Edit()
        {
            string sErrorMsg = "";
            string sItemCode = "";
            int i = 0;

            try
            {

                for (i = 0; i <= gvShow_Selected_Edit.Rows.Count - 1; i++)
                {
                    CheckBox chkRow = (gvShow_Selected_Edit.Rows[i].FindControl("chkShowEditItem") as CheckBox);
                    if (chkRow.Checked)
                    {
                        sItemCode = gvShow_Selected_Edit.Rows[i].Cells[5].Text;

                        if (sItemCode != "")
                        {
                            break;
                        }
                    }
                }

                if (sItemCode == "")
                {
                    sErrorMsg += "Please select at least 1 item!";
                }
            }
            catch
            {
                sErrorMsg += "Please contact IT Support.";
            }

            return sErrorMsg;
        }


        protected void btnEdit_Edit_Click(object sender, EventArgs e)
        {
            string sCheckError = CheckStatus_Edit("E");
            if (sCheckError != "")
            {
                FailureEditText.Text = sCheckError;
                return;
            }

            //Check if at least 1 item is selected
            sCheckError += CheckCheckedBox_Edit();
            if (sCheckError != "")
            {
                FailureEditText.Text = sCheckError;
                return;
            }

            FailureEditText.Text = "";

            int i = 0;
            string sToBorrowDate = "";// txtEdit_ActionE_BorrowDateFrom.Text;
            string sToReturnDate = "";// txtEdit_ActionE_BorrowDateTo.Text;
            string sErrorMsg = "";
            string sNoOfLagDays = "";   //J20180605_20190603

            //Get the first checked value as default value
            for (i = 0; i <= gvShow_Selected_Edit.Rows.Count - 1; i++)
            {
                try
                {

                    CheckBox chkRow = (gvShow_Selected_Edit.Rows[i].FindControl("chkShowEditItem") as CheckBox);
                    if (chkRow.Checked)
                    {
                        sToBorrowDate = gvShow_Selected_Edit.Rows[i].Cells[12].Text;
                        sToReturnDate = gvShow_Selected_Edit.Rows[i].Cells[13].Text;
                        if (gvShow_Selected_Edit.Rows[i].Cells[17].Text.Trim() == "&nbsp;")
                        {
                            sNoOfLagDays = "0";
                        }
                        else
                        {
                            sNoOfLagDays = gvShow_Selected_Edit.Rows[i].Cells[17].Text;  //J20180605_20190603
                        }

                        txtEdit_ActionE_BorrowDateFrom.Text = sToBorrowDate;
                        lblEdit_ActionE_BorrowDateFrom_Old.Text = sToBorrowDate;

                        txtEdit_ActionE_BorrowDateTo.Text = sToReturnDate;
                        lblEdit_ActionE_BorrowDateTo_Old.Text = sToReturnDate;

                        txtEdit_ActionE_NoOfLagDay.Text = sNoOfLagDays; //J20180605_20190603
                        txtEdit_ActionE_NoOfLagDay_Old.Text = sNoOfLagDays; //J20180605_20190603

                        break;
                    }
                }
                catch
                {
                    sErrorMsg += "Please contact IT Support.";
                }
            }

            mvEdit_Action.ActiveViewIndex = 0;
        }

        protected void btnEdit_Renew_Click(object sender, EventArgs e)
        {
            string sCheckError = CheckStatus_Edit("N");
            if (sCheckError != "")
            {
                FailureEditText.Text = sCheckError;
                return;
            }

            //Check if at least 1 item is selected
            sCheckError += CheckCheckedBox_Edit();
            if (sCheckError != "")
            {
                FailureEditText.Text = sCheckError;
                return;
            }

            FailureEditText.Text = "";

            int i = 0;
            string sToBorrowDate = "";// txtEdit_ActionE_BorrowDateFrom.Text;
            string sToReturnDate = "";// txtEdit_ActionE_BorrowDateTo.Text;
            string sNoOfLagDays = "";    //J20180605_20190603

            string sErrorMsg = "";
            for (i = 0; i <= gvShow_Selected_Edit.Rows.Count - 1; i++)
            {
                try
                {


                    //Get the first checked value as default value
                    CheckBox chkRow = (gvShow_Selected_Edit.Rows[i].FindControl("chkShowEditItem") as CheckBox);
                    if (chkRow.Checked)
                    {

                        if (gvShow_Selected_Edit.Rows[i].Cells[12].Text.Trim() != "&nbsp;") //Directly borrow without ToBorrowDate
                        {
                            sToBorrowDate = gvShow_Selected_Edit.Rows[i].Cells[12].Text;    //BorrowDate
                        }
                        else
                        {
                            sToBorrowDate = gvShow_Selected_Edit.Rows[i].Cells[14].Text;
                        }

                        sToReturnDate = gvShow_Selected_Edit.Rows[i].Cells[13].Text;
                        sNoOfLagDays = gvShow_Selected_Edit.Rows[i].Cells[17].Text;  //J20180605_20190603

                        txtEdit_ActionN_BorrowDateFrom.Text = sToBorrowDate;

                        txtEdit_ActionN_BorrowDateTo.Text = sToReturnDate;
                        lblEdit_ActionN_BorrowDateTo_Old.Text = sToReturnDate;

                        txtEdit_ActionN_NoOfLagDay.Text = sNoOfLagDays;
                        txtEdit_ActionN_NoOfLagDay_Old.Text = sNoOfLagDays;

                        break;

                    }
                }
                catch
                {
                    sErrorMsg += "Please contact IT Support.";
                }
            }

            mvEdit_Action.ActiveViewIndex = 3;
        }

        protected void btnEdit_Borrow_Click(object sender, EventArgs e)
        {
            string sCheckError = CheckStatus_Edit("B");
            if (sCheckError != "")
            {
                FailureEditText.Text = sCheckError;
                return;
            }

            //Check if at least 1 item is selected
            sCheckError += CheckCheckedBox_Edit();
            if (sCheckError != "")
            {
                FailureEditText.Text = sCheckError;
                return;
            }

            FailureEditText.Text = "";

            //Setting default value
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now.AddDays(7);

            txtEdit_ActionB_BorrowDateFrom.Text = dtStart.ToString("yyyy/MM/dd");
            //txtEdit_ActionB_BorrowDateTo.Text = dtEnd.ToString("yyyy/MM/dd");
            //txtEdit_ActionB_BorrowDateFrom.Enabled = false;

            mvEdit_Action.ActiveViewIndex = 2;
        }

        protected void btnEdit_Return_Click(object sender, EventArgs e)
        {
            string sCheckError = CheckStatus_Edit("R");
            if (sCheckError != "")
            {
                FailureEditText.Text = sCheckError;
                return;
            }

            //Check if at least 1 item is selected
            sCheckError += CheckCheckedBox_Edit();
            if (sCheckError != "")
            {
                FailureEditText.Text = sCheckError;
                return;
            }

            FailureEditText.Text = "";

            //Setting default value
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now.AddDays(7);

            //txtEdit_ActionB_BorrowDateFrom.Text = dtStart.ToString("yyyy/MM/dd");
            txtEdit_ActionR_ReturnDate.Text = dtStart.ToString("yyyy/MM/dd");

            mvEdit_Action.ActiveViewIndex = 4;
        }


        protected string CheckStatus_Edit(string sTranAction)
        {
            string sErrorMsg = "";
            string sAction = "";
            string sItemCode = "";
            string sItemName = "";
            int i = 0;

            try
            {

                for (i = 0; i <= gvShow_Selected_Edit.Rows.Count - 1; i++)
                {
                    CheckBox chkRow = (gvShow_Selected_Edit.Rows[i].FindControl("chkShowEditItem") as CheckBox);
                    if (chkRow.Checked)
                    {
                        sAction = gvShow_Selected_Edit.Rows[i].Cells[3].Text;
                        sItemCode = gvShow_Selected_Edit.Rows[i].Cells[5].Text;
                        sItemName = gvShow_Selected_Edit.Rows[i].Cells[6].Text;

                        //Handling the status
                        if (sTranAction == "E" && sAction != "BOOK")
                        {
                            sErrorMsg += sItemCode + " " + sItemName + " - Only BOOK Status is allowed to be edited." + "<br>";
                        }
                        if (sTranAction == "C" && sAction != "BOOK")
                        {
                            sErrorMsg += sItemCode + " " + sItemName + " - Only BOOK Status is allowed to be cancelled." + "<br>";
                        }
                        if (sTranAction == "B" && sAction != "BOOK")
                        {
                            sErrorMsg += sItemCode + " " + sItemName + " - Only BOOK Status is allowed to be borrowed." + "<br>";
                        }
                        if (sTranAction == "N" && sAction != "BORROW")
                        {
                            sErrorMsg += sItemCode + " " + sItemName + " - Only BORROW Status is allowed to be renewed." + "<br>";
                        }
                        if (sTranAction == "R" && sAction != "BORROW")
                        {
                            sErrorMsg += sItemCode + " " + sItemName + " - Only BORROW Status is allowed to be returned." + "<br>";
                        }
                    }
                }
            }
            catch
            {
                sErrorMsg += "Please contact IT Support.";
            }

            return sErrorMsg;
        }

        protected string CheckFieldsAddItem_Add(string ItemID, int iBorrowingQty)
        {
            string sErrorMsg = "";

            try
            {
                //foreach (GridViewRow rwo in gvShow_Selected.Rows)
                foreach (GridViewRow rwo in gvSelected.Rows)
                {
                    int iItemID = Int32.Parse(rwo.Cells[1].Text);


                    if (Int32.Parse(ItemID) == iItemID)
                    {
                        string sItemNo = rwo.Cells[2].Text;
                        string sItemDesc = rwo.Cells[3].Text;
                        int iAvailableQty = Int32.Parse(rwo.Cells[4].Text);
                        //int iBorrowingQty = Int32.Parse(rwo.Cells[5].Text);
                        iBorrowingQty += Int32.Parse(rwo.Cells[5].Text);

                        //iBorrowingQty += Int32.Parse(BorrowingQty);

                        if (iBorrowingQty > iAvailableQty)
                        {
                            sErrorMsg += sItemNo + " " + sItemDesc + " - Borrowing Qty cannot be greater than Available Qty. Item cannot be added." + "<br>";
                            break;
                        }


                    }
                    /*
                    //foreach (GridViewRow rwc in gvShow_Selected.Rows)
                    foreach (GridViewRow rwc in gvSelected.Rows)
                    {
                        if (Int32.Parse(rwc.Cells[1].Text) == iItemID)
                        {
                            iBorrowingQty += Int32.Parse(rwc.Cells[5].Text);
                        }
                    }
                    */


                }
            }
            catch
            {
                sErrorMsg += "Please contact IT Support. ";
            }
            return sErrorMsg;
        }

        protected string CheckFieldsAddItem_Edit(string ItemID, int iBorrowingQty)
        {
            string sErrorMsg = "";
            try
            {
                //foreach (GridViewRow rwo in gvShow_Selected.Rows)
                foreach (GridViewRow rwo in gvEdit_Selected.Rows)
                {
                    int iItemID = Int32.Parse(rwo.Cells[1].Text);


                    if (Int32.Parse(ItemID) == iItemID)
                    {
                        string sItemNo = rwo.Cells[2].Text;
                        string sItemDesc = rwo.Cells[3].Text;
                        int iAvailableQty = Int32.Parse(rwo.Cells[4].Text);
                        //int iBorrowingQty = Int32.Parse(rwo.Cells[5].Text);
                        //iBorrowingQty += Int32.Parse(BorrowingQty);
                        iBorrowingQty += Int32.Parse(rwo.Cells[5].Text);

                        if (iBorrowingQty > iAvailableQty)
                        {
                            sErrorMsg += sItemNo + " " + sItemDesc + " - Borrowing Qty cannot be greater than Available Qty. Item cannot be added." + "<br>";
                            break;
                        }
                    }
                    /*
                    //foreach (GridViewRow rwc in gvShow_Selected.Rows)
                    foreach (GridViewRow rwc in gvSelected.Rows)
                    {
                        if (Int32.Parse(rwc.Cells[1].Text) == iItemID)
                        {
                            iBorrowingQty += Int32.Parse(rwc.Cells[5].Text);
                        }
                    }
                    */


                }
            }
            catch
            {
                sErrorMsg += "Please contact IT Support. ";
            }
            return sErrorMsg;
        }


        protected string CheckFields_Add()
        {
            string sErrorMsg = "";
            try
            {

                int iTranType = Int32.Parse(ddlTranType_Add.SelectedValue);

                if (iTranType == 1) //Client
                {
                    if (txtAdd_ClientNameDescValue.Text == "" || lblAdd_ContactPersonNameValue.Text == "")
                    {
                        sErrorMsg += "Please select Client Name and Contact Person Name. " + "<br>";
                    }
                }
                else //Internal User
                {
                    if (lblAdd_InternalUserDeptValue.Text == "" || lblAdd_InternalUserNameValue.Text == "")
                    {
                        sErrorMsg += "Please select Internal User Dept and Internal User Name. " + "<br>";
                    }
                }


                //int iCntItem = gvShow_Selected.Rows.Count;
                int iCntItem = gvSelected.Rows.Count;
                if (iCntItem == 0)
                {
                    sErrorMsg += "Please select at least 1 item. " + "<br>";
                }

                /* HEIDI 20171006
                //foreach (GridViewRow rwo in gvShow_Selected.Rows)
                foreach (GridViewRow rwo in gvSelected.Rows)
                {
                    int iItemID = Int32.Parse(rwo.Cells[1].Text);
                    string sItemNo = rwo.Cells[2].Text;
                    string sItemDesc = rwo.Cells[3].Text;
                    int iAvailableQty= Int32.Parse(rwo.Cells[4].Text);
                    int iBorrowingQty = 0;

                    //foreach (GridViewRow rwc in gvShow_Selected.Rows)
                    foreach (GridViewRow rwc in gvSelected.Rows)
                    {
                        if (Int32.Parse(rwc.Cells[1].Text) == iItemID)
                        {
                            iBorrowingQty += Int32.Parse(rwc.Cells[5].Text);
                        }
                    }

                    if (iBorrowingQty> iAvailableQty)
                    {
                        sErrorMsg += sItemNo + " " + sItemDesc + " - Borrowing Qty cannot be greater than Available Qty. Transaction cannot be created." + "<br>";
                        break;
                    }
                }
                */

                /*
                foreach (GridViewRow row in gvShow_Selected_Edit.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkShowEditItem") as CheckBox);
                        //CheckBox chkRow = (row.Cells[0].Controls[0] as CheckBox);
                        if (chkRow.Checked)
                        {
                            iTranDetailID = Int32.Parse(row.Cells[1].Text);
                            sAction = row.Cells[2].Text;

                            sItemCode = row.Cells[4].Text;
                            sItemName = row.Cells[5].Text;
                            iItemID = Int32.Parse(row.Cells[15].Text);

                            sOldToBorrowDate = row.Cells[11].Text;
                            sOldToReturnDate = row.Cells[12].Text;

                            if (sNewToBorrowDate == sOldToBorrowDate && sNewToReturnDate == sOldToReturnDate)
                            {
                                sErrorMsg += sItemCode + " " + sItemName + " - Same Requesting Date Range. The record won't be updated." + "<br>";
                            }
*/


            }
            catch
            {
                sErrorMsg += "Please contact IT Support. ";
            }
            return sErrorMsg;
        }

        protected string CheckFields_Edit(string sTranAction)
        {
            string sErrorMsg = "";

            try
            {
                int iTranDetailID = 0;
                string sAction = "";
                int i = 0;

                string sItemCode = "";
                string sItemName = "";
                int iItemID = 0;
                bool bIsAvailable = false;

                string sOldToBorrowDate = "";
                string sOldToReturnDate = "";
                string sNewToBorrowDate = "";
                string sNewToReturnDate = "";
                int iOldNoOfLagDay = 0; //J20180605_20190603
                int iNewNoOfLagDay = 0; //J20180605_20190603

                string UID = "";
                if (Session["UID"] != null)
                    UID = Session["UID"].ToString();

                //Not updating if the date has not changed
                switch (sTranAction.ToString().ToUpper())
                {
                    case "E":    //Edit Booking
                        sNewToBorrowDate = txtEdit_ActionE_BorrowDateFrom.Text;
                        sNewToReturnDate = txtEdit_ActionE_BorrowDateTo.Text;
                        iOldNoOfLagDay = Int32.Parse(txtEdit_ActionE_NoOfLagDay_Old.Text);  //J20180605_20190603
                        iNewNoOfLagDay = Int32.Parse(txtEdit_ActionE_NoOfLagDay.Text);  //J20180605_20190603
                        break;
                    /*
                case "B":   //Borrow
                    sNewToBorrowDate = txtEdit_ActionB_BorrowDateFrom.Text;
                    sNewToReturnDate = txtEdit_ActionB_BorrowDateTo.Text;
                    iNoOfLagDay = Int32.Parse(txtEdit_ActionB_NoOfLagDay.Text);
                    break;
                    */
                    case "N":   //Renew
                        sNewToBorrowDate = txtEdit_ActionN_BorrowDateFrom.Text;
                        sNewToReturnDate = txtEdit_ActionN_BorrowDateTo.Text;
                        iOldNoOfLagDay = Int32.Parse(txtEdit_ActionN_NoOfLagDay_Old.Text);  //J20180605_20190603
                        iNewNoOfLagDay = Int32.Parse(txtEdit_ActionN_NoOfLagDay.Text);  //J20180605_20190603
                        break;
                }

                foreach (GridViewRow row in gvShow_Selected_Edit.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkShowEditItem") as CheckBox);
                        //CheckBox chkRow = (row.Cells[0].Controls[0] as CheckBox);
                        if (chkRow.Checked)
                        {
                            iTranDetailID = Int32.Parse(row.Cells[2].Text);
                            sAction = row.Cells[3].Text;

                            sItemCode = row.Cells[5].Text;
                            sItemName = row.Cells[6].Text;
                            iItemID = Int32.Parse(row.Cells[16].Text);

                            //sOldToBorrowDate = row.Cells[12].Text;
                            if (row.Cells[12].Text.Trim() != "&nbsp;") //Directly borrow without ToBorrowDate
                            {
                                sOldToBorrowDate = row.Cells[12].Text;    //BorrowDate
                            }
                            else
                            {
                                sOldToBorrowDate = row.Cells[14].Text;
                            }
                            sOldToReturnDate = row.Cells[13].Text;

                            if (sNewToBorrowDate == sOldToBorrowDate && sNewToReturnDate == sOldToReturnDate && iOldNoOfLagDay == iNewNoOfLagDay)  //J20180605_20190603
                            {
                                sErrorMsg += sItemCode + " " + sItemName + " - Same Requesting Date Range. The record won't be updated." + "<br>";
                            }
                            else
                            {
                                DataSet di;
                                DataRow dr;
                                di = new ItemController().getAvailableItems(iItemID, sNewToBorrowDate, sNewToReturnDate, iNewNoOfLagDay, iTranDetailID, Session["WindowAuthID"].ToString());
                                if (di.Tables[0].Rows.Count > 0)
                                {
                                    dr = di.Tables[0].Rows[0];
                                    bIsAvailable = Convert.ToBoolean(dr["Available"]);

                                    if (bIsAvailable == false)
                                    {
                                        sErrorMsg += "Item " + sItemCode + " " + sItemName + " is not available for borrowing within the requesting date range. ";
                                    }
                                }
                            }

                        }
                    }
                }
            }
            catch
            {
                sErrorMsg += "Please contact IT Support.";
            }

            /*
            for (i = 0; i <= gvShow_Selected_Edit.Rows.Count - 1; i++)
            {
                CheckBox chkRow = (gvShow_Selected_Edit.Rows[i].FindControl("chkShowEditItem") as CheckBox);
                if (chkRow.Checked)
                {
                    iTranDetailID = Int32.Parse(gvShow_Selected_Edit.Rows[i].Cells[1].Text);
                    sAction = gvShow_Selected_Edit.Rows[i].Cells[2].Text;

                    sItemCode = gvShow_Selected_Edit.Rows[i].Cells[4].Text;
                    sItemName = gvShow_Selected_Edit.Rows[i].Cells[5].Text;
                    iItemID = Int32.Parse(gvShow_Selected_Edit.Rows[i].Cells[15].Text);

                    sOldToBorrowDate = gvShow_Selected_Edit.Rows[i].Cells[11].Text;
                    sOldToReturnDate = gvShow_Selected_Edit.Rows[i].Cells[12].Text;

                    if (sNewToBorrowDate == sOldToBorrowDate && sNewToReturnDate == sOldToReturnDate)
                    {
                        sErrorMsg += sItemCode + " " + sItemName + " - Same Requesting Date Range. The record won't be updated." + "<br>";
                    }
                    //Check the availability if the date is changed
                    else
                    {
                        DataSet di;
                        DataRow dr;
                        di = new ItemController().getAvailableItems(iItemID, sNewToBorrowDate, sNewToReturnDate, iNoOfLagDay, iTranDetailID);
                        if (di.Tables[0].Rows.Count > 0)
                        {
                            dr = di.Tables[0].Rows[0];
                            bIsAvailable = Convert.ToBoolean(dr["Available"]);

                            if (bIsAvailable == false)
                            {
                                sErrorMsg += "Item " + sItemCode + " " + sItemName + " is not available for borrowing within the requesting date range. ";
                            }
                        }
                    }
                }
            }
        }
        catch
        {
            sErrorMsg += "Please contact IT Support.";
        }
        */
            return sErrorMsg;
        }

        /*
        private List<string> GetSiteId()
        {
            var listIds = new List<string>();

            for (int i = 0; i < gvShow_Selected_Edit.Rows.Count - 1; i++)
            {
                CheckBox chkRow = (gvShow_Selected_Edit.Rows[i].FindControl("chkShowEditItem") as CheckBox);
                if (chkRow.Checked)
                {
                    listIds.Add(gvShow_Selected_Edit.Rows[i].Cells[11].Text);
                }
            }
            var distinctRowsToBorrowDate = (from r in listIds
                                            select r).Distinct();
            return listIds;
        }
        */



        protected void btnEdit_ActionE_Save_Click(object sender, EventArgs e)
        {
            try
            {
                string sCheckError = CheckFields_Edit("E");

                if (sCheckError != "")
                {
                    FailureEditText.Text = sCheckError;
                    return;
                }
                FailureEditText.Text = "";  //Clear the error msg

                string sErrorMsg = "";

                int iTranDetailID = 0;
                int iTranID = Int32.Parse(txtEdit_TranID.Text);
                //int i = 0;

                string sToBorrowDate = txtEdit_ActionE_BorrowDateFrom.Text;
                string sToReturnDate = txtEdit_ActionE_BorrowDateTo.Text;
                int iNoOfLagDay = Int32.Parse(txtEdit_ActionE_NoOfLagDay.Text); //J20180605_20190603 

                //string sItemCode = "";
                //string sItemName = "";

                //int iItemID = 0;
                //bool bIsAvailable=false;

                string UID = "";
                if (Session["UID"] != null)
                    UID = Session["UID"].ToString();

                try
                {
                    foreach (GridViewRow row in gvShow_Selected_Edit.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chkShowEditItem") as CheckBox);
                            //CheckBox chkRow = (row.Cells[0].Controls[0] as CheckBox);
                            if (chkRow.Checked)
                            {
                                iTranDetailID = Int32.Parse(row.Cells[2].Text);
                                DataSet ds;
                                //ds = new TransactionController().updateTransactionDetail(iTranDetailID, 0, 0, 1, 1, false, false, sToBorrowDate, sToReturnDate, UID, "E");
                                //ds = new TransactionController().updateTransactionDetail(iTranDetailID, iTranID, 0, 1, 1, false, false, sToBorrowDate, sToReturnDate, UID, "E");
                                ds = new TransactionController().updateTransactionDetail(iTranDetailID, iTranID, 0, 1, 1, false, false, sToBorrowDate, sToReturnDate, UID, "E", iNoOfLagDay);    //J20180605_20190603


                            }
                            //int iTranID = Int32.Parse(txtEdit_TranID.Text);
                            loadTranDetailsList_Edit(iTranID);
                        }
                    }
                }
                catch
                {
                    sErrorMsg += "Please contact IT Support.";
                }

                /*
                for (i = 0; i <= gvShow_Selected_Edit.Rows.Count - 1; i++)
                {
                    try
                    {
                        CheckBox chkRow = (gvShow_Selected_Edit.Rows[i].FindControl("chkShowEditItem") as CheckBox);
                        if (chkRow.Checked)
                        {
                            //iTranDetailID = Int32.Parse(gvShow_Selected_Edit.Rows[i].FindControl("ID").ToString());
                            iTranDetailID = Int32.Parse(gvShow_Selected_Edit.Rows[i].Cells[1].Text);
                            //sAction = gvShow_Selected_Edit.Rows[i].FindControl("Action").ToString();
                            //sAction = gvShow_Selected_Edit.Rows[i].Cells[2].Text;

                            //sItemCode = gvShow_Selected_Edit.Rows[i].Cells[4].Text;
                            //sItemName = gvShow_Selected_Edit.Rows[i].Cells[5].Text;
                            //iItemID = Int32.Parse(gvShow_Selected_Edit.Rows[i].Cells[15].Text);

                            DataSet ds;
                            ds = new TransactionController().updateTransactionDetail(iTranDetailID, 0, 0, 1, 1, false, false, sToBorrowDate, sToReturnDate, UID, "E");
                        }
                        int iTranID = Int32.Parse(txtEdit_TranID.Text);
                        loadTranDetailsList_Edit(iTranID);
                    }
                    catch
                    {
                        sErrorMsg += "Please contact IT Support.";
                    }

                }
                */




            }
            catch
            {
            }

            mvEdit_Action.ActiveViewIndex = 5;

        }

        protected void btnEdit_ActionC_Save_Click(object sender, EventArgs e)
        {

            int iTranDetailID = 0;
            int iTranID = Int32.Parse(txtEdit_TranID.Text);
            //string sAction = "";
            //int i = 0;

            string sCheckError = CheckStatus_Edit("C");
            if (sCheckError != "")
            {
                FailureEditText.Text = sCheckError;
                return;
            }

            //Check if at least 1 item is selected
            sCheckError += CheckCheckedBox_Edit();
            if (sCheckError != "")
            {
                FailureEditText.Text = sCheckError;
                return;
            }

            FailureEditText.Text = "";  //Clear the error msg

            string sErrorMsg = "";

            string UID = "";
            if (Session["UID"] != null)
                UID = Session["UID"].ToString();

            try
            {
                foreach (GridViewRow row in gvShow_Selected_Edit.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkShowEditItem") as CheckBox);
                        //CheckBox chkRow = (row.Cells[0].Controls[0] as CheckBox);
                        if (chkRow.Checked)
                        {
                            iTranDetailID = Int32.Parse(row.Cells[2].Text);
                            DataSet ds;
                            //ds = new TransactionController().updateTransactionDetail(iTranDetailID, 0, 0, 1, 1, false, false, "", "", UID, "C");
                            //ds = new TransactionController().updateTransactionDetail(iTranDetailID, iTranID, 0, 1, 1, false, false, "", "", UID, "C");
                            ds = new TransactionController().updateTransactionDetail(iTranDetailID, iTranID, 0, 1, 1, false, false, "", "", UID, "C", 0);   //J20180605_20190603

                        }
                        //int iTranID = Int32.Parse(txtEdit_TranID.Text);
                        loadTranDetailsList_Edit(iTranID);
                    }
                }
            }
            catch
            {
                sErrorMsg += "Please contact IT Support.";
            }


            /*
            for (i = 0; i <= gvShow_Selected_Edit.Rows.Count - 1; i++)
            {

                string sErrorMsg = "";

                string UID = "";
                if (Session["UID"] != null)
                    UID = Session["UID"].ToString();

                try
                {

                    CheckBox chkRow = (gvShow_Selected_Edit.Rows[i].FindControl("chkShowEditItem") as CheckBox);
                    if (chkRow.Checked)
                    {
                        //iTranDetailID = Int32.Parse(gvShow_Selected_Edit.Rows[i].FindControl("ID").ToString());
                        iTranDetailID = Int32.Parse(gvShow_Selected_Edit.Rows[i].Cells[1].Text);
                        //sAction = gvShow_Selected_Edit.Rows[i].FindControl("Action").ToString();
                        sAction = gvShow_Selected_Edit.Rows[i].Cells[2].Text;

                        if (sAction == "BOOK")
                        {

                            DataSet ds;
                            ds = new TransactionController().updateTransactionDetail(iTranDetailID, 0, 0, 0, 0, false, false, "", "", UID, "C");

                        }
                    }

                }
                catch
                {
                    sErrorMsg += "Please contact IT Support.";
                }

            }
            

            int iTranID = Int32.Parse(txtEdit_TranID.Text);
            loadTranDetailsList_Edit(iTranID);
            */
        }

        protected void btnEdit_ActionB_Save_Click(object sender, EventArgs e)
        {

            string sCheckError = CheckFields_Edit("B");

            if (sCheckError != "")
            {
                FailureEditText.Text = sCheckError;
                return;
            }
            FailureEditText.Text = "";  //Clear the error msg

            string sErrorMsg = "";

            int iTranID = Int32.Parse(txtEdit_TranID.Text);
            int iTranDetailID = 0;
            //int i = 0;

            string sToBorrowDate = txtEdit_ActionB_BorrowDateFrom.Text;
            //string sToReturnDate = txtEdit_ActionB_BorrowDateTo.Text;
            //int iNoOfLagDay = Int32.Parse(txtEdit_ActionB_NoOfLagDay.Text);

            string UID = "";
            if (Session["UID"] != null)
                UID = Session["UID"].ToString();


            try
            {
                foreach (GridViewRow row in gvShow_Selected_Edit.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkShowEditItem") as CheckBox);
                        //CheckBox chkRow = (row.Cells[0].Controls[0] as CheckBox);
                        if (chkRow.Checked)
                        {
                            iTranDetailID = Int32.Parse(row.Cells[2].Text);
                            DataSet ds;
                            //ds = new TransactionController().updateTransactionDetail(iTranDetailID, 0, 0, 1, 1, false, false, "", sToReturnDate, UID, "B");
                            //ds = new TransactionController().updateTransactionDetail(iTranDetailID, 0, 0, 1, 1, false, false, sToBorrowDate, "", UID, "B");
                            //ds = new TransactionController().updateTransactionDetail(iTranDetailID, iTranID, 0, 1, 1, false, false, sToBorrowDate, "", UID, "B"); 
                            ds = new TransactionController().updateTransactionDetail(iTranDetailID, iTranID, 0, 1, 1, false, false, sToBorrowDate, "", UID, "B", 0);   //J20180605_20190603

                        }
                        //int iTranID = Int32.Parse(txtEdit_TranID.Text);
                        loadTranDetailsList_Edit(iTranID);
                    }
                }
            }
            catch
            {
                sErrorMsg += "Please contact IT Support.";
            }

            /*
            for (i = 0; i <= gvShow_Selected_Edit.Rows.Count - 1; i++)
            {
                try
                {
                    CheckBox chkRow = (gvShow_Selected_Edit.Rows[i].FindControl("chkShowEditItem") as CheckBox);
                    if (chkRow.Checked)
                    {
                        //iTranDetailID = Int32.Parse(gvShow_Selected_Edit.Rows[i].FindControl("ID").ToString());
                        iTranDetailID = Int32.Parse(gvShow_Selected_Edit.Rows[i].Cells[1].Text);
                        //sAction = gvShow_Selected_Edit.Rows[i].FindControl("Action").ToString();
                        //sAction = gvShow_Selected_Edit.Rows[i].Cells[2].Text;

                        DataSet ds;
                        ds = new TransactionController().updateTransactionDetail(iTranDetailID, 0, 0, 0, 0, false, false, "", sToReturnDate, UID, "B");

                    }
                    int iTranID = Int32.Parse(txtEdit_TranID.Text);
                    loadTranDetailsList_Edit(iTranID);
                }
                catch
                {
                    sErrorMsg += "Please contact IT Support.";
                }
            }
            */
            mvEdit_Action.ActiveViewIndex = 5;
        }

        protected void btnEdit_ActionR_Save_Click(object sender, EventArgs e)
        {

            string sCheckError = CheckStatus_Edit("R");
            if (sCheckError != "")
            {
                FailureEditText.Text = sCheckError;
                return;
            }

            //Check if at least 1 item is selected
            sCheckError += CheckCheckedBox_Edit();
            if (sCheckError != "")
            {
                FailureEditText.Text = sCheckError;
                return;
            }

            FailureEditText.Text = "";  //Clear the error msg

            string sErrorMsg = "";

            int iTranDetailID = 0;
            int iTranID = Int32.Parse(txtEdit_TranID.Text);

            //string sAction = "";
            //int i = 0;
            string sReturnDate = txtEdit_ActionR_ReturnDate.Text;

            string UID = "";
            if (Session["UID"] != null)
                UID = Session["UID"].ToString();

            try
            {
                foreach (GridViewRow row in gvShow_Selected_Edit.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkShowEditItem") as CheckBox);
                        //CheckBox chkRow = (row.Cells[0].Controls[0] as CheckBox);
                        if (chkRow.Checked)
                        {
                            iTranDetailID = Int32.Parse(row.Cells[2].Text);
                            DataSet ds;
                            //ds = new TransactionController().updateTransactionDetail(iTranDetailID, 0, 0, 2, 1, false, false, "", sReturnDate, UID, "R");
                            //ds = new TransactionController().updateTransactionDetail(iTranDetailID, iTranID, 0, 2, 1, false, false, "", sReturnDate, UID, "R");
                            ds = new TransactionController().updateTransactionDetail(iTranDetailID, iTranID, 0, 2, 1, false, false, "", sReturnDate, UID, "R", 0); //J20180605_20190603

                        }
                        //int iTranID = Int32.Parse(txtEdit_TranID.Text);
                        loadTranDetailsList_Edit(iTranID);
                    }
                }
            }
            catch
            {
                sErrorMsg += "Please contact IT Support.";
            }


            /*
            for (i = 0; i <= gvShow_Selected_Edit.Rows.Count - 1; i++)
            {

                string sErrorMsg = "";

                string UID = "";
                if (Session["UID"] != null)
                    UID = Session["UID"].ToString();

                try
                {

                    CheckBox chkRow = (gvShow_Selected_Edit.Rows[i].FindControl("chkShowEditItem") as CheckBox);
                    if (chkRow.Checked)
                    {
                        //iTranDetailID = Int32.Parse(gvShow_Selected_Edit.Rows[i].FindControl("ID").ToString());
                        iTranDetailID = Int32.Parse(gvShow_Selected_Edit.Rows[i].Cells[1].Text);
                        //sAction = gvShow_Selected_Edit.Rows[i].FindControl("Action").ToString();
                        sAction = gvShow_Selected_Edit.Rows[i].Cells[2].Text;

                        if (sAction == "BORROW")
                        {

                            DataSet ds;
                            ds = new TransactionController().updateTransactionDetail(iTranDetailID, 0, 0, 0, 0, false, false, "", "", UID, "R");

                        }


                    }

                }
                catch
                {
                    sErrorMsg += "Please contact IT Support.";
                }

            }
            int iTranID = Int32.Parse(txtEdit_TranID.Text);
            loadTranDetailsList_Edit(iTranID);
            */
            mvEdit_Action.ActiveViewIndex = 5;
        }


        protected void btnEdit_ActionN_Save_Click(object sender, EventArgs e)
        {
            string sCheckError = CheckFields_Edit("N");

            if (sCheckError != "")
            {
                FailureEditText.Text = sCheckError;
                return;
            }

            FailureEditText.Text = "";  //Clear the error msg

            string sErrorMsg = "";

            int iTranDetailID = 0;
            int iTranID = Int32.Parse(txtEdit_TranID.Text);
            //int i = 0;

            string sToReturnDate = txtEdit_ActionN_BorrowDateTo.Text;
            int iNoOfLagDay = Int32.Parse(txtEdit_ActionN_NoOfLagDay.Text);  //J20180605_20190603


            string UID = "";
            if (Session["UID"] != null)
                UID = Session["UID"].ToString();


            try
            {
                foreach (GridViewRow row in gvShow_Selected_Edit.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkShowEditItem") as CheckBox);
                        //CheckBox chkRow = (row.Cells[0].Controls[0] as CheckBox);
                        if (chkRow.Checked)
                        {
                            iTranDetailID = Int32.Parse(row.Cells[2].Text);
                            DataSet ds;
                            //ds = new TransactionController().updateTransactionDetail(iTranDetailID, 0, 0, 2, 1, false, false, "", sToReturnDate, UID, "N");
                            //ds = new TransactionController().updateTransactionDetail(iTranDetailID, iTranID, 0, 2, 1, false, false, "", sToReturnDate, UID, "N");
                            ds = new TransactionController().updateTransactionDetail(iTranDetailID, iTranID, 0, 2, 1, false, false, "", sToReturnDate, UID, "N", iNoOfLagDay);   //J20180605_20190603
                        }
                        //int iTranID = Int32.Parse(txtEdit_TranID.Text);
                        loadTranDetailsList_Edit(iTranID);
                    }
                }
            }
            catch
            {
                sErrorMsg += "Please contact IT Support.";
            }

            /*
            for (i = 0; i <= gvShow_Selected_Edit.Rows.Count - 1; i++)
            {
                try
                {

                    CheckBox chkRow = (gvShow_Selected_Edit.Rows[i].FindControl("chkShowEditItem") as CheckBox);
                    if (chkRow.Checked)
                    {
                        //iTranDetailID = Int32.Parse(gvShow_Selected_Edit.Rows[i].FindControl("ID").ToString());
                        iTranDetailID = Int32.Parse(gvShow_Selected_Edit.Rows[i].Cells[1].Text);
                        //sAction = gvShow_Selected_Edit.Rows[i].FindControl("Action").ToString();
                        //sAction = gvShow_Selected_Edit.Rows[i].Cells[2].Text;

                        DataSet ds;
                        ds = new TransactionController().updateTransactionDetail(iTranDetailID, 0, 0, 0, 0, false, false, "", sToReturnDate, UID, "N");
                    }
                    int iTranID = Int32.Parse(txtEdit_TranID.Text);
                    loadTranDetailsList_Edit(iTranID);
                }
                catch
                {
                    sErrorMsg += "Please contact IT Support.";
                }

            }
            */
            mvEdit_Action.ActiveViewIndex = 5;
        }

        protected void btnEdit_ActionE_Close_Click(object sender, EventArgs e)
        {
            mvEdit_Action.ActiveViewIndex = 5;
        }

        protected void btnEdit_ActionN_Close_Click(object sender, EventArgs e)
        {
            mvEdit_Action.ActiveViewIndex = 5;
        }

        protected void btnEdit_ActionB_Close_Click(object sender, EventArgs e)
        {
            mvEdit_Action.ActiveViewIndex = 5;
        }

        protected void btnEdit_ActionR_Close_Click(object sender, EventArgs e)
        {
            mvEdit_Action.ActiveViewIndex = 5;
        }

        protected void btnEdit_Cancel_Click(object sender, EventArgs e)
        {

        }

        protected void gvClient_RowCommand(object sender, GridViewCommandEventArgs e)
        {


        }

        protected void gvClient_RowCommand_edit(object sender, GridViewCommandEventArgs e)
        {


        }

        protected void gvItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {


        }

        protected void gvItem_RowCommand_edit(object sender, GridViewCommandEventArgs e)
        {


        }



        protected void SetupDdlFormStatusUpdate()
        {




        }


        protected void ExportCSVBtn_Click(object sender, EventArgs e)
        {

        }



        protected void GridViewItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlFilter_InternalUserName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        /*
        protected void CustomValidator_EditTran(object source, ServerValidateEventArgs args)
        {
            bool isAnySelected = Convert.ToBoolean(GridViewEdit_Items.);

            foreach (GridViewRow row in GridViewEdit_Items.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkEditItem");
                if (ChkBoxRows.Checked = true)


            }

            bool isAnySelected = checkBoxList.Items.Any(i => i.Selected);
            args.IsValid = CheckBox1.Checked;
        }
        */

        /*
    protected void showPrintOutForm(string sID, string sRefNo)
    {
        try
        {
            TransactionController tc = new TransactionController();

            //DataSet dsHashName;
            //DataRow drHashName;

            string sRptFullLocation = string.Empty;
            string sReportFolder = "/" + ConfigurationSettings.AppSettings["SsrsFolder"].ToString() + "/";

            string[] sParaField = new string[2];
            string[] sParaValue = new string[2];

            string sFormat = "PDF";

            string sExportFileName = sID + "_" + sRefNo;// +DateTime.Now.ToString("yyyyMMddhhmmss", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
            sRptFullLocation = sReportFolder + ConfigurationSettings.AppSettings["SsrsReportConfirmationNotePrintOut"].ToString();

            sParaField[0] = "sID";
            sParaValue[0] = sID;

            sParaField[1] = "sRefNo";
            sParaValue[1] = sRefNo;

            new ReportController().getSsrsReportViewer(rptViewer, sParaField, sParaValue, sRptFullLocation, sExportFileName, sFormat);

            ifrmPrintRBS.Attributes["src"] = "../" + ConfigurationSettings.AppSettings["PrintOutPath"].ToString() + sExportFileName + "." + sFormat;
        }
        catch (Exception ex)
        {
            lblReportErrorMessage.Text = ex.Message;
        }
    }
    */
        protected void showPrintOutForm(string sID, string sRefNo)
        {
            try
            {
                TransactionController tc = new TransactionController();

                //DataSet dsHashName;
                //DataRow drHashName;

                string sRptFullLocation = string.Empty;
                string sReportFolder = "/" + ConfigurationSettings.AppSettings["SsrsFolder"].ToString() + "/";

                string[] sParaField = new string[2];
                string[] sParaValue = new string[2];

                //string sFormat = "PDF";

                //string sExportFileName = sID + "_" + sRefNo;// +DateTime.Now.ToString("yyyyMMddhhmmss", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                sRptFullLocation = sReportFolder + ConfigurationSettings.AppSettings["SsrsReportConfirmationNotePrintOut"].ToString();

                sParaField[0] = "sID";
                sParaValue[0] = sID;

                sParaField[1] = "sRefNo";
                sParaValue[1] = sRefNo;

                //new ReportController().getSsrsReportViewer(ConfirmNoteRptViewer, sParaField, sParaValue, sRptFullLocation, sExportFileName, sFormat);
                new ReportController().getSsrsReportViewer(ConfirmNoteRptViewer, sParaField, sParaValue, sRptFullLocation, "", "");

                //ifrmPrintRBS.Attributes["src"] = "../" + ConfigurationSettings.AppSettings["PrintOutPath"].ToString() + sExportFileName + "." + sFormat;
            }
            catch (Exception ex)
            {
                lblReportErrorMessage.Text = ex.Message;
            }
        }


        protected void btnAddNew_ContactPersonNameValue_Click(object sender, EventArgs e)
        {
            string UID = "";
            //int iClientID = Convert.ToInt32(lblAdd_ClientNameValue.Text);
            FailureAddText.Text = "";

            if (Session["UID"] != null)
                UID = Session["UID"].ToString();

            if (txtAddNew_ContactPersonNameValue.Text.Trim() == "")
            {
                lblAddNew_ContactPersonNameValue.Text = "Please enter the contact person name.";

                if (lblAdd_ClientNameValue.Text.Trim() == "")
                {
                    lblAddNew_ContactPersonNameValue.Text = "Please select the client.";
                }
            }

            else
            {
                int iClientID = Convert.ToInt32(lblAdd_ClientNameValue.Text);

                if (AddContactPerson(iClientID, txtAddNew_ContactPersonNameValue.Text, UID))
                {
                    TransactionController tc = new TransactionController();
                    DataSet c;
                    DataRow drClient;

                    c = tc.getClientName(iClientID);
                    if (c.Tables[0].Rows.Count > 0)
                    {
                        drClient = c.Tables[0].Rows[0];
                        txtAdd_ClientNameDescValue.Text = Convert.ToString(drClient["Client Name"]);
                    }

                    lblAddNew_ContactPersonNameValue.Text = "Contact Person '" + txtAddNew_ContactPersonNameValue.Text + "' has been added to the Client '" + txtAdd_ClientNameDescValue.Text + "' successfully!";
                    SetupDdlFormStatus_SelectContactPerson(iClientID);  //Refresh


                }
                else
                    FailureAddText.Text = FailureAddText.Text + "Add Contact Person Unsuccess! Please try again.";
            }


        }
        private bool AddContactPerson(int iClientID, string sName, string sAddUser)
        {
            bool addSuccess;
            DataSet ds = new ClientController().AddContactPerson(iClientID, sName, "", "", "", "", "", "", "", sAddUser);

            addSuccess = false;

            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    addSuccess = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return addSuccess;
        }

    }
}