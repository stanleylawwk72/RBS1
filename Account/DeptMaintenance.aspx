<%@ Page Title="DeptMaintenance Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="DeptMaintenance.aspx.cs" Inherits="DeptMaintenance" EnableEventValidation = "false" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContentAdmin">

<asp:ObjectDataSource ID="odsDepartments" runat="server" TypeName="wv.Users.UserController" SelectMethod="getDepartments"  />
    <h2>
       Department Maintenance
    </h2>
<asp:MultiView ID="mvDepartments" runat="server" ActiveViewIndex=0>

    <asp:View ID="vDepartmentList" runat="server">
        <p>
        <asp:Button ID="btnAddDepartment" runat="server" Text="Add Department" OnClick="btnAdd_Click" CssClass="button" />
        </p>

        <asp:GridView ID="GridViewDepartments" runat="server" 
            DataSourceID="odsDepartments" AutoGenerateColumns="true"  
            OnRowCommand="gvDepartment_RowCommand" ShowHeader="true" AllowSorting="true" 
            AllowPaging="true" OnPageIndexChanging = "PageDeptIndexChanging" 
            SortedAscendingHeaderStyle-Font-Underline="true" PageSize="20" >
                    <Columns> 
                        <asp:TemplateField HeaderText="Edit" >
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <HeaderStyle Width="10%" />
                            <ItemTemplate>
                                <asp:Button ID="btnEditDepartment" runat="server" Text="Edit" CommandName="E" CommandArgument='<%# Eval("Code") %>' CssClass="button" />
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

    <asp:View ID="vEditDepartment" runat="server">
        <h2>
            Edit an Existing Department
        </h2>
        <asp:ValidationSummary ID="EditDepartmentValidationSummary" runat="server" CssClass="failureNotification"  ValidationGroup="EditDepartmentValidationSummary" />

        <div class="DepartmentItem">
            <fieldset class="Department">
                <span class="failureNotification">
                    <asp:Literal ID="FailureUpdateText" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="CheckDepartmentPasswordValidationSummary_Edit" runat="server" CssClass="failureNotification" 
                 ValidationGroup="CheckDepartmentPasswordValidationGroup_Edit"/>
                <legend>Department Information</legend>

                <div class="leftCorner">
                    <table>
                        <tr>
                            <td><asp:Label ID="lblCode_Edit" runat="server" AssociatedControlID="lblCode_Edit">Code:</asp:Label></td>
                            <td><asp:TextBox ID="txtCode_Edit" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox><asp:TextBox ID="txtCode_Edit_Old" runat="server" CssClass="textEntry" Enabled="false" Visible="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblDescription_Edit" runat="server" AssociatedControlID="lblDescription_Edit">Description:</asp:Label></td>
                            <td><asp:TextBox ID="txtDescription_Edit" runat="server" CssClass="textEntry" Enabled="true"></asp:TextBox><asp:TextBox ID="txtDescription_Edit_Old" runat="server" CssClass="textEntry" Enabled="false" Visible="false"></asp:TextBox></td>
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
                            <td><asp:Label ID="lblObsoleteDate_Edit" runat="server" AssociatedControlID="lblObsoleteDate_Edit">Obsolete Date:</asp:Label></td>
                            <td>
                            <asp:TextBox ID="txtObsoleteDate_Edit" runat="server" CssClass="textEntry" Enabled="true"></asp:TextBox><asp:TextBox ID="txtObsoleteDate_Edit_Old" runat="server" CssClass="textEntry" Enabled="true" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                                                <tr>
                            <td><asp:Label ID="lblAddBy_Edit" runat="server" AssociatedControlID="lblAddDate_Edit">Add by:</asp:Label></td>
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
                   </table>
                </div>


            </fieldset>
            <asp:Button ID="EditDepartmentBtn" runat="server" Text="Update" ValidationGroup="CheckDepartmentPasswordValidationGroup_Edit" OnClientClick="if(!confirm('Confirm to update the Department record?')) {return false;};" OnClick="UpdateDepartmentBtn_Click" CssClass="button" />
            <asp:Button ID="CancelUpdateDepartmentBtn" runat="server" Text="Cancel" OnClick="CancelUpdateDepartmentBtn_Click" CssClass="button"  />
        </div>
    </asp:View>

    <asp:View ID="vAddDepartment" runat="server">
        <h2>
            Add a New Department
        </h2>
        <span class="failureNotification">
            <asp:Literal ID="ErrorMessageAdd" runat="server"></asp:Literal>
        </span>
        <div class="DepartmentItem">
            <fieldset class="Department">
                <span class="failureNotification">
                    <asp:Literal ID="FailureAddText" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="CheckDepartmentPasswordValidationSummary_Add" runat="server" CssClass="failureNotification" 
                 ValidationGroup="CheckDepartmentPasswordValidationGroup_Add"/>
                <legend>Department Information</legend>

                <div class="leftCorner">
                    <table>
                        <tr>
                            <td><asp:Label ID="lblCode_Add" runat="server" AssociatedControlID="lblCode_Add">Code:</asp:Label></td>
                            <td><asp:TextBox ID="txtCode_Add" runat="server" CssClass="textEntry" Enabled="true"></asp:TextBox><asp:TextBox ID="txtCode_Add_Old" runat="server" CssClass="textEntry" Enabled="false" Visible="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblDescription_Add" runat="server" AssociatedControlID="lblDescription_Add">Description:</asp:Label></td>
                            <td><asp:TextBox ID="txtDescription_Add" runat="server" CssClass="textEntry" Enabled="true"></asp:TextBox><asp:TextBox ID="txtDescription_Add_Old" runat="server" CssClass="textEntry" Enabled="false" Visible="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblStatus_Add" runat="server" AssociatedControlID="lblStatus_Add">Status:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlStatus_Add" runat="server">
                                <asp:ListItem Text="Active" Value="True" />
                                <asp:ListItem Text="Inactive" Value="False" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                   </table>
                </div>
            </fieldset>
            <asp:Button ID="AddDepartmentBtn" runat="server" Text="Add" ValidationGroup="CheckDepartmentPasswordValidationGroup_Add"  OnClientClick="if(!confirm('Confirm to add the Department record?')) {return false;};"  OnClick="AddDepartmentBtn_Click" CssClass="button" />
            <asp:Button ID="CancelAddDepartmentBtn" runat="server" Text="Cancel" OnClick="CancelAddDepartmentBtn_Click" CssClass="button" />
        </div>
    </asp:View>

</asp:MultiView>

</asp:Content>