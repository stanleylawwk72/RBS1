<%@ Page Title="User Maintenance Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="UserMaintenance.aspx.cs" Inherits="UserMaintenance" EnableEventValidation = "false" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContentAdmin">

<asp:ObjectDataSource ID="odsUsers" runat="server" TypeName="wv.Users.UserController" SelectMethod="getUsers"  >
    <SelectParameters><asp:ControlParameter ControlID="txtFilter_ID" Name="hfID" Type="String" DefaultValue="" /></SelectParameters>
    <SelectParameters><asp:ControlParameter ControlID="txtFilter_UserName" Name="sUserName" Type="String" DefaultValue="" /> </SelectParameters>
    <SelectParameters><asp:ControlParameter ControlID="ddlFilter_Dept" PropertyName="SelectedValue" Name="sDeptCode" Type="String" /></SelectParameters>
    <SelectParameters><asp:ControlParameter ControlID="ddlFilter_Status" PropertyName="SelectedValue" Name="iStatus" Type="Int32" /></SelectParameters>
    <SelectParameters><asp:ControlParameter ControlID="ddlFilter_Role" PropertyName="SelectedValue" Name="sRole" Type="String" /></SelectParameters>
</asp:ObjectDataSource> 
    <h2>
       User Maintenance
    </h2>
<asp:MultiView ID="mvUsers" runat="server" ActiveViewIndex=0>

    <asp:View ID="vUserList" runat="server">

    <table>
            <tr>
                <td><asp:Label ID="lblFilter" runat="server" AssociatedControlID="lblFilter" Text="Filter" Font-Bold="true" Font-Underline="true"/></td>
                <td> </td>
            </tr>
           <tr>
                <td><asp:Label ID="lblFilter_ID" runat="server" AssociatedControlID="lblFilter_ID" Text="ID:" Visible="false"/></td>
                <td><asp:TextBox ID="txtFilter_ID" runat="server" CssClass="textEntry" Enabled="true" Text="" Visible="false"/></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblFilter_UserName" runat="server" AssociatedControlID="lblFilter_UserName" Text="User Name :" Visible="true"/></td>
                <td><asp:TextBox ID="txtFilter_UserName" runat="server" CssClass="textEntry" Enabled="true" Text="" Visible="true"/></td>
            </tr>
             <tr>
                <td><asp:Label ID="lblFilter_Dept" runat="server" AssociatedControlID="lblFilter_Dept" Text="Department :    "/></td>
                <td>
                    <asp:DropDownList ID="ddlFilter_Dept" runat="server" Enabled="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
            <td><asp:Label ID="lblFilter_Status" runat="server" AssociatedControlID="lblFilter_Dept" Text="Status :    "/></td>
                <td>
                    <asp:DropDownList ID="ddlFilter_Status" runat="server">
                    <asp:ListItem Text="All" Value="2" />
                    <asp:ListItem Text="Active" Value="1" />
                    <asp:ListItem Text="Inactive" Value="0" />
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
            <td><asp:Label ID="lblFilter_Role" runat="server" AssociatedControlID="lblFilter_Role" Text="Role :    "/></td>
                <td>
                    <asp:DropDownList ID="ddlFilter_Role" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                <asp:Button ID="FilterBtn" runat="server" Text="Apply Filter" OnClick="FilterBtn_Click" CssClass="button"  />
                </td>
                <td><asp:Button ID="FilterClearBtn" runat="server" Text="Clear Filter" OnClick="FilterClearBtn_Click" CssClass="button"  /> </td>
            </tr>
        </table>

        <p>
        <asp:Button ID="btnAddUser" runat="server" Text="Add User" OnClick="btnAdd_Click" CssClass="button" />
        <asp:Button ID="btnSyncADUser" runat="server" Text="Sync AD Users" OnClick="btnSyncADUser_Click" CssClass="button" />
        </p>
        <asp:GridView ID="GridViewUsers" runat="server" DataSourceID="odsUsers" 
            AutoGenerateColumns="false"  OnRowCommand="gvUser_RowCommand" ShowHeader="true" 
            AllowSorting="true" AllowPaging="true" 
            SortedAscendingHeaderStyle-Font-Underline="true" 
            OnPageIndexChanging = "PageUserIndexChanging" PageSize="20"
            OnRowDataBound="GridViewUsers_RowDataBound">
                    <Columns>
                    <asp:TemplateField HeaderText="Edit" >
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <HeaderStyle Width="10%" />
                            <ItemTemplate>
                                <asp:Button ID="btnEditUser" runat="server" Text="Edit" CommandName="E" CommandArgument='<%# Eval("sys_ID") %>' CssClass="button" />
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Reset password by email" >
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <HeaderStyle Width="10%" />
                            <ItemTemplate>
                                <asp:Button ID="btnResetPwd" runat="server" Text="Reset" CommandName="R" CommandArgument='<%# Eval("sys_ID") %>' CssClass="button" />
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="ID" DataField="ID" SortExpression="ID">
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle Width="2%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Department" DataField="DeptCode" SortExpression="DeptCode">
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle Width="5%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Role" DataField="RoleCode" SortExpression="RoleCode">
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle Width="5%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="User Name" DataField="UserName" SortExpression="UserName">
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle Width="10%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        
                        <asp:BoundField HeaderText="User ID" DataField="UserID" SortExpression="UserID" Visible="false">
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle Width="10%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Windows Auth User ID" DataField="WindowsAuthUserID" SortExpression="WindowsAuthUserID" Visible="false">
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle Width="10%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Email Address" DataField="EmailAddress" SortExpression="EmailAddress">
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle Width="20%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Status" SortExpression="Status">
                            <ItemTemplate>
                            <asp:CheckBox  ID="ChkboxActive" runat="server" Checked=<%# Eval("Status")%>  Enabled="false"/>
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        

                        <asp:TemplateField HeaderText="Change the password when login" SortExpression="ChangePwdWhenLogin">
                            <ItemTemplate>
                            <asp:CheckBox ID="ChkboxChangePwdWhenLogin"  runat="server" Checked=<%# Eval("ChangePwdWhenLogin")%>  Enabled="false"/>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Total Fail Login" DataField="TotalLoginFail" SortExpression="TotalLoginFail">
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle Width="5%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Locked" SortExpression="Locked">
                            <ItemTemplate>
                            <asp:CheckBox ID="Locked"  runat="server" Checked=<%# Eval("Locked")%>  Enabled="false"/>
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Obsolete Date" SortExpression="ObsoleteDate">
                            <ItemTemplate>
                                <%# Eval("ObsoleteDate", "{0:yyyy/MM/dd}")%>
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Latest Change Password Date" SortExpression="LatestChangePwdDate">
                            <ItemTemplate>
                                <%# Eval("LatestChangePwdDate", "{0:yyyy/MM/dd}")%>
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        
                        <asp:TemplateField HeaderText="Add Date" SortExpression="AddDate">
                            <ItemTemplate>
                                <%# Eval("AddDate", "{0:yyyy/MM/dd}")%>
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Last Update Date" SortExpression="EditDate">
                            <ItemTemplate>
                                <%# Eval("EditDate", "{0:yyyy/MM/dd}")%>
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>


                        <asp:BoundField HeaderText="Edit By" DataField="EditBy" SortExpression="EditBy" Visible="false">
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle Width="5%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Latest Success Login Date" SortExpression="LatestSuccessLoginDate">
                            <ItemTemplate>
                                <%# Eval("LatestSuccessLoginDate", "{0:yyyy/MM/dd HH:mm:ss}")%>
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        
                    </Columns>

                    <EmptyDataTemplate>
                        No Record Found!
                    </EmptyDataTemplate>

                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="20" />
                    <SortedAscendingHeaderStyle Font-Underline="True" />

        </asp:GridView>
    </asp:View>

    <asp:View ID="vEditUser" runat="server">
        <h2>
            Edit an Existing User
        </h2>
        <asp:ValidationSummary ID="EditUserValidationSummary" runat="server" CssClass="failureNotification"  ValidationGroup="EditUserValidationSummary" />

        <div class="userItem">
            <fieldset class="user">
                <span class="failureNotification">
                    <asp:Literal ID="FailureUpdateText" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="CheckUserValidationSummary_Edit" runat="server" CssClass="failureNotification" 
                 ValidationGroup="CheckUserValidationGroup_Edit"/>
                <legend>User Information</legend>

                <div class="leftCorner">
                    <table>
                        <tr>
                            <td><asp:Label ID="lblID_Edit" runat="server" AssociatedControlID="lblID_Edit">ID:</asp:Label></td>
                            <td><asp:TextBox ID="txtID_Edit" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox><asp:TextBox ID="txtSysID_Edit" runat="server" CssClass="textEntry" Enabled="false" Visible="false"></asp:TextBox></td>
                        </tr>
 
                        <tr>
                            <td><asp:Label ID="lblStatus_Edit" runat="server" AssociatedControlID="lblStatus_Edit">Status:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlStatus_Edit" runat="server">
                                <asp:ListItem Text="Active" Value="True" />
                                <asp:ListItem Text="Inactive" Value="False" />
                                </asp:DropDownList>

                                <asp:DropDownList ID="ddlStatus_Edit_Old" runat="server" Visible="false">
                                <asp:ListItem Text="Active" Value="True" />
                                <asp:ListItem Text="Inactive" Value="False" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                       
                        <tr>
                            <td><asp:Label ID="lblLocked_Edit" runat="server" AssociatedControlID="lblLocked_Edit">Locked:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlLocked_Edit" runat="server" Enabled="true">
                                <asp:ListItem Text="Yes" Value="True" />
                                <asp:ListItem Text="No" Value="False" />
                                </asp:DropDownList>

                                <asp:DropDownList ID="ddlLocked_Edit_Old" runat="server" Enabled="true" Visible="false">
                                <asp:ListItem Text="Yes" Value="True" />
                                <asp:ListItem Text="No" Value="False" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblChangePwdWhenLogin_Edit" runat="server" AssociatedControlID="lblChangePwdWhenLogin_Edit" >Change the password when login:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlChangePwdWhenLogin_Edit" runat="server" Enabled="false">
                                <asp:ListItem Text="Yes" Value="True" />
                                <asp:ListItem Text="No" Value="False" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblDept_Edit" runat="server" AssociatedControlID="lblDept_Edit">Department:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlDept_Edit" runat="server" Enabled="true">
                                </asp:DropDownList>

                                <asp:DropDownList ID="ddlDept_Edit_Old" runat="server" Enabled="true" Visible="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblRole_Edit" runat="server" AssociatedControlID="lblRole_Edit">Role:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlRole_Edit" runat="server" Enabled="true">
                                </asp:DropDownList>

                                <asp:DropDownList ID="ddlRole_Edit_Old" runat="server" Enabled="true" Visible="false">
                                </asp:DropDownList>
                            </td>
                        </tr>

                       <tr>
                            <td><asp:Label ID="lblTotalLoginFail_Edit" runat="server" AssociatedControlID="lblTotalLoginFail_Edit">Total Fail Login :</asp:Label></td>
                            <td><asp:TextBox ID="txtTotalLoginFail_Edit" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblUserName_Edit" runat="server" AssociatedControlID="lblUserName_Edit">User Name:</asp:Label></td>
                            <td><asp:TextBox ID="txtUserName_Edit" runat="server" CssClass="textEntry" Enabled="true"></asp:TextBox><asp:TextBox ID="txtUserName_Edit_Old" runat="server" CssClass="textEntry" Enabled="true" Visible="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblWinWuthUserID_Edit" runat="server" AssociatedControlID="lblWinWuthUserID_Edit">Windows Auth User ID:</asp:Label></td>
                            <td><asp:TextBox ID="txtWinAuthUserID_Edit" runat="server" CssClass="textEntry" Enabled="true"></asp:TextBox><asp:TextBox ID="txtWinAuthUserID_Edit_Old" runat="server" CssClass="textEntry" Enabled="true" Visible="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblUserID_Edit" runat="server" AssociatedControlID="lblUserID_Edit">User ID:</asp:Label></td>
                            <td><asp:TextBox ID="txtUserID_Edit" runat="server" CssClass="textEntry" Enabled="true"></asp:TextBox><asp:TextBox ID="txtUserID_Edit_Old" runat="server" CssClass="textEntry" Enabled="true" Visible="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblEmailAddress_Edit" runat="server" AssociatedControlID="lblEmailAddress_Edit">Email Address:</asp:Label></td>
                            <td><asp:TextBox ID="txtEmailAddress_Edit" runat="server" CssClass="textEntry"></asp:TextBox><asp:TextBox ID="txtEmailAddress_Edit_Old" runat="server" CssClass="textEntry" Visible="false"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmailAddress_Edit" runat="server" ControlToValidate="txtEmailAddress_Edit" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            CssClass="failureNotification"  ErrorMessage="The email format is incorrect! " ToolTip="" 
                            Display="Dynamic" ValidationGroup="CheckUserValidationGroup_Edit">*</asp:RegularExpressionValidator>
                            
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblObsoleteDate_Edit" runat="server" AssociatedControlID="lblObsoleteDate_Edit">Obsolete Date:</asp:Label></td>
                            <td>
                            <asp:TextBox ID="txtObsoleteDate_Edit" runat="server" CssClass="textEntry" Enabled="true"></asp:TextBox><asp:TextBox ID="txtObsoleteDate_Edit_Old" runat="server" CssClass="textEntry" Enabled="true" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblLatestChangePwdDate_Edit" runat="server" AssociatedControlID="lblLatestChangePwdDate_Edit">Latest Change Password Date:</asp:Label></td>
                            <td><asp:TextBox ID="txtLatestChangePwdDate_Edit" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblAddBy_Edit" runat="server" AssociatedControlID="lblAddBy_Edit">Add by:</asp:Label></td>
                            <td><asp:TextBox ID="txtAddBy_Edit" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox></td>
                        </tr>
                         <tr>
                            <td><asp:Label ID="lblAddDate_Edit" runat="server" AssociatedControlID="lblAddDate_Edit">Add Date:</asp:Label></td>
                            <td><asp:TextBox ID="txtAddDate_Edit" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblEditBy_Edit" runat="server" AssociatedControlID="lblEditBy_Edit">Edit by:</asp:Label></td>
                            <td><asp:TextBox ID="txtEditBy_Edit" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblEditDate_Edit" runat="server" AssociatedControlID="lblEditDate_Edit">Edit Date:</asp:Label></td>
                            <td><asp:TextBox ID="txtEditDate_Edit" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblLatestSuccessLoginDate_Edit" runat="server" AssociatedControlID="lblLatestSuccessLoginDate_Edit">Latest Success Login Date:</asp:Label></td>
                            <td><asp:TextBox ID="txtLatestSuccessLoginDate_Edit" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblPwd_Edit" runat="server" AssociatedControlID="lblPwd_Edit">Change Password:</asp:Label></td>
                            <td><asp:CheckBox  ID="chkboxChgPwd" runat="server" Text="Checked the box when you want to change the password" AutoPostBack="True"  Checked="false" OnCheckedChanged="chkboxChgPwd_Click"></asp:CheckBox>
                            <asp:CheckBox  ID="chkboxChgPwd_Old" runat="server" AutoPostBack="True"  Checked="false" Visible="false"></asp:CheckBox>
                            <br>
                            <asp:TextBox ID="txtPwd_Edit" runat="server" CssClass="passwordEntry" Enabled="false"  TextMode="Password"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="CheckChangePwd_Edit" runat="server" ControlToValidate="txtPwd_Edit" 
                            ValidationExpression="^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$"
                            CssClass="failureNotification"  ErrorMessage="The password should be At least one lower case letter; At least one upper case letter; At least special character; At least one number; At least 8 characters length" ToolTip="The password should be At least one lower case letter; At least one upper case letter; At least special character; At least one number; At least 8 characters length" 
                            Display="Dynamic" ValidationGroup="CheckUserValidationGroup_Edit">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>

                   </table>
                </div>


            </fieldset>
            <asp:Button ID="EditUserBtn" runat="server" Text="Update" ValidationGroup="CheckUserValidationGroup_Edit" OnClientClick="if(!confirm('Confirm to update the user record?')) {return false;};" OnClick="UpdateUserBtn_Click" CssClass="button" />
            <asp:Button ID="CancelUpdateUserBtn" runat="server" Text="Cancel" OnClick="CancelUpdateUserBtn_Click" CssClass="button"  />
        </div>
    </asp:View>

    <asp:View ID="vAddUser" runat="server">
        <h2>
            Add a New User
        </h2>
        <span class="failureNotification">
            <asp:Literal ID="ErrorMessageAdd" runat="server"></asp:Literal>
        </span>
        <div class="userItem">
            <fieldset class="user">
                <span class="failureNotification">
                    <asp:Literal ID="FailureAddText" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="CheckUserValidationSummary_Add" runat="server" CssClass="failureNotification" 
                 ValidationGroup="CheckUserValidationGroup_Add"/>
                <legend>User Information</legend>

                <div class="leftCorner">
                    <table>
                        <tr>
                            <td><asp:Label ID="lblStatus_Add" runat="server" AssociatedControlID="lblStatus_Add">Status:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlStatus_Add" runat="server">
                                <asp:ListItem Text="Active" Value="True" />
                                <asp:ListItem Text="Inactive" Value="False" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                       
                       <tr>
                            <td><asp:Label ID="lblDept_Add" runat="server" AssociatedControlID="lblDept_Add">Department:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlDept_Add" runat="server" Enabled="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblRole_Add" runat="server" AssociatedControlID="lblRole_Add">Role:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlRole_Add" runat="server" Enabled="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblUserName_Add" runat="server" AssociatedControlID="lblUserName_Add">User Name:</asp:Label></td>
                            <td><asp:TextBox ID="txtUserName_Add" runat="server" CssClass="textEntry" Enabled="true"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblWinWuthUserID_Add" runat="server" AssociatedControlID="lblWinWuthUserID_Add">Windows Auth User ID:</asp:Label></td>
                            <td><asp:TextBox ID="txtWinAuthUserID_Add" runat="server" CssClass="textEntry" Enabled="true"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblUserID_Add" runat="server" AssociatedControlID="lblUserID_Add">User ID:</asp:Label></td>
                            <td><asp:TextBox ID="txtUserID_Add" runat="server" CssClass="textEntry" Enabled="true"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblEmailAddress_Add" runat="server" AssociatedControlID="lblEmailAddress_Add">Email Address:</asp:Label></td>
                            <td><asp:TextBox ID="txtEmailAddress_Add" runat="server" CssClass="textEntry"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmailAddress_Add" runat="server" ControlToValidate="txtEmailAddress_Add" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            CssClass="failureNotification"  ErrorMessage="The email format is incorrect! " ToolTip="" 
                            Display="Dynamic" ValidationGroup="CheckUserValidationGroup_Add">*</asp:RegularExpressionValidator>
                            
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblPwd_Add" runat="server" AssociatedControlID="lblPwd_Add">Password:</asp:Label></td>
                            <td>
                            <asp:TextBox ID="txtPwd_Add" runat="server" CssClass="passwordEntry" Enabled="true"  TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPwd_Add" 
                             CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="New Password is required." 
                             Display="Dynamic"  ValidationGroup="CheckUserValidationGroup_Add">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorPwd" runat="server" ControlToValidate="txtPwd_Add" 
                            ValidationExpression="^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$"
                            CssClass="failureNotification"  ErrorMessage="The password should be At least one lower case letter; At least one upper case letter; At least special character; At least one number; At least 8 characters length" ToolTip="The password should be At least one lower case letter; At least one upper case letter; At least special character; At least one number; At least 8 characters length" 
                            Display="Dynamic" ValidationGroup="CheckUserValidationGroup_Add">*</asp:RegularExpressionValidator>

                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblPwd_Confirm_Add" runat="server" AssociatedControlID="lblPwd_Add">Confirm Password:</asp:Label></td>
                            <td>
                            <asp:TextBox ID="txtPwd_Confirm_Add" runat="server" CssClass="passwordEntry" Enabled="true" TextMode="Password"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="txtPwd_Confirm_Add" 
                             CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm Password is required."
                             ToolTip="Confirm Password is required." ValidationGroup="CheckUserValidationGroup_Add">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="txtPwd_Add" ControlToValidate="txtPwd_Confirm_Add" 
                             CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Confirm Password must match the Password entry."
                             ValidationGroup="CheckUserValidationGroup_Add">*</asp:CompareValidator>
 
                            </td>
                        </tr>
                   </table>
                </div>
            </fieldset>
            <asp:Button ID="AddUserBtn" runat="server" Text="Add" ValidationGroup="CheckUserValidationGroup_Add"  OnClientClick="if(!confirm('Confirm to add the user record?')) {return false;};"  OnClick="AddUserBtn_Click" CssClass="button" />
            <asp:Button ID="CancelAddUserBtn" runat="server" Text="Cancel" OnClick="CancelAddUserBtn_Click" CssClass="button" />
        </div>
    </asp:View>

    <asp:View ID="vSyncADUserList" runat="server">
        <h2>
            Sync AD Users
        </h2>
        <span class="failureNotification">
            <asp:Literal ID="ErrorMessageSync" runat="server"></asp:Literal>
        </span>

    <table>

        <tr>
    <td> <asp:Label ID="lblAD_DN" runat="server" Text="Active Directory  DN:" /></td><td><asp:TextBox ID="txtAD_DN" runat="server" enabled="false" /></td>
    </tr>
        <tr>
    <td> <asp:Label ID="lblsRole" runat="server" Text="New User Default Role :" /></td><td><asp:TextBox ID="txtRole" runat="server" enabled="false" /></td>
    </tr>
        <tr>
    <td> <asp:Label ID="lblStatus" runat="server" Text="New User Default Status:" /></td><td><asp:TextBox ID="txtStatus" runat="server" enabled="false" /></td>
    </tr>
        <tr>
    <td> <asp:Label ID="lblWindowAuthID" runat="server" Text="Windows Authentication User ID:" /></td><td><asp:TextBox ID="txtWindowAuthID" runat="server" enabled="false" /></td>
    </tr>
    <tr>
    <td><asp:Label ID="lblPwd" runat="server" Text="Windows Password:" /></td><td><asp:TextBox ID="txtPwd" runat="server"  enabled="true" TextMode="Password" />
    </td>
    </tr>

     <tr>
    <td><asp:Label ID="lblUserList" runat="server" Text="User List:" Visible="false" /></td><td><asp:TextBox ID="txtUserList" runat="server" Visible="false"  enabled="true" width="500" Height="1000" TextMode="MultiLine" />
    <asp:TextBox ID="txtUserList2" runat="server"  enabled="true" width="500" Height="1000" Visible="false" TextMode="MultiLine"/><asp:Label ID="lblUserID" Text="" runat="server"/>
    </td>
    </tr>

    <tr>
    <td><asp:Button ID="btnSyncADUserProcess" runat="server" Text="Sync"  OnClientClick="if(!confirm('Confirm to syn AD users?')) {return false;};"  OnClick="btnSyncADUserProcess_Click" CssClass="button" /> <asp:Button ID="btnSyncADUserProcess_Cancel" runat="server" Text="Cancel" OnClick="CancelAddUserCancelBtn_Click" CssClass="button" /></td>
    <td></td>
    </tr>
    </table>
    </asp:View>

</asp:MultiView>

</asp:Content>