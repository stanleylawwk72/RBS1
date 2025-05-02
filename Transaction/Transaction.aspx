<%@ Page Title="Resource Borrowing System" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transaction.aspx.cs" Inherits="ResourceBorrowing.Transaction.Transaction" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">


    <%--<script type="text/javascript">
   var TargetBaseControl = null;
        
   window.onload = function()
   {
      try
      {
         //get target base control.
         TargetBaseControl = 
           document.getElementById('<%= this.gvShow_Selected_Edit.ClientID %>');
      }
      catch(err)
      {
         TargetBaseControl = null;
      }
   }
        
   function Validation()
   {              
      if(TargetBaseControl == null) return false;
      
      //get target child control.
      var TargetChildControl = "chkShowEditItem";
            
      //get all the control of the type INPUT in the base control.
      var Inputs = TargetBaseControl.getElementsByTagName("input"); 
            
      for(var n = 0; n < Inputs.length; ++n)
         if(Inputs[n].type == 'checkbox' && 
            Inputs[n].id.indexOf(TargetChildControl,0) >= 0 && 
            Inputs[n].checked)
          return true;        
            
      alert('Please select at least one item!');
      return false;
   }
</script>--%>
    <asp:ObjectDataSource ID="odsTransaction" runat="server" TypeName="ResourceBorrowing.Transaction.TransactionController" SelectMethod="getTransaction">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtFilter_ID" Name="iID" Type="String" DefaultValue="0" />
        </SelectParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="txtFilter_RefNo" Name="sRefNo" Type="String" DefaultValue="" />
        </SelectParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="txtFilter_TranStatus" Name="sStatus" Type="String" DefaultValue="" />
        </SelectParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="txtFilter_Overdue" Name="sOverdue" Type="String" DefaultValue="" />
        </SelectParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="txtFilter_ToBorrowDateFrom" Name="sToBorrowDateFrom" Type="String" DefaultValue="" />
        </SelectParameters>
        <%--        <SelectParameters>
            <asp:ControlParameter ControlID="txtFilter_ToBorrowDateTo" Name="sToBorrowDateTo" Type="String" DefaultValue="" />
        </SelectParameters>--%>
        <SelectParameters>
            <asp:ControlParameter ControlID="txtFilter_ToReturnDateFrom" Name="sToReturnDateFrom" Type="String" DefaultValue="" />
        </SelectParameters>
        <%--        <SelectParameters>
            <asp:ControlParameter ControlID="txtFilter_ToReturnDateTo" Name="sToReturnDateTo" Type="String" DefaultValue="" />
        </SelectParameters>--%>
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlFilter_InternalUserDept" PropertyName="SelectedValue" Name="sInternalDept" Type="String" />
        </SelectParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlFilter_InternalUserName" PropertyName="SelectedValue" Name="sInternalUser" Type="String" />
        </SelectParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="txtFilter_ClientName" Name="sClientName" Type="String" DefaultValue="" />
        </SelectParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="txtFilter_ContactPersonName" Name="sContactPersonName" Type="String" DefaultValue="" />
        </SelectParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="txtFilter_PartnerID" Name="sPartnerID" Type="String" DefaultValue="" />
        </SelectParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="txtFilter_ItemCode" Name="sItemCode" Type="String" DefaultValue="" />
        </SelectParameters>
        <%--<SelectParameters><asp:ControlParameter ControlID="txtFilter_ItemCopiesID" Name="sItemCopiesID" Type="String" DefaultValue="" /> </SelectParameters>--%>
        <SelectParameters>
            <asp:ControlParameter ControlID="txtFilter_Description" Name="sDescription" Type="String" DefaultValue="" />
        </SelectParameters>
        <%-- J20180605 --%>
        <SelectParameters>
            <asp:ControlParameter  ControlID="txtFilter_WindowsAuthUserID" Name="sWindowsAuthUserID" Type="String" DefaultValue="" />
        <%--    <asp:SessionParameter Name="sWindowsAuthUserID"  Type="String" SessionField="WindowAuthID"/> --%>
        </SelectParameters>

    </asp:ObjectDataSource>
    <h1>Transactions</h1>

    <asp:MultiView ID="mvTransaction" runat="server" ActiveViewIndex="0">

        <asp:View ID="vTransactionList" runat="server">
            <span class="failureNotification">
                <asp:Literal ID="FailureFilterText" runat="server"></asp:Literal>
            </span>

            <asp:ValidationSummary ID="FilterTransactionValidationSummary" runat="server" CssClass="failureNotification"
                ValidationGroup="FilterTransactionValidationGroup" />
            <asp:Panel ID="pTranFilter" runat="server" DefaultButton="FilterBtn">

                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblFilter" runat="server" AssociatedControlID="lblFilter" Text="Filter" Font-Bold="true" Font-Underline="true" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFilter_ID" runat="server" AssociatedControlID="lblFilter_ID" Text="ID:" Visible="false" /></td>
                        <td>
                            <asp:TextBox ID="txtFilter_ID" runat="server" CssClass="textEntry" Enabled="true" Text="0" Visible="false" /><asp:TextBox ID="txtFilter_RedirectStatus" runat="server" CssClass="textEntry" Enabled="false" Text="" Visible="false" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFilter_RefNo" runat="server" AssociatedControlID="lblFilter_RefNo" Text="Ref No.: " /></td>
                        <td>
                            <asp:TextBox ID="txtFilter_RefNo" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFilter_TranStatus" runat="server" AssociatedControlID="lblFilter_TranStatus" Text="Action:    " /></td>
                        <td>
                            <asp:CheckBoxList ID="checkBoxList_TranStatus" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal" Visible="true">
                            </asp:CheckBoxList>
                            <asp:Button ID="Filterbtn_SelectAllTranStatus" Width="100" runat="server" OnCommand="FilterTranStatusSelection" Text="[Select All]" CommandName="A" CssClass="button" /><asp:Button ID="Filterbtn_UnselectAllTranStatus" Width="100" runat="server" OnCommand="FilterTranStatusSelection" Text="[Unselect All]" CommandName="U" CssClass="button" />
                            <asp:Label ID="txtFilter_TranStatus" runat="server" Visible="false" Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFilter_Overdue" runat="server" AssociatedControlID="lblFilter_Overdue" Text="Overdue:    "></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBoxList ID="checkBoxList_Overdue" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal" Visible="true">
                                <asp:ListItem Text="Active" Value="1" Selected="false">Yes</asp:ListItem>
                                <asp:ListItem Text="Active" Value="0" Selected="false">No</asp:ListItem>
                            </asp:CheckBoxList>
                            <asp:Label ID="txtFilter_Overdue" runat="server" Visible="false" Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFilter_ToBorrowDateFrom" runat="server" AssociatedControlID="lblFilter_ToBorrowDateFrom" Text="To Borrow Date:    "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFilter_ToBorrowDateFrom" runat="server" CssClass="textEntry" Enabled="true" Text="" />
                            <asp:Button ID="Filterbtn_ToBorrowDateFrom" runat="server" OnClick="Filterbtn_ToBorrowDateFrom_Click" Text="[Pick Date]" CssClass="button" />
                            (yyyy/mm/dd)
                <div>
                    <asp:Calendar ID="CalendarFilter_ToBorrowDateFrom" runat="server" Visible="False" OnSelectionChanged="CalendarFilter_ToBorrowDateFrom_SelectionChanged">
                        <TodayDayStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" />
                    </asp:Calendar>
                </div>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorTBDateFrm_Edit_1" runat="server" ControlToValidate="txtFilter_ToBorrowDateFrom"
                                ValidationExpression="^[0-9]{4}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30)))$"
                                CssClass="failureNotification" ErrorMessage="Incorrect date format of To Borrow Date (From)! " ToolTip=""
                                Display="Dynamic" ValidationGroup="FilterTransactionValidationGroup">* Wrong Date Format!</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <%--                    <tr>
                        <td>
                            <asp:Label ID="lblFilter_ToBorrowDateTo" runat="server" AssociatedControlID="lblFilter_ToBorrowDateTo" Text="To Borrow Date (To):    "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFilter_ToBorrowDateTo" runat="server" CssClass="textEntry" Enabled="true" Text="" />
                            <asp:Button ID="Filterbtn_ToBorrowDateTo" runat="server" OnClick="Filterbtn_ToBorrowDateTo_Click" Text="[Pick Date]" CssClass="button" />
                            (yyyy/mm/dd)
                <div>
                    <asp:Calendar ID="CalendarFilter_ToBorrowDateTo" runat="server" Visible="False" OnSelectionChanged="CalendarFilter_ToBorrowDateTo_SelectionChanged">
                        <TodayDayStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" />
                    </asp:Calendar>
                    &nbsp;</div>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorTBDateTo_Edit_1" runat="server" ControlToValidate="txtFilter_ToBorrowDateTo"
                                ValidationExpression="^[0-9]{4}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30)))$"
                                CssClass="failureNotification" ErrorMessage="Incorrect date format of To Borrow Date (To)! " ToolTip=""
                                Display="Dynamic" ValidationGroup="FilterTransactionValidationGroup">* Wrong Date Format!</asp:RegularExpressionValidator>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <asp:Label ID="lblFilter_ToReturnDateFrom" runat="server" AssociatedControlID="lblFilter_ToReturnDateFrom" Text="To Return Date:    "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFilter_ToReturnDateFrom" runat="server" CssClass="textEntry" Enabled="true" Text="" />
                            <asp:Button ID="Filterbtn_ToReturnDateFrom" runat="server" OnClick="Filterbtn_ToReturnDateFrom_Click" Text="[Pick Date]" CssClass="button" />
                            (yyyy/mm/dd)
                <div>
                    <asp:Calendar ID="CalendarFilter_ToReturnDateFrom" runat="server" Visible="False" OnSelectionChanged="CalendarFilter_ToReturnDateFrom_SelectionChanged">
                        <TodayDayStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" />
                    </asp:Calendar>
                </div>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorTRDateFrm_Edit_1" runat="server" ControlToValidate="txtFilter_ToReturnDateFrom"
                                ValidationExpression="^[0-9]{4}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30)))$"
                                CssClass="failureNotification" ErrorMessage="Incorrect date format of To Return Date (From)! " ToolTip=""
                                Display="Dynamic" ValidationGroup="FilterTransactionValidationGroup">* Wrong Date Format!</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <%--                    <tr>
                        <td>
                            <asp:Label ID="lblFilter_ToReturnDateTo" runat="server" AssociatedControlID="lblFilter_ToReturnDateTo" Text="To Return Date (To):    "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFilter_ToReturnDateTo" runat="server" CssClass="textEntry" Enabled="true" Text="" />
                            <asp:Button ID="Filterbtn_ToReturnDateTo" runat="server" OnClick="Filterbtn_ToReturnDateTo_Click" Text="[Pick Date]" CssClass="button" />
                            (yyyy/mm/dd)
                            <div>
                                <asp:Calendar ID="CalendarFilter_ToReturnDateTo" runat="server" Visible="False" OnSelectionChanged="CalendarFilter_ToReturnDateTo_SelectionChanged">
                                    <TodayDayStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" />
                                </asp:Calendar>
                            </div>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorTRDateTo_Edit_1" runat="server" ControlToValidate="txtFilter_ToReturnDateTo"
                                ValidationExpression="^[0-9]{4}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30)))$"
                                CssClass="failureNotification" ErrorMessage="Incorrect date format of To Return Date (To)! " ToolTip=""
                                Display="Dynamic" ValidationGroup="FilterTransactionValidationGroup">* Wrong Date Format!</asp:RegularExpressionValidator>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <asp:Label ID="lblFilter_ItemCode" runat="server" AssociatedControlID="lblFilter_ItemCode" Text="Item No.: " /></td>
                        <td>
                            <asp:TextBox ID="txtFilter_ItemCode" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                    </tr>
                    <%--                    <tr>
                        <td><asp:Label ID="lblFilter_ItemCopiesID" runat="server" AssociatedControlID="lblFilter_ItemCopiesID" Text="Item Copies ID: "/></td>
                        <td><asp:TextBox ID="txtFilter_ItemCopiesID" runat="server" CssClass="textEntry" Enabled="true" Text=""/></td>
                    </tr>--%>
                    <tr>
                        <td>
                            <asp:Label ID="lblFilter_Description" runat="server" AssociatedControlID="lblFilter_Description" Text="Item Name: " /></td>
                        <td>
                            <asp:TextBox ID="txtFilter_Description" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="txtFilter_Client" runat="server" Style="font-style: italic" Text="For Client:"></asp:Label>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFilter_ClientName" runat="server" AssociatedControlID="lblFilter_ClientName" Text="Client Name:    "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFilter_ClientName" runat="server" CssClass="textEntry" Enabled="true" Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFilter_ContactPersonName" runat="server" AssociatedControlID="lblFilter_ContactPersonName" Text="Contact Person Name:    "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFilter_ContactPersonName" runat="server" CssClass="textEntry" Enabled="true" Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFilter_PartnerID" runat="server" AssociatedControlID="lblFilter_PartnerID" Text="Partner ID:    "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFilter_PartnerID" runat="server" CssClass="textEntry" Enabled="true" Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="txtFilter_InternalUser" runat="server" Text="For Internal User:" Style="font-style: italic"></asp:Label>
                        </td>
                        <td class="auto-style6"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFilter_InternalUserDept" runat="server" AssociatedControlID="lblFilter_InternalUserDept" Text="Internal User Dept.:    "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFilter_InternalUserDept" runat="server" Enabled="true" OnSelectedIndexChanged="ddlFilter_InternalUserDept_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFilter_InternalUserName" runat="server" AssociatedControlID="lblFilter_InternalUserName" Text="Internal User Name:    "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFilter_InternalUserName" runat="server" Enabled="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                     
                    <%-- J20180605 --%>
                    <tr>
                        <td>
                            <asp:Label ID="lblFilter_WindowsAuthUserID" runat="server" AssociatedControlID="lblFilter_WindowsAuthUserID" Visible="false" Text="Window Auth User ID:    "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFilter_WindowsAuthUserID" runat="server" CssClass="textEntry" Enabled="false" Visible="false" Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="FilterBtn" runat="server" Text="Search Transaction" ValidationGroup="FilterTransactionValidationGroup" OnClick="FilterBtn_Click" CssClass="button" />
                        </td>
                        <td>
                            <asp:Button ID="FilterClearBtn" runat="server" Text="Clear Filter" OnClick="FilterClearBtn_Click" CssClass="button" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />

            <p>
                <asp:Button ID="CreateTranBtn" runat="server" CssClass="Button1" OnClick="CreateTranBtn_Click" Text="Create Transaction" Width="180px" />
                <%--                    <asp:Button ID="CreateTranBtn" runat="server" CssClass="button1" OnClick="CreateTranBtn_Click" Text="Create Transaction" Width="180px" />--%>
                <asp:Button ID="ExportCSVBtn" runat="server" CssClass="button" OnClick="ExportCSVBtn_Click" Text="Export to CSV" Width="145px" Visible="false" />
            </p>

            <table>
                <tr>
                    <td>
                        <asp:GridView ID="GridViewTransaction" runat="server"
                            DataSourceID="odsTransaction" AutoGenerateColumns="true"
                            OnRowCommand="gvTransaction_RowCommand" ShowHeader="true"
                            AllowPaging="True" AllowSorting="true"
                            OnPageIndexChanging="PageTransactionIndexChanging"
                            SortedAscendingHeaderStyle-Font-Underline="true" PageSize="20"
                            OnRowDataBound="GridViewTransaction_RowDataBound">

                            <Columns>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle VerticalAlign="Top" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <HeaderStyle Width="10%" />
                                    <ItemTemplate>
                                        <%--                                            <asp:Button ID="btnEditBooking" runat="server" Text="Edit" CommandName="E" CommandArgument='<%# Eval("TranID") +"#"+ Eval("RefNo") +"#"+ Eval("Action") %>' CssClass="button" OnCommand="EditTransaction" />--%>
                                        <%--                                                <asp:Button ID="btnEditBooking" runat="server" Text="Edit" CommandName="E" CommandArgument='<%# Eval("TranID") %>' CssClass="button" Enabled='<%#Eval("Action").ToString().Equals("1")%>'/> <!--BOOK-->--%>
                                        <asp:Button ID="btnEditBooking" runat="server" Text="Edit" CommandName="E" CommandArgument='<%# Eval("TransactionID") %>' CssClass="Button2" OnCommand="EditTransaction"></asp:Button>
                                        <asp:Button ID="btnTranPrintOut" runat="server" Text="Print" CommandName="P" CommandArgument='<%# Eval("TransactionID") %>' CssClass="Button3" OnCommand="EditTransaction" />

                                        <%--                                <asp:Button ID="btnCancelBooking" runat="server" Text="Cancel" CommandName="C" CommandArgument='<%# Eval("ID") %>' CssClass="button" Enabled='<%#Eval("Action").ToString().Equals("1")%>'/> <!--BOOK-->
                                <asp:Button ID="btnBorrow" runat="server" Text="Borrow" CommandName="B" CommandArgument='<%# Eval("ID") %>' CssClass="button" Enabled='<%#Eval("Action").ToString().Equals("1")%>'/> <!--BOOK-->--%>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>

                            <EmptyDataTemplate>
                                No Record Found!
                            </EmptyDataTemplate>
                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="20" />
                            <SortedAscendingHeaderStyle Font-Underline="True" />
                        </asp:GridView>
                    </td>


            </table>
        </asp:View>

        <asp:View ID="vAddTransaction" runat="server">
            <h2>Create Transaction
            </h2>
            <span class="failureNotification">
                <asp:Literal ID="ErrorMessageAdd" runat="server"></asp:Literal>
            </span>
            <div class="Transaction">
                <fieldset class="Transaction">
                    <span class="failureNotification">
                        <asp:Literal ID="FailureAddText" runat="server"></asp:Literal>
                    </span>
                    <%--                    <asp:ValidationSummary ID="CreateTranValidationSummary_Add" runat="server" ForeColor="Red" CssClass="failureNotification" ValidationGroup="CreateTranValidationGroup_Add" />--%>
                    <asp:ValidationSummary ID="CreateTranValidationSummary_Add" runat="server" ForeColor="Red" CssClass="failureNotification" ValidationGroup="AddTransactionValidationGroup" />

                    <legend>Transaction Information</legend>

                    <div class="leftCorner">
                        <table>
                            <tr>
                                <td class="TranColWidth">
                                    <asp:Label ID="lblTranType_Add" runat="server" AssociatedControlID="lblTranType_Add" Text="Type:" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTranType_Add" runat="server" AutoPostBack="true" Enabled="true" OnSelectedIndexChanged="ddlAdd_TranType_SelectedIndexChanged">
                                        <asp:ListItem Text="Client" Value="1" />
                                        <asp:ListItem Text="Internal User" Value="2" />
                                    </asp:DropDownList>
                                    <asp:Label ID="lblTranType_AddValue" runat="server" Text="" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TranColWidth">
                                    <asp:Label ID="lblAction_Add" runat="server" AssociatedControlID="lblAction_Add" Text="Action:" /></td>
                                <td>
                                    <asp:DropDownList ID="ddlAction_Add" runat="server" Enabled="true">
                                        <asp:ListItem Text="BOOK" Value="1" />
                                        <asp:ListItem Text="BORROW" Value="2" />
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style1" colspan="2">
                                    <strong>Borrowing User:</strong>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:MultiView ID="mvBorrowingUser" runat="server" ActiveViewIndex="0">
                                        <asp:View ID="vClient" runat="server">
                                            <table>
                                                <%--                                                <tr>
                                    <td class="TranColWidth">
                                        <asp:Label ID="txtAdd_Client" runat="server" Style="font-style: italic" Text="For Client:" Visible="true"></asp:Label>
                                    </td>
                                    <td>
                                        
                                    </td>
                                </tr>--%>
                                                <tr>
                                                    <td class="TranColWidth">
                                                        <asp:Label ID="lblAdd_ClientName" runat="server" AssociatedControlID="lblAdd_ClientName" Text="Client Name:    "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="TranSelectClientBtn" runat="server" CssClass="button" Enabled="true" OnClick="TranSelectClientBtn_Click" Text="Select Client" Visible="true" />
                                                        <%--                                <asp:TextBox ID="txtAdd_ClientName" runat="server" CssClass="textEntry" Enabled="true" Text="" Visible="false"/>    --%>
                                                        <%--<asp:Label ID="lblAdd_ClientNameValue" runat="server" Visible="true" Text="" />--%>
                                                        <asp:TextBox ID="txtAdd_ClientNameDescValue" runat="server" CssClass="textEntry" Enabled="false" Visible="True" Text="" />
                                                        <asp:Label ID="lblAdd_ClientNameValue" runat="server" CssClass="textEntry" Enabled="false" Visible="False" Text="" />
                                                        <%--                                <asp:Label ID="lblAdd_ClientNameDesc" runat="server" Visible="true" Text="" />--%>

                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TranColWidth">
                                                        <asp:Label ID="lblAdd_ContactPersonName" runat="server" AssociatedControlID="lblAdd_ContactPersonName" Text="Contact Person:    "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAdd_ContactPersonName" runat="server" Enabled="true" AutoPostBack="true" OnSelectedIndexChanged="ddlAdd_ContactPersonName_SelectedIndexChanged" />
                                                        <asp:Label ID="lblAdd_ContactPersonNameValue" runat="server" Text="" Visible="false" />

                                                        <%--                                                        <asp:CompareValidator ControlToValidate="ddlAdd_ContactPersonName" ID="CompareValidatorAdd_ContactPersonName"
                                                            ValidationGroup="AddTransactionValidationGroup" CssClass="failureNotification" 
                                                            ErrorMessage="Please select the Contact Person Name"
                                                            runat="server" Display="Dynamic" 
                                                            Operator="NotEqual" ValueToCompare="0" Type="Integer" > * Required
                                                        </asp:CompareValidator>--%>

                                                        <%--<asp:RequiredFieldValidator 
                                                            ID="RequiredFieldValidatorContactPersonNameValue" runat="server" 
                                                            CssClass="failureNotification"
                                                            ErrorMessage="Contact Person Name cannot be blank! Select Client first to generate list of related Contact Person."
                                                            ValidationGroup="AddTransactionValidationGroup"
                                                            ControlToValidate="lblAdd_ContactPersonNameValue">* Required!
                                                        </asp:RequiredFieldValidator>--%>

                                                        <asp:TextBox ID="txtAddNew_ContactPersonNameValue" runat="server" CssClass="textEntry" Enabled="true" Visible="True" Text="" placeholder="Enter New Contact Person Name."/>
                                                        <asp:Button ID="btnAddNew_ContactPersonNameValue" runat="server" CssClass="button" Enabled="true" OnClick="btnAddNew_ContactPersonNameValue_Click" Text="Add Contact Person" Visible="true" />
                                                        <br />
                                                        <asp:Label ID="lblAddNew_ContactPersonNameValue" runat="server" Text="" Visible="true" Font-Italic="true"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:MultiView ID="mvClientList" runat="server" ActiveViewIndex="0">
                                                            <asp:View ID="vSelectClient" runat="server">
                                                                <asp:Panel ID="panelClientList" runat="server" DefaultButton="TranClientFilterBtn" BorderWidth="2" BorderColor="LightGray" BorderStyle="Inset">
                                                                    <%--                                                                    <asp:ObjectDataSource ID="odsClient" TypeName="ResourceBorrowing.Clients.ClientController" runat="server" SelectMethod="getClient">
                                                                        <SelectParameters>
                                                                            <asp:ControlParameter ControlID="ddlTranClientFilter_ClientType" PropertyName="SelectedValue" Name="sClientTypeID" Type="String" />
                                                                        </SelectParameters>
                                                                        <SelectParameters>
                                                                            <asp:ControlParameter ControlID="txtTranClientFilter_ClientName" Name="sClientName" Type="String" DefaultValue="" />
                                                                        </SelectParameters>
                                                                        <SelectParameters>
                                                                            <asp:ControlParameter ControlID="txtTranClientFilter_ContactPersonName" Name="sContactPersonName" Type="String" DefaultValue="" />
                                                                        </SelectParameters>
                                                                        <SelectParameters>
                                                                            <asp:ControlParameter ControlID="txtTranClientFilter_ClientPhoneNo" Name="sClientPhoneNo" Type="String" DefaultValue="" />
                                                                        </SelectParameters>
                                                                        <SelectParameters>
                                                                            <asp:ControlParameter ControlID="txtTranClientFilter_ContactPersonPhoneNo" Name="sContactPersonPhoneNo" Type="String" DefaultValue="" />
                                                                        </SelectParameters>--%>
                                                                    <asp:ObjectDataSource ID="odsClient" runat="server" TypeName="ResourceBorrowing.Clients.ClientController" SelectMethod="GetClient">
                                                                        <SelectParameters>
                                                                            <asp:ControlParameter ControlID="txtTranClientFilter_PartnerID" Name="sPartnerID" Type="String" />
                                                                        </SelectParameters>
                                                                        <SelectParameters>
                                                                            <asp:ControlParameter ControlID="ddlTranClientFilter_ClientType" PropertyName="SelectedValue" Name="sClientTypeID" Type="String" />
                                                                        </SelectParameters>
                                                                        <SelectParameters>
                                                                            <asp:ControlParameter ControlID="txtTranClientFilter_ClientName" Name="sClientName" Type="String" DefaultValue="" />
                                                                        </SelectParameters>
                                                                        <SelectParameters>
                                                                            <asp:ControlParameter ControlID="txtTranClientFilter_ClientPhoneNo" Name="sClientPhoneNo" Type="String" DefaultValue="" />
                                                                        </SelectParameters>
                                                                        <SelectParameters>
                                                                            <asp:ControlParameter ControlID="txtTranClientFilter_ClientFax" Name="sClientFax" Type="String" DefaultValue="" />
                                                                        </SelectParameters>
                                                                        <%-- J20180605 --%>
                                                                        <SelectParameters>
                                                                            <asp:ControlParameter  ControlID="txtTranClientFilter_WindowsAuthUserID" Name="sWindowsAuthUserID" Type="String" DefaultValue="" />
                                                                        <%--    <asp:SessionParameter Name="sWindowsAuthUserID"  Type="String" SessionField="WindowAuthID"/> --%>
                                                                        </SelectParameters>

                                                                    </asp:ObjectDataSource>

                                                                    <asp:ValidationSummary ID="FilterClientValidationSummary" runat="server" CssClass="failureNotification"
                                                                        ValidationGroup="FilterClientValidationGroup" />

                                                                    <table>
                                                                        <tr>
                                                                            <td style="height: 22px">
                                                                                <asp:Label ID="lblTranClientFilter" runat="server" AssociatedControlID="lblTranClientFilter" Text="Search Client" Font-Bold="true" Font-Underline="true" />
                                                                                <asp:TextBox ID="txtTranClientFilter_RedirectStatus" runat="server" CssClass="textEntry" Enabled="false" Text="" Visible="false" />
                                                                            </td>
                                                                            <td style="height: 22px"></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTranClientFilter_PartnerID" runat="server" AssociatedControlID="lblTranClientFilter_PartnerID" Text="PartnerID:" /></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTranClientFilter_PartnerID" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTranClientFilter_ClientName" runat="server" AssociatedControlID="lblTranClientFilter_ClientName" Text="Name:" /></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTranClientFilter_ClientName" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTranClientFilter_ClientType" runat="server" AssociatedControlID="lblTranClientFilter_ClientType" Text="Type:" Visible="true" /></td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlTranClientFilter_ClientType" runat="server" Enabled="true">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTranClientFilter_ClientPhoneNo" runat="server" AssociatedControlID="lblTranClientFilter_ClientPhoneNo" Text="Phone No.: " /></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTranClientFilter_ClientPhoneNo" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTranClientFilter_ClientFax" runat="server" AssociatedControlID="lblTranClientFilter_ClientFax" Text="Fax: " /></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTranClientFilter_ClientFax" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                                                                        </tr>


                                                                        <%--                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTranClientFilter_ClientType" runat="server" AssociatedControlID="lblTranClientFilter_ClientType" Text="Client Type:" Visible="true" /></td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlTranClientFilter_ClientType" runat="server" Enabled="true">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTranClientFilter_ClientName" runat="server" AssociatedControlID="lblTranClientFilter_ClientName" Text="Client Name:" /></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTranClientFilter_ClientName" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTranClientFilter_ClientPhoneNo" runat="server" AssociatedControlID="lblTranClientFilter_ClientPhoneNo" Text="Client Phone No.: " /></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTranClientFilter_ClientPhoneNo" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTranClientFilter_ContactPersonName" runat="server" AssociatedControlID="lblTranClientFilter_ContactPersonName" Text="Contact Person Name:" /></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTranClientFilter_ContactPersonName" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTranClientFilter_ContactPersonPhoneNo" runat="server" AssociatedControlID="lblTranClientFilter_ContactPersonPhoneNo" Text="Contact Person Phone No.: " /></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTranClientFilter_ContactPersonPhoneNo" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                                                                        </tr>--%>
                                                                        <%-- J20180605 --%>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTranClientFilter_WindowsAuthUserID" runat="server" AssociatedControlID="lblFilter_WindowsAuthUserID" Visible="false" Text="Window Auth User ID:    "></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtTranClientFilter_WindowsAuthUserID" runat="server" CssClass="textEntry" Enabled="false" Visible="false" Text="" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Button ID="TranClientFilterBtn" runat="server" Text="Search Client" ValidationGroup="FilterClientValidationGroup" OnClick="btnAdd_TranClientFilter_Click" CssClass="button" /></td>
                                                                            <td>
                                                                                <asp:Button ID="TranClientFilterClearBtn" runat="server" Text="Clear Filter" OnClick="btnAdd_TranClientFilterClear_Click" CssClass="button" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td></td>
                                                                            <td></td>
                                                                        </tr>

                                                                    </table>



                                                                    <asp:GridView ID="GridViewClientForms" runat="server" DataSourceID="odsClient"
                                                                        AutoGenerateColumns="true"
                                                                        OnRowCommand="gvClient_RowCommand"
                                                                        ShowHeader="true" AllowSorting="true"
                                                                        AllowPaging="true" OnPageIndexChanging="PageClientFormIndexChanging"
                                                                        SortedAscendingHeaderStyle-Font-Underline="true" PageSize="10"
                                                                        OnRowDataBound="GridViewClientForms_RowDataBound" PagerStyle-CssClass="pager">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Select">
                                                                                <ItemStyle VerticalAlign="Top" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <HeaderStyle Width="10%" />
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="lblSelectClient" runat="server" Text="Select" CommandArgument='<%# Eval("ClientID") %>' CssClass="button" OnCommand="Tran_SelectClient"></asp:Button>
                                                                                </ItemTemplate>
                                                                                <ItemStyle VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                        </Columns>



                                                                        <EmptyDataTemplate>
                                                                            No Record Found!
                                                                        </EmptyDataTemplate>

                                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" />
                                                                        <PagerStyle Font-Bold="False" />
                                                                        <SortedAscendingHeaderStyle Font-Underline="True" />
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </asp:View>
                                                            <asp:View ID="vDisableSelectClient" runat="server">
                                                            </asp:View>
                                                        </asp:MultiView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <asp:View ID="vInternalUser" runat="server">
                                            <table>
                                                <tr>
                                                    <%--                                <td colspan="2">
                                    <asp:Label ID="txtAdd_InternalUser" runat="server" Style="font-style: italic" Text="For Internal User:" Visible="false" ></asp:Label>
                                </td>--%>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style8">
                                                        <asp:Label ID="lblAdd_InternalUserDept" runat="server" AssociatedControlID="lblAdd_InternalUserDept" Text="Internal User Dept.:    " Visible="false"></asp:Label>
                                                    </td>

                                                    <td class="auto-style7">
                                                        <asp:DropDownList ID="ddlAdd_InternalUserDept" runat="server" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlAdd_InternalUserDept_SelectedIndexChanged" Visible="false">
                                                        </asp:DropDownList>
                                                        <%--                                    <asp:CompareValidator ControlToValidate="ddlAdd_InternalUserDept" ID="CompareValidatorAdd_InternalUserDept"
                                        ValidationGroup="AddTransactionValidationGroup" CssClass="failureNotification" 
                                        ErrorMessage="Please select the Internal User Dept."
                                        runat="server" Display="Dynamic" 
                                        Operator="NotEqual" ValueToCompare="0" Type="Integer" > * Required
                                    </asp:CompareValidator>--%>
                                                        <asp:Label ID="lblAdd_InternalUserDeptValue" runat="server" Text="" Visible="false" />
                                                        <%--<asp:RequiredFieldValidator 
                                        ID="RequiredFieldValidatorInternalUserDept" runat="server" 
                                        CssClass="failureNotification"
                                        ErrorMessage="Internal User Dept cannot be blank! " 
                                        ValidationGroup="AddTransactionValidationGroup"
                                        ControlToValidate="txtAdd_InternalUserDept">* Required!
                                    </asp:RequiredFieldValidator>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TranColWidth">
                                                        <asp:Label ID="lblAdd_InternalUserName" runat="server" AssociatedControlID="lblAdd_InternalUserName" Text="Internal User Name:    " Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAdd_InternalUserName" runat="server" Enabled="false" OnSelectedIndexChanged="ddlAdd_InternalUserName_SelectedIndexChanged" Visible="true">
                                                        </asp:DropDownList>
                                                        <%--                                    <asp:CompareValidator ControlToValidate="ddlAdd_InternalUserName" ID="CompareValidatorAdd_InternalUserName"
                                        ValidationGroup="AddTransactionValidationGroup" CssClass="failureNotification" 
                                        ErrorMessage="Please select the Internal User Name."
                                        runat="server" Display="Dynamic" 
                                        Operator="NotEqual" ValueToCompare="0" Type="Integer" > * Required
                                    </asp:CompareValidator>--%>
                                                        <asp:Label ID="lblAdd_InternalUserNameValue" runat="server" Text="" Visible="false" />
                                                        <%--<asp:RequiredFieldValidator 
                                        ID="RequiredFieldValidatorInternalUserName" runat="server" 
                                        CssClass="failureNotification"
                                        ErrorMessage="Internal User Name cannot be blank! " 
                                        ValidationGroup="AddTransactionValidationGroup"
                                        ControlToValidate="txtAdd_InternalUserName">* Required!
                                    </asp:RequiredFieldValidator>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                    </asp:MultiView>
                                </td>
                            </tr>
                            <%--Client here--%>
                            <%--Internal User here--%>

                            <tr>
                                <td class="auto-style1" colspan="2">
                                    <strong>Borrowing Period:</strong>
                                </td>
                            </tr>
                            <tr>
                                <td class="TranColWidth">
                                    <asp:Label ID="lblAdd_BorrowDateFrom" runat="server" AssociatedControlID="lblAdd_BorrowDateFrom" Text="From:    "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAdd_BorrowDateFrom" runat="server" CssClass="textEntry" Enabled="true" Text="" />
                                    <asp:Button ID="btnAdd_BorrowDateFrom" runat="server" OnClick="btnAdd_BorrowDateFrom_Click" Text="[Pick Date]" CssClass="button" />
                                    (yyyy/mm/dd)
                                <div>
                                    <asp:Calendar ID="CalendarAdd_BorrowDateFrom" runat="server" Visible="False" OnSelectionChanged="CalendarAdd_BorrowDateFrom_SelectionChanged">
                                        <TodayDayStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" />
                                    </asp:Calendar>
                                </div>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorBDateFrm_Add" runat="server" ControlToValidate="txtAdd_BorrowDateFrom"
                                        ValidationExpression="^[0-9]{4}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30)))$"
                                        CssClass="failureNotification" ErrorMessage="Incorrect date format of Borrow Date (From)! " ToolTip=""
                                        Display="Dynamic" ValidationGroup="AddTransactionValidationGroup">* Wrong Date Format!</asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="TranColWidth">
                                    <asp:Label ID="lblAdd_BorrowDateTo" runat="server" AssociatedControlID="lblAdd_BorrowDateTo" Text="To:    "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAdd_BorrowDateTo" runat="server" CssClass="textEntry" Enabled="true" Text="" />
                                    <asp:Button ID="btnAdd_BorrowDateTo" runat="server" OnClick="btnAdd_BorrowDateTo_Click" Text="[Pick Date]" CssClass="button" />
                                    (yyyy/mm/dd)
                                <div>
                                    <asp:Calendar ID="CalendarAdd_BorrowDateTo" runat="server" Visible="False" OnSelectionChanged="CalendarAdd_BorrowDateTo_SelectionChanged">
                                        <TodayDayStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" />
                                    </asp:Calendar>
                                </div>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorBDateTo_Add" runat="server" ControlToValidate="txtAdd_BorrowDateTo"
                                        ValidationExpression="^[0-9]{4}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30)))$"
                                        CssClass="failureNotification" ErrorMessage="Incorrect date format of Borrow Date (To)! " ToolTip=""
                                        Display="Dynamic" ValidationGroup="AddTransactionValidationGroup">* Wrong Date Format!
                                    </asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="TranColWidth">
                                    <asp:Label ID="lblAdd_NoOfLagDay" runat="server" AssociatedControlID="lblAdd_NoOfLagDay" Text="No. Of Lag Days:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAdd_NoOfLagDay" runat="server" CssClass="textEntry" Enabled="true" Text="3" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorNoOfLagDay" runat="server" ControlToValidate="txtAdd_NoOfLagDay"
                                        ValidationExpression="^[0-9]\d*$"
                                        CssClass="failureNotification" ErrorMessage="No. Of Lag Days is incorrect. " ToolTip=""
                                        Display="Dynamic" ValidationGroup="AddTransactionValidationGroup">* Positive integar only!
                                    </asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="TranColWidth"><strong>Item(s):</strong></td>
                                <td>
                                    <asp:Button ID="btnAdd_TranAddItemBtn" runat="server" Text="Add Item" OnClick="btnAdd_TranAddItemBtn_Click" CssClass="button" Visible="true" ValidationGroup="AddTransactionValidationGroup" />
                                    <%--                                    <asp:Button ID="btnAdd_TranRefresh" runat="server" Text="Refresh" OnClick="btnAdd_TranRefresh_Click" CssClass="button" Visible="true" ValidationGroup="AddTransactionValidationGroup" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <%--                                    <asp:GridView ID="gvShow_Selected" runat="server" AutoGenerateColumns="true" HeaderStyle-BackColor="orange" HeaderStyle-ForeColor="White" >
                                        <Columns>
                                        </Columns>
                                    </asp:GridView>--%>


                                    <asp:GridView ID="gvSelected" runat="server" HeaderStyle-BackColor="orange" HeaderStyle-ForeColor="White"
                                        OnRowDeleting="gvSelected_RowDeleting" AutoGenerateColumns="false">
                                        <Columns>

                                            <%--<asp:TemplateField HeaderText="X" >
                                                <HeaderTemplate>
                                                    <asp:Button ID="btnRemoveAll"  AutoPostBack="true" OnClick="btnRemoveAll_OnClick" runat="server" Text="X"/>
                                                </HeaderTemplate>
                                            </asp:TemplateField>--%>
                                            <%--                                                                    <asp:TemplateField HeaderText="Remove">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnAddItem_Remove" runat="server" Text="X" CommandArgument='<%# Eval("ItemID") %>' CssClass="button"></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:CommandField ShowDeleteButton="True" ButtonType="Button" DeleteText="X" ControlStyle-CssClass="button" />
                                            <asp:BoundField DataField="ItemID" HeaderText="ItemID" ItemStyle-Width="80" />
                                            <asp:BoundField DataField="ItemNo" HeaderText="ItemNo" ItemStyle-Width="80" />
                                            <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="150" />
                                            <asp:BoundField DataField="AvailableQty" HeaderText="Available Qty" ItemStyle-Width="100" />
                                            <asp:BoundField DataField="BorrowingQty" HeaderText="Borrowing Qty" ItemStyle-Width="100" />

                                        </Columns>
                                    </asp:GridView>

                                    <asp:MultiView ID="mvItemList" runat="server" ActiveViewIndex="0">
                                        <asp:View ID="vSelectItem" runat="server">
                                            <asp:Panel ID="panelItemList" runat="server" DefaultButton="TranItemFilterBtn" BorderWidth="2" BorderColor="LightGray" BorderStyle="Inset">

                                                <asp:ObjectDataSource ID="odsItems" runat="server" TypeName="ResourceBorrowing.Items.ItemController" SelectMethod="getAvailableItems">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtTranItemFilter_ID" Name="iID" Type="String" DefaultValue="999999" />
                                                    </SelectParameters>
                                                    <%--<SelectParameters>
                                                        <asp:ControlParameter ControlID="txtTranItemFilter_Status" Name="sStatus" Type="String" DefaultValue="" />
                                                    </SelectParameters>--%>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtTranItemFilter_ItemCode" Name="sItemCode" Type="String" DefaultValue="" />
                                                    </SelectParameters>
                                                    <%--<SelectParameters>
                                                        <asp:ControlParameter ControlID="txtTranItemFilter_ItemCopiesID" Name="sItemCopiesID" Type="String" DefaultValue="" />
                                                    </SelectParameters>--%>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtTranItemFilter_Description" Name="sDescription" Type="String" DefaultValue="" />
                                                    </SelectParameters>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtTranItemFilter_ItemTypeID" Name="sTypeID" Type="String" DefaultValue="" />
                                                    </SelectParameters>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="ddlTranItemFilter_Dept" PropertyName="SelectedValue" Name="sDeptCode" Type="String" />
                                                    </SelectParameters>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtTranItemFilter_ItemLangID" Name="sLangID" Type="String" DefaultValue="" />
                                                    </SelectParameters>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtTranItemFilter_NeedApproval" Name="sNeedApproval" Type="String" DefaultValue="" />
                                                    </SelectParameters>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtTranItemFilter_GlobalUse" Name="sGlobalUse" Type="String" DefaultValue="" />
                                                    </SelectParameters>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtTranItemFilter_RequestDateFrom" Name="dRequestDateFrom" Type="String" DefaultValue="" />
                                                    </SelectParameters>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtTranItemFilter_RequestDateTo" Name="dRequestDateTo" Type="String" DefaultValue="" />
                                                    </SelectParameters>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtTranItemFilter_NoOfLagDay" Name="iNoOfLagDay" Type="String" DefaultValue="3" />
                                                    </SelectParameters>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtTranItemFilter_WindowsAuthUserID" Name="sWindowsAuthUserID" Type="String" DefaultValue="" />   <%-- J20180605 --%>
                                                    </SelectParameters>

                                                </asp:ObjectDataSource>

                                                <span class="failureNotification">
                                                    <asp:Literal ID="FailureCreateTranAddItemText" runat="server"></asp:Literal>
                                                </span>
                                                <asp:ValidationSummary ID="CreateTranAddItemValidationSummary" runat="server" ForeColor="Red" CssClass="failureNotification" ValidationGroup="CreateTranAddItemValidationGroup" />


                                                <table>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblTranItemFilter" runat="server" AssociatedControlID="lblTranItemFilter" Text="Add Item" Font-Bold="true" Font-Underline="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTranItemFilter_Type" runat="server" AssociatedControlID="lblTranItemFilter_Type" Text="Type:    " /></td>
                                                        <td>
                                                            <asp:CheckBoxList ID="chkTranItemFilter_Type" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal" Visible="true">
                                                            </asp:CheckBoxList>
                                                            <asp:Button ID="TranItemFilterbtn_SelectAllType" Width="100" runat="server" OnCommand="TranItemFilterTypeSelection" Text="[select all]" CommandName="A" CssClass="button" />
                                                            <asp:Button ID="TranItemFilterbtn_UnselectAllType" Width="100" runat="server" OnCommand="TranItemFilterTypeSelection" Text="[unselect all]" CommandName="U" CssClass="button" />
                                                            <asp:Label ID="txtTranItemFilter_ItemTypeID" runat="server" Visible="false" Text="" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTranItemFilter_Lang" runat="server" AssociatedControlID="lblTranItemFilter_Lang" Text="Language:    " /></td>
                                                        <td>
                                                            <asp:CheckBoxList ID="chkTranItemFilter_Lang" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal" Visible="true">
                                                            </asp:CheckBoxList>
                                                            <asp:Button ID="TranItemFilterbtn_SelectAllLang" Width="100" runat="server" OnCommand="TranItemFilterLangSelection" Text="[select all]" CommandName="A" CssClass="button" />
                                                            <asp:Button ID="TranItemFilterbtn_UnselectAllLang" Width="100" runat="server" OnCommand="TranItemFilterLangSelection" Text="[unselect all]" CommandName="U" CssClass="button" />
                                                            <asp:Label ID="txtTranItemFilter_ItemLangID" runat="server" Visible="false" Text="" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTranItemFilter_ID" runat="server" AssociatedControlID="lblTranItemFilter_ID" Text="ID:" Visible="false" /></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTranItemFilter_ID" runat="server" CssClass="textEntry" Enabled="true" Text="999999" Visible="false" /><asp:TextBox ID="txtTranItemFilter_RedirectStatus" runat="server" CssClass="textEntry" Enabled="false" Text="" Visible="false" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTranItemFilter_ItemCode" runat="server" AssociatedControlID="lblTranItemFilter_ItemCode" Text="Item No.: " /></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTranItemFilter_ItemCode" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                                                    </tr>
                                                    <%--<tr>
                                                        <td>
                                                            <asp:Label ID="lblTranItemFilter_ItemCopiesID" runat="server" AssociatedControlID="lblTranItemFilter_ItemCopiesID" Text="Item Copies ID: " /></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTranItemFilter_ItemCopiesID" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTranItemFilter_Description" runat="server" AssociatedControlID="lblTranItemFilter_Description" Text="Item Name: " /></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTranItemFilter_Description" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                                                    </tr>

                                                    <%--<tr>
                                                        <td>
                                                            <asp:Label ID="lblTranItemFilter_Status" runat="server" AssociatedControlID="lblTranItemFilter_Status" Text="Status:    " /></td>
                                                        <td>
                                                            <asp:CheckBoxList ID="checkBoxList_Status" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal" Visible="true">
                                                                <asp:ListItem Text="Active" Value="1" Selected="True">Active</asp:ListItem>
                                                                <asp:ListItem Text="Inactive" Value="0" Selected="True">Inactive</asp:ListItem>
                                                            </asp:CheckBoxList>
                                                            <asp:Label ID="txtTranItemFilter_Status" runat="server" Visible="false" Text="" />
                                                        </td>
                                                    </tr>--%>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTranItemFilter_Dept" runat="server" AssociatedControlID="lblTranItemFilter_Dept" Text="Department:    " Visible="false" /></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlTranItemFilter_Dept" runat="server" Enabled="true" Visible="false">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTranItemFilter_NeedApproval" runat="server" AssociatedControlID="lblTranItemFilter_NeedApproval" Text="Need Approval:    " Visible="false" /></td>
                                                        <td>
                                                            <asp:CheckBoxList ID="checkBoxList_NeedApproval" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal" Visible="false">
                                                                <asp:ListItem Text="Active" Value="1" Selected="True">Yes</asp:ListItem>
                                                                <asp:ListItem Text="Active" Value="0" Selected="True">No</asp:ListItem>
                                                            </asp:CheckBoxList>
                                                            <asp:Label ID="txtTranItemFilter_NeedApproval" runat="server" Visible="false" Text="" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTranItemFilter_GlobalUse" runat="server" AssociatedControlID="lblTranItemFilter_GlobalUse" Text="Global Use:    " Visible="false" /></td>
                                                        <td>
                                                            <asp:CheckBoxList ID="checkBoxList_GlobalUse" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal" Visible="false">
                                                                <asp:ListItem Text="Active" Value="1" Selected="True">Yes</asp:ListItem>
                                                                <asp:ListItem Text="Active" Value="0" Selected="True">No</asp:ListItem>
                                                            </asp:CheckBoxList>
                                                            <asp:Label ID="txtTranItemFilter_GlobalUse" runat="server" Visible="false" Text="" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td><strong>
                                                            <asp:Label ID="lblTranItemFilter_RequestDateFrom" runat="server" Text="Request Date From:    " />
                                                        </strong></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTranItemFilter_RequestDateFrom" runat="server" CssClass="textEntry" Enabled="false" Text="" />
                                                            <strong>
                                                                <asp:Label ID="lblTranItemFilter_RequestDateTo" runat="server" Text="To:" />
                                                            </strong>
                                                            <asp:TextBox ID="txtTranItemFilter_RequestDateTo" runat="server" CssClass="textEntry" Enabled="false" Text="" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTranItemFilter_NoOfLagDay" runat="server" AssociatedControlID="lblTranItemFilter_NoOfLagDay" Text="No Of Lag Day:    " /></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTranItemFilter_NoOfLagDay" runat="server" CssClass="textEntry" Enabled="false" Text="" /></td>
                                                    </tr>
                                                    
                                                    <%-- J20180605 --%>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTranItemFilter_WindowsAuthUserID" runat="server" AssociatedControlID="lblTranItemFilter_WindowsAuthUserID" Visible="false" Text="Window Auth User ID:    "></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTranItemFilter_WindowsAuthUserID" runat="server" CssClass="textEntry" Enabled="false" Visible="false" Text="" />
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="TranItemFilterBtn" runat="server" CssClass="button" OnClick="btnAdd_TranItemFilter_Click" Text="Search Item" ValidationGroup="TranItemFilterItemValidationGroup" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="TranItemFilterClearBtn" runat="server" CssClass="button" OnClick="TranItemFilterClearBtn_Click" Text="Clear Filter" />
                                                        </td>
                                                    </tr>

                                                </table>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="GridViewItems" runat="server"
                                                                DataSourceID="odsItems" AutoGenerateColumns="true"
                                                                OnRowCommand="gvItem_RowCommand"
                                                                OnRowDataBound="GridViewItems_RowDataBound"
                                                                ShowHeader="true" AllowSorting="true"
                                                                AllowPaging="true" OnPageIndexChanging="PageItemIndexChanging"
                                                                SortedAscendingHeaderStyle-Font-Underline="true" PageSize="10">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Select">
                                                                        <ItemStyle VerticalAlign="Top" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <HeaderStyle Width="10%" />
                                                                        <ItemTemplate>
                                                                            <%--                                        <asp:CheckBox ID="chkAddItem" runat="server" CommandName="A" CommandArgument='<%# Eval("ID") %>' OnRowCommand="Tran_AddItem"/>--%>
                                                                            <asp:CheckBox ID="chkAddItem" runat="server" CommandName="A" CommandArgument='<%# Eval("ID") %>' />
                                                                        </ItemTemplate>
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkboxSelectAll" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" runat="server" />
                                                                        </HeaderTemplate>
                                                                        <ItemStyle VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <%--                                                                    <asp:TemplateField HeaderText="View Item Transaction">
                                                                        <ItemStyle VerticalAlign="Top" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <HeaderStyle Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnAdd_ViewItemTxn" runat="server" Text="View" CommandArgument='<%# Eval("ID") %>' CssClass="button" OnCommand="btnAdd_ViewItemTxn_Click"></asp:Button>
                                                                        </ItemTemplate>
                                                                        <ItemStyle VerticalAlign="Top" />
                                                                    </asp:TemplateField>--%>


                                                                    <asp:TemplateField HeaderText="View Transaction">
                                                                        <ItemStyle VerticalAlign="Top" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <HeaderStyle Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnItemViewTxn" runat="server" Text="View Txn" CommandArgument='<%# Eval("ID") %>' CssClass="button" OnCommand="Tran_AddItem_ViewTxn"></asp:Button>
                                                                        </ItemTemplate>
                                                                        <ItemStyle VerticalAlign="Top" />
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="BorrowingQty (AvailableQty)">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="BorrowingQty" Width="40" runat="server" Text="1"></asp:TextBox>
                                                                            <asp:TextBox ID="AvailableQty" Width="40" runat="server" Enabled="false"></asp:TextBox>
                                                                            <asp:CompareValidator ID="CompareValidatorBorrowingQty" runat="server"
                                                                                Operator="LessThanEqual" ControlToCompare="AvailableQty" ControlToValidate="BorrowingQty"
                                                                                CssClass="failureNotification"
                                                                                ErrorMessage="BorrowingQty is incorrect. " Type="Integer"
                                                                                Display="Dynamic" ValidationGroup="CheckAddSelectedItemsValidationGroup_Add">* Borrowing Qty cannot be greater than Available Qty!
                                                                            </asp:CompareValidator>
                                                                            <asp:RequiredFieldValidator
                                                                                ID="RequiredFieldValidatorBorrowingQty" runat="server"
                                                                                CssClass="failureNotification"
                                                                                ErrorMessage="BorrowingQty cannot be blank! "
                                                                                ControlToValidate="BorrowingQty">* Required!
                                                                            </asp:RequiredFieldValidator>
                                                                            <asp:RegularExpressionValidator
                                                                                ID="RegularExpressionValidatorBorrowingQty" runat="server" ControlToValidate="BorrowingQty"
                                                                                ValidationExpression="^[1-9]\d*$"
                                                                                CssClass="failureNotification" ErrorMessage="BorrowingQty is incorrect. " ToolTip=""
                                                                                Display="Dynamic" ValidationGroup="CheckAddSelectedItemsValidationGroup_Add">* Positive integar > 0 only!
                                                                            </asp:RegularExpressionValidator>


                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>

                                                                <%--<EmptyDataTemplate>
                                                                    No Record Found!
                                                                </EmptyDataTemplate>--%>

                                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="20" />
                                                                <SortedAscendingHeaderStyle Font-Underline="True" />

                                                            </asp:GridView>

                                                    </tr>




                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnGetSelected" CssClass="button" runat="server" Text="Add Selected Item(s)" OnClick="GetSelectedRecords" OnClientClick="if(!confirm('Confirm to add selected item(s)?')) {return false;};" ValidationGroup="CreateTranAddItemValidationGroup" />
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:MultiView ID="mvAdd_AddItem_ViewTxn" runat="server" ActiveViewIndex="0">
                                                                <asp:View ID="vAdd_AddItem_ViewTxn" runat="server">
                                                                    <table>
                                                                        <asp:ObjectDataSource ID="odsAdd_AddItem_ViewTxn" runat="server" TypeName="ResourceBorrowing.Items.ItemController" SelectMethod="GetTxnByItemID">
                                                                            <SelectParameters>
                                                                                <asp:ControlParameter ControlID="txtFilter_txnItemID_Add" Name="iItemID" Type="String" DefaultValue="" />
                                                                            </SelectParameters>
                                                                            <SelectParameters>
                                                                                <asp:ControlParameter ControlID="txtFilter_txnStatus_Add" Name="sStatus" Type="String" DefaultValue="1,2" />
                                                                            </SelectParameters>
                                                                        </asp:ObjectDataSource>
                                                                        <tr>
                                                                            <td class="auto-style5">
                                                                                <em><strong>View Item Transaction:</strong></em>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="auto-style7">
                                                                                <asp:TextBox ID="txtFilter_txnItemID_Add" runat="server" CssClass="textEntry" Enabled="false" Visible="false" Text="" />
                                                                                <asp:TextBox ID="txtFilter_txnStatus_Add" runat="server" Enabled="false" Visible="false" Text="1,2" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <asp:GridView ID="GridViewTxns_Add" runat="server" DataSourceID="odsAdd_AddItem_ViewTxn"
                                                                                AutoGenerateColumns="true"
                                                                                ShowHeader="true" AllowSorting="true" AllowPaging="true"
                                                                                SortedAscendingHeaderStyle-Font-Underline="true"
                                                                                OnRowDataBound="GridViewTxns_Add_RowDataBound" />
                                                                            <sortedascendingheaderstyle font-underline="True" />
                                                                            </asp:GridView>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Button ID="btnAdd_ViewTxn_Close" CssClass="button" runat="server" Text="Close View Txn" OnClick="btnAdd_ViewTxn_Close_OnClick" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:View>
                                                                <asp:View ID="vAdd_AddItem_ViewTxn_Disable" runat="server">
                                                                </asp:View>
                                                            </asp:MultiView>
                                                        </td>
                                                    </tr>


                                                </table>


                                            </asp:Panel>
                                        </asp:View>
                                        <asp:View ID="vSelectedItem" runat="server">
                                        </asp:View>
                                        <asp:View ID="vDisableSelectItem" runat="server">
                                        </asp:View>
                                    </asp:MultiView>
                                </td>
                            </tr>
                            <tr>
                                <td class="TranColWidth">
                                    <asp:Label ID="lblAdd_Remarks" runat="server" AssociatedControlID="lblAdd_Remarks" Text="Remarks:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAdd_Remarks" TextMode="MultiLine" runat="server" CssClass="textEntry" Enabled="true" Text="" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="AddTranBtn" runat="server" Text="Save" CommandName="A" CommandArgument="A" OnCommand="AddTranBtn_Click" ValidationGroup="AddTransactionValidationGroup" OnClientClick="if(!confirm('Confirm to save?')) {return false;};" CssClass="button" />
                                    &nbsp;
                                    <asp:Button ID="btnAdd_AddTran_Print" runat="server" Text="Save & Print Confirmation Note" CommandName="P" CommandArgument="P" OnCommand="AddTranBtn_Click" ValidationGroup="AddTransactionValidationGroup" OnClientClick="if(!confirm('Confirm to save?')) {return false;};" CssClass="button" />
                                    &nbsp;
                                    <asp:Button ID="btnAdd_AddTran_Back" runat="server" CssClass="button" OnClick="btnAdd_AddTran_Back_Click" Text="Back" />
                                </td>
                            </tr>
                        </table>

                    </div>

                </fieldset>

                <%--            <asp:Button ID="CancelAddTranBtn" runat="server" Text="Cancel" OnClientClick="if(!confirm('Confirm to cancel?')) {return false;};" OnClick="CancelAddTranBtn_Click" CssClass="button" />--%>
            </div>
        </asp:View>

        <asp:View ID="vTransactionPrintOut" runat="server">
            <h2>Transaction - Print Out
            </h2>
            <asp:TextBox ID="txtPrintTranID" runat="server" CssClass="textEntry" Enabled="false" Visible="false" />
            <asp:TextBox ID="txtPrintRefNo" runat="server" CssClass="textEntry" Enabled="false" Visible="false" />
            <asp:Button ID="BtnPrintBackToList" runat="server" Text="Back" OnClick="BackToListBtn_Click" CssClass="button" />
            <!--<iframe id="ifrmPrintRBS" name="ifrmPrintRBS" scrolling="auto" runat="server" height="1000" width="100%" />-->
            <asp:Label ID="lblReportErrorMessage" runat="server" />
            <table>
                <tr>
                    <td colspan="2">
                        <ssrs:ReportViewer ID="ConfirmNoteRptViewer" runat="server" Visible="true" Width="95%" Height="45%" ShowCredentialPrompts="false" ShowParameterPrompts="false" ShowPrintButton="True" />
                    </td>
                </tr>
            </table>
        </asp:View>

        <asp:View ID="vEditTransaction" runat="server">

            <asp:ObjectDataSource ID="odsTransactionDetails" runat="server" TypeName="ResourceBorrowing.Transaction.TransactionController" SelectMethod="getTransactionDetails">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtEdit_TranDetails_ID" Name="iID" Type="String" DefaultValue="0" />
                </SelectParameters>
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtEdit_TranDetails_TranID" Name="iTranID" Type="String" DefaultValue="0" />
                </SelectParameters>
            </asp:ObjectDataSource>

            <h2>Edit Transaction
            </h2>
            <%--<span class="failureNotification">
                <asp:Literal ID="ErrorMessageEdit" runat="server"></asp:Literal>
            </span>--%>
            <div class="Transaction">
                <fieldset class="Transaction">
                    <span class="failureNotification">
                        <asp:Literal ID="FailureEditText" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="CreateTranValidationSummary_Edit" runat="server" ForeColor="Red" CssClass="failureNotification" ValidationGroup="CreateTranValidationGroup_Edit" />
                    <legend>Transaction Information</legend>

                    <div class="leftCorner">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="txtEdit_TranDetails_ID" runat="server" Visible="false" Text="0" /></td>
                                <td>
                                    <asp:Label ID="txtEdit_TranDetails_TranID" runat="server" Visible="false" Text="" />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lblEdit_TranID" runat="server" AssociatedControlID="lblEdit_TranID" Text="Transaction ID:" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEdit_TranID" runat="server" CssClass="textEntry" Enabled="false" Text="" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEdit_RefNo" runat="server" AssociatedControlID="lblEdit_RefNo" Text="Ref. No.:" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEdit_RefNo" runat="server" CssClass="textEntry" Enabled="false" Text="" />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lblEdit_TranType" runat="server" AssociatedControlID="lblEdit_TranType" Text="Type:" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlEdit_TranType" runat="server" Enabled="true">
                                        <asp:ListItem Text="Client" Value="1" />
                                        <asp:ListItem Text="Internal User" Value="2" />
                                    </asp:DropDownList>
                                    <asp:Label ID="lblEdit_TranTypeValue" runat="server" Text="" Visible="false" />
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style1" colspan="2"><strong>Borrowing User:</strong></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="txtEdit_Client" runat="server" Style="font-style: italic" Text="For Client:"></asp:Label>
                                </td>
                                <%--                            <td>
                                <asp:Button ID="btnEdit_TranSelectClient" runat="server" CssClass="button" Enabled="true" OnClick="btnEdit_TranSelectClient_Click" Text="Edit Client" Visible="true" />
                            </td>--%>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEdit_ClientName" runat="server" AssociatedControlID="lblEdit_ClientName" Text="Client Name:    "></asp:Label>
                                </td>
                                <td>
                                    <%--                                <asp:TextBox ID="txtEdit_ClientName" runat="server" CssClass="textEntry" Enabled="true" Text="" Visible="false" />    --%>

                                    <%--                                <asp:TextBox ID="txtEdit_ClientNameValue" runat="server" CssClass="textEntry" Enabled="false" Text="" />--%>
                                    <%--                                <asp:Label ID="lblEdit_ClientNameDesc" runat="server" Visible="true" Text="" />--%>
                                    <asp:TextBox ID="txtEdit_ClientNameDesc" runat="server" CssClass="textEntry" Enabled="false" Text="" />

                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style3">
                                    <asp:Label ID="lblEdit_ContactPersonName" runat="server" AssociatedControlID="lblEdit_ContactPersonName" Text="Contact Person:    "></asp:Label>
                                </td>
                                <td class="auto-style3">
                                    <asp:DropDownList ID="ddlEdit_ContactPersonName" runat="server" Enabled="true" AutoPostBack="true" OnSelectedIndexChanged="ddlEdit_ContactPersonName_SelectedIndexChanged" />
                                    <asp:Label ID="lblEdit_ContactPersonNameValue" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lblEdit_ContactPersonNameValue_Old" runat="server" Text="" Visible="false" />
                                </td>
                            </tr>



                            <%--<asp:MultiView ID="mvEdit_ClientList" runat="server" ActiveViewIndex="0">
                        <asp:View ID="vEdit_SelectClient" runat="server">
                            <asp:Panel ID="panelEdit_ClientList" runat="server" DefaultButton="FilterBtn" BorderWidth="2" BorderColor="LightGray" BorderStyle="Inset">
                                <asp:ObjectDataSource ID="odsEdit_Client" runat="server" TypeName="ResourceBorrowing.Clients.ClientController" SelectMethod="getClient">
                                    <SelectParameters><asp:ControlParameter ControlID="ddlEdit_TranClientFilter_ClientType" PropertyName="SelectedValue" Name="sClientTypeID" Type="String" /></SelectParameters>
                                    <SelectParameters><asp:ControlParameter ControlID="txtEdit_TranClientFilter_ClientName" Name="sClientName" Type="String" DefaultValue="" /> </SelectParameters>
                                    <SelectParameters><asp:ControlParameter ControlID="txtEdit_TranClientFilter_ContactPersonName" Name="sContactPersonName" Type="String" DefaultValue="" /> </SelectParameters>
                                    <SelectParameters><asp:ControlParameter ControlID="txtEdit_TranClientFilter_ClientPhoneNo" Name="sClientPhoneNo" Type="String" DefaultValue="" /> </SelectParameters>
                                    <SelectParameters><asp:ControlParameter ControlID="txtEdit_TranClientFilter_ContactPersonPhoneNo" Name="sContactPersonPhoneNo" Type="String" DefaultValue="" /> </SelectParameters>
                                </asp:ObjectDataSource>

                                <asp:ValidationSummary ID="FilterClientValidationSummaryEdit" runat="server" CssClass="failureNotification"
                                                       ValidationGroup="FilterClientValidationGroup" />

                                <table>
                                    <tr>
                                        <td style="height: 22px">
                                            <asp:Label ID="lblEdit_TranClientFilter" runat="server" AssociatedControlID="lblEdit_TranClientFilter" Text="Search Client" Font-Bold="true" Font-Underline="true" />
                                            <asp:TextBox ID="txtEdit_TranClientFilter_RedirectStatus" runat="server" CssClass="textEntry" Enabled="false" Text="" Visible="false" />
                                        </td>
                                        <td style="height: 22px"> </td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="lblEdit_TranClientFilter_ClientType" runat="server" AssociatedControlID="lblEdit_TranClientFilter_ClientType" Text="Client Type:" Visible="true" /></td>
                                        <td>
                                            <asp:DropDownList ID="ddlEdit_TranClientFilter_ClientType" runat="server" Enabled="true">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="lblEdit_TranClientFilter_ClientName" runat="server" AssociatedControlID="lblEdit_TranClientFilter_ClientName" Text="Client Name:" /></td>
                                        <td><asp:TextBox ID="txtEdit_TranClientFilter_ClientName" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="lblEdit_TranClientFilter_ClientPhoneNo" runat="server" AssociatedControlID="lblEdit_TranClientFilter_ClientPhoneNo" Text="Client Phone No.: " /></td>
                                        <td><asp:TextBox ID="txtEdit_TranClientFilter_ClientPhoneNo" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="lblEdit_TranClientFilter_ContactPersonName" runat="server" AssociatedControlID="lblEdit_TranClientFilter_ContactPersonName" Text="Contact Person Name:" /></td>
                                        <td><asp:TextBox ID="txtEdit_TranClientFilter_ContactPersonName" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                                    </tr>

                                    <tr>
                                        <td><asp:Label ID="lblEdit_TranClientFilter_ContactPersonPhoneNo" runat="server" AssociatedControlID="lblEdit_TranClientFilter_ContactPersonPhoneNo" Text="Contact Person Phone No.: " /></td>
                                        <td><asp:TextBox ID="txtEdit_TranClientFilter_ContactPersonPhoneNo" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Button ID="btnEdit_TranClientFilter" runat="server" Text="Apply Filter" ValidationGroup="TranClientFilterClientValidationGroup" OnClick="btnEdit_TranClientFilter_Click" CssClass="button" /></td>
                                        <td><asp:Button ID="btnEdit_TranClientFilterClear" runat="server" Text="Clear Filter" OnClick="btnEdit_TranClientFilterClear_Click" CssClass="button" /></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                    </tr>

                                </table>



                                <asp:GridView ID="GridViewClientForms_edit" runat="server" DataSourceID="odsEdit_Client"
                                              AutoGenerateColumns="true"
                                              OnRowCommand="gvClient_RowCommand_edit"
                                              ShowHeader="true" AllowSorting="true"
                                              AllowPaging="true" OnPageIndexChanging="PageClientFormIndexChanging_edit"
                                              SortedAscendingHeaderStyle-Font-Underline="true" PageSize="10"
                                              OnRowDataBound="GridViewClientForms_RowDataBound" PagerStyle-CssClass="pager">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <HeaderStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:Button ID="lblEdit_SelectClient" runat="server" Text="Select" CommandArgument='<%# Eval("ClientID") %>' CssClass="button" OnCommand="Tran_SelectClient_edit"></asp:Button>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>



                                    <EmptyDataTemplate>
                                        No Record Found!
                                    </EmptyDataTemplate>

                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" />
                                    <PagerStyle Font-Bold="False" />
                                    <SortedAscendingHeaderStyle Font-Underline="True" />
                                </asp:GridView>
                            </asp:Panel>
                        </asp:View>
                        <asp:View ID="vEdit_DisableSelectClient" runat="server">
                        </asp:View>
                    </asp:MultiView>--%>


                            <%--<tr>
                            <td class="auto-style1"><strong>Borrowing Period:</strong></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEdit_BorrowDateFrom" runat="server" AssociatedControlID="lblEdit_BorrowDateFrom" Text="From:    "></asp:Label>
                            </td>
                        <tr>
                            <td>
                                <asp:Label ID="lblEdit_BorrowDateTo" runat="server" AssociatedControlID="lblEdit_BorrowDateTo" Text="To:    "></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEdit_NoOfLagDay" runat="server" AssociatedControlID="lblEdit_NoOfLagDay" Text="No. Of Lag Days:"></asp:Label>
                            </td>
                            <td></td>
                        </tr>--%>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="txtEdit_InternalUser" runat="server" Style="font-style: italic" Text="For Internal User:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEdit_InternalUserDept" runat="server" AssociatedControlID="lblEdit_InternalUserDept" Text="Internal User Dept.:    "></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlEdit_InternalUserDept" runat="server" AutoPostBack="true" Enabled="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEdit_InternalUserName" runat="server" AssociatedControlID="lblEdit_InternalUserName" Text="Internal User Name:    "></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlEdit_InternalUserName" runat="server" Enabled="true">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblEdit_InternalUserName_Old" runat="server" Text="" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1"><strong>Item(s):</strong></td>
                                <td>
                                    <asp:Button ID="btnEdit_TranAddItem" runat="server" Text="Add Item" OnClick="btnEdit_TranAddItem_Click" CssClass="button" Visible="true" />
                                    <asp:Button ID="btnEdit_TranRefresh" runat="server" Text="Refresh" OnClick="btnEdit_TranRefresh_Click" CssClass="button" Visible="true" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1"></td>
                                <td>
                                    <asp:GridView ID="gvShow_Selected_Edit" runat="server" AutoGenerateColumns="true" HeaderStyle-BackColor="orange" HeaderStyle-ForeColor="White"
                                        OnRowDataBound="gvShow_Selected_Edit_RowDataBound">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkShowEditItem" runat="server" CommandName="E" CommandArgument='<%# Eval("ID") %>' />
                                                    <%--                                                <asp:TextBox ID="txtDate1" runat="server" CommandName="E" CommandArgument='<%# Eval("ID") %>' />
                                                <asp:TextBox ID="txtDate2" runat="server" CommandName="E" CommandArgument='<%# Eval("ID") %>' />
                                                <asp:TextBox ID="txtQty" runat="server" CommandName="E" CommandArgument='<%# Eval("ID") %>' />
                                                    --%>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkboxShowEditSelectAll" AutoPostBack="true" OnCheckedChanged="chkboxShowEditSelectAll_CheckedChanged" runat="server" />
                                                </HeaderTemplate>
                                                <ItemStyle VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="View Transaction">
                                                <ItemStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Button ID="btnSelectedItemViewTxn_Edit" runat="server" Text="View Txn" CommandArgument='<%# Eval("itemID") %>' CssClass="button" OnCommand="Tran_SelectedItem_ViewTxn_Edit"></asp:Button>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <%--                            <tr>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="failureNotification" Display="Dynamic" ErrorMessage="Please select at least 1 item." onservervalidate="CustomValidator_EditTran" ToolTip="">
                                </asp:CustomValidator>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="failureNotification" 
                                    Display="Dynamic" ErrorMessage="Please select at least 1 item." ClientValidationFunction="Validate" >                                   
                                </asp:CustomValidator>
                                <script type="text/javascript">
                                    function Validation(source, args) {
                                        var gridView = document.getElementById("<%=gvShow_Selected_Edit.ClientID %>");
                                        var checkBoxes = gridView.getElementsByTagName("input");
                                        var args.IsValid = false;

                                        for (var i = 0; i < checkBoxes.length; i++) {
                                            if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                                                args.IsValid = true;
                                                return;
                                            }
                                        }
                                        args.IsValid = false;
                                    }
                                </script> 
                            </tr>--%>
                            <tr>
                                <td></td>
                                <td>
                                    <%--                                      <asp:Button ID="btnEdit_ViewItemTxn" runat="server" Text="View Item Transaction" OnClick="btnEdit_ViewItemTxn_Click" CssClass="button" Visible="true" OnClientClick="javascript:return Validation();" />
                                  <asp:Button ID="btnEdit_Edit" runat="server" Text="Edit Booking" OnClick="btnEdit_Edit_Click" CssClass="button" Visible="true" ValidationGroup="EditTranValidationGroup" />
                                    <asp:Button ID="btnEdit_Edit" runat="server" Text="Edit Booking" OnClick="btnEdit_Edit_Click" CssClass="button" Visible="true" OnClientClick="javascript:return Validation();" />--%>
                                    <asp:Button ID="btnEdit_Edit" runat="server" Text="Edit Booking" OnClick="btnEdit_Edit_Click" CssClass="button" Visible="true" />

                                    <asp:Button ID="btnEdit_Cancel" runat="server" Text="Cancel Booking" OnClick="btnEdit_ActionC_Save_Click" OnClientClick="if(!confirm('Confirm to cancel selected booked item(s)?')) {return false;};" CssClass="button" Visible="true" />
                                    <%--                                    <asp:Button ID="btnEdit_Cancel" runat="server" Text="Cancel Booking" OnClick="btnEdit_ActionC_Save_Click" OnClientClick="if (javascript:Validation()) if(!confirm('Confirm to cancel selected booked item(s)?')) {return false;};" CssClass="button" Visible="true" />--%>

                                    <%--                                    <asp:Button ID="btnEdit_Borrow" runat="server" Text="Borrow" OnClick="btnEdit_ActionB_Save_Click" OnClientClick="if(!confirm('Confirm to borrow selected item(s)?')) {return false;};" CssClass="button" Visible="true" />--%>
                                    <asp:Button ID="btnEdit_Borrow" runat="server" Text="Borrow" OnClick="btnEdit_Borrow_Click" CssClass="button" Visible="true" />
                                    <asp:Button ID="btnEdit_Renew" runat="server" Text="Renew" OnClick="btnEdit_Renew_Click" CssClass="button" Visible="true" />
                                    <asp:Button ID="btnEdit_Return" runat="server" Text="Return" OnClick="btnEdit_Return_Click" CssClass="button" Visible="true" />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:MultiView ID="mvEdit_SelectedItem_ViewTxn" runat="server" ActiveViewIndex="0">
                                        <asp:View ID="vEdit_SelectedItem_ViewTxn" runat="server">
                                            <table>
                                                <asp:ObjectDataSource ID="odsEdit_SelectedItem_ViewTxn" runat="server" TypeName="ResourceBorrowing.Items.ItemController" SelectMethod="GetTxnByItemID">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtFilter_txnItemID" Name="iItemID" Type="String" DefaultValue="" />
                                                    </SelectParameters>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtFilter_txnStatus" Name="sStatus" Type="String" DefaultValue="1,2" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                                <tr>
                                                    <td class="auto-style5" colspan="2">
                                                        <em><strong>View Item Transaction:</strong></em>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFilter_txnItemID" runat="server" CssClass="textEntry" Enabled="false" Visible="false" Text="999999" />
                                                        <asp:TextBox ID="txtFilter_txnStatus" runat="server" Enabled="false" Visible="false" Text="1,2" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="GridViewTxns_SelectedItem" runat="server" DataSourceID="odsEdit_SelectedItem_ViewTxn"
                                                            AutoGenerateColumns="true"
                                                            ShowHeader="true" AllowSorting="true" AllowPaging="true"
                                                            SortedAscendingHeaderStyle-Font-Underline="true"
                                                            OnRowDataBound="GridViewTxns_SelectedItem_RowDataBound">
                                                            <EmptyDataTemplate>
                                                                No Record Found!
                                                            </EmptyDataTemplate>
                                                            <SortedAscendingHeaderStyle Font-Underline="True" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnEdit_SelectedItem_ViewTxn_Close" CssClass="button" runat="server" Text="Close View Txn" OnClick="btnEdit_SelectedItem_ViewTxn_Close_OnClick" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <asp:View ID="vEdit_SelectedItem_ViewTxn_Disable" runat="server">
                                        </asp:View>
                                    </asp:MultiView>
                                </td>
                            </tr>

                            <tr>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:GridView ID="gvEdit_Selected" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="orange" HeaderStyle-ForeColor="White" OnRowDeleting="gvEdit_Selected_RowDeleting">
                                        <Columns>
                                            <asp:CommandField ButtonType="Button" ControlStyle-CssClass="button" DeleteText="X" ShowDeleteButton="True" />
                                            <asp:BoundField DataField="ItemID" HeaderText="ItemID" ItemStyle-Width="80" />
                                            <asp:BoundField DataField="ItemNo" HeaderText="ItemNo" ItemStyle-Width="80" />
                                            <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="150" />
                                            <asp:BoundField DataField="AvailableQty" HeaderText="Available Qty" ItemStyle-Width="100" />
                                            <asp:BoundField DataField="BorrowingQty" HeaderText="Borrowing Qty" ItemStyle-Width="100" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Button ID="BtnEdit_AddItem" runat="server" CommandArgument="A" CommandName="A" CssClass="button" OnClientClick="if(!confirm('Confirm to add selected item(s)?')) {return false;};" OnCommand="BtnEdit_AddItem_Click" Text="Confirm Add" Visible="false" />
                                    <asp:MultiView ID="mvEdit_Action" runat="server" ActiveViewIndex="0">

                                        <asp:View ID="vEdit_Action_Edit" runat="server">
                                            <asp:Panel ID="panelEdit_Action_Edit" runat="server" BorderWidth="2" BorderColor="LightGray" BorderStyle="Inset">
                                                <table>
                                                    <tr>
                                                        <td class="auto-style5" colspan="2">
                                                            <em><strong>Edit Booking:</strong></em>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style5">Requesting Date From:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEdit_ActionE_BorrowDateFrom" runat="server" CssClass="textEntry" Enabled="true" Text="" />
                                                            <asp:Button ID="btnEdit_ActionE_BorrowDateFrom" runat="server" OnClick="btnEdit_ActionE_BorrowDateFrom_Click" Text="[Pick Date]" CssClass="button" />
                                                            (yyyy/mm/dd)<br />
                                                            <asp:Label ID="lblEdit_ActionE_BorrowDateFrom_Old" runat="server" Text="" Visible="false" />
                                                            <div>
                                                                <asp:Calendar ID="CalendarEdit_ActionE_BorrowDateFrom" runat="server" Visible="False" OnSelectionChanged="CalendarEdit_ActionE_BorrowDateFrom_SelectionChanged">
                                                                    <TodayDayStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" />
                                                                </asp:Calendar>
                                                            </div>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorBDateFrm_Edit_ActionE" runat="server" ControlToValidate="txtEdit_ActionE_BorrowDateFrom"
                                                                ValidationExpression="^[0-9]{4}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30)))$"
                                                                CssClass="failureNotification" ErrorMessage="Incorrect date format of Borrow Date (From)! " ToolTip=""
                                                                Display="Dynamic" ValidationGroup="Edit_ActionETransactionValidationGroup">* Wrong Date Format!
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style5">To:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEdit_ActionE_BorrowDateTo" runat="server" CssClass="textEntry" Enabled="true" Text="" style="height: 23px" />
                                                            <asp:Button ID="btnEdit_ActionE_BorrowDateTo" runat="server" OnClick="btnEdit_ActionE_BorrowDateTo_Click" Text="[Pick Date]" CssClass="button" />
                                                            (yyyy/mm/dd)<br />
                                                            <asp:Label ID="lblEdit_ActionE_BorrowDateTo_Old" runat="server" Text="" Visible="false" />
                                                            <div>
                                                                <asp:Calendar ID="CalendarEdit_ActionE_BorrowDateTo" runat="server" Visible="False" OnSelectionChanged="CalendarEdit_ActionE_BorrowDateTo_SelectionChanged">
                                                                    <TodayDayStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" />
                                                                </asp:Calendar>
                                                            </div>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorBDateTo_Edit_ActionE" runat="server" ControlToValidate="txtEdit_ActionE_BorrowDateTo"
                                                                ValidationExpression="^[0-9]{4}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30)))$"
                                                                CssClass="failureNotification" ErrorMessage="Incorrect date format of Borrow Date (To)! " ToolTip=""
                                                                Display="Dynamic" ValidationGroup="Edit_ActionETransactionValidationGroup">* Wrong Date Format!
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>No Of Lag Day:</td>
                                                        <td>
                                                            <asp:TextBox ID="txtEdit_ActionE_NoOfLagDay" runat="server" CssClass="textEntry" Enabled="true" Text="3" />
                                                            <asp:Label ID="txtEdit_ActionE_NoOfLagDay_Old" runat="server" Text="" Visible="false" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorActionE_NoOfLagDay" runat="server" ControlToValidate="txtEdit_ActionE_NoOfLagDay"
                                                                ValidationExpression="^[0-9]\d*$"
                                                                CssClass="failureNotification" ErrorMessage="No. Of Lag Days is incorrect. " ToolTip=""
                                                                Display="Dynamic" ValidationGroup="EditTransactionValidationGroup">* Positive integar only!
                                                            </asp:RegularExpressionValidator>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style5">
                                                            <asp:Button ID="btnEdit_ActionE_Save" runat="server" Text="Confirm Edit" OnClick="btnEdit_ActionE_Save_Click" OnClientClick="if(!confirm('Confirm to edit booking?')) {return false;};" CssClass="button" Visible="true" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnEdit_ActionE_Close" runat="server" Text="Close" OnClick="btnEdit_ActionE_Close_Click" CssClass="button" Visible="true" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </asp:View>
                                        <asp:View ID="vEdit_Action_Cancel" runat="server">
                                            <asp:Panel ID="panelEdit_Action_Cancel" runat="server" BorderWidth="2" BorderColor="LightGray" BorderStyle="Inset">
                                                <table>
                                                    <tr>
                                                        <em><strong>Cancel Booking:</strong></em>
                                                    </tr>
                                                </table>
                                            </asp:Panel>

                                        </asp:View>
                                        <asp:View ID="vEdit_Action_Borrow" runat="server">
                                            <asp:Panel ID="panelEdit_Action_Borrow" runat="server" BorderWidth="2" BorderColor="LightGray" BorderStyle="Inset">
                                                <table>
                                                    <tr>
                                                        <td colspan="2">
                                                            <em><strong>Borrow:</strong></em>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style5">To Borrow Date:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEdit_ActionB_BorrowDateFrom" runat="server" CssClass="textEntry" Enabled="true" Text="" />
                                                            <asp:Button ID="btnEdit_ActionB_BorrowDateFrom" runat="server" OnClick="btnEdit_ActionB_BorrowDateFrom_Click" Text="[Pick Date]" CssClass="button" />
                                                            (yyyy/mm/dd)<br />
                                                            <asp:Label ID="lblEdit_ActionB_BorrowDateFrom_Old" runat="server" Text="" Visible="true" />
                                                            <div>
                                                                <asp:Calendar ID="CalendarEdit_ActionB_BorrowDateFrom" runat="server" Visible="False" OnSelectionChanged="CalendarEdit_ActionB_BorrowDateFrom_SelectionChanged">
                                                                    <TodayDayStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" />
                                                                </asp:Calendar>
                                                            </div>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorBDateFrom_Edit_ActionB" runat="server" ControlToValidate="txtEdit_ActionB_BorrowDateFrom"
                                                                ValidationExpression="^[0-9]{4}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30)))$"
                                                                CssClass="failureNotification" ErrorMessage="Incorrect date format of To Borrow Date! " ToolTip=""
                                                                Display="Dynamic" ValidationGroup="Edit_ActionBTransactionValidationGroup">* Wrong Date Format!
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>

                                                    <%--                                                <tr>
                                                    <td class="auto-style5"> To Return Date:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEdit_ActionB_BorrowDateTo" runat="server" CssClass="textEntry" Enabled="true" Text="" />
                                                        <asp:Button ID="btnEdit_ActionB_BorrowDateTo" runat="server" OnClick="btnEdit_ActionB_BorrowDateTo_Click" Text="[Pick Date]" CssClass="button" />
                                                        (yyyy/mm/dd)<br />
                                                        <asp:Label ID="lblEdit_ActionB_BorrowDateTo_Old" runat="server" Text="" Visible="true" />
                                                        <div>
                                                            <asp:Calendar ID="CalendarEdit_ActionB_BorrowDateTo" runat="server" Visible="False" OnSelectionChanged="CalendarEdit_ActionB_BorrowDateTo_SelectionChanged">
                                                                <TodayDayStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" />
                                                            </asp:Calendar>
                                                        </div>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorBDateTo_Edit_ActionB" runat="server" ControlToValidate="txtEdit_ActionB_BorrowDateTo"
                                                            ValidationExpression="^[0-9]{4}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30)))$"
                                                            CssClass="failureNotification" ErrorMessage="Incorrect date format of Borrow Date (To)! " ToolTip=""
                                                            Display="Dynamic" ValidationGroup="Edit_ActionBTransactionValidationGroup">* Wrong Date Format!
                                                        </asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>No Of Lag Day:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtEdit_ActionB_NoOfLagDay" runat="server" CssClass="textEntry" Enabled="true" Text="3" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorActionB_NoOfLagDay" runat="server" ControlToValidate="txtEdit_ActionB_NoOfLagDay"
                                                            ValidationExpression="^[0-9]\d*$"
                                                            CssClass="failureNotification" ErrorMessage="No. Of Lag Days is incorrect. " ToolTip=""
                                                            Display="Dynamic" ValidationGroup=EditTransactionValidationGroup>* Positive integar only!
                                                        </asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>--%>

                                                    <tr>
                                                        <td class="auto-style5">
                                                            <asp:Button ID="btnEdit_ActionB_Save" runat="server" Text="Confirm Borrow" OnClick="btnEdit_ActionB_Save_Click" OnClientClick="if(!confirm('Confirm to borrow selected item(s)?')) {return false;};" CssClass="button" Visible="true" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnEdit_ActionB_Close" runat="server" Text="Close" OnClick="btnEdit_ActionB_Close_Click" CssClass="button" Visible="true" />
                                                        </td>
                                                    </tr>
                                                </table>

                                            </asp:Panel>
                                        </asp:View>
                                        <asp:View ID="vEdit_Action_Renew" runat="server">
                                            <asp:Panel ID="panelEdit_Action_Renew" runat="server" BorderWidth="2" BorderColor="LightGray" BorderStyle="Inset">
                                                <table>
                                                    <tr>
                                                        <td class="auto-style5" colspan="2">
                                                            <em><strong>Renew Borrowing:</strong></em>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style5">Borrow Date From: </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEdit_ActionN_BorrowDateFrom" runat="server" CssClass="textEntry" Enabled="false" Text="" />
                                                            <%--                                                    <asp:Button ID="btnEdit_ActionN_BorrowDateFrom" runat="server" CssClass="button" onclick="btnEdit_ActionN_BorrowDateFrom_Click" Text="[Pick Date]" />
                                                    (yyyy/mm/dd)<br />
                                                    <asp:Label ID="lblEdit_ActionN_BorrowDateFrom_Old" runat="server" Text="" Visible="true" />
                                                    <div>
                                                        <asp:Calendar ID="CalendarEdit_ActionN_BorrowDateFrom" runat="server" OnSelectionChanged="CalendarEdit_ActionN_BorrowDateFrom_SelectionChanged" Visible="False">
                                                            <TodayDayStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" />
                                                        </asp:Calendar>
                                                    </div>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorBDateFrm_Edit_ActionN" runat="server" ControlToValidate="txtEdit_ActionN_BorrowDateFrom" CssClass="failureNotification" Display="Dynamic" ErrorMessage="Incorrect date format of Borrow Date (From)! " ToolTip="" ValidationExpression="^[0-9]{4}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30)))$" ValidationGroup="Edit_ActionNTransactionValidationGroup">* Wrong Date Format!
											        </asp:RegularExpressionValidator>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style5">To:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEdit_ActionN_BorrowDateTo" runat="server" CssClass="textEntry" Enabled="true" Text="" />
                                                            <asp:Button ID="btnEdit_ActionN_BorrowDateTo" runat="server" OnClick="btnEdit_ActionN_BorrowDateTo_Click" Text="[Pick Date]" CssClass="button" />
                                                            (yyyy/mm/dd)<br />
                                                            <asp:Label ID="lblEdit_ActionN_BorrowDateTo_Old" runat="server" Text="" Visible="false" />
                                                            <div>
                                                                <asp:Calendar ID="CalendarEdit_ActionN_BorrowDateTo" runat="server" Visible="False" OnSelectionChanged="CalendarEdit_ActionN_BorrowDateTo_SelectionChanged">
                                                                    <TodayDayStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" />
                                                                </asp:Calendar>
                                                            </div>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorBDateTo_Edit_ActionN" runat="server" ControlToValidate="txtEdit_ActionN_BorrowDateTo"
                                                                ValidationExpression="^[0-9]{4}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30)))$"
                                                                CssClass="failureNotification" ErrorMessage="Incorrect date format of Borrow Date (To)! " ToolTip=""
                                                                Display="Dynamic" ValidationGroup="Edit_ActionNTransactionValidationGroup">* Wrong Date Format!
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>No Of Lag Day:</td>
                                                        <td>
                                                            <asp:TextBox ID="txtEdit_ActionN_NoOfLagDay" runat="server" CssClass="textEntry" Enabled="true" Text="3"  />
                                                            <asp:Label ID="txtEdit_ActionN_NoOfLagDay_Old" runat="server" Text="" Visible="false" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorActionN_NoOfLagDay" runat="server" ControlToValidate="txtEdit_ActionN_NoOfLagDay"
                                                                ValidationExpression="^[0-9]\d*$"
                                                                CssClass="failureNotification" ErrorMessage="No. Of Lag Days is incorrect. " ToolTip=""
                                                                Display="Dynamic" ValidationGroup="EditTransactionValidationGroup">* Positive integar only!
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style5">
                                                            <asp:Button ID="btnEdit_ActionN_Save" runat="server" Text="Confirm Renew" OnClick="btnEdit_ActionN_Save_Click" OnClientClick="if(!confirm('Confirm to renew borrowing?')) {return false;};" CssClass="button" Visible="true" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnEdit_ActionN_Close" runat="server" Text="Close" OnClick="btnEdit_ActionN_Close_Click" CssClass="button" Visible="true" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>

                                        </asp:View>
                                        <asp:View ID="vEdit_Action_Return" runat="server">
                                            <asp:Panel ID="panelEdit_Action_Return" runat="server" BorderWidth="2" BorderColor="LightGray" BorderStyle="Inset">
                                                <em><strong>Return:</strong></em>
                                                <table>
                                                    <tr>
                                                        <td class="auto-style5">To Return Date:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEdit_ActionR_ReturnDate" runat="server" CssClass="textEntry" Enabled="true" Text="" />
                                                            <asp:Button ID="btnEdit_ActionR_ReturnDate" runat="server" OnClick="btnEdit_ActionR_ReturnDate_Click" Text="[Pick Date]" CssClass="button" />
                                                            (yyyy/mm/dd)<br />
                                                            <asp:Label ID="lblEdit_ActionR_ReturnDate_Old" runat="server" Text="" Visible="true" />
                                                            <div>
                                                                <asp:Calendar ID="CalendarEdit_ActionR_ReturnDate" runat="server" Visible="False" OnSelectionChanged="CalendarEdit_ActionR_ReturnDate_SelectionChanged">
                                                                    <TodayDayStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" />
                                                                </asp:Calendar>
                                                            </div>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorRDate_Edit_ActionR" runat="server" ControlToValidate="txtEdit_ActionR_ReturnDate"
                                                                ValidationExpression="^[0-9]{4}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30)))$"
                                                                CssClass="failureNotification" ErrorMessage="Incorrect date format of Return Date! " ToolTip=""
                                                                Display="Dynamic" ValidationGroup="Edit_ActionRTransactionValidationGroup">* Wrong Date Format!
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style5">
                                                            <asp:Button ID="btnEdit_ActionR_Save" runat="server" Text="Confirm Return" OnClick="btnEdit_ActionR_Save_Click" OnClientClick="if(!confirm('Confirm to return selected item(s)?')) {return false;};" CssClass="button" Visible="true" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnEdit_ActionR_Close" runat="server" Text="Close" OnClick="btnEdit_ActionR_Close_Click" CssClass="button" Visible="true" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </asp:View>
                                        <asp:View ID="vEdit_Action_Disable" runat="server">
                                        </asp:View>

                                        <%--<asp:View ID="vEdit_Action_ViewItemTxn" runat="server">
                                            <table>
                                                <asp:ObjectDataSource ID="odsTxn" runat="server" TypeName="ResourceBorrowing.Items.ItemController" SelectMethod="GetTxnByItemID"  >
                                                    <SelectParameters><asp:ControlParameter ControlID="txtFilter_txnItemID" Name="iItemID" Type="String" DefaultValue="" /> </SelectParameters>
                                                    <SelectParameters><asp:ControlParameter ControlID="txtFilter_txnStatus" Name="sStatus" Type="String" DefaultValue="1,2" /> </SelectParameters>
                                                </asp:ObjectDataSource> 
                                                <tr>
                                                    <td class="auto-style5" colspan="2">
                                                        <em><strong>View Item Transaction:</strong></em>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><asp:TextBox ID="txtFilter_txnItemID" runat="server" CssClass="textEntry" Enabled="false" Visible="false" Text=""/>
                                                        <asp:TextBox ID="txtFilter_txnStatus" runat="server" Enabled="false" Visible="false" Text="1,2" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <asp:GridView ID="GridViewTxns" runat="server"  DataSourceID="odsTxn" 
                                                        AutoGenerateColumns="true"  
                                                        ShowHeader="true" AllowSorting="true" AllowPaging="true" 
                                                        SortedAscendingHeaderStyle-Font-Underline="true" >
                                                        <EmptyDataTemplate>
                                                            No Record Found!
                                                        </EmptyDataTemplate>
                                                        <SortedAscendingHeaderStyle Font-Underline="True" />
                                                    </asp:GridView>
                                                </tr>
                                            </table>
                                        </asp:View>--%>
                                    </asp:MultiView>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:MultiView ID="mvEdit_ItemList" runat="server" ActiveViewIndex="0">
                                        <asp:View ID="vEdit_SelectItem" runat="server">
                                            <asp:Panel ID="panelEdit_ItemList" runat="server" DefaultButton="btnEdit_TranItemFilter" BorderWidth="2" BorderColor="LightGray" BorderStyle="Inset">

                                                <asp:ObjectDataSource ID="odsEdit_Items" runat="server" TypeName="ResourceBorrowing.Items.ItemController" SelectMethod="getAvailableItems">

                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtEdit_TranItemFilter_ID" Name="iID" Type="String" DefaultValue="999999" />
                                                    </SelectParameters>
                                                    <%--<SelectParameters>
                                                        <asp:ControlParameter ControlID="txtEdit_TranItemFilter_Status" Name="sStatus" Type="String" DefaultValue="" />
                                                    </SelectParameters>--%>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtEdit_TranItemFilter_ItemCode" Name="sItemCode" Type="String" DefaultValue="" />
                                                    </SelectParameters>
                                                    <%--<SelectParameters>
                                                        <asp:ControlParameter ControlID="txtEdit_TranItemFilter_ItemCopiesID" Name="sItemCopiesID" Type="String" DefaultValue="" />
                                                    </SelectParameters>--%>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtEdit_TranItemFilter_ItemTypeID" Name="sTypeID" Type="String" DefaultValue="" />
                                                    </SelectParameters>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtEdit_TranItemFilter_Description" Name="sDescription" Type="String" DefaultValue="" />
                                                    </SelectParameters>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="ddlEdit_TranItemFilter_Dept" PropertyName="SelectedValue" Name="sDeptCode" Type="String" />
                                                    </SelectParameters>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtEdit_TranItemFilter_ItemLangID" Name="sLangID" Type="String" DefaultValue="" />
                                                    </SelectParameters>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtEdit_TranItemFilter_NeedApproval" Name="sNeedApproval" Type="String" DefaultValue="" />
                                                    </SelectParameters>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtEdit_TranItemFilter_GlobalUse" Name="sGlobalUse" Type="String" DefaultValue="" />
                                                    </SelectParameters>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtEdit_TranItemFilter_RequestDateFrom" Name="dRequestDateFrom" Type="String" DefaultValue="" />
                                                    </SelectParameters>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtEdit_TranItemFilter_RequestDateTo" Name="dRequestDateTo" Type="String" DefaultValue="" />
                                                    </SelectParameters>
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtEdit_TranItemFilter_NoOfLagDay" Name="iNoOfLagDay" Type="String" DefaultValue="3" />
                                                    </SelectParameters>

                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtEdit_TranItemFilter_WindowsAuthUserID" Name="sWindowsAuthUserID" Type="String" DefaultValue="" />   <%-- J20180605 --%>
                                                    </SelectParameters>

                                                </asp:ObjectDataSource>

                                                <span class="failureNotification">
                                                    <asp:Literal ID="FailureEditTranAddItemText" runat="server"></asp:Literal>
                                                </span>
                                                <asp:ValidationSummary ID="EditTranAddItemValidationSummary" runat="server" ForeColor="Red" CssClass="failureNotification" ValidationGroup="EditTranAddItemValidationGroup" />


                                                <table>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblEdit_TranItemFilter" runat="server" AssociatedControlID="lblEdit_TranItemFilter" Text="Add Item" Font-Bold="true" Font-Underline="true" />
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblEdit_TranItemFilter_Type" runat="server" AssociatedControlID="lblEdit_TranItemFilter_Type" Text="Type:    " /></td>
                                                        <td>
                                                            <asp:CheckBoxList ID="chkEdit_TranItemFilter_Type" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal" Visible="true">
                                                            </asp:CheckBoxList>
                                                            <asp:Button ID="btnEdit_TranItemFilter_SelectAllType" Width="100" runat="server" OnCommand="TranItemFilterTypeSelection_Edit" Text="[select all]" CommandName="A" CssClass="button" /><asp:Button ID="TranItemFilter_UnselectAllType" Width="100" runat="server" OnCommand="TranItemFilterTypeSelection_Edit" Text="[unselect all]" CommandName="U" CssClass="button" />
                                                            <asp:Label ID="txtEdit_TranItemFilter_ItemTypeID" runat="server" Visible="false" Text="" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblEdit_TranItemFilter_Lang" runat="server" AssociatedControlID="lblEdit_TranItemFilter_Lang" Text="Language:    " /></td>
                                                        <td>
                                                            <asp:CheckBoxList ID="chkEdit_TranItemFilter_Lang" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal" Visible="true">
                                                            </asp:CheckBoxList>
                                                            <asp:Button ID="btnEdit_TranItemFilter_SelectAllLang" Width="100" runat="server" OnCommand="TranItemFilterLangSelection_Edit" Text="[select all]" CommandName="A" CssClass="button" /><asp:Button ID="btnEdit_TranItemFilter_UnselectAllLang" Width="100" runat="server" OnCommand="TranItemFilterLangSelection_Edit" Text="[unselect all]" CommandName="U" CssClass="button" />
                                                            <asp:Label ID="txtEdit_TranItemFilter_ItemLangID" runat="server" Visible="false" Text="" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblEdit_TranItemFilter_ID" runat="server" AssociatedControlID="lblEdit_TranItemFilter_ID" Text="ID:" Visible="false" /></td>
                                                        <td>
                                                            <asp:TextBox ID="txtEdit_TranItemFilter_ID" runat="server" CssClass="textEntry" Enabled="true" Text="999999" Visible="false" /><asp:TextBox ID="txtEdit_TranItemFilter_RedirectStatus" runat="server" CssClass="textEntry" Enabled="false" Text="" Visible="false" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblEdit_TranItemFilter_ItemCode" runat="server" AssociatedControlID="lblEdit_TranItemFilter_ItemCode" Text="Item No.: " /></td>
                                                        <td>
                                                            <asp:TextBox ID="txtEdit_TranItemFilter_ItemCode" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                                                    </tr>
                                                    <%--<tr>
                                                        <td>
                                                            <asp:Label ID="lblEdit_TranItemFilter_ItemCopiesID" runat="server" AssociatedControlID="lblEdit_TranItemFilter_ItemCopiesID" Text="Item Copies ID: " /></td>
                                                        <td>
                                                            <asp:TextBox ID="txtEdit_TranItemFilter_ItemCopiesID" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblEdit_TranItemFilter_Description" runat="server" AssociatedControlID="lblFilter_Description" Text="Item Name: " /></td>
                                                        <td>
                                                            <asp:TextBox ID="txtEdit_TranItemFilter_Description" runat="server" CssClass="textEntry" Enabled="true" Text="" /></td>
                                                    </tr>

                                                    <%--<tr>
                                                        <td>
                                                            <asp:Label ID="lblEdit_TranItemFilter_Status" runat="server" AssociatedControlID="lblEdit_TranItemFilter_Status" Text="Status:    " /></td>
                                                        <td>
                                                            <asp:CheckBoxList ID="checkBoxListEdit_Status" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal" Visible="true">
                                                                <asp:ListItem Text="Active" Value="1" Selected="True">Active</asp:ListItem>
                                                                <asp:ListItem Text="Active" Value="0" Selected="True">Inactive</asp:ListItem>
                                                            </asp:CheckBoxList>
                                                            <asp:Label ID="txtEdit_TranItemFilter_Status" runat="server" Visible="false" Text="" />
                                                        </td>
                                                    </tr>--%>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblEdit_TranItemFilter_Dept" runat="server" AssociatedControlID="lblEdit_TranItemFilter_Dept" Text="Department:    " Visible="false" /></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlEdit_TranItemFilter_Dept" runat="server" Enabled="true" Visible="false">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblEdit_TranItemFilter_NeedApproval" runat="server" AssociatedControlID="lblEdit_TranItemFilter_NeedApproval" Text="Need Approval:    " Visible="false" /></td>
                                                        <td>
                                                            <asp:CheckBoxList ID="checkBoxListEdit_NeedApproval" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal" Visible="false">
                                                                <asp:ListItem Text="Active" Value="1" Selected="True">Yes</asp:ListItem>
                                                                <asp:ListItem Text="Active" Value="0" Selected="True">No</asp:ListItem>
                                                            </asp:CheckBoxList>
                                                            <asp:Label ID="txtEdit_TranItemFilter_NeedApproval" runat="server" Visible="false" Text="" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblEdit_TranItemFilter_GlobalUse" runat="server" AssociatedControlID="lblEdit_TranItemFilter_GlobalUse" Text="Global Use:    " Visible="false" /></td>
                                                        <td>
                                                            <asp:CheckBoxList ID="checkBoxListEdit_GlobalUse" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal" Visible="false">
                                                                <asp:ListItem Text="Active" Value="1" Selected="True">Yes</asp:ListItem>
                                                                <asp:ListItem Text="Active" Value="0" Selected="True">No</asp:ListItem>
                                                            </asp:CheckBoxList>
                                                            <asp:Label ID="txtEdit_TranItemFilter_GlobalUse" runat="server" Visible="false" Text="" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style4" colspan="2">
                                                            <strong>Requesting Period:</strong></td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td><strong>
                                                            <asp:Label ID="lblEdit_TranItemFilter_RequestDateFrom" runat="server" Text="From:    " />
                                                        </strong>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEdit_TranItemFilter_RequestDateFrom" runat="server" CssClass="textEntry" Enabled="true" Text="" />
                                                            <asp:Button ID="btnEdit_BorrowDateFrom" runat="server" OnClick="btnEdit_BorrowDateFrom_Click" Text="[Pick Date]" CssClass="button" />
                                                            (yyyy/mm/dd)<br />
                                                            <asp:Label ID="lblEdit_BorrowDateFrom_Old" runat="server" Text="" Visible="true" />
                                                            <div>
                                                                <asp:Calendar ID="CalendarEdit_BorrowDateFrom" runat="server" Visible="False" OnSelectionChanged="CalendarEdit_BorrowDateFrom_SelectionChanged">
                                                                    <TodayDayStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" />
                                                                </asp:Calendar>
                                                            </div>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorBDateFrm_Edit" runat="server" ControlToValidate="txtEdit_TranItemFilter_RequestDateFrom"
                                                                ValidationExpression="^[0-9]{4}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30)))$"
                                                                CssClass="failureNotification" ErrorMessage="Incorrect date format of Borrow Date (From)! " ToolTip=""
                                                                Display="Dynamic" ValidationGroup="EditTransactionValidationGroup">* Wrong Date Format!</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <strong>
                                                                <asp:Label ID="lblEdit_TranItemFilter_RequestDateTo" runat="server" Text="To:    " />
                                                            </strong>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEdit_TranItemFilter_RequestDateTo" runat="server" CssClass="textEntry" Enabled="true" Text="2017/08/11" />
                                                            <asp:Button ID="btnEdit_BorrowDateTo" runat="server" OnClick="btnEdit_BorrowDateTo_Click" Text="[Pick Date]" CssClass="button" />
                                                            (yyyy/mm/dd)<br />
                                                            <asp:Label ID="lblEdit_BorrowDateTo_Old" runat="server" Text="" Visible="true" />
                                                            &nbsp;<div>
                                                                <asp:Calendar ID="CalendarEdit_BorrowDateTo" runat="server" Visible="False" OnSelectionChanged="CalendarEdit_BorrowDateTo_SelectionChanged">
                                                                    <TodayDayStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" />
                                                                </asp:Calendar>
                                                            </div>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorBDateTo_Edit" runat="server" ControlToValidate="txtEdit_TranItemFilter_RequestDateTo"
                                                                ValidationExpression="^[0-9]{4}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30)))$"
                                                                CssClass="failureNotification" ErrorMessage="Incorrect date format of Borrow Date (To)! " ToolTip=""
                                                                Display="Dynamic" ValidationGroup="EditTransactionValidationGroup">* Wrong Date Format!</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>




                                                    <%--                                        <td>
                                            <asp:TextBox ID="txtEdit_TranItemFilter_RequestDateFrom" runat="server" CssClass="textEntry" Enabled="false" Text="" />
                                            <strong>
                                            <asp:Label ID="lblEdit_TranItemFilter_RequestDateTo" runat="server" Text="To:" />
                                            </strong>
                                            <asp:TextBox ID="txtEdit_TranItemFilter_RequestDateTo" runat="server" CssClass="textEntry" Enabled="false" Text="" />
                                        </td>--%>



                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblEdit_TranItemFilter_NoOfLagDay" runat="server" AssociatedControlID="lblEdit_TranItemFilter_NoOfLagDay" Text="No Of Lag Day:    " /></td>
                                                        <td>
                                                            <asp:TextBox ID="txtEdit_TranItemFilter_NoOfLagDay" runat="server" CssClass="textEntry" Enabled="true" Text="3" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorTranItemFilter_NoOfLagDay" runat="server" ControlToValidate="txtEdit_TranItemFilter_NoOfLagDay"
                                                                ValidationExpression="^[0-9]\d*$"
                                                                CssClass="failureNotification" ErrorMessage="No. Of Lag Days is incorrect. " ToolTip=""
                                                                Display="Dynamic" ValidationGroup="EditTransactionValidationGroup">* Positive integar only!
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>

                                                    <%-- J20180605 --%>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblEdit_TranItemFilter_WindowsAuthUserID" runat="server" AssociatedControlID="lblEdit_TranItemFilter_WindowsAuthUserID" Visible="false" Text="Window Auth User ID:    "></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEdit_TranItemFilter_WindowsAuthUserID" runat="server" CssClass="textEntry" Enabled="false" Visible="false" Text="" />
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnEdit_TranItemFilter" runat="server" CssClass="button" OnClick="btnEdit_TranItemFilter_Click" Text="Search Item" ValidationGroup="TranItemFilterItemValidationGroup" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnEdit_TranItemFilterClear" runat="server" CssClass="button" OnClick="btnEdit_TranItemFilterClear_Click" Text="Clear Filter" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="GridViewEdit_Items" runat="server"
                                                                DataSourceID="odsEdit_Items" AutoGenerateColumns="true"
                                                                OnRowCommand="gvItem_RowCommand"
                                                                OnRowDataBound="GridViewEdit_Items_RowDataBound"
                                                                ShowHeader="true" AllowSorting="true"
                                                                AllowPaging="true" OnPageIndexChanging="PageItemIndexChanging"
                                                                SortedAscendingHeaderStyle-Font-Underline="true" PageSize="10">

                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Select">
                                                                        <ItemStyle VerticalAlign="Top" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <HeaderStyle Width="10%" />
                                                                        <ItemTemplate>
                                                                            <%--                                        <asp:CheckBox ID="chkEdit_EditItem" runat="server" CommandName="A" CommandArgument='<%# Eval("ID") %>' OnRowCommand="Tran_EditItem" />--%>
                                                                            <asp:CheckBox ID="chkEditItem" runat="server" CommandName="A" CommandArgument='<%# Eval("ID") %>' />

                                                                        </ItemTemplate>
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkboxEditSelectAll" AutoPostBack="true" OnCheckedChanged="chkboxEditSelectAll_CheckedChanged" runat="server" />
                                                                        </HeaderTemplate>
                                                                        <ItemStyle VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="View Transaction">
                                                                        <ItemStyle VerticalAlign="Top" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <HeaderStyle Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnItemViewTxn_Edit" runat="server" Text="View Txn" CommandArgument='<%# Eval("ID") %>' CssClass="button" OnCommand="Tran_AddItem_ViewTxn_Edit"></asp:Button>
                                                                        </ItemTemplate>
                                                                        <ItemStyle VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="BorrowingQty (AvailableQty)">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="BorrowingQtyEdit" Width="40" runat="server" Text="1"></asp:TextBox>
                                                                            <asp:TextBox ID="AvailableQtyEdit" Width="40" runat="server" Enabled="false"></asp:TextBox>
                                                                            <asp:CompareValidator ID="CompareValidatorBorrowingQty_Edit" runat="server"
                                                                                Operator="LessThanEqual" ControlToCompare="AvailableQtyEdit" ControlToValidate="BorrowingQtyEdit"
                                                                                CssClass="failureNotification"
                                                                                ErrorMessage="BorrowingQty is incorrect. " Type="Integer"
                                                                                Display="Dynamic" ValidationGroup="CheckAddSelectedItemsValidationGroup_Edit">* Borrowing Qty cannot be greater than Available Qty!
                                                                            </asp:CompareValidator>
                                                                            <asp:RequiredFieldValidator
                                                                                ID="RequiredFieldValidatorBorrowingQty_Edit" runat="server"
                                                                                CssClass="failureNotification"
                                                                                ErrorMessage="BorrowingQty cannot be blank! "
                                                                                ControlToValidate="BorrowingQtyEdit">* Required!
                                                                            </asp:RequiredFieldValidator>
                                                                            <asp:RegularExpressionValidator
                                                                                ID="RegularExpressionValidatorBorrowingQty_Edit" runat="server" ControlToValidate="BorrowingQtyEdit"
                                                                                ValidationExpression="^[1-9]\d*$"
                                                                                CssClass="failureNotification" ErrorMessage="BorrowingQty is incorrect. " ToolTip=""
                                                                                Display="Dynamic" ValidationGroup="CheckAddSelectedItemsValidationGroup_Edit">* Positive integar >0 only!
                                                                            </asp:RegularExpressionValidator>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>

                                                                <%--<EmptyDataTemplate>
                                                                    No Record Found!
                                                                </EmptyDataTemplate>--%>

                                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="20" />
                                                                <SortedAscendingHeaderStyle Font-Underline="True" />

                                                            </asp:GridView>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnEdit_GetSelected" runat="server" CssClass="button" Text="Add Selected Item(s)" OnClick="GetSelectedRecords_Edit" ValidationGroup="EditTranAddItemValidationGroup" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:MultiView ID="mvEdit_AddItem_ViewTxn" runat="server" ActiveViewIndex="0">
                                                                <asp:View ID="vEdit_AddItem_ViewTxn" runat="server">
                                                                    <table>
                                                                        <asp:ObjectDataSource ID="odsEdit_AddItem_ViewTxn" runat="server" TypeName="ResourceBorrowing.Items.ItemController" SelectMethod="GetTxnByItemID">
                                                                            <SelectParameters>
                                                                                <asp:ControlParameter ControlID="txtFilter_txnItemID_Edit" Name="iItemID" Type="String" DefaultValue="" />
                                                                            </SelectParameters>
                                                                            <SelectParameters>
                                                                                <asp:ControlParameter ControlID="txtFilter_txnStatus_Edit" Name="sStatus" Type="String" DefaultValue="1,2" />
                                                                            </SelectParameters>
                                                                        </asp:ObjectDataSource>
                                                                        <tr>
                                                                            <td class="auto-style5">
                                                                                <strong><em>
                                                                                    <asp:Label ID="lblEdit_ViewItemTran" runat="server" Text="View Item Transaction: " />
                                                                                </em></strong>
                                                                                <%--<em><strong>View Item Transaction:</strong></em>--%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox ID="txtFilter_txnItemID_Edit" runat="server" CssClass="textEntry" Enabled="false" Visible="false" Text="999999" />
                                                                                <asp:TextBox ID="txtFilter_txnStatus_Edit" runat="server" Enabled="false" Visible="false" Text="1,2" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <asp:GridView ID="GridViewTxns_Edit" runat="server" DataSourceID="odsEdit_AddItem_ViewTxn"
                                                                                AutoGenerateColumns="true"
                                                                                ShowHeader="true" AllowSorting="true" AllowPaging="true"
                                                                                SortedAscendingHeaderStyle-Font-Underline="true"
                                                                                OnRowDataBound="GridViewTxns_Edit_RowDataBound" />
                                                                            <sortedascendingheaderstyle font-underline="True" />
                                                                            </asp:GridView>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Button ID="btnEdit_ViewTxn_Close" CssClass="button" runat="server" Text="Close View Txn" OnClick="btnEdit_ViewTxn_Close_OnClick" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:View>
                                                                <asp:View ID="vEdit_AddItem_ViewTxn_Disable" runat="server">
                                                                </asp:View>
                                                            </asp:MultiView>
                                                        </td>
                                                    </tr>

                                                    <%--HEIDI                                                    <tr>
                                                        <td>
                                                            <asp:MultiView ID="mvEdit_AddItem_ViewTxn" runat="server" ActiveViewIndex="0">
												                <asp:View ID="vEdit_AddItem_ViewTxn" runat="server">
													                <table>
														                <asp:ObjectDataSource ID="odsEdit_AddItem_ViewTxn" runat="server" TypeName="ResourceBorrowing.Items.ItemController" SelectMethod="GetTxnByItemID"  >
															                <SelectParameters><asp:ControlParameter ControlID="txtFilter_txnItemID" Name="iItemID" Type="String" DefaultValue="" /> </SelectParameters>
															                <SelectParameters><asp:ControlParameter ControlID="txtFilter_txnStatus" Name="sStatus" Type="String" DefaultValue="1,2" /> </SelectParameters>
														                </asp:ObjectDataSource> 
														                <tr>
															                <td class="auto-style5" colspan="2">
																                <em><strong>View Item Transaction:</strong></em>
															                </td>
														                </tr>
														                <tr>
															                <td><asp:TextBox ID="txtFilter_txnItemID_Edit" runat="server" CssClass="textEntry" Enabled="false" Visible="false" Text=""/>
																                <asp:TextBox ID="txtFilter_txnStatus_Edit" runat="server" Enabled="false" Visible="false" Text="1,2" />
															                </td>
														                </tr>
														                <tr>
															                <asp:GridView ID="GridViewTxns_Edit" runat="server"  DataSourceID="odsTxn" 
																                AutoGenerateColumns="true"  
																                ShowHeader="true" AllowSorting="true" AllowPaging="true" 
																                SortedAscendingHeaderStyle-Font-Underline="true" 
																                OnRowDataBound="GridViewTxns_RowDataBound" >
																                <EmptyDataTemplate>
																	                No Record Found!
																                </EmptyDataTemplate>
																                <SortedAscendingHeaderStyle Font-Underline="True" />
															                </asp:GridView>
														                </tr>
													                </table>
													                </asp:View>
													            <asp:View ID="vEdit_AddItem_ViewTxn_Disable" runat="server">
													            </asp:View>
												            </asp:MultiView>
                                                        </td>
                                                    </tr>--%>


                                                    <%--
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="BtnEdit_AddItem" runat="server" Text="Confirm Add" CommandName="A" CommandArgument="A" OnCommand="BtnEdit_AddItem_Click" ValidationGroup="AddTransactionValidationGroup" OnClientClick="if(!confirm('Confirm to add?')) {return false;};" CssClass="button" Visible="false" />
                                                        </td>
                                                    </tr>
                                                    --%>
                                                </table>

                                                <%--                                <asp:GridView ID="GridView1" runat="server" HeaderStyle-BackColor="orange" HeaderStyle-ForeColor="White"
                                                                                     AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEdit_Row" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="150" />
                                        <asp:TemplateField HeaderText="Country" ItemStyle-Width="150">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEdit_Country" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>--%>
                                            </asp:Panel>
                                        </asp:View>
                                        <asp:View ID="vEdit_DisableSelectItem" runat="server">
                                        </asp:View>

                                    </asp:MultiView>
                                </td>
                            </tr>
                            <tr>
                                <td class="TranColWidth">
                                    <asp:Label ID="lblEdit_Remarks" runat="server" AssociatedControlID="lblEdit_Remarks" Text="Remarks:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEdit_Remarks" TextMode="MultiLine" runat="server" CssClass="textEntry" Enabled="true" Text="" />
                                    <asp:Label ID="txtEdit_Remarks_old" runat="server" Text="" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TranColWidth">
                                    <asp:Label ID="lblEdit_AddedBy" runat="server" AssociatedControlID="lblEdit_AddedBy" Text="Added By:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEdit_AddedBy" runat="server" CssClass="textEntry" Enabled="false" Text="" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TranColWidth">
                                    <asp:Label ID="lblEdit_AddedOn" runat="server" AssociatedControlID="lblEdit_AddedOn" Text="Added On:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEdit_AddedOn" runat="server" CssClass="textEntry" Enabled="false" Text="" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TranColWidth">
                                    <asp:Label ID="lblEdit_EditedBy" runat="server" AssociatedControlID="lblEdit_EditedBy" Text="Edited By:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEdit_EditedBy" runat="server" CssClass="textEntry" Enabled="false" Text="" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TranColWidth">
                                    <asp:Label ID="lblEdit_EditedOn" runat="server" AssociatedControlID="lblEdit_EditedOn" Text="Edited On:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEdit_EditedOn" runat="server" CssClass="textEntry" Enabled="false" Text="" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnEdit_EditTranBtn" runat="server" Text="Save" CommandName="S" CommandArgument="S" OnCommand="btnEdit_EditTranBtn_Click" ValidationGroup="EditTransactionValidationGroup" OnClientClick="if(!confirm('Confirm to save?')) {return false;};" CssClass="button" Visible="true" />
                                    <asp:Button ID="btnEdit_EditTran_Print" runat="server" Text="Print Confirmation Note" CommandName="P" CommandArgument="P" OnCommand="btnEdit_EditTranBtn_Click" ValidationGroup="EditTransactionValidationGroup" CssClass="button" />
                                    &nbsp;&nbsp;                                    
                                    <asp:Button ID="btnEdit_EditTran_Back" runat="server" CssClass="button" OnClick="btnEdit_EditTran_Back_Click" Text="Back" />
                                </td>
                                <td>
                                    <%--                                <asp:Button ID="PrintTranBtn" runat="server" Text="Save & Print"  CommandName="P" CommandArgument="P" oncommand ="AddTranBtn_Click"  ValidationGroup="AddTransactionValidationGroup"  OnClientClick="if(!confirm('Confirm to save and print confirmation note?')) {return false;};" CssClass="button" />         --%>
                                </td>
                            </tr>
                        </table>
                    </div>

                </fieldset>

                <%--            <asp:Button ID="btnEdit_EditTran" runat="server" Text="Save" CommandName="A" CommandArgument="A" oncommand="btnEdit_EditTran_Click" ValidationGroup="EditTransactionValidationGroup" OnClientClick="if(!confirm('Confirm to Edit?')) {return false;};" CssClass="button" />
            <asp:Button ID="btnEdit_PrintTran" runat="server" Text="Save & Print" CommandName="P" CommandArgument="P" oncommand="btnEdit_EditTran_Click" ValidationGroup="EditTransactionValidationGroup" OnClientClick="if(!confirm('Confirm to save and print confirmation note?')) {return false;};" CssClass="button" />--%>
                <%--            <asp:Button ID="btnEdit_CancelEditTran" runat="server" Text="Cancel" OnClientClick="if(!confirm('Confirm to cancel?')) {return false;};" OnClick="btnEdit_CancelEditTran_Click" CssClass="button" />--%>
            </div>
        </asp:View>


    </asp:MultiView>

</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .TranColWidth {
            width: 165px;
        }

        .auto-style1 {
            text-decoration: underline;
            height: 22px;
        }

        .auto-style3 {
            height: 31px;
        }

        .auto-style4 {
            text-decoration: underline;
        }

        .auto-style5 {
            width: 199px;
        }

        .auto-style6 {
            height: 22px;
        }

        .auto-style7 {
            height: 24px;
        }

        .auto-style8 {
            width: 165px;
            height: 24px;
        }
    </style>
</asp:Content>


