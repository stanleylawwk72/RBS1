<%@ Page Title="Role Maintenance Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="RoleMaintenance.aspx.cs" Inherits="RoleMaintenance" EnableEventValidation = "false" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContentAdmin">

<asp:ObjectDataSource ID="odsRoles" runat="server" TypeName="wv.Users.UserController" SelectMethod="getRoles"  />
    <h2>
       Role Maintenance
    </h2>
<asp:MultiView ID="mvRoles" runat="server" ActiveViewIndex=0>

    <asp:View ID="vRoleList" runat="server">
        <p>
        <asp:Button ID="btnAddRole" runat="server" Text="Add Role" OnClick="btnAddRole_Click" CssClass="button" />
        <asp:Button ID="btnAddRoleDetail_EmtyRoleCode" runat="server" Text="Add RoleDetail" OnClick="btnAddRoleDetail_Click" CssClass="button" />  
        </p>

        <asp:GridView ID="GridViewRoles" runat="server" DataSourceID="odsRoles" 
            AutoGenerateColumns="true"  OnRowCommand="gvRole_RowCommand" ShowHeader="true" 
            AllowSorting="true" AllowPaging="true" 
            SortedAscendingHeaderStyle-Font-Underline="true" 
            OnPageIndexChanging = "PageRoleIndexChanging" PageSize="20">
                    <Columns> 
                        <asp:TemplateField HeaderText="Edit" >
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <HeaderStyle Width="10%" />
                            <ItemTemplate>
                                <asp:Button ID="btnEditRole" runat="server" Text="Edit" CommandName="E" CommandArgument='<%# Eval("Code") %>' CssClass="button" />
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Show Details" >
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <HeaderStyle Width="10%" />
                            <ItemTemplate>
                                <asp:Button ID="btnShowRoleDetails" runat="server" Text="Show Details" CommandName="D" CommandArgument='<%# Eval("Code") %>' CssClass="button" />
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
    </asp:View>

    <asp:View ID="vEditRole" runat="server">
        <h2>
            Edit an Existing Role
        </h2>
        <asp:ValidationSummary ID="EditRoleValidationSummary" runat="server" CssClass="failureNotification"  ValidationGroup="EditRoleValidationSummary" />

        <div class="RoleItem">
            <fieldset class="Role">
                <span class="failureNotification">
                    <asp:Literal ID="FailureRoleUpdateText" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="CheckRolePasswordValidationSummary_Role_Edit" runat="server" CssClass="failureNotification" 
                 ValidationGroup="CheckRolePasswordValidationGroup_Role_Edit"/>
                <legend>Role Information</legend>

                <div class="leftCorner">
                    <table>
                        <tr>
                            <td><asp:Label ID="lblCode_Role_Edit" runat="server" AssociatedControlID="lblCode_Role_Edit">Code:</asp:Label></td>
                            <td><asp:TextBox ID="txtCode_Role_Edit" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox><asp:TextBox ID="txtCode_Role_Edit_Old" runat="server" CssClass="textEntry" Enabled="false" Visible="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblDescription_Role_Edit" runat="server" AssociatedControlID="lblDescription_Role_Edit">Description:</asp:Label></td>
                            <td><asp:TextBox ID="txtDescription_Role_Edit" runat="server" CssClass="textEntry" Enabled="true"></asp:TextBox><asp:TextBox ID="txtDescription_Role_Edit_Old" runat="server" CssClass="textEntry" Enabled="false" Visible="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                        <td><asp:Label ID="lblStatus_Role_Edit" runat="server" AssociatedControlID="lblStatus_Role_Edit">Status:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlStatus_Role_Edit" runat="server">
                                <asp:ListItem Text="Active" Value="True" />
                                <asp:ListItem Text="Inactive" Value="False" />
                                </asp:DropDownList>

                                <asp:DropDownList ID="ddlStatus_Role_Edit_Old" runat="server" Visible="false">
                                <asp:ListItem Text="Active" Value="True" />
                                <asp:ListItem Text="Inactive" Value="False" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                       <tr>
                            <td><asp:Label ID="lblObsoleteDate_Role_Edit" runat="server" AssociatedControlID="lblObsoleteDate_Role_Edit">Obsolete Date:</asp:Label></td>
                            <td>
                            <asp:TextBox ID="txtObsoleteDate_Role_Edit" runat="server" CssClass="textEntry" Enabled="true"></asp:TextBox><asp:TextBox ID="txtObsoleteDate_Role_Edit_Old" runat="server" CssClass="textEntry" Enabled="true" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                                                <tr>
                            <td><asp:Label ID="lblAddBy_Role_Edit" runat="server" AssociatedControlID="lblAddDate_Role_Edit">Add by:</asp:Label></td>
                            <td><asp:TextBox ID="txtAddBy_Role_Edit" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblAddDate_Role_Edit" runat="server" AssociatedControlID="lblAddDate_Role_Edit">Add Date:</asp:Label></td>
                            <td><asp:TextBox ID="txtAddDate_Role_Edit" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox></td>
                        </tr>
                                                <tr>
                            <td><asp:Label ID="lblEditBy_Role_Edit" runat="server" AssociatedControlID="lblEditBy_Role_Edit">Edit by:</asp:Label></td>
                            <td><asp:TextBox ID="txtEditBy_Role_Edit" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblEditDate_Role_Edit" runat="server" AssociatedControlID="lblEditDate_Role_Edit">Edit Date:</asp:Label></td>
                            <td><asp:TextBox ID="txtEditDate_Role_Edit" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox></td>
                        </tr>

                   </table>
                </div>


            </fieldset>
            <asp:Button ID="EditRoleBtn" runat="server" Text="Update" ValidationGroup="CheckRolePasswordValidationGroup_Role_Edit" OnClientClick="if(!confirm('Confirm to update the Role record?')) {return false;};" OnClick="UpdateRoleBtn_Click" CssClass="button" />
            <asp:Button ID="CancelUpdateRoleBtn" runat="server" Text="Cancel" OnClick="CancelUpdateRoleBtn_Click" CssClass="button"  />
        </div>
    </asp:View>

    <asp:View ID="vAddRole" runat="server">
        <h2>
            Add a New Role
        </h2>
        <span class="failureNotification">
            <asp:Literal ID="ErrorMessageAdd" runat="server"></asp:Literal>
        </span>
        <div class="RoleItem">
            <fieldset class="Role">
                <span class="failureNotification">
                    <asp:Literal ID="FailureRoleAddText" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="CheckRolePasswordValidationSummary_Role_Add" runat="server" CssClass="failureNotification" 
                 ValidationGroup="CheckRolePasswordValidationGroup_Role_Add"/>
                <legend>Role Information</legend>

                <div class="leftCorner">
                    <table>
                        <tr>
                            <td><asp:Label ID="lblCode_Role_Add" runat="server" AssociatedControlID="lblCode_Role_Add">Code:</asp:Label></td>
                            <td><asp:TextBox ID="txtCode_Role_Add" runat="server" CssClass="textEntry" Enabled="true"></asp:TextBox><asp:TextBox ID="txtCode_Role_Add_Old" runat="server" CssClass="textEntry" Enabled="false" Visible="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblDescription_Role_Add" runat="server" AssociatedControlID="lblDescription_Role_Add">Description:</asp:Label></td>
                            <td><asp:TextBox ID="txtDescription_Role_Add" runat="server" CssClass="textEntry" Enabled="true"></asp:TextBox><asp:TextBox ID="txtDescription_Role_Add_Old" runat="server" CssClass="textEntry" Enabled="false" Visible="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblStatus_Role_Add" runat="server" AssociatedControlID="lblStatus_Role_Add">Status:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlStatus_Role_Add" runat="server">
                                <asp:ListItem Text="Active" Value="True" />
                                <asp:ListItem Text="Inactive" Value="False" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                   </table>
                </div>
            </fieldset>
            <asp:Button ID="AddRoleBtn" runat="server" Text="Add" ValidationGroup="CheckRolePasswordValidationGroup_Role_Add"  OnClientClick="if(!confirm('Confirm to add the Role record?')) {return false;};"  OnClick="AddRoleBtn_Click" CssClass="button" />
            <asp:Button ID="CancelAddRoleBtn" runat="server" Text="Cancel" OnClick="CancelAddRoleBtn_Click" CssClass="button" />
        </div>
    </asp:View>


    <asp:View ID="vRoleDetailList" runat="server">
        <p>
        <asp:Button ID="btnAddRoleDetail" runat="server" Text="Add RoleDetail" OnClick="btnAddRoleDetail_Click" CssClass="button" />  
        <asp:Button ID="btnBackToRole" runat="server" Text="Back to Role" OnClick="btnBackToRole_Click" CssClass="button" />
        <asp:TextBox ID="txtRoleCodeForAddNewRoleDetail" runat="server" CssClass="textEntry" Enabled="false" Visible="false"></asp:TextBox>
        </p>
        
        <asp:GridView ID="GridViewRoleDetails" runat="server"  
            AutoGenerateColumns="true"  OnRowCommand="gvRoleDetail_RowCommand" 
            ShowHeader="true" AllowSorting="false" AllowPaging="true" 
            SortedAscendingHeaderStyle-Font-Underline="true" 
            OnPageIndexChanging = "PageRoleDetailIndexChanging" PageSize="20"   >
                    <Columns> 
                        <asp:TemplateField HeaderText="Edit" >
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <HeaderStyle Width="10%" />
                            <ItemTemplate>
                                <asp:Button ID="btnEditRoleDetail" runat="server" Text="Edit" CommandName="E" CommandArgument='<%# Eval("ID") %>' CssClass="button" />
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
    </asp:View>


        <asp:View ID="vEditRoleDetail" runat="server">
        <h2>
            Edit an Existing Role Detail
        </h2>
        <asp:ValidationSummary ID="EditRoleDetailValidationSummary" runat="server" CssClass="failureNotification"  ValidationGroup="EditRoleDetailValidationSummary" />

        <div class="RoleDetailItem">
            <fieldset class="RoleDetail">
                <span class="failureNotification">
                    <asp:Literal ID="FailureRoleDetailUpdateText" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="CheckRoleDetailPasswordValidationSummary_RoleDetail_Edit" runat="server" CssClass="failureNotification" 
                 ValidationGroup="CheckRoleDetailPasswordValidationGroup_RoleDetail_Edit"/>
                <legend>RoleDetail Information</legend>

                <div class="leftCorner">
                    <table>

                        <tr>
                            <td><asp:Label ID="lblRoleCode_RoleDetail_Edit" runat="server" AssociatedControlID="lblRoleCode_RoleDetail_Edit">Role Code:</asp:Label><asp:TextBox ID="txtID_RoleDetail_Edit" runat="server" CssClass="textEntry" Enabled="false" Visible="false"></asp:TextBox></td>
                            <td><asp:TextBox ID="txtRoleCode_RoleDetail_Edit" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox></td>
                        </tr>
                       <tr>
                            <td><asp:Label ID="lblRoleDetailCode_RoleDetail_Edit" runat="server" AssociatedControlID="lblRoleDetailCode_RoleDetail_Edit">Role Detail Code:</asp:Label></td>
                            <td><asp:TextBox ID="txtRoleDetailCode_RoleDetail_Edit" runat="server" CssClass="textEntry" Enabled="true"></asp:TextBox><asp:TextBox ID="txtRoleDetailCode_RoleDetail_Edit_Old" runat="server" CssClass="textEntry" Enabled="true" Visible="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblDescription_RoleDetail_Edit" runat="server" AssociatedControlID="lblDescription_RoleDetail_Edit">Description:</asp:Label></td>
                            <td><asp:TextBox ID="txtDescription_RoleDetail_Edit" runat="server" CssClass="textEntry" Enabled="true"></asp:TextBox><asp:TextBox ID="txtDescription_RoleDetail_Edit_Old" runat="server" CssClass="textEntry" Enabled="false" Visible="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                        <td><asp:Label ID="lblPermission_RoleDetail_Edit" runat="server" AssociatedControlID="lblStatus_RoleDetail_Edit">Permission:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlPermission_RoleDetail_Edit" runat="server">
                                <asp:ListItem Text="Yes" Value="True" />
                                <asp:ListItem Text="No" Value="False" />
                                </asp:DropDownList>

                                <asp:DropDownList ID="ddlPermission_RoleDetail_Edit_Old" runat="server" Visible="false">
                                <asp:ListItem Text="Yes" Value="True" />
                                <asp:ListItem Text="No" Value="False" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                        <td><asp:Label ID="lblStatus_RoleDetail_Edit" runat="server" AssociatedControlID="lblStatus_RoleDetail_Edit">Status:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlStatus_RoleDetail_Edit" runat="server">
                                <asp:ListItem Text="Active" Value="True" />
                                <asp:ListItem Text="Inactive" Value="False" />
                                </asp:DropDownList>

                                <asp:DropDownList ID="ddlStatus_RoleDetail_Edit_Old" runat="server" Visible="false">
                                <asp:ListItem Text="Active" Value="True" />
                                <asp:ListItem Text="Inactive" Value="False" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                                                
                       <tr>
                            <td><asp:Label ID="lblObsoleteDate_RoleDetail_Edit" runat="server" AssociatedControlID="lblObsoleteDate_RoleDetail_Edit">Obsolete Date:</asp:Label></td>
                            <td>
                            <asp:TextBox ID="txtObsoleteDate_RoleDetail_Edit" runat="server" CssClass="textEntry" Enabled="true"></asp:TextBox><asp:TextBox ID="txtObsoleteDate_RoleDetail_Edit_Old" runat="server" CssClass="textEntry" Enabled="true" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                                                <tr>
                            <td><asp:Label ID="lblAddBy_RoleDetail_Edit" runat="server" AssociatedControlID="lblAddDate_RoleDetail_Edit">Add by:</asp:Label></td>
                            <td><asp:TextBox ID="txtAddBy_RoleDetail_Edit" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblAddDate_RoleDetail_Edit" runat="server" AssociatedControlID="lblAddDate_RoleDetail_Edit">Add Date:</asp:Label></td>
                            <td><asp:TextBox ID="txtAddDate_RoleDetail_Edit" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblEditBy_RoleDetail_Edit" runat="server" AssociatedControlID="lblEditBy_RoleDetail_Edit">Edit by:</asp:Label></td>
                            <td><asp:TextBox ID="txtEditBy_RoleDetail_Edit" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblEditDate_RoleDetail_Edit" runat="server" AssociatedControlID="lblEditDate_RoleDetail_Edit">Edit Date:</asp:Label></td>
                            <td><asp:TextBox ID="txtEditDate_RoleDetail_Edit" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox></td>
                        </tr>

                   </table>
                </div>


            </fieldset>
            <asp:Button ID="EditRoleDetailBtn" runat="server" Text="Update" ValidationGroup="CheckRoleDetailPasswordValidationGroup_RoleDetail_Edit" OnClientClick="if(!confirm('Confirm to update the RoleDetail record?')) {return false;};" OnClick="UpdateRoleDetailBtn_Click" CssClass="button" />
            <asp:Button ID="CancelUpdateRoleDetailBtn" runat="server" Text="Cancel" OnClick="CancelUpdateRoleDetailBtn_Click" CssClass="button"  />
        </div>
    </asp:View>

    <asp:View ID="vAddRoleDetail" runat="server">
        <h2>
            Add a New Role Detail
        </h2>
        <span class="failureNotification">
            <asp:Literal ID="Literal2" runat="server"></asp:Literal>
        </span>
        <div class="RoleDetailItem">
            <fieldset class="RoleDetail">
                <span class="failureNotification">
                    <asp:Literal ID="FailureRoleDetailAddText" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="CheckRoleDetailPasswordValidationSummary_RoleDetail_Add" runat="server" CssClass="failureNotification" 
                 ValidationGroup="CheckRoleDetailPasswordValidationGroup_RoleDetail_Add"/>
                <legend>RoleDetail Information</legend>

                <div class="leftCorner">
                    <table>
                        <tr>
                            <td><asp:Label ID="lblRoleCode_RoleDetail_Add" runat="server" AssociatedControlID="lblRoleCode_RoleDetail_Add">Role Code:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlRoleCode_RoleDetail_Add" runat="server" Enabled="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                       <tr>
                            <td><asp:Label ID="lblRoleDetailCode_RoleDetail_Add" runat="server" AssociatedControlID="lblRoleDetailCode_RoleDetail_Add">Role Detail Code:</asp:Label></td>
                            <td><asp:TextBox ID="txtRoleDetailCode_RoleDetail_Add" runat="server" CssClass="textEntry" Enabled="true"></asp:TextBox><asp:TextBox ID="txtRoleDetailCode_RoleDetail_Add_Old" runat="server" CssClass="textEntry" Enabled="true" Visible="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblDescription_RoleDetail_Add" runat="server" AssociatedControlID="lblDescription_RoleDetail_Add">Description:</asp:Label></td>
                            <td><asp:TextBox ID="txtDescription_RoleDetail_Add" runat="server" CssClass="textEntry" Enabled="true"></asp:TextBox><asp:TextBox ID="txtDescription_RoleDetail_Add_Old" runat="server" CssClass="textEntry" Enabled="false" Visible="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblStatus_RoleDetail_Add" runat="server" AssociatedControlID="lblStatus_RoleDetail_Add">Status:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlStatus_RoleDetail_Add" runat="server">
                                <asp:ListItem Text="Active" Value="True" />
                                <asp:ListItem Text="Inactive" Value="False" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td><asp:Label ID="lblPermission_RoleDetail_Add" runat="server" AssociatedControlID="lblPermission_RoleDetail_Add">Permission:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlPermission_RoleDetail_Add" runat="server">
                                <asp:ListItem Text="Yes" Value="True" />
                                <asp:ListItem Text="No" Value="False" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                   </table>
                </div>
            </fieldset>
            <asp:Button ID="AddRoleDetailBtn" runat="server" Text="Add" ValidationGroup="CheckRoleDetailPasswordValidationGroup_RoleDetail_Add"  OnClientClick="if(!confirm('Confirm to add the RoleDetail record?')) {return false;};"  OnClick="AddRoleDetailBtn_Click" CssClass="button" />
            <asp:Button ID="CancelAddRoleDetailBtn" runat="server" Text="Cancel" OnClick="CancelAddRoleDetailBtn_Click" CssClass="button" />
        </div>
    </asp:View>
</asp:MultiView>

</asp:Content>