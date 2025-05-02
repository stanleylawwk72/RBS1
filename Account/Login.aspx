<%@ Page Title="Login Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="Login.aspx.cs" Inherits="Login" EnableEventValidation = "false" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainLogin">
    <h2>
       Login Page
    </h2>
<asp:View ID="DebugMsg"  runat="server"  >
<p>
<asp:Label ID="lblConnection" runat="server" Text="SQL Server:"/>
<asp:Label ID="lblSvrName" runat="server"/>
<asp:Label ID="lblDbName" runat="server"/>
</p>
</asp:View>

<span class="failureNotification">
    <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
</span>
<br />
<table>
<tr>
<td> <asp:Label ID="lblWindowAuthID" runat="server" Text="Windows Authentication User ID:" /></td><td><asp:TextBox ID="txtWindowAuthID" runat="server" enabled="false" Width="200" /></td>
</tr>
<tr>
<td></td><td></td>
</tr>
<tr>
<td><asp:Label ID="lblLogin" runat="server" Text="User ID:" /></td><td><asp:TextBox ID="txtLogin" runat="server" enabled="false" Width="200" /><asp:TextBox ID="txtReferenceNoFromOutSide" runat="server" enabled="false" Visible="false" Width="200" /></td>
</tr>
<tr>
<td><asp:Label ID="lblPwd" runat="server" Text="Password:" /></td><td><asp:TextBox ID="txtPwd" runat="server" enabled="false" TextMode="Password" Width="200" />
</td>
</tr>

<tr>
<td><asp:checkbox ID="chkWinAuth" runat="server" Text="Use Windows Authentication"  Checked="true" AutoPostBack="True" OnCheckedChanged="chkWinAuth_Click"  /> </td><td></td>
</tr>
<tr>
<td><asp:Button ID="btnLogin" runat="server" Text="Login"  OnClick="btnLogin_Click" CssClass="button" /></td><td></td>
</tr>
</table>
<p>
 <asp:HyperLink  NavigateUrl="~/Account/RequestResetPassword.aspx" ID="hypRequestResetPassword" runat="server" Text="[Reset the password]"></asp:HyperLink>
 </p>
</asp:Content>